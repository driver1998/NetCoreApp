# NetCoreApp

This is a simple demo of an UWP app running on .NET 7, with XAML codegen (Yes!).

![demo](Images/demo.png)

## Acknowledgements

XAML codegen enablement is based on [Mile.Xaml](https://github.com/ProjectMile/Mile.Xaml) from [Project Mile](https://github.com/ProjectMile)

Major hack is required to make XAML codegen work on modern .NET without builtin WinRT Interop, see [NetCoreApp.ModernDotNetUAPXamlCompiler](Props/NetCoreApp.ModernDotNetUAPXamlCompiler.targets) for details.
