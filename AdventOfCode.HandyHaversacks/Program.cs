using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Common;

namespace AdventOfCode.HandyHaversacks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");  
            FileParser fileParser = new FileParser(Path.Combine("C:\\Users\\Matt\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode.HandyHaversacks", "Input.txt"), Environment.NewLine);
            var bagRules = fileParser.ToStringList();

            List<OuterBag> outerBags = new List<OuterBag>(); 
            foreach(var rule in bagRules)
            {
                Console.WriteLine(rule);
                var r = rule.Replace("bags contain","|").Split("|");
                Console.WriteLine(r.Length);
                var innerBags = r[1].Split(",");
                var innerBagList = new List<Bag>();
                foreach(var bag in innerBags)
                {
                    var bagSplit = bag. bag.IndexOf(' ') ;
                    innerBagList.Add(new Bag{Color = bagSplit[1].Trim(), Quantity = Int32.Parse(bagSplit[0])});
                }

                outerBags.Add(new OuterBag {
                        Color = r[0].Trim(),
                        ContainingBags = innerBagList
                    });
            }
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
