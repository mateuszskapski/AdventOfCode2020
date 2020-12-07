using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;

namespace AdventOfCode.BinaryBoarding
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser parser = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt"), Environment.NewLine);
            var boardingCards = parser.ToStringList();

            List<int> ids = new List<int>();

            foreach (var card in boardingCards)
            {
                var bin = card.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
                ids.Add(Convert.ToInt32(bin, 2));
            }

            ids.Sort();

            Console.WriteLine($"Part One: {ids.Max()}");

            int seat = ids.Max();
            while (ids.Contains(seat))
                seat--;

            Console.WriteLine($"Part Two: {seat}");
        }
    }
}
