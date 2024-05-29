using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Model;

internal class ListGraph
{
    //public int[,] ListExample { get; set; }

    /*public int[,] AdjacencyList(Graph graph)
    {
        int numVertices = graph.Vertices.Count;
        ListExample = new int[numVertices, numVertices];

        // Inicjalizacja macierzy wartościami 0
        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                ListExample[i, j] = 0;
            }
        }

        // Ustawianie wartości 1 w miejscach, gdzie istnieją krawędzie
        foreach (var edge in graph.Edges)
        {
            int sourceIndex = edge.Source.Id;
            int destinationIndex = edge.Destination.Id;
            ListExample[sourceIndex, destinationIndex] = edge.Weight; // Możesz ustawić wagę krawędzi, jeśli to konieczne
            // Jeśli graf jest nieskierowany, odkomentuj poniższą linię
            // Matrix[destinationIndex, sourceIndex] = 1;
        }
        return ListExample;
    }*/

    Dictionary<Vertex, List<Vertex>> ListExample = new Dictionary<Vertex, List<Vertex>>();
    public Dictionary<Vertex, List<Vertex>> GetPredecessors(Graph graph)
    {
        var startVertex = graph.Vertices.First();
        // Inicjalizacja listy odwiedzonych wierzchołków i kolejki BFS
        var visited = new HashSet<Vertex>();
        var queue = new Queue<Vertex>();
        queue.Enqueue(startVertex);

        // Inicjalizacja listy poprzedników
        ListExample[startVertex] = null;

        while (queue.Count > 0)
        {
            // Pobranie wierzchołka z kolejki
            var vertex = queue.Dequeue();

            // Jeśli wierzchołek nie był jeszcze odwiedzony
            if (!visited.Contains(vertex))
            {
                // Dodanie go do listy odwiedzonych
                visited.Add(vertex);

                // Przetworzenie sąsiadów wierzchołka
                foreach (var neighbor in graph.GetNeighbors(vertex))
                {
                    if (!visited.Contains(neighbor))
                    {
                        // Dodanie sąsiada do kolejki
                        queue.Enqueue(neighbor);

                        // Dodanie informacji o poprzedniku
                        if (!ListExample.ContainsKey(neighbor))
                        {
                            ListExample[neighbor] = new List<Vertex>();
                        }
                        ListExample[neighbor].Add(vertex);
                    }
                }
            }
        }

        return ListExample;
    }
}
