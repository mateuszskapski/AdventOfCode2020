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

            input.Insert(0, 0);
            input.Insert(input.Count, input.Last() + 3);
            int combinationsCount = CountCombinations(input);
        }

        private static int CountCombinations(List<int> input)
        {
            int count = 0;
            SequenceFinder sequenceFinder = new SequenceFinder();

            for (int i = 0; i < input.Count; i++)
            {
                sequenceFinder.Add(input[i]);
            }

            foreach (var range in sequenceFinder.GetSequences())
            {
                Console.WriteLine($"S: {range.NumStart} E: {range.NumEnd} Count: {range.Count} Combinations: {range.CombinationCount}");
            }
            Console.WriteLine($"Total adapter combinations: {sequenceFinder.GetSequences().Select(x => x.CombinationCount).Aggregate(1, (long x, int y) => x * y)}");

            return count;
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

        class Range
        {
            public int NumStart { get; set; }
            public int NumEnd { get; set; }
            public int Count { get; set; }
            public int CombinationCount { get; set; } = 1;

            public Range(int numStart, int numEnd)
            {
                NumStart = numStart;
                NumEnd = numEnd;
                Count = numEnd - numStart - 1;
                
                for (int i = Count; i > 0; i--)
                {
                    CombinationCount += i;
                }
            }
        }

        class SequenceFinder
        {
            private int _sequenceStart = 0;
            private int _previousNum = 0;

            private List<Range> _ranges = new List<Range>();

            public void Add(int num)
            {
                if (num == 0)
                {
                    _sequenceStart = 0;
                    _previousNum = 0;

                    return; 
                }

                if (num - _previousNum == 1)
                {
                    _previousNum = num;
                    return;
                }
                else
                {
                    if (_previousNum > _sequenceStart)
                    {
                        _ranges.Add(new Range(_sequenceStart, _previousNum));
                    }

                    _previousNum = num;
                    _sequenceStart = num;
                }
            }

            public List<Range> GetSequences() => _ranges;
        }
    }
}
