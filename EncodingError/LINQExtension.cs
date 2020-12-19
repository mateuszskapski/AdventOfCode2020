using System;
using System.Collections.Generic;
using System.Text;

namespace EncodingError
{
    public static class LINQExtension
    {
        public static ulong Sum(this IEnumerable<ulong> source)
        {
            ulong sum = 0;
            foreach (var n in source)
            {
                sum += n;
            }

            return sum;
        }
    }
}
