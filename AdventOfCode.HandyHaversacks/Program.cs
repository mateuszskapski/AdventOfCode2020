using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using AdventOfCode.Common;

namespace AdventOfCode.HandyHaversacks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input.txt answer = 9339
            // Input2.txt answer = 32
            // Input3.txt answer = 126
            Console.WriteLine("Hello");
            FileParser fileParser = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt"), Environment.NewLine);
            var bagRules = fileParser.ToStringList();

            List<OuterBag> outerBags = new List<OuterBag>();
            foreach (var rule in bagRules)
            {
                var ruleSplit = rule.Replace("bags contain", "|").Split("|");
                var innerBags = ruleSplit[1].Split(",");
                var innerBagList = new List<Bag>();
                foreach (var bag in innerBags)
                {
                    Int32.TryParse(bag.Trim().Substring(0, bag.Trim().IndexOf(' ')), out int quantity);
                    if (quantity == 0)
                    {
                        continue;
                    }

                    string bagColor = bag.Trim().Substring(bag.Trim().IndexOf(' ')).Replace("bags", string.Empty).Replace("bag", string.Empty).Replace(".", string.Empty).Trim();
                    innerBagList.Add(new Bag { Color = bagColor, Quantity = quantity });
                }

                outerBags.Add(new OuterBag
                {
                    Color = ruleSplit[0].Trim(),
                    ContainingBags = innerBagList
                });
            }

            // Part one
            string myBagColor = "shiny gold";
            int bagsCount = CountContainingColorBags(outerBags, myBagColor);
            Console.WriteLine($"Shiny gold can be contained in {bagsCount} bags.");
            
            // Part two
            int totalBags = 0;
            var shinyGoldBag = outerBags.Where(b => b.Color == myBagColor).FirstOrDefault();
            foreach (var bag in shinyGoldBag.ContainingBags)
            {
                totalBags += bag.Quantity + (bag.Quantity * CountContainingBags(outerBags, bag.Color));
            }
            Console.WriteLine($"Total {totalBags} bags.");
        }

        private static int CountContainingColorBags(List<OuterBag> outerBags, string bagColor)
        {
            int bagsCount = 0;
            List<string> colors = new List<string> { bagColor };
            List<string> searchedColor = new List<string>();

            while (colors.Count != searchedColor.Count)
            {
                string colToSearch = "";
                foreach (var col in colors)
                {
                    if (!searchedColor.Contains(col))
                    {
                        colToSearch = col;
                    }
                }

                List<OuterBag> outerBagContainingGold = outerBags.Where(x => x.ContainingBags.Where(x => x.Color == colToSearch)
                                                                                             .Count() > 0)
                                                                                             .Select(x => x)
                                                                                             .ToList();
                searchedColor.Add(colToSearch);

                foreach (var bag in outerBagContainingGold)
                {
                    if (!colors.Contains(bag.Color))
                    {
                        colors.Add(bag.Color);
                        bagsCount++;
                    }
                }

            }

            return bagsCount;
        }

        private static int CountContainingBags(List<OuterBag> outerBags, string bagColor)
        {
            int containingBagsCount = 0;
            
            var outerBag = outerBags.Where(b => b.Color == bagColor).FirstOrDefault();
            
            foreach (var innerBag in outerBag.ContainingBags)
            {
                if (innerBag == null || innerBag.Quantity == 0)
                {
                    return containingBagsCount;
                }
                else
                {
                    containingBagsCount += innerBag.Quantity + (innerBag.Quantity * CountContainingBags(outerBags, innerBag.Color));
                }
            }

            return containingBagsCount;
        }

        class Bag
        {
            public string Color { get; set; }
            public int Quantity { get; set; }
        }

        class OuterBag
        {
            public string Color {get; set;}
            public List<Bag> ContainingBags {get; set;}
        } 
    }
}
