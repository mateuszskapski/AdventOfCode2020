using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace HandheldHalting
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser fileParser = new FileParser(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt"), Environment.NewLine);
            var instructionsList = fileParser.ToStringList();

            List<Instruction> instructions = new List<Instruction>(instructionsList.Count);
            instructions.AddRange(instructionsList.Select(i => new Instruction { Id = Guid.NewGuid(),  InstructionType = (i.Split(" "))[0], Value = Int32.Parse((i.Split(" "))[1]) }));

            List<Instruction> executionList = new List<Instruction>();
            int index = 0;
            GetExecutionList(instructions, ref executionList, index);

            // Result 1137
            int accumulator = executionList.Where(i => i.InstructionType == "acc").Select(s => s.Value).Sum();
            Console.WriteLine($"Part 1: Accumulator value: { accumulator }");

            // Result 1125
            FixInstructions(instructions, ref executionList);
            accumulator = executionList.Where(i => i.InstructionType == "acc").Select(s => s.Value).Sum();
            Console.WriteLine($"Part 2: Accumulator value: { accumulator }");
        }

        private static void GetExecutionList(List<Instruction> instructions, ref List<Instruction> executionList, int index)
        {
            if (index >= instructions.Count)
                return;

            if (instructions[index].WasAccessed == true)
                return;
            else
                instructions[index].WasAccessed = true;
            
            executionList.Add(instructions[index]);

            switch (instructions[index].InstructionType)
            {
                case "nop": ++index; break;
                case "acc": ++index; break;
                case "jmp": index += instructions[index].Value; break;
                default: break;
            }

            GetExecutionList(instructions, ref executionList, index);
        }

        private static void FixInstructions(List<Instruction> instructions, ref List<Instruction> executionList)
        {
            for (int i = 0; i < instructions.Count; i++)
            {
                List<Instruction> updatedList = new List<Instruction>();
                updatedList.AddRange(instructions.ToList());

                if (instructions[i].InstructionType == "jmp")
                {
                    updatedList.Where(x => x.Id == instructions[i].Id).First().InstructionType = "nop";
                }
                else if (instructions[i].InstructionType == "nop")
                {
                    updatedList.Where(x => x.Id == instructions[i].Id).First().InstructionType = "jmp";
                }
                else
                {
                    continue;
                }

                executionList = new List<Instruction>();
                updatedList.ForEach(b => b.WasAccessed = false);
                GetExecutionList(updatedList, ref executionList, 0);

                if (executionList.Last().Id == instructions.Last().Id)
                {
                    return;
                }

                if (instructions[i].InstructionType == "jmp")
                {
                    updatedList.Where(x => x.Id == instructions[i].Id).First().InstructionType = "nop";
                }
                else if (instructions[i].InstructionType == "nop")
                {
                    updatedList.Where(x => x.Id == instructions[i].Id).First().InstructionType = "jmp";
                }
                else
                {
                    continue;
                }
            }
        }
    }

    class Instruction
    {
        public Guid Id { get; set; }
        public string InstructionType { get; set; }
        public int Value { get; set; }
        public bool WasAccessed { get; set; } = false;
    }
}
