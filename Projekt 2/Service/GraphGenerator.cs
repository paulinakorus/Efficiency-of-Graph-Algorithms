using Projekt_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class GraphGenerator
{
    private Random random = new Random();

    public List<Graph> GenerateGraphList(int numberOfGraphs, int numberOfVertices, double density)
    {
        List<Graph> graphs = new List<Graph>();

        Parallel.For(0, numberOfGraphs, x =>
        {
            graphs.Add(GenerateRandomGraph(numberOfVertices, density));
        });

        return graphs;
    }

    public Graph GenerateRandomGraph(int numberOfVertices, double density)
    {
        List<Vertex> vertices = GenerateVertices(numberOfVertices);
        List<Edge> edges = GenerateEdges(vertices, density);
        return new Graph(vertices, edges);
    }

    private List<Vertex> GenerateVertices(int numberOfVertices)
    {
        List<Vertex> vertices = Enumerable.Range(0, numberOfVertices)
                                           .Select(i => new Vertex(i, 0))
                                           .ToList();
        return vertices;
    }

    private List<Edge> GenerateEdges(List<Vertex> verticesList, double density)
    {
        int maxPossibleEdges = (verticesList.Count * (verticesList.Count - 1)) / 2;             // Max possible edges in a complete graph
        int edges = (int)Math.Round(maxPossibleEdges * density / 100);
        HashSet<Tuple<int, int>> usedEdges = new HashSet<Tuple<int, int>>();

        List<Edge> edgeList = new List<Edge>();

        // Connect each vertex with at least one other vertex
        for (int i = 0; i < verticesList.Count; i++)
        {
            int destination = random.Next(verticesList.Count);
            while (destination == i)
            {
                destination = random.Next(verticesList.Count);
            }
            int weight = random.Next(1, 10); // Random weight from 1 to 10
            edgeList.Add(new Edge(verticesList[i], verticesList[destination], weight));
            usedEdges.Add(new Tuple<int, int>(Math.Min(i, destination), Math.Max(i, destination)));
        }

        edges -= verticesList.Count; // Reduce the remaining edges count

        // Create remaining edges
        while (edges > 0)
        {
            int source = random.Next(verticesList.Count);
            int destination = random.Next(verticesList.Count);

            if (source != destination && !usedEdges.Contains(new Tuple<int, int>(Math.Min(source, destination), Math.Max(source, destination))))
            {
                usedEdges.Add(new Tuple<int, int>(Math.Min(source, destination), Math.Max(source, destination)));
                int weight = random.Next(1, 5); // Random weight from 1 to 5
                edgeList.Add(new Edge(verticesList[source], verticesList[destination], weight));
                edges--;
            }
        }

        return edgeList;
    }
}
