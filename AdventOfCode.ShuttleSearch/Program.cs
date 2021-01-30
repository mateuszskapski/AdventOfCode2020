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

            //PartOne(input);

            PartTwo(input);
            

            static void PartOne(string[] input)
            {
                int currentTimestamp = int.Parse(input[0]);
                var nextBus = input[1].Replace("x,", string.Empty)
                    .Split(',')
                    .Select(int.Parse)
                    .Select(b => (b, currentTimestamp - (currentTimestamp % b) + b))
                    .OrderBy(b => b.Item2)
                    .Where(x => x.Item2 > currentTimestamp)
                    .First();

                Console.WriteLine($"Next bus ID: {nextBus.Item1} is at: {nextBus.Item2}");
                Console.WriteLine($"Part one answer: {(nextBus.Item2 - currentTimestamp) * nextBus.Item1}");
            }

            static void PartTwo(string[] input)
            {
                var buses = input[1].Replace("x,", "0")
                .Split(',')
                .Select(long.Parse)
                .ToList();

                /*
                    23,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,829,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29,x,677,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,19
                */

                bool keepGoing = true;
                while (keepGoing)
                {
                    var maxTimestampBus = buses.Where(b => b.Item2 == buses.Select(x => x.Item2).Max()).Select(b => b).Max();

                    var lastBus = buses.Select(b => b).Last();

                    if (lastBus.Item2 < maxTimestampBus.Item2 && lastBus.Item1 != maxTimestampBus.Item1)
                    {
                        lastBus = (lastBus.Item1,lastBus.Item1 * (maxTimestampBus.Item2 / lastBus.Item1) + lastBus.Item1);
                    }

                    ulong nextTimestamp = 0;
                    for (int i = buses.Count - 1; i >= 0; i--)
                    {
                        if (nextTimestamp == 0)
                        {
                            nextTimestamp = buses[i].Item2;
                            continue;
                        }

                        if (buses[i].Item2 > nextTimestamp)
                        {

                        }
                    }
                }
            }

        }
    }
}
