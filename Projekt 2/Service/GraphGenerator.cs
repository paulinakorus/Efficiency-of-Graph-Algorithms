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

    public List<Graph> GenerateGraphList(int numberOfGraphs, int numberOfVertices, double density, string type)
    {
        List<Graph> graphs = new List<Graph>();

        Parallel.For(0, numberOfGraphs, x =>
        {
            graphs.Add(GenerateRandomGraph(numberOfVertices, density, type));
        });

        return graphs;
    }

    private Graph GenerateRandomGraph(int numberOfVertices, double density, string type)
    {
        List<Vertex> vertices = GenerateVertices(numberOfVertices);
        List<Edge> edges = GenerateEdges(vertices, density, type);
        return new Graph(vertices, edges);
    }

    private List<Vertex> GenerateVertices(int numberOfVertices)
    {
        List<Vertex> vertices = Enumerable.Range(0, numberOfVertices)
                                           .Select(i => new Vertex(i, int.MaxValue))
                                           .ToList();
        return vertices;
    }

    private List<Edge> GenerateEdges(List<Vertex> verticesList, double density, string type)
    {
        int maxPossibleEdges = (type == "undirected") ? verticesList.Count * (verticesList.Count - 1) / 2 : verticesList.Count * (verticesList.Count - 1);
        int edges = (int)Math.Round(maxPossibleEdges * density / 100);
        HashSet<Tuple<int, int>> usedEdges = new HashSet<Tuple<int, int>>();
        List<Edge> edgeList = new List<Edge>();

        // Create a minimum spanning tree to ensure all vertices are connected
        List<int> connected = new List<int> { 0 }; // Start with the first vertex
        List<int> remaining = Enumerable.Range(1, verticesList.Count - 1).ToList();

        while (remaining.Any())
        {
            int sourceIndex = connected[random.Next(connected.Count)];
            int destinationIndex = remaining[random.Next(remaining.Count)];
            int weight = random.Next(1, 10);

            edgeList.Add(new Edge(verticesList[sourceIndex], verticesList[destinationIndex], weight));
            usedEdges.Add(new Tuple<int, int>(Math.Min(sourceIndex, destinationIndex), Math.Max(sourceIndex, destinationIndex)));

            connected.Add(destinationIndex);
            remaining.Remove(destinationIndex);
        }

        edges -= verticesList.Count - 1; // Reduce the remaining edges count by the edges already used in the minimum spanning tree

        // Create remaining edges to achieve the desired density
        while (edges > 0)
        {
            int source = random.Next(verticesList.Count);
            int destination = random.Next(verticesList.Count);

            if (source != destination && !usedEdges.Contains(new Tuple<int, int>(Math.Min(source, destination), Math.Max(source, destination))))
            {
                usedEdges.Add(new Tuple<int, int>(Math.Min(source, destination), Math.Max(source, destination)));
                int weight = random.Next(1, 5);
                edgeList.Add(new Edge(verticesList[source], verticesList[destination], weight));
                edges--;
            }
        }

        return edgeList;
    }
}
