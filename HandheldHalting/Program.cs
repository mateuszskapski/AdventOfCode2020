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
            instructions.AddRange(instructionsList.Select(i => new Instruction { InstructionType = (i.Split(" "))[0], Value = Int32.Parse((i.Split(" "))[1]) }));

            // Result 1137;
            int accumulator = 0;
            int index = 0;
            GetAccumulatorValue(instructions, ref accumulator, index);
            Console.WriteLine($"Accumulator value: { accumulator }");
        }

        private static void GetAccumulatorValue(List<Instruction> instructions, ref int accumulator, int index)
        {
            if (instructions[index].WasAccessed == true)
                return;
            else
                instructions[index].WasAccessed = true;

            switch (instructions[index].InstructionType)
            {
                case "nop": GetAccumulatorValue(instructions, ref accumulator, ++index); break;
                case "acc": accumulator += instructions[index].Value; GetAccumulatorValue(instructions, ref accumulator, ++index); break;
                case "jmp": index += instructions[index].Value; GetAccumulatorValue(instructions, ref accumulator, index); break;
                default: break;
            }
        }
    }

    class Instruction
    {
        public string InstructionType { get; set; }
        public int Value { get; set; }
        public bool WasAccessed { get; set; } = false;
    }
}
