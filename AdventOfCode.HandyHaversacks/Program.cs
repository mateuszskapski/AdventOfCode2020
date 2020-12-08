using AdventOfCode.Common;
using System;
using System.IO;

namespace AdventOfCode.HandyHaversacks
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser fileParser1 = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt"), Environment.NewLine);
        }
    }
}
