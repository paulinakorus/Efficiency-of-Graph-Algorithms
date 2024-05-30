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
        Console.WriteLine(graph.Edges.Count + " " + graph.Vertices.Count);
        foreach (var edge in graph.Edges)
        {
            Console.WriteLine(edge.Source.Id + " " + edge.Destination.Id + " " + edge.Weight);
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
