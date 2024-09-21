using Rectangle.Interface;
using System.IO;
using System.Reflection;

public class Program
{
    private static readonly string pluginFolder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

    public static void Main(string[] args)
    {
        // Print the path to check where the application is looking for plugins
        Console.WriteLine($"Plugin folder path: {pluginFolder}");

        if (!Directory.Exists(pluginFolder))
        {
            Console.WriteLine("Plugins folder not found, creating it...");
            Directory.CreateDirectory(pluginFolder);
        }

        // Process existing DLLs in the Plugins folder at startup
        ProcessExistingDlls();

        // Initialize the FileSystemWatcher
        FileSystemWatcher watcher = new FileSystemWatcher
        {
            Path = pluginFolder,
            Filter = "*.dll",
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime
        };

        // Event handler for when a new DLL is added
        watcher.Created += OnDllAdded;
        watcher.Renamed += OnDllAdded;
        watcher.Changed += OnDllAdded;

        // Start watching the directory
        watcher.EnableRaisingEvents = true;

        Console.WriteLine("Monitoring Plugins folder. Add a DLL to trigger event.");
        Console.ReadLine();  // Keep the program running
    }

    // Process existing DLLs in the Plugins folder at startup
    private static void ProcessExistingDlls()
    {
        Console.WriteLine("Processing existing DLLs...");

        var dllFiles = Directory.GetFiles(pluginFolder, "*.dll");

        if (dllFiles.Length == 0)
        {
            Console.WriteLine("No DLLs found in the Plugins folder.");
        }
        else
        {
            foreach (var dllFile in dllFiles)
            {
                Console.WriteLine($"Found DLL: {Path.GetFileName(dllFile)}");
                LoadAndCalculateArea(dllFile);
            }
        }
    }

    // Event handler for newly added DLLs
    private static void OnDllAdded(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"New DLL detected: {e.Name}");

        // Wait for the file to be fully copied
        WaitForFile(e.FullPath);

        // Load and process the DLL
        LoadAndCalculateArea(e.FullPath);
    }

    // Method to ensure the DLL is fully copied before attempting to load it
    private static void WaitForFile(string fullPath)
    {
        int retries = 10;
        while (retries > 0)
        {
            try
            {
                using (FileStream fs = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    fs.Close();
                }
                Console.WriteLine($"File ready: {fullPath}");
                break;
            }
            catch (IOException)
            {
                retries--;
                Console.WriteLine($"Waiting for file {fullPath} to be ready...");
                System.Threading.Thread.Sleep(500);  // Retry after short delay
            }
        }
    }

    // Method to load the DLL and calculate the area of the shape
    private static void LoadAndCalculateArea(string dllPath)
    {
        Console.WriteLine($"Loading DLL: {dllPath}");
        try
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IShape).IsAssignableFrom(type) && !type.IsInterface)
                {
                    // Create an instance of the shape
                    Console.WriteLine($"Found shape: {type.Name}");
                    object shapeInstance = Activator.CreateInstance(type);
                    IShape shape = (IShape)shapeInstance;

                    // Calculate and display the area
                    int area = shape.CalculateArea(1, 2);
                    Console.WriteLine($"Shape: {type.Name}, Area: {area}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading DLL: {ex.Message}");
        }
    }
}
