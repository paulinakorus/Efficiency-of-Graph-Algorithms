using Projekt_2.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class Algorithms
{
    public void Dijkstra(Graph graph, Vertex startVertex)
    {
        var pq = new SortedSet<Vertex>(Comparer<Vertex>.Create((v1, v2) =>
        {
            int result = v1.Weight.CompareTo(v2.Weight);
            if (result == 0)
            {
                return v1.Id.CompareTo(v2.Id); // Aby uniknąć problemów z równymi wagami
            }
            return result;
        }));

        foreach (var v in graph.Vertices)
        {
            if (v.Equals(startVertex))
            {
                v.Weight = 0;
            }
            else
            {
                v.Weight = int.MaxValue;
            }
            pq.Add(v);
        }

        while (pq.Count > 0)
        {
            var u = pq.Min;
            pq.Remove(u);
            foreach (var v in graph.GetNeighbors(u))
            {
                int altDist = u.Weight + GetEdgeWeight(graph, u, v);
                if (altDist < v.Weight)
                {
                    pq.Remove(v);
                    v.Weight = altDist;
                    pq.Add(v);
                }
            }
        }

        // print the shortest distances
        Console.WriteLine($"Shortest distances from node {startVertex.Id} to:");
        foreach (var v in graph.Vertices)
        {
            Console.WriteLine($"{v.Id}: {v.Weight}");
        }
    }

    private static int GetEdgeWeight(Graph graph, Vertex u, Vertex v)
    {
        foreach (var e in graph.Edges)
        {
            if (e.Source.Equals(u) && e.Destination.Equals(v))
            {
                return e.Weight;
            }
        }
        throw new ArgumentException("Edge not found");
    }
}
