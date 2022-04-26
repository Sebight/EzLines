using System;
using System.IO;

namespace EzLines
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sizeUnits = { "B", "KB", "MB", "GB", "TB" };
            
            string path = "";
            string fileType = "";
            
            if (args.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid arguments. Please use the correct format: EzLines.exe <path> <file type>, or refer to the README for more information.");
                Console.ResetColor();
                return;
            }
            
            path = args[0];
            fileType = args[1];

            bool outputFiles = args.Length == 3 && args[2] == "-o" ? true : false; 

            int totalLines = 0;
            int totalFiles = 0;
            long totalSize = 0;

            if (!Directory.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid directory. The directory does not exist or is inaccessible.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Searching for files...");
            foreach (var file in Directory.EnumerateFiles(path, $"*.{fileType}", SearchOption.AllDirectories))
            {
                int amountOfLines = File.ReadAllLines(file).Length;
                //Get the file size
                FileInfo fileInfo = new FileInfo(file);
                long fileSize = fileInfo.Length;
                totalSize += fileSize;
                totalLines += amountOfLines;

                if (outputFiles) Console.WriteLine(file);
                
                totalFiles++;
            
                if (totalFiles % 500 == 0)
                {
                    Console.Write(".");
                }
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            float totalSizeInUnit = totalSize / 1000f;
            string unit = "KB";

            while (totalSizeInUnit > 1000)
            {
                totalSizeInUnit /= 1000f;
                unit = sizeUnits[Array.IndexOf(sizeUnits, unit) + 1];
            }
            
            Console.WriteLine($"Total amount of lines: {totalLines} in {totalFiles} files. ({Math.Round(totalSizeInUnit, 1)} {unit})");
            Console.ResetColor();
        }
    }
}