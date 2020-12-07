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

            int yesAllQuestionsCount = CountAllYesAnswers(groups);
            Console.WriteLine($"(Yes) all answers: {yesAllQuestionsCount}");
            
            int sameQuestionAnswersCount = CountSameQuestionAnswers(groups);
            Console.WriteLine($"(Yes) same questions all persons: {sameQuestionAnswersCount}");
        }

        private static int CountAllYesAnswers(List<string> groups)
        {
            int answersCount = 0;
            groups.ForEach(group => answersCount += group.Replace(Environment.NewLine, string.Empty).Select(x => x).Distinct().Count());

            return answersCount;
        }

        private static int CountSameQuestionAnswers(List<string> groups)
        {
            int sameQuestionAnswersCount = 0;
            groups.ForEach(group => sameQuestionAnswersCount += group.Replace(Environment.NewLine, string.Empty)
                                                                     .GroupBy(x => x)
                                                                     .Select(x => new { Letter = x.Key, Count = x.Count() })
                                                                     .Where(x => x.Count == group.Split(Environment.NewLine).Length)
                                                                     .OrderBy(x => x.Letter)
                                                                     .Select(x => x)
                                                                     .Count());
            return sameQuestionAnswersCount;
        }
    }
}
