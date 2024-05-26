using Projekt_2.Service;

namespace Projekt_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GraphGenerator graphGenerator = new GraphGenerator();
            var g = graphGenerator.GenerateRandomGraph(10, 25);
        }
    }
}
