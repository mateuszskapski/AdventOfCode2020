using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode.ReportRepair
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser fileParser = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt"), Environment.NewLine);

            var ints = fileParser.ToIntList();
            Console.WriteLine($"Parsed {ints.Count} rows.");

            // FInding duplicates
            IEnumerable<int> duplicates = ints.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            Console.WriteLine("Duplicate elements are: " + String.Join(",", duplicates));

            if (duplicates.Count() > 0)
            {
                Console.WriteLine("Duplicates present. This algorithm will not work :)");
                return;
            }    

            foreach (int n1 in ints)
            {
                foreach (int n2 in ints)
                {
                    if (n1 == n2) continue;

                    foreach (int n3 in ints)
                    {
                        if (n2 == n3) continue;

                        var result = (n1 + n2 + n3) == 2020 ? n1 * n2 * n3 : 0;

                        if (result > 0)
                        {
                            Console.WriteLine($"Sum of number {n1}, {n2} and {n3} is 2020. Multiply result is: {result}");
                            return;
                        }
                    }
                }
            }

        }
    }
}
