using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2.Service;

internal class TimeCalculator
{
    public List<double> AvTime { get; set; } = new List<double>();

    public List<double> AverageTime(List<List<double>> list)
    {
        List<double> all_sums = new List<double>();
        for (int i = 0; i < list[0].Count; i++)
        {
            var sum = list.Sum(sublist => sublist.ElementAtOrDefault(i));
            all_sums.Add(sum);
        }

        foreach (var sum in all_sums) 
        {
            var average_time = sum / list.Count;
            AvTime.Add(average_time);
        }
        return AvTime;
    }
}
