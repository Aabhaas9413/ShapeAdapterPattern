# Shape Plugin Adapter Pattern

This project demonstrates the use of the **Plugin Adapter Pattern** to dynamically load and execute DLLs for calculating the areas of different shapes (Square and Rectangle) using .NET Reflection.

## Overview

The project monitors a `Plugins` folder for newly added DLLs containing shape definitions (such as Square and Rectangle). Once a DLL is detected, it loads the DLL dynamically, creates an instance of the shape, and calculates the area using the `IShape` interface. This approach allows the program to be extensible and flexible for any shape added as a DLL in the future.

## Features

- **Dynamic Plugin Loading**: Monitors a folder for new shape plugins (DLLs) and loads them at runtime using Reflection.
- **Extensibility**: New shapes can be added as separate DLLs without changing the core codebase.
- **File Monitoring**: Uses `FileSystemWatcher` to monitor the `Plugins` folder for new or changed DLLs.
- **Implements `IShape` Interface**: All shapes implement the `IShape` interface to ensure consistency.

## Project Structure

- **Main Program**: Contains the logic to watch the `Plugins` folder and load shape DLLs dynamically.
- **Shape Plugins**: DLLs for shapes like Square and Rectangle that implement the `IShape` interface.

### IShape Interface

Each shape must implement the following interface:

```csharp
public interface IShape
{
    int CalculateArea(int length, int width);
}
