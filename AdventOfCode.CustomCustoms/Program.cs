using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.CustomCustoms
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser parser = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt"), $"{Environment.NewLine}{Environment.NewLine}");
            var groups = parser.ToStringList();

            int yesAnswersCount = CountAllYesAnswers(groups);
            Console.WriteLine($"(Yes) answers: {yesAnswersCount}");
            
            int sameQuestionAnswersCount = CountSameQuestionAnswers(groups);
            Console.WriteLine($"(Yes) same questions answers: {sameQuestionAnswersCount}");
        }

        private static int CountSameQuestionAnswers(List<string> groups)
        {
            int sameQuestionAnswersCount = 0;
            foreach (var group in groups)
            {
                int groupMembersCount = group.Split(Environment.NewLine).Length;

                var answersCountSameQuestion = group.Replace("\r\n", string.Empty)
                                                    .GroupBy(x => x)
                                                    .Select(x => new { Letter = x.Key, Count = x.Count() })
                                                    .Where(x => x.Count == groupMembersCount)
                                                    .OrderBy(x => x.Letter)
                                                    .ToList();

                sameQuestionAnswersCount += answersCountSameQuestion.Select(x => x).Count();
            }

            return sameQuestionAnswersCount;
        }

        private static int CountAllYesAnswers(List<string> groups)
        {
            int answersCount = 0;
            foreach (var group in groups)
            {
                answersCount += group.Replace("\r\n", string.Empty).Select(x => x).Distinct().Count();
            }

            return answersCount;
        }
    }
}

// psyjxulrdtfejeusdrlxyftpufdpjsxrlztyeyeorabxsdnhftujlppedfxtsryujl
