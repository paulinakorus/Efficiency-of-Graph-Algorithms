using Projekt_2.Service;

namespace Projekt_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GraphGenerator graphGenerator = new GraphGenerator();
            var g = graphGenerator.GenerateRandomGraph(10, 25);
            WorkingWithFile workingWithFile = new WorkingWithFile();
            workingWithFile.WriteDataToFile("25%\\10", g);
            var g1 = workingWithFile.ReadDataFromFile("C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\25%\\10\\0.txt");
            var graph = await g1;
        }
    }
}
