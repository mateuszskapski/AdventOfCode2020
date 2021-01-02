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
    }
}
