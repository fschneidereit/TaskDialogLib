@echo off
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild TaskDialogLib.sln /m /nr:false /p:Configuration=Debug "/p:Platform=Any CPU"
pause