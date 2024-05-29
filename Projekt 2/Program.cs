using Projekt_2.Service;

namespace Projekt_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DirectoryFolder directoryFolder = new DirectoryFolder();
            await directoryFolder.GeneratingGraphsAndWritingToFileAsync();
        }
    }
}
