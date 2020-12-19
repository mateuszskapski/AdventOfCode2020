using AdventOfCode.Common;
using System;
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
        }
    }
}
