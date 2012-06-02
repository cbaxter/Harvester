# Harvester #

Harvester enables you to monitor all Win32 debug output from all local applications running on your machine. Watch real time Trace, NLog and Log4Net output across multiple applications at the same time. Trace a call from client to server and back without having to look at multiple log files.

Inspired by tools such as Windows Sysinternals DbgView, DBMon.NET and Chainsaw. Harvester provides a simple .NET implementation that aims to merge the best of all aforementioned applications. Watch all OutputDebugString output from all application running on your local machine in a single view (Client/Server and everything in between). Great for monitoring websites during development to ensure that they are behaving as expected!

With Harvester there is no longer a need to monitor your applications log files during application development.

## Documentation ##

See [GitHub Pages](http://cbaxter.github.com/Harvester/documentation.html) for complete documentation.

## License ##

Copyright (c) 2012 CBaxter

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Development Environment ##

- Visual Studio 2010
  - Microsoft.NET Framework 4.0
  - Indentation: 4 spaces
- Dependencies are managed with [NuGet](http://nuget.org) (where available)
  - [NuGet Package Manager](http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c)
  - Run `build.cmd` to install all dependencies
  - Run `package.cmd` to create deployment pacakge
