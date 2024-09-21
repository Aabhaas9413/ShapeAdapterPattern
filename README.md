
# Shape Plugin Adapter Pattern

This project demonstrates a dynamic plugin system to calculate the area of different shapes (Square, Rectangle) using .NET Reflection. Shape DLLs are monitored, loaded, and executed at runtime through the Plugin Adapter Pattern.

## Features
- **Dynamic Plugin Loading**: Loads shape plugins (DLLs) from a `Plugins` folder at runtime.
- **Extensible**: Easily add new shape DLLs without modifying the core code.
- **Monitors Plugins**: Uses `FileSystemWatcher` to detect changes in the `Plugins` folder.

## Project Structure
- **Main Program**: Loads DLLs dynamically.
- **Plugins Folder**: Contains shape DLLs (e.g., Square, Rectangle).
- **Shape Plugins**: Implement the `IShape` interface.

### IShape Interface
All shapes must implement:
```csharp
public interface IShape
{
    int CalculateArea(int length, int width);
}

## Setup Instructions

1. **Clone the Repo**:
   ```bash
   git clone <repository-url>
   cd ShapePluginAdapterPattern
   ```

2. **Build Shape Plugins**:
   - Build the shape projects (Square, Rectangle) to generate the DLLs.

3. **Copy DLLs**:
   - Place the generated `Square.dll` and `Rectangle.dll` into the `Plugins` folder.

4. **Run the Application**:
   ```bash
   dotnet run
   ```

   Example output:
   ```
   Plugin folder path: C:\path\to\Plugins
   Processing existing DLLs...
   Found DLL: Square.dll
   Loading DLL: Square.dll
   Shape: Square, Area: 2
   ```

## Adding New Shapes

1. **Create a New Project** for the shape (e.g., Circle).
2. **Implement `IShape`** in the new shape class.
3. **Build the Project** to generate the DLL.
4. **Copy the DLL** to the `Plugins` folder.

## Troubleshooting

- **DLL Not Detected**: Ensure the DLL is in the `Plugins` folder and properly built.
- **Access Issues**: Verify the DLL is fully copied before loading.

