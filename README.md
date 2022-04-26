# EzLines
Command-line utility used for checking lines in various projects and codebases.

# Installation
1. Download the .exe build from GitHub Releases, or download the source code and compile it yourself.
2. Open the directory which contains the downloaded .exe file.

# Usage
1. Open the directory with the .exe file (if you haven't done so yet).
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
The output displays amount of all lines as well as amount of files (with the extension you've provided) and their size on disk.

# TODO:
1. Include / exclude blank space and/or comments.