# EzLines
Command-line utility used for checking lines in various projects and codebases.

# Usage
1. Open the directory with the output .exe file.
2. Enter a command in format: 
```
.\EzLines.exe <path> <fileType> <optional: output files?>
```
Here is example command that reads all csharp files with **"cs"** extension. In whole disk **D**. With optional argument **"-o""** which outputs the files it reads.
```
.\EzLines.exe "D:" ".cs" "-o"
```

3. Reading the output is very easy. The program outputs the following format: 
```
Total amount of lines: 3421 in 4 files. (434,3 KB)
```