using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode.PassportProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser parser = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt"),Environment.NewLine);
            List<string> file = parser.ToStringList();
            Console.WriteLine($"Parsed {file.Count} rows.");

            List<Passport> passports = new List<Passport>();
            Dictionary<string, string> passportEntries = new Dictionary<string, string>();
            foreach (var row in file)
            {
                var details = row.Split(' ');

                foreach (var entry in details)
                {
                    if (string.IsNullOrEmpty(entry))
                    {
                        break;
                    }    
                    passportEntries.Add(entry.Split(':')?[0], entry.Split(':')?[1]);
                }

                if (string.IsNullOrEmpty(details[0]) || file.Select(x => x).Last() == row)
                {
                    passports.Add(new Passport(passportEntries));
                    passportEntries = new Dictionary<string, string>();
                }
            }

            int validPassports = 0;
            foreach (var passport in passports)
            {
                if (passport.IsValid)
                {
                    validPassports++;
                }
            }

            Console.WriteLine($"Valid passports count: {validPassports}");
        }
        class Passport
        {
            public string BirthYear { get; set; }
            public string IssueYear { get; set; }
            public string ExpirationYear { get; set; }
            public string Height { get; set; }
            public string HairColor { get; set; }
            public string EyeColor { get; set; }
            public string PassportId { get; set; }
            public string CountryId { get; set; }
            public bool IsValid 
            { 
                get 
                { 
                    return !string.IsNullOrWhiteSpace(BirthYear) && int.TryParse(BirthYear, out int birthYear) && birthYear >= 1920 && birthYear <= 2002 && 
                           !string.IsNullOrWhiteSpace(IssueYear) && int.TryParse(IssueYear, out int issueYear) && issueYear >= 2010 && issueYear <= 2020 &&
                           !string.IsNullOrWhiteSpace(ExpirationYear) && int.TryParse(ExpirationYear, out int expiryYear) && expiryYear >= 2020 && expiryYear <= 2030 &&
                           !string.IsNullOrWhiteSpace(Height) && IsHeightValid(Height) &&
                           !string.IsNullOrWhiteSpace(HairColor) && IsHairColorValid(HairColor) &&
                           !string.IsNullOrEmpty(EyeColor) && IsEyeColorValid(EyeColor) &&
                           !string.IsNullOrWhiteSpace(PassportId) && IsPassportNumberValid(PassportId)
                        ? true : false; 
                } 
            }

            public Passport(Dictionary<string, string> passportDetails)
            {
                foreach (var row in passportDetails)
                {
                    switch (row.Key)
                    {
                        case "byr": BirthYear = row.Value; break;
                        case "iyr": IssueYear = row.Value; break;
                        case "eyr": ExpirationYear = row.Value; break;
                        case "hgt": Height = row.Value; break;
                        case "hcl": HairColor = row.Value; break;
                        case "ecl": EyeColor = row.Value; break;
                        case "pid": PassportId = row.Value; break;
                        case "cid": CountryId = row.Value; break;
                        default: throw new ArgumentException($"{row.Key} is not supported.");
                    }
                }
            }

            public static bool IsHeightValid(string height)
            {
                string strHeight = "";
                for (int i = 0; i < height.Length; i++)
                {
                    if (char.IsDigit(height[i]))
                        strHeight += height[i];
                }

                return (height.Contains("cm") && int.TryParse(strHeight, out int h) && h >= 150 && h <= 193) ||
                       (height.Contains("in") && int.TryParse(strHeight, out h) && h >= 59 && h <= 76);
            }

            public static bool IsHairColorValid(string color)
            {
                if (color.IndexOf('#') != 0 || color.Length != 7) return false;

                for (int i = 1; i < color.Length; i++)
                {
                    if (!((color[i] >= 'a' && color[i] <= 'f') || (color[i] >= '0' && color[i] <= '9'))) 
                        return false;
                }

                return true;
            }

            public static bool IsEyeColorValid(string color)
            {
                string[] colors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

                return !string.IsNullOrEmpty((colors.Select(x => x).Where(c => c == color).FirstOrDefault()));
            }

            public static bool IsPassportNumberValid(string number)
            {
                if (number.Length != 9)
                    return false;

                for (int i = 0; i < number.Length; i++)
                {
                    if (!char.IsDigit(number[i]))
                        return false;
                }

                return true;
            }
        }
    }
}
