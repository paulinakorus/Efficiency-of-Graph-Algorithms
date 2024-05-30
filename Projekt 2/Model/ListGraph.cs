using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Model;

internal class ListGraph
{
    public int[,] ListExample { get; set; }

    public int[,] GeneratingList(Graph graph)
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
    }

    /*
     * public Dictionary<int, List<int>> GeneratingList(Graph graph)
    {
        Dictionary<int, List<int>> adjacencyList = new Dictionary<int, List<int>>();

        // Inicjalizujemy listę sąsiedztwa dla każdego wierzchołka
        foreach (var vertex in graph.Vertices)
        {
            adjacencyList[vertex.Id] = new List<int>();
        }

        // Przechodzimy przez każdą krawędź
        foreach (var edge in graph.Edges)
        {
            // Dodajemy krawędź do listy sąsiedztwa tylko jeśli jej źródło znajduje się w wierzchołkach grafu
            if (adjacencyList.ContainsKey(edge.Source.Id))
            {
                adjacencyList[edge.Source.Id].Add(edge.Destination.Id);
            }
            else
            {
                // Jeśli źródło krawędzi nie znajduje się w wierzchołkach grafu, zgłaszamy błąd
                throw new ArgumentException("Invalid edge source vertex.");
            }
        }

        foreach (var kvp in adjacencyList)
        {
            Console.Write($"Następnicy dla wierzchołka {kvp.Key}: ");
            foreach (var neighbor in kvp.Value)
            {
                Console.Write($"{neighbor} ");
            }
            Console.WriteLine();
        }

        return adjacencyList;
    }*/
}
