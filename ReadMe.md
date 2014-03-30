# TaskDialogLib #
<strong>A free and open source library for XAML-based Task Dialogs.</strong>

<small>Copyright &copy; 2014 Florian Schneidereit. All Rights Reserved.</small>

The TaskDialogLib is a thin wrapper library of the [Task Dialog Common Control](http://msdn.microsoft.com/en-us/library/windows/desktop/bb787471.aspx) introduced in Windows Vista for the .NET Framework and Windows Presentation Foundation. It allows you to declare task dialogs in XAML and define their logic in code-behind, including support for data binding with dependency properties and MVVM scenarios.

[Official Git Repository](http://github.com/sevenacids/TaskDialogLib)  
[Official NuGet Package](http://www.nuget.org/packages/TaskDialogLib)

## Requirements ##
To build and use the TaskDialogLib, the following system requirements must be met:

- Microsoft Windows Vista, or higher
- Microsoft .NET Framework 4.0, or higher

To use the Task Dialog Common Control within your application, a dependency to the Microsoft Common Controls library, version 6, must be declared within your application manifest. Please make sure the following markup section exists within your application manifest file:
<pre>
&lt;dependency&gt;
    &lt;dependentAssembly&gt;
        &lt;assemblyIdentity
            type="win32"
            name="Microsoft.Windows.Common-Controls"
            version="6.0.0.0"
            processorArchitecture="*"
            publicKeyToken="6595b64144ccf1df"
            language="*" /&gt;
    &lt;/dependentAssembly&gt;
&lt;/dependency&gt;
</pre>

## Compilation ##
The TaskDialogLib can be build using the supplied command-line script files located in the root of this distribution (namely *BuildClean.cmd*, *BuildDebug.cmd*, and *BuildRelease.cmd*), or within an IDE that supports the Visual Studio Solution File format.

## License ##
The TaskDialogLib is licensed and distributed under the terms of the GNU Lesser General Public License (LGPL), version 2.1. For more information, please see the file "License.txt" in the root of this distribution or visit <http://www.gnu.org/licenses/>.

----------
<small>File: ReadMe.md, Last Update: March 30, 2014</small>