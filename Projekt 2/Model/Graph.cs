using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Model;

internal class Graph
{
    public List<Vertex> Vertices { get; set; } = new List<Vertex>();
    public List<Edge> Edges { get; set; } = new List<Edge>();

    public Graph(List<Vertex> vertices, List<Edge> edges)
    {
        Vertices = vertices;
        Edges = edges;
    }

    public List<Vertex> GetNeighbors(Vertex vertex)
    {
        List<Vertex> neighbors = new List<Vertex>();
        foreach (var edge in Edges)
        {
            if (edge.Source.Equals(vertex))
            {
                neighbors.Add(edge.Destination);
            }
        }
        return neighbors;
    }
}
