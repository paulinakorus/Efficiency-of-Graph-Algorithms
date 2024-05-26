using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class DirectoryFolder
{
    private string BeginningPath { get; set; } = "C:\\Users\\pauko\\Desktop\\Studia\\Semestr IV\\AIZO\\Projekt\\Projekt 2\\Projekt 2\\Data\\";
    private async Task GeneratingDifferentTypesAsync()
    {
        string[] mainPaths = { Path.Combine(BeginningPath, @"25%\\"), Path.Combine(BeginningPath, @"50%\\"), Path.Combine(BeginningPath, @"99%\\") };
        // 10, 20, 50, 100, 200, 500, 1000 | 25%, 50% oraz 99%


    }

    private async Task OpenTypeFolderAscync(string path)
    {

    }
}
