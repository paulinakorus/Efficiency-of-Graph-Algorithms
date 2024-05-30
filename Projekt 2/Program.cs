using Projekt_2.Model;
using Projekt_2.Service;
using System.Threading.Tasks;

namespace Projekt_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Menu menu = new Menu();
            //menu.GeneratorMenu();
            
            DirectoryFolder directoryFolder = new DirectoryFolder();
            await directoryFolder.OpenAndCalculate();
        }
    }
}
