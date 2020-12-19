using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Common;

namespace AdapterArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Array.ConvertAll<string, int>(File.ReadAllText(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt")).Split(Environment.NewLine), x => Int32.Parse(x)).ToList();
            input.Sort();

            Dictionary<int, int> ratings = new Dictionary<int, int>();
            ratings.Add(1, 0);
            ratings.Add(3, 0);
            int previousRating = 0;
            input.ForEach(rating => CountDifferences(rating, ref previousRating, ref ratings));

            Console.WriteLine($"Diff of 1: {ratings[1]}, diff of 3: {ratings[3] + 1}");
            Console.WriteLine($"Part one answer is: {ratings[1] * (ratings[3] +1)}");
        }

        private static void CountDifferences(int rating, ref int previousRating, ref Dictionary<int, int> ratings)
        {
            int diff = rating - previousRating;

            switch(diff)
            {
                case 1: ratings[1]++; break;
                case 3: ratings[3]++; break;
                default: throw new InvalidDataException($"Difference of {diff} is not supported.");
            }

            previousRating = rating;
        }
    }
}
