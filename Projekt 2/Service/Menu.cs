using System;
using System.Collections.Generic;
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
                break;
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
                    if (representation == "list")
                    {
                        ListAlgorithms listAlgorithms = new ListAlgorithms();
                        var algorithmsOptions = new (string key, Action action)[]
                        {
                            ("Prim", () => listAlgorithms.PrimList(graph)),
                            ("Kruskal", () => listAlgorithms.KruskalList(graph)),
                            ("Dijkstra", () => ),
                            ("Ford-Bellman", () => ),
                            ("Ford-Fulkerson", () => )
                        };

                        var action = algorithmsOptions.Where(x => x.key == algorithm).FirstOrDefault().action;
                        if (action != null)
                        {
                            action();
                        }

                        wrongRepresentation = false;
                    } 
                    else if (representation == "matrix")
                    {
                        MatrixAlgorithms matrixAlgorithms = new MatrixAlgorithms();

                        var algorithmsOptions = new (string key, Action action)[]
                        {
                            ("Prim", () => ),
                            ("Kruskal", () => ),
                            ("Dijkstra", () => ),
                            ("Ford-Bellman", () => ),
                            ("Ford-Fulkerson", () => )
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
                break;
            }
        }
    }
}
