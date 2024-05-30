using Projekt_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class Uploading
{
    public void UploadListGraph(Graph graph)
    {
        ListGraph listGraph = new ListGraph();
        var adjacencyList = listGraph.GeneratedList(graph);

        foreach (var kvp in adjacencyList)
        {
            Console.Write($"{kvp.Key}: ");
            foreach (var neighbor in kvp.Value)
            {
                Console.Write($"{neighbor} ");
            }
            Console.WriteLine();
        }

    }

    public void UploadMatrixGraph(Graph graph)
    {
        MatrixGraph matrix = new MatrixGraph();
        matrix.GeneratingMatrix(graph);
        var matrixExample = matrix.MatrixExample;
        var len = graph.Vertices.Count;

        Console.WriteLine(graph.Edges.Count + " " + graph.Vertices.Count);
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                Console.Write(matrixExample[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
