using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class DirectoryFolder
{
    private string[] BeginningPath { get; set; } = { "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\undirected\\", "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\directed\\" };
    private string[] Types { get; set; } = {"undirected", "directed" };
    private List<List<double>> FolderTime { get; set; } = new List<List<double>>();
    private List<List<List<double>>>[] AllTime { get; set; } =
    {
        new List<List<List<double>>>(),
        new List<List<List<double>>>()
    };

    private readonly object _locker = new object();
    public async Task GeneratingGraphsAndWritingToFileAsync()
    {
        for (int k = 0; k < 2; k++)
        {
            string[] mainPaths = { Path.Combine(BeginningPath[k], @"25%\"), Path.Combine(BeginningPath[k], @"50%\"), Path.Combine(BeginningPath[k], @"99%\") };
            double[] density = { 25, 50, 99 };
            int[] vertices = { 5, 10, 20, 50, 100, 150, 200 };

            for (int i = 0; i < mainPaths.Length; i++)
            {
                GraphGenerator graphGenerator = new GraphGenerator();
                WorkingWithFile workingWithFile = new WorkingWithFile();
                List<Task> tasks = new List<Task>();
                string[] middlePath = { Path.Combine(mainPaths[i], @"5\"), Path.Combine(mainPaths[i], @"10\"), Path.Combine(mainPaths[i], @"20\"), Path.Combine(mainPaths[i], @"50\"), Path.Combine(mainPaths[i], @"100\"), Path.Combine(mainPaths[i], @"150\"), Path.Combine(mainPaths[i], @"200\") };

                for (int j = 0; j < middlePath.Length; j++)
                {
                    ClearingFiles(middlePath[j]);
                    var list = graphGenerator.GenerateGraphList(50, vertices[j], density[i], Types[k]);
                    tasks.Add(workingWithFile.WriteListToFile(middlePath[j], list));
                    Console.WriteLine("Generated " + i + " " + j);
                }

                await Task.WhenAll(tasks);
            }
        }
    }

    public void ClearingFiles(string path)
    {
        string[] files = System.IO.Directory.GetFiles(path);
        Parallel.ForEach(files, file =>
        {
            File.Delete(file);
        });
    }

    public async Task OpenAndCalculate()
    {
        var tasks = new List<Task>();
        foreach (var path in BeginningPath)
        {
            var task = OpenTypeFolderAsync(path);
            tasks.Add(task);
            //await OpenTypeFolderAsync(path);
        }
        await Task.WhenAll(tasks);

        foreach (string path in BeginningPath)
        {
            CSV(path);
        }
    }

    private void CSV(string type)
    {
        string path;
        if (type == BeginningPath[0])
        {
            path = "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\CSV\\undirected\\";
            string[] paths = { Path.Combine(path, "25%.csv"), Path.Combine(path, "50%.csv"), Path.Combine(path, "99%.csv") };
            var time = AllTime[0];
            List<List<double>>[] lists = { time[0], time[1], time[2] };

            var iterator = 0;
            foreach(string pat in paths)
            {
                CsvWriteToFile(pat, lists[iterator++]);
            }
        }
        else if (type == BeginningPath[1])
        {
            path = "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\CSV\\directed\\";
            string[] paths = { Path.Combine(path, "25%.csv"), Path.Combine(path, "50%.csv"), Path.Combine(path, "99%.csv") };
            var time = AllTime[0];
            List<List<double>>[] lists = { time[0], time[1], time[2] };

            var iterator = 0;
            foreach (string pat in paths)
            {
                CsvWriteToFile(pat, lists[iterator++]);
            }
        }
    }

    private void CsvWriteToFile(string filePath, List<List<double>> list)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (List<double> row in list)
            {
                string line = string.Join(",", row);
                sw.WriteLine(line);
            }
        }
    }

    private async Task OpenTypeFolderAsync(string path)
    {
        List<List<List<double>>> typeTime = new List<List<List<double>>>(); 
        List<List<double>> numberTime = new List<List<double>>();

        string[] mainPaths = { Path.Combine(path, @"25%\"), Path.Combine(path, @"50%\"), Path.Combine(path, @"99%\") };
        foreach (string p in mainPaths)
        {
            numberTime.Clear();
            string[] middlePath = { Path.Combine(p, @"5\"), Path.Combine(p, @"10\"), Path.Combine(p, @"20\"), Path.Combine(p, @"50\"), Path.Combine(p, @"100\"), Path.Combine(p, @"150\"), Path.Combine(p, @"200\") };
            foreach (string all_path in middlePath)
            {
                var list = await OpenDirectory(all_path);
                numberTime.Add(list);
            }
            typeTime.Add(numberTime);
        }

        if (path == BeginningPath[0])
        {
            lock (_locker)
            {
                AllTime[0] = typeTime;
            }
        }else
        {
            lock (_locker)
            {
                AllTime[1] = typeTime;
            }
        }
    }

    public async Task<List<double>> OpenDirectory(string filePath)
    {
        if (Directory.Exists(filePath))
        {
            var files = Directory.GetFiles(filePath);
            var tasks = new List<Task>();
            FolderTime.Clear();

            foreach (var file in files)
            {
                var task =  CalculateFile(file);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }

        TimeCalculator timeCalculator = new TimeCalculator();
        var avTime = timeCalculator.AverageTime(FolderTime);

        Console.WriteLine(filePath);
        return avTime;
    }

    public async Task CalculateFile(string path)
    {
        WorkingWithFile workingWithFile = new WorkingWithFile();
        ListAlgorithms listAlgorithms = new ListAlgorithms();
        MatrixAlgorithms matrixAlgorithms = new MatrixAlgorithms();
        var graph = await workingWithFile.ReadDataFromFileAsync(path);
        var stopwatch = Stopwatch.StartNew();
        List<double> time = new List<double>();

        stopwatch.Start();
        listAlgorithms.PrimList(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        listAlgorithms.KruskalList(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        listAlgorithms.DijkstraList(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        listAlgorithms.BellmanFordList(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        listAlgorithms.FordFulkersonList(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        matrixAlgorithms.PrimMatrix(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        matrixAlgorithms.KruskalMatrix(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        matrixAlgorithms.DijkstraMatrix(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        matrixAlgorithms.BellmanFordMatrix(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        stopwatch.Start();
        matrixAlgorithms.FordFulkersonMatrix(graph);
        stopwatch.Stop();
        time.Add(stopwatch.Elapsed.TotalSeconds);
        stopwatch.Restart();

        FolderTime.Add(time);
    }
}
