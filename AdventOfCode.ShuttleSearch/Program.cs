using AdventOfCode.Common;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.ShuttleSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt")).Split(Environment.NewLine);

            //int currentTimestamp = int.Parse(input[0]);
            //var nextBus = input[1].Replace("x,", string.Empty)
            //    .Split(',')
            //    .Select(int.Parse)
            //    .Select(b => (b, currentTimestamp - (currentTimestamp % b) + b))
            //    .OrderBy(b => b.Item2)
            //    .Where(x => x.Item2 > currentTimestamp)
            //    .First();

            //Console.WriteLine($"Next bus ID: {nextBus.Item1} is at: {nextBus.Item2}");
            //Console.WriteLine($"Part one answer: {(nextBus.Item2 - currentTimestamp) * nextBus.Item1}");

            var buses = input[1].Replace("x,", string.Empty)
                .Split(',')
                .Select(ulong.Parse)
                .ToList();

            foreach (var bus in buses)
            {
                Console.Write($"{bus}  ");
            }

            for (int i = 0; i < buses.Count(); i++)
            {
                Console.WriteLine(buses[i]);
                while (i < buses.Count() - 1 ? buses[i] + buses[i] < (buses[i + 1]) : buses[i] > buses[i - 1] + buses[i - 1] ||
                    i > 0 ? buses[i] > buses[i-1] : buses[i] < buses[i+1])
                {
                    buses[i] += buses[i];
                }

                int counter = 0;
                for (int j = 0; j < buses.Count(); j++)
                {
                    if (j < buses.Count() - 1 ? buses[j] < buses[j + 1] : buses[j] > buses[j - 1])
                    {
                        counter++;
                    }
                }

                if (counter == buses.Count)
                {
                    Console.WriteLine($"Nearest timestamp: {buses[i]}");
                    break;
                }

                if (i+1 == buses.Count)
                {
                    i = 0;
                }
            }



        }
    }
}
