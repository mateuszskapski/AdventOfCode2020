using AdventOfCode.Common;
using System;
using System.IO;

namespace AdventOfCode.PasswordPhilosophy
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser fileParser = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(),"Input.txt"),Environment.NewLine);

            var passwords = fileParser.ToStringList();
            Console.WriteLine($"Parsed {passwords.Count} rows.");

            int countOldRules = 0;
            int countNewRules = 0;
            foreach (var psw in passwords)
            {
                // Min occurrences
                int rangeCharPos = psw.IndexOf("-");
                int endRangeCharPos = psw.IndexOf(" ");
                int searchCharPos = psw.IndexOf(":") - 1;
                int passwordStartPos = psw.IndexOf(":") + 1;
                int min = int.Parse(psw.Substring(0, rangeCharPos));
                int max = int.Parse(psw.Substring(rangeCharPos + 1, endRangeCharPos - rangeCharPos));
                char searchChar = psw[searchCharPos];
                string password = psw.Substring(passwordStartPos + 1, psw.Length - passwordStartPos - 1);

                int charOccurrences = password.Length - password.Replace(searchChar.ToString(),"").Length;

                if (charOccurrences >= min && charOccurrences <= max)
                {
                    countOldRules++;
                }

                if (max > password.Length && password[min - 1].Equals(searchChar))
                {
                    countNewRules++;
                    continue;
                }

                if (password[min-1].Equals(searchChar) && !password[max-1].Equals(searchChar) ||
                    !password[min-1].Equals(searchChar) && password[max-1].Equals(searchChar))
                {
                    countNewRules++;
                }

            }
            
            Console.WriteLine($"Found {countOldRules} passwords matching old rules.");
            Console.WriteLine($"Found {countNewRules} passwords matching new rules.");
        }
    }
}
