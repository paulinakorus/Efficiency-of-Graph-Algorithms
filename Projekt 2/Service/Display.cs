using Projekt_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class Display
{
    public void DisplayListGraph(Graph graph)
    {
        ListGraph listGraph = new ListGraph();
        var adjacencyList = listGraph.GeneratedList(graph);

        Console.WriteLine("\nList representation");
        foreach (var kvp in adjacencyList)
        {
            Console.Write($"\t{kvp.Key}: ");
            foreach (var neighbor in kvp.Value)
            {
                Console.Write($"{neighbor} ");
            }
            Console.WriteLine();
        }

    }

    public void DisplayMatrixGraph(Graph graph)
    {
        MatrixGraph matrix = new MatrixGraph();
        matrix.GeneratingMatrix(graph);
        var matrixExample = matrix.MatrixExample;
        var len = graph.Vertices.Count;

        Console.WriteLine("\nMatrix representation");
        Console.WriteLine("\t" + graph.Edges.Count + " " + graph.Vertices.Count);
        for (int i = 0; i < len; i++)
        {
            Console.Write("\t");
            for (int j = 0; j < len; j++)
            {
                Console.Write(matrixExample[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
