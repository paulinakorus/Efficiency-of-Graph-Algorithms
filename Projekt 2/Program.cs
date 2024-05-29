using Projekt_2.Model;
using Projekt_2.Service;

namespace Projekt_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DirectoryFolder directoryFolder = new DirectoryFolder();
            //await directoryFolder.GeneratingGraphsAndWritingToFileAsync();
            WorkingWithFile workingWithFile = new WorkingWithFile();
            var graph = await workingWithFile.ReadDataFromFileAsync("C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\undirected\\25%\\20\\0.txt");
            MatrixGraph matrix = new MatrixGraph();
            //var ma = matrix.AdjacencyMatrix(graph);
            
            //Algorithms algorithms = new Algorithms();
            //var list = graph.Vertices;
            //algorithms.Dijkstra(graph, list[0]);


            
            Vertex v0 = new Vertex(0, int.MaxValue);
            Vertex v1 = new Vertex(1, int.MaxValue);
            Vertex v2 = new Vertex(2, int.MaxValue);
            Vertex v3 = new Vertex(3, int.MaxValue);
            Vertex v4 = new Vertex(4, int.MaxValue);
            List<Vertex> vertices = new List<Vertex>{ v0, v1, v2, v3, v4 };

            Edge e01 = new Edge(v0, v1, 10);
            Edge e03 = new Edge(v0, v4, 5);
            Edge e04 = new Edge(v1, v2, 1);
            Edge e05 = new Edge(v1, v4, 2);
            Edge e06 = new Edge(v2, v3, 4);
            Edge e07 = new Edge(v3, v4, 3);

            List<Edge> edges = new List<Edge> { e01, e03, e04, e05, e06, e07 };

            /*
             *
            Uploading uploading = new Uploading();
            uploading.UploadListGraph(graph1);
            uploading.UploadMatrixGraph(graph1);
            ListGraph matrixList = new ListGraph();
            */

            Graph graph1 = new Graph(vertices, edges);

            Algorithms algorithms = new Algorithms();
            var start = graph.Vertices[0];
            //ListGraph listGraph = new ListGraph();
            //var list = listGraph.GetPredecessors(graph);

            algorithms.BellmanFordMatrix(graph);
            
        }
    }
}
