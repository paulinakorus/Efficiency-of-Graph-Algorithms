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
    
    public async Task WriteListToFile(string option, List<Graph> graphs)
    {
        List<Task> tasks = new List<Task>();
        foreach (var graph in graphs)
        {
             tasks.Add(WriteDataToFile(option, graph));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine(option);
    }
    
    private async Task WriteDataToFile(string option, Graph graph)
    {
        string meddiumPath = Path.Combine(PathToData, option);
        var files = Directory.GetFiles(meddiumPath);
        int numberOfGraphs = files.Length;
        if (!string.IsNullOrEmpty(option) && graph != null)
        {
            string wholePath = Path.Combine(Path.Combine(PathToData, option + "\\"), numberOfGraphs + ".txt");
            using (StreamWriter input = new StreamWriter(wholePath, false))
            {
                input.WriteLine(graph.Edges.Count + " " + graph.Vertices.Count);
                foreach (var edge in graph.Edges)
                {
                    input.WriteLine(edge.Source.Id + " " + edge.Destination.Id + " " + edge.Weight);
                }
            }
        }
    }

    public async Task<Graph> ReadDataFromFile(string path)
    {
        using (var reader = new StreamReader(File.OpenRead(path)))
        {
            List<Vertex> verticesList = new List<Vertex>();
            List<Edge> edgesList = new List<Edge>();
            int verticesNumber = 0;
            int edgesNumber = 0;
            int iterator = 0;

            while (!reader.EndOfStream)
            {
                if (iterator == 0)
                {
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    var splittedLine = line.Split(" ");
                    int.TryParse(splittedLine[0], out edgesNumber);
                    int.TryParse(splittedLine[1], out verticesNumber);
                    iterator++;
                }
                else
                {
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    var splittedLine = line.Split(" ");
                    var source = new Vertex(int.Parse(splittedLine[0]), 0);
                    var destination = new Vertex(int.Parse(splittedLine[1]), 0);
                    verticesList = AddingVertex(source, destination, verticesList);

                    var edge = new Edge(source, destination, int.Parse(splittedLine[2]));
                    edgesList = AddingEdge(edge, edgesList);
                }
            }
            verticesList.Sort((a, b) => a.Id.CompareTo(b.Id));
            if (verticesList.Count == verticesNumber && edgesList.Count == edgesNumber)
            {
                return new Graph(verticesList, edgesList);
            }
            return null;
        }
    }

    private List<Vertex> AddingVertex(Vertex vertex, Vertex vertex1, List<Vertex> vertices)
    {
        if (!IfElementExist(vertices, vertex))
        {
            vertices.Add(vertex);
        }
        if (!IfElementExist(vertices, vertex1))
        {
            vertices.Add(vertex1);
        }
        return vertices;
    }

    private bool IfElementExist(List<Vertex> vertices, Vertex vertex)
    {
        foreach (var v in vertices) 
        {
            if (v.Id == vertex.Id)
                return true;
        }
        return false;
    }

    private List<Edge> AddingEdge(Edge edge, List<Edge> edgesList)
    {
        if (!edgesList.Contains(edge))
        {
            edgesList.Add(edge);
        }
        return edgesList;
    }
}
