using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Model;

internal class Edge
{
    public Vertex Source { get; set; }
    public Vertex Destination { get; set; }
    public int Weight { get; set; }
    public Edge(Vertex source, Vertex destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
}
