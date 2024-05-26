using Projekt_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class WorkingWithFile
{
    private string PathToData { get; set; } = "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\";
    public void WriteDataToFile(string option, Graph graph)
    {
        var files = Directory.GetFiles(option);
        int numberOfGraphs = files.Length;
        if (string.IsNullOrEmpty(option) && graph != null) 
        {
            string wholePath = Path.Combine(Path.Combine(PathToData, option + "\\"), numberOfGraphs + ".txt");
            using (StreamWriter input = new StreamWriter(wholePath, false))
            {
                input.WriteLine(graph.Edges.Count + " " + graph.Vertices.Count);
                foreach (var edge in graph.Edges)
                {
                    input.WriteLine(edge.Source + " " + edge.Destination + " " + edge.Weight);
                }
            }
        }
    }

    public async Task<Graph> ReadDataFromFile(string path)
    {
        using (var reader = new StreamReader(File.OpenRead(path)))
        {
            int iterator = 0;
            while (!reader.EndOfStream)
            {
                if (iterator == 0)
                {
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    var splittedLine = line.Split(" ");

                    List<Vertex> vertices = new List<Vertex>();
                    List<Edge> edges = new List<Edge>();

                }
                

            }
        }
    }
