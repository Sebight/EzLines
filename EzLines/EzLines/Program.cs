using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EzLines
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sizeUnits = {"B", "KB", "MB", "GB", "TB"};

            if (args.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid arguments. Please use the correct format: EzLines.exe <path> <file type>, or refer to the README for more information.");
                Console.ResetColor();
                return;
            }

            string path = args[0];
            string fileType = args[1];

            int threadBuffer = 200;

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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<string> files = Directory.EnumerateFiles(path, $"*.{fileType}", SearchOption.AllDirectories).ToList();

            // foreach (var file in files)
            // {
            //     int amountOfLines = File.ReadAllLines(file).Length;
            //     //Get the file size
            //     FileInfo fileInfo = new FileInfo(file);
            //     long fileSize = fileInfo.Length;
            //     totalSize += fileSize;
            //     totalLines += amountOfLines;
            //
            //     if (outputFiles) Console.WriteLine(file);
            //     
            //     totalFiles++;
            //
            //     if (totalFiles % 500 == 0)
            //     {
            //         Console.Write(".");
            //     }
            // }

            for (int segmentIndex = 0; segmentIndex < files.Count; segmentIndex += threadBuffer)
            {
                List<string> tempFiles = new List<string>();

                int segmentStart = segmentIndex;
                int segmentEnd = segmentIndex + threadBuffer;

                bool isLastSegment = segmentEnd >= files.Count;

                for (int i = segmentStart; i < segmentEnd; i++)
                {
                    if (i > files.Count - 1)
                    {
                        isLastSegment = true;
                        break;
                    }

                    tempFiles.Add(files[i]);
                }

                Thread t = new Thread(() =>
                {
                    foreach (var file in tempFiles)
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

                        if (isLastSegment)
                        {
                            Console.Clear();
                            sw.Stop();
                                Console.ForegroundColor = ConsoleColor.Red;
                            float totalSizeInUnit = totalSize / 1000f;
                            string unit = "KB";

                            while (totalSizeInUnit > 1000)
                            {
                                totalSizeInUnit /= 1000f;
                                unit = sizeUnits[Array.IndexOf(sizeUnits, unit) + 1];
                            }

                            Console.WriteLine(files.Count);
                            Console.WriteLine($"Total amount of lines: {totalLines} in {totalFiles} files. ({Math.Round(totalSizeInUnit, 1)} {unit})");
                            Console.ResetColor();
                        }
                    }
                });
                t.Start();
            }
        }
    }
}