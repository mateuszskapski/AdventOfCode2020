using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Common
{
    public static class PathHelper
    {
        public static string ProjectRootFolder()
        {
            var currentDir = Environment.CurrentDirectory;

            return Directory.GetParent(currentDir).Parent.Parent.FullName;
        }
    }
}
