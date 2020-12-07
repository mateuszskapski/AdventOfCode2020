using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Common
{
    public class FileParser
    {
        private readonly string _path;
        private readonly string _separator;

        public FileParser(string path, string separator)
        {
            _path = path;
            _separator = separator;
        }

        private string FetchData() => File.ReadAllText(_path);

        public List<int> ToIntList()
        {
            string data = FetchData();

            return new List<int>(Array.ConvertAll<string, int>(data.Split(_separator), (input => Int32.Parse(input)))).ToList();
        }

        public List<string> ToStringList()
        {
            string data = FetchData();
            return new List<string>(data.Split(_separator));
        }
    }
}
