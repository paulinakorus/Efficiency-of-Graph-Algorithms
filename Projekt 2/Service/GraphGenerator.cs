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

    public Graph GenerateRandomGraph(int numberOfVertices, double density)
    {
        List<Vertex> vertices = GenerateVertices(numberOfVertices);
        List<Edge> edges = GenerateEdges(vertices, density);
        return new Graph(vertices, edges);
    }

    private List<Vertex> GenerateVertices(int numberOfVertices)
    {
        List<Vertex> vertices = new List<Vertex>();
        for (int i = 0; i < numberOfVertices; i++)
        {
            vertices.Add(new Vertex(i, random.Next(1, 5)));
        }
        return vertices;
    }

    private List<Edge> GenerateEdges(List<Vertex> vertices, double density)
    {
        List<Edge> edges = new List<Edge>();
        int maxPossibleEdges = (vertices.Count * (vertices.Count - 1)) / 2;             // Max possible edges in a complete graph
        int numberOfEdges = (int)Math.Round(maxPossibleEdges * density / 100);

        HashSet<Tuple<int, int>> existingEdges = new HashSet<Tuple<int, int>>();        // To avoid duplicate edges

        for (int i = 0; i < numberOfEdges; i++)
        {
            int sourceIndex = random.Next(vertices.Count);
            int destinationIndex = random.Next(vertices.Count);

            // Avoid creating self-loops
            while (sourceIndex == destinationIndex)
            {
                destinationIndex = random.Next(vertices.Count);
            }

            var edgeTuple = new Tuple<int, int>(sourceIndex, destinationIndex);

            // Avoid duplicate edges
            if (!existingEdges.Contains(edgeTuple))
            {
                existingEdges.Add(edgeTuple);
                edges.Add(new Edge(vertices[sourceIndex], vertices[destinationIndex], random.Next(1, 5)));    // Assign random weight to edge
            }
            else
            {
                i--; // Try generating another edge
            }
        }

        return edges;
    }
}
