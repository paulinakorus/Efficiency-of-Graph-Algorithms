using Projekt_2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class Menu
{
    private string MainPath { get; set; } = "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\";
    public void GeneratorMenu() 
    {
        DirectoryFolder directoryFolder = new DirectoryFolder();
        
        var menuOptions = new (string text, string key, Action action)[]
        {
            ("generating random graphs", "a", async () => await directoryFolder.GeneratingGraphsAndWritingToFileAsync()),
            ("display the graph in list and matrix form", "b", async () => await DisplayGraph()),
            ("calculating the time of all algorithms", "c", () => Console.WriteLine("c")),
            ("the Prim's algorithm - minimum spanning tree", "d", async () => await Algorithm("Prim")),
            ("the Kruskal's algorithm - minimum spanning tree", "e", async () => await Algorithm("Kruskal")),
            ("the Dijkstra's algorithm - shortest path", "f", async () => await Algorithm("Dijkstra")),
            ("the Ford-Bellman's algorithm - shortest path", "g", async () => await Algorithm("Ford-Bellman")),
            ("the Ford-Fulkerson's algorithm - maximum flow", "h", async () => await Algorithm("Ford-Fulkerson"))
        };

        bool continuing = true;
        while (continuing)
        {
            Console.Clear();
            Console.WriteLine("MENU");
            foreach (var (text, key, _) in menuOptions)
            {
                Console.WriteLine($"\t{key} - {text}");
            }

            bool wrongAction = true;
            while (wrongAction)
            {
                Console.WriteLine("\nPlease enter the symbol of the function");
                Console.Write("\tfunction: ");
                var keyChosen = Console.ReadLine();
                var action = menuOptions.Where(x => x.key == keyChosen.ToLower()).FirstOrDefault().action;
                if (action != null)
                {
                    action();
                    wrongAction = false;
                }
                else
                {
                    Console.WriteLine("Wrong symbol. Try again, please");
                    wrongAction = true;
                    Console.WriteLine("Please enter any key");
                    Console.ReadLine();
                    break;
                }
                continuing = Answer();
                Console.ReadLine();
            }   
        }
    }

    private bool Answer()
    {
        bool correct = false;
        while (!correct)
        {
            Console.WriteLine("\nDo you want to continue?");
            Console.Write("\tanswer (yes/no): ");

            var ans = Console.ReadLine();
            ans = ans.ToUpper();

            switch (ans)
            {
                case "YES":
                    Console.WriteLine("Please enter any key");
                    return true;
                case "NO":
                    Console.WriteLine("Please enter any key");
                    return false;
                default:
                    Console.WriteLine("Incorrect answer");
                    Console.WriteLine("Try again\n");
                    correct = false;
                    break;
            }
        }
        Console.WriteLine("Please enter any key");
        return false;
    }

    private async Task DisplayGraph()
    {
        bool wrongPath = true;
        while (wrongPath)
        {
            Console.WriteLine("\nPlease enter the path of the graph");
            Console.Write("\tpath: ");
            var path = Console.ReadLine();
            path = Path.Combine(MainPath, path);

            if (File.Exists(path))
            {
                WorkingWithFile workingWithFile = new WorkingWithFile();
                var graph = await workingWithFile.ReadDataFromFileAsync(path);

                Display display = new Display();
                display.DisplayListGraph(graph);
                display.DisplayMatrixGraph(graph);
                wrongPath = false;
            }
            else
            {
                Console.WriteLine("Wrong path. Try again, please\n");
                wrongPath = true;
            }
        }
    }

    private async Task Algorithm(string algorithm)
    {
        string[] algorithms = { "Prim", "Kruskal", "Dijkstra", "Ford-Bellman", "Ford-Fulkerson"};
        bool wrongPath = true;
        while (wrongPath)
        {
            Console.WriteLine("\nPlease enter the path of the graph");
            Console.Write("\tpath: ");
            var path = Console.ReadLine();
            path = Path.Combine(MainPath, path);

            if (File.Exists(path))
            {
                Console.WriteLine("\nPlease the representation of the graph (list/matrix)");
                Console.Write("\trepresentation: ");
                var representation = Console.ReadLine();
                bool wrongRepresentation = true;

                WorkingWithFile workingWithFile = new WorkingWithFile();
                var graph = await workingWithFile.ReadDataFromFileAsync(path);

                while (wrongRepresentation) 
                {
                    if (representation == "list" || representation == "matrix")
                    {
                        var algorithmsOptions = new (string key, Action action)[]
                        {
                            ("Prim", () => PrimAlgorithm(graph, representation)),
                            ("Kruskal", () => KruskalAlgorithm(graph, representation)),
                            ("Dijkstra", () => DijkstraAlgorithm(graph, representation)),
                            ("Ford-Bellman", () => FordBellmanAlgorithm(graph, representation)),
                            ("Ford-Fulkerson", () => FordFulkersonAlgorithm(graph, representation))
                        };

                        var action = algorithmsOptions.Where(x => x.key == algorithm).FirstOrDefault().action;
                        if (action != null)
                        {
                            action();
                        }
                        wrongRepresentation = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong representation. Try again, please\n");
                        wrongRepresentation = true;
                        break;
                    }
                }
                wrongPath = false;
            } 
            else
            {
                Console.WriteLine("Wrong path. Try again, please\n");
                wrongPath = true;
            }
        }
    }

    private void PrimAlgorithm(Graph graph, string representation)
    {
        MatrixAlgorithms matrixAlgorithms = new MatrixAlgorithms();
        ListAlgorithms listAlgorithms = new ListAlgorithms();
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Restart();
        List <Edge> result = new List<Edge>();

        if (representation == "matrix")
        {
            stopwatch.Start();
            result = matrixAlgorithms.PrimMatrix(graph);
            stopwatch.Stop();
        }else
        {
            stopwatch.Start();
            result = listAlgorithms.PrimList(graph);
            stopwatch.Stop();
        }

        Console.WriteLine("\nThe Prim's Algorithm");
        foreach (var edge in result)
        {
            Console.WriteLine($"\tSource: {edge.Source.Id} Destination: {edge.Destination.Id} Weight: {edge.Weight}");
        }
        Console.WriteLine("Time: " + stopwatch.Elapsed.Microseconds + " ms");
    }

    private void KruskalAlgorithm(Graph graph, string representation)
    {
        MatrixAlgorithms matrixAlgorithms = new MatrixAlgorithms();
        ListAlgorithms listAlgorithms = new ListAlgorithms();
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Restart();
        List<Edge> result = new List<Edge>();

        if (representation == "matrix")
        {
            stopwatch.Start();
            result = matrixAlgorithms.KruskalMatrix(graph);
            stopwatch.Stop();
        }
        else
        {
            stopwatch.Start();
            result = listAlgorithms.KruskalList(graph);
            stopwatch.Stop();
        }

        Console.WriteLine("\nThe Kruskal's Algorithm");
        foreach (var edge in result)
        {
            Console.WriteLine($"\tSource: {edge.Source.Id} Destination: {edge.Destination.Id} Weight: {edge.Weight}");
        }
        Console.WriteLine("Time: " + stopwatch.Elapsed.Microseconds + " ms");
    }

    private void DijkstraAlgorithm(Graph graph, string representation)
    {
        MatrixAlgorithms matrixAlgorithms = new MatrixAlgorithms();
        ListAlgorithms listAlgorithms = new ListAlgorithms();
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Restart();
        int[] result;

        if (representation == "matrix")
        {
            stopwatch.Start();
            result = matrixAlgorithms.DijkstraMatrix(graph);
            stopwatch.Stop();
        }
        else
        {
            stopwatch.Start();
            result = listAlgorithms.DijkstraList(graph);
            stopwatch.Stop();
        }

        Console.WriteLine("\nThe Dijkstra's Algorithm");
        for (int i = 0; i < result.Length; i++)
        {
            Console.WriteLine($"\tVertex {i}: {result[i]}");
        }
        Console.WriteLine("Time: " + stopwatch.Elapsed.Microseconds + " ms");
    }

    private void FordBellmanAlgorithm(Graph graph, string representation)
    {
        MatrixAlgorithms matrixAlgorithms = new MatrixAlgorithms();
        ListAlgorithms listAlgorithms = new ListAlgorithms();
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Restart();
        List<int> result = new List<int>();

        if (representation == "matrix")
        {
            stopwatch.Start();
            result = matrixAlgorithms.BellmanFordMatrix(graph);
            stopwatch.Stop();
        }
        else
        {
            stopwatch.Start();
            result = listAlgorithms.BellmanFordList(graph);
            stopwatch.Stop();
        }

        Console.WriteLine("\nThe Ford-Bellman's Algorithm");
        for (int i = 0; i < result.Count; i++)
        {
            Console.WriteLine($"\tVertex {i}: {result[i]}");
        }
        Console.WriteLine("Time: " + stopwatch.Elapsed.Microseconds + " ms");
    }

    private void FordFulkersonAlgorithm(Graph graph, string representation)
    {
        MatrixAlgorithms matrixAlgorithms = new MatrixAlgorithms();
        ListAlgorithms listAlgorithms = new ListAlgorithms();
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Restart();
        int result;

        if (representation == "matrix")
        {
            stopwatch.Start();
            result = matrixAlgorithms.FordFulkersonMatrix(graph);
            stopwatch.Stop();
        }
        else
        {
            stopwatch.Start();
            result = listAlgorithms.FordFulkersonList(graph);
            stopwatch.Stop();
        }
        Console.WriteLine("\nThe Ford-Fulkerson's Algorithm");
        Console.WriteLine("\tmaximum flow: " + result);
        Console.WriteLine("Time: " + stopwatch.Elapsed.Microseconds + " ms");
    }
}
