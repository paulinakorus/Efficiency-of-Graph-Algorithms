using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Model;

internal class Vertex
{
    public int Id { get; set; }
    public int Weight { get; set; }
    public Vertex(int id, int weight)
    {
        Id = id;
        Weight = weight;
    }
}
