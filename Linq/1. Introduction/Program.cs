﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\windows";

            ShowLargeFilesWithoutLinq(path);

            Console.WriteLine();

            ShowLargeFilesWithLinq(path);
        }

        private static void ShowLargeFilesWithoutLinq(string path) 
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());

            foreach (FileInfo file in files)
            {
                Console.WriteLine($"{file.Name, -30} : {file.Length, 10:N0}");
            }
        }

        private static void ShowLargeFilesWithLinq(string path) 
        {
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;

            // or
            // var query = new DirectoryInfo(path).GetFiles()
            //    .OrderByDescending(f => f.Length)
            //    .ToList();

            foreach (FileInfo file in query)
            {
                Console.WriteLine($"{file.Name, -30} : {file.Length, 10:N0}");
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
