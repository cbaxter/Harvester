# JSTest.NET #

Harvester enables you to monitor all Win32 debug output from all local applications running on your machine. Watch real time Trace, NLog and Log4Net output across multiple applications at the same time. Trace a call from client to server and back without having to look at multiple log files.

Inspired by tools such as Windows Sysinternals DbgView, DBMon.NET and Chainsaw. Harvester provides a simple .NET implementation that aims to merge the best of all aforementioned applications. Watch all OutputDebugString output from all application running on your local machine in a single view (Client/Server and everything in between). Great for monitoring websites during development to ensure that they are behaving as expected!

With Harvester there is no longer a need to monitor your applications log files during application development.

## Documentation ##

See [GitHub Pages](http://cbaxter.github.com/Harvester/documentation.html) for complete documentation.

## Development Environment ##

- Visual Studio 2010
  - Microsoft.NET Framework 4.0
  - Indentation: 4 spaces
- Dependencies are managed with [NuGet](http://nuget.org) (where available)
  - [NuGet Package Manager](http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c)
  - Run `build.cmd` to install all dependencies
  - Run `package.cmd` to create deployment pacakge
