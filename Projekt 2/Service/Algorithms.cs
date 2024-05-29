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
    public int[] DijkstraMatrix(Graph graph, Vertex vertex)
    {
        var startVertex = vertex.Id;
        MatrixGraph matrixGraph = new MatrixGraph();
        matrixGraph.AdjacencyMatrix(graph);
        var matrixExample = matrixGraph.MatrixExample;
        Uploading uploading = new Uploading();
        uploading.UploadMatrixGraph(graph);

        int nVertices = matrixExample.GetLength(0);

        // Najkrótsze odległości od wierzchołka początkowego do wszystkich innych wierzchołków
        int[] shortestDistances = new int[nVertices];

        // czy dany wierzchołek został odwiedzony
        bool[] added = new bool[nVertices];

        // Inicjalizacja wszystkich odległości na nieskończoność
        for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
        {
            shortestDistances[vertexIndex] = int.MaxValue;
            added[vertexIndex] = false;
        }

        // Odległość od wierzchołka początkowego do siebie samego jest zawsze 0
        shortestDistances[startVertex] = 0;

        // Algorytm Dijkstry
        for (int i = 0; i < nVertices - 1; i++)
        {
            // Znajdowanie wierzchołka o minimalnej odległości
            int nearestVertex = -1;
            int shortestDistance = int.MaxValue;
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (!added[vertexIndex] && shortestDistances[vertexIndex] < shortestDistance)
                {
                    nearestVertex = vertexIndex;
                    shortestDistance = shortestDistances[vertexIndex];
                }
            }
            if (nearestVertex == -1)
            {
                break;
            }
            // Oznacz wybrany wierzchołek jako odwiedzony
            added[nearestVertex] = true;

            // Aktualizacja odległości sąsiadów
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                int edgeDistance = matrixExample[nearestVertex, vertexIndex];

                if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < shortestDistances[vertexIndex]))
                {
                    shortestDistances[vertexIndex] = shortestDistance + edgeDistance;
                }
            }
        }

        Console.WriteLine("Najkrótsze odległości od wierzchołka początkowego:");
        for (int i = 0; i < shortestDistances.Length; i++)
        {
            Console.WriteLine($"Wierzchołek {i}: {shortestDistances[i]}");
        }
        return shortestDistances;
    }

    /*public Dictionary<Vertex, int> DijkstraList (Graph graph, Vertex startVertex)
    {
        ListGraph listGraph = new ListGraph();
        var graphList = listGraph.AdjacencyList(graph);

        var distances = new Dictionary<Vertex, int>();
        var priorityQueue = new SortedSet<(int distance, Vertex vertex)>();

        foreach (var vertex in graphList.Keys)
        {
            distances[vertex] = int.MaxValue;
        }

        distances[startVertex] = 0;
        priorityQueue.Add((0, startVertex));

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentVertex) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            foreach (var neighbor in graphList[currentVertex])
            {
                var edgeWeight = GetEdgeWeight(graph.Edges, currentVertex, neighbor);
                var newDist = currentDistance + edgeWeight;

                if (newDist < distances[neighbor])
                {
                    priorityQueue.Remove((distances[neighbor], neighbor));
                    distances[neighbor] = newDist;
                    priorityQueue.Add((newDist, neighbor));
                }
            }
        }

        Console.WriteLine("Najkrótsze odległości od wierzchołka początkowego:");
        foreach (var vertex in distances.Keys)
        {
            Console.WriteLine($"Wierzchołek {vertex.Id}: {distances[vertex]}");
        }

        return distances;
    }

    private int GetEdgeWeight(List<Edge> edges, Vertex source, Vertex destination)
    {
        foreach (var edge in edges)
        {
            if (edge.Source.Equals(source) && edge.Destination.Equals(destination))
            {
                return edge.Weight;
            }
        }
        return int.MaxValue; // W przypadku gdy krawędź nie istnieje, zwróć maksymalną wartość
    }*/

    public List<int> BellmanFordMatrix(Graph graph)
    {
        MatrixGraph matrixGraph = new MatrixGraph();
        var adjacencyMatrix = matrixGraph.AdjacencyMatrix(graph);
        var source = graph.Vertices.First().Id;

        int numVertices = adjacencyMatrix.GetLength(0);
        List<int> distances = Enumerable.Repeat(int.MaxValue, numVertices).ToList();
        distances[source] = 0;

        // Relaksacja krawędzi
        for (int i = 0; i < numVertices - 1; i++)
        {
            for (int u = 0; u < numVertices; u++)
            {
                for (int v = 0; v < numVertices; v++)
                {
                    if (adjacencyMatrix[u, v] != 0 && distances[u] != int.MaxValue && distances[u] + adjacencyMatrix[u, v] < distances[v])
                    {
                        distances[v] = distances[u] + adjacencyMatrix[u, v];
                    }
                }
            }
        }

        // Sprawdzenie cykli ujemnych
        for (int u = 0; u < numVertices; u++)
        {
            for (int v = 0; v < numVertices; v++)
            {
                if (adjacencyMatrix[u, v] != 0 && distances[u] != int.MaxValue && distances[u] + adjacencyMatrix[u, v] < distances[v])
                {
                    throw new InvalidOperationException("Graf zawiera cykl ujemny");
                }
            }
        }

        Console.WriteLine("Najkrótsze odległości od wierzchołka początkowego:");
        for (int i = 0; i < distances.Count; i++)
        {
            Console.WriteLine($"Wierzchołek {i}: {distances[i]}");
        }
        return distances;
    }

    public List<Edge> PrimMatrix(Graph graph)
    {
        MatrixGraph matrixGraph = new MatrixGraph();
        var adjacencyMatrix = matrixGraph.AdjacencyMatrix(graph);

        int numVertices = adjacencyMatrix.GetLength(0);
        bool[] visited = new bool[numVertices];
        List<Edge> mst = new List<Edge>();

        // Start from the first vertex
        visited[0] = true;

        while (mst.Count < numVertices - 1)
        {
            int minWeight = int.MaxValue;
            Vertex nullVertex = new Vertex(-1);
            Edge minEdge = new Edge(nullVertex, nullVertex, int.MaxValue);

            // Find the minimum weight edge
            for (int i = 0; i < numVertices; i++)
            {
                if (visited[i])
                {
                    for (int j = 0; j < numVertices; j++)
                    {
                        if (!visited[j] && adjacencyMatrix[i, j] != 0 && adjacencyMatrix[i, j] < minWeight)
                        {
                            Vertex vertexI = new Vertex(i);
                            Vertex vertexJ = new Vertex(j);
                            minEdge = new Edge(vertexI, vertexJ, adjacencyMatrix[i, j]);
                            minWeight = adjacencyMatrix[i, j];
                        }
                    }
                }
            }

            if (minEdge.Source.Id == -1 || minEdge.Destination.Id == -1)
            {
                throw new Exception("Graph is not connected.");
            }

            // Mark the destination vertex as visited
            visited[minEdge.Destination.Id] = true;

            // Add the minimum weight edge to the minimum spanning tree
            mst.Add(minEdge);
        }

        Console.WriteLine("Minimum Spanning Tree (edges and their weights):");
        foreach (var edge in mst)
        {
            Console.WriteLine($"Source: {edge.Source.Id} Destination: {edge.Destination.Id} Weight: {edge.Weight}");
        }

        return mst;
    }
}
