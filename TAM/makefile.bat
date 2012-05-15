@echo off 
echo Compiling resources... 
.\res\resgen /compile .\res\EN.txt .\res\RU.txt
echo Compiling assembly... 
C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /res:res\EN.resources /res:res\RU.resources /out:messages.dll /t:library .\Messages.cs .\AssemblyInfo.cs /platform:anycpu
del .\res\EN.resources .\res\RU.resources
pause 