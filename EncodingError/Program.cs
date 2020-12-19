using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace EncodingError
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Array.ConvertAll<string, ulong>(File.ReadAllText(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt")).Split(Environment.NewLine), x => UInt64.Parse(x)).ToList();

            int preambleSize = 25;
            int index = input.FindIndex(preambleSize, input.Count - preambleSize, x => SumPreamble(x, input.GetRange(input.IndexOf(x,preambleSize) - preambleSize, preambleSize)));

            if (index > -1)
            {
                Console.WriteLine($"Number not matching sum rule: {input[index]}");
            }
        }

        public static bool SumPreamble(ulong num, List<ulong> preamble)
        {   
            foreach (var n1 in preamble)
            {
                foreach (var n2 in preamble)
                {
                    if (n1 == n2) continue;

                    if (n1 + n2 == num)
                        return false;
                }
            }

            return true;
        }

    }
}
