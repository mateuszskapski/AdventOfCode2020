using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode.TobogganTrajectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt");
            FileParser parser = new FileParser(filePath, Environment.NewLine);

            List<string> forest = parser.ToStringList();

            int c1 = CountTrees(forest, 1, 1);
            Console.WriteLine($"Encounter r:1 d:1 {c1} trees.");

            int c2 = CountTrees(forest, 3, 1);
            Console.WriteLine($"Encounter r:3 d:1 {c2} trees.");

            int c3 = CountTrees(forest, 5, 1);
            Console.WriteLine($"Encounter r:5 d:1 {c3} trees.");

            int c4 = CountTrees(forest, 7, 1);
            Console.WriteLine($"Encounter r:7 d:1 {c4} trees.");

            int c5 = CountTrees(forest, 1, 2);
            Console.WriteLine($"Encounter r:1 d:2 {c5} trees.");

            ulong together = (ulong)c1 * (ulong)c2 * (ulong)c3 * (ulong)c4 * (ulong)c5;
            Console.WriteLine($"Trees all together {together}");
        }

        static int CountTrees(List<string> forest, int positionRight, int positionDown)
        {
            int row = positionDown;
            int col = 0;
            int treesCount = 0;
            for (; row < forest.Count; row = row + positionDown)
            {
                col = col + positionRight;
                if (col >= forest[row].Length)
                {
                    col = col - forest[row].Length;
                }

                char x = forest[row][col];
                if (x.Equals('#'))
                {
                    treesCount++;
                    //Console.WriteLine($"Tree position ({positionRight},{positionDown}). R: {row} C: {col}");
                }
            }

            return treesCount;
        }
    }
}
