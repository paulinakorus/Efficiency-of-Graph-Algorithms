using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Model;

internal class MatrixGraph
{
    public int[,] MatrixExample { get; set; }

    public int[,] GeneratingMatrix(Graph graph)
    {
        int numVertices = graph.Vertices.Count;
        MatrixExample = new int[numVertices, numVertices];

        // Inicjalizacja macierzy wartościami 0
        for (int i = 0; i < numVertices; i++)
        {
            for (int j = 0; j < numVertices; j++)
            {
                MatrixExample[i, j] = 0;
            }
        }

        // Ustawianie wartości 1 w miejscach, gdzie istnieją krawędzie
        foreach (var edge in graph.Edges)
        {
            int sourceIndex = edge.Source.Id;
            int destinationIndex = edge.Destination.Id;
            MatrixExample[sourceIndex, destinationIndex] = edge.Weight; // Możesz ustawić wagę krawędzi, jeśli to konieczne
            // Jeśli graf jest nieskierowany, odkomentuj poniższą linię
            // Matrix[destinationIndex, sourceIndex] = 1;
        }
        return MatrixExample;
    }
}
