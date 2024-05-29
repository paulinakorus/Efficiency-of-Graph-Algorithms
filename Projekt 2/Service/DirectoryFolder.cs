﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class DirectoryFolder
{
    private string BeginningPath { get; set; } = "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\";
    public async Task GeneratingGraphsAndWritingToFileAsync()
    {
        string[] mainPaths = { Path.Combine(BeginningPath, @"25%\"), Path.Combine(BeginningPath, @"50%\"), Path.Combine(BeginningPath, @"99%\") };
        double[] density = { 25, 50, 99 };
        int[] vertices = { 10, 20, 50, 100, 200, 500, 1000 };

        for (int i = 0; i < mainPaths.Length; i++)
        {
            GraphGenerator graphGenerator = new GraphGenerator();
            WorkingWithFile workingWithFile = new WorkingWithFile();
            List<Task> tasks = new List<Task>();
            string[] middlePath = { Path.Combine(mainPaths[i], @"10\"), Path.Combine(mainPaths[i], @"20\"), Path.Combine(mainPaths[i], @"50\"), Path.Combine(mainPaths[i], @"100\"), Path.Combine(mainPaths[i], @"200\"), Path.Combine(mainPaths[i], @"500\"), Path.Combine(mainPaths[i], @"1000\") };
            
            for (int j = 0; j < middlePath.Length; j++)
            {
                ClearingFiles(middlePath[j]);
                var list = graphGenerator.GenerateGraphList(50, vertices[j], density[i]);
                tasks.Add(workingWithFile.WriteListToFile(middlePath[j], list));
                Console.WriteLine("Generated " + i + " " + j);
            }
            
            await Task.WhenAll(tasks);
        }
    }

    private void ClearingFiles(string path)
    {
        string[] files = System.IO.Directory.GetFiles(path);
        Parallel.ForEach(files, file =>
        {
            File.Delete(file);
        });
    }

    private async Task OpenTypeFolderAscync(string path)
    {

    }
}
