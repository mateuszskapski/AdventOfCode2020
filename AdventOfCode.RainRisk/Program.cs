using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.RainRisk
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt")).Split(Environment.NewLine).Select(x => new NavigatorInstruction() { Action = x[0], Value = Int32.Parse(x[Range.StartAt(new Index(1))]) }).ToList();

            Navigator nav = new Navigator();
            var (posX, posY) = nav.Navigate(input);
            var partOne = nav.GetManhattanDistance();
            Console.WriteLine($"Part one: {partOne}");
        }

        struct NavigatorInstruction
        {
            public char Action { get; set; }
            public int Value { get; set; }
        }

        class Navigator
        {
            enum Face
            {
                North = 0,
                East = 90, 
                South = 180,
                West = 270
            }

            private int _posX = 0;
            private int _posY = 0;
            private int _currentFace = (int)Face.East;

            private void MoveNorth(int value) => _posY += value;
            private void MoveEast(int value) => _posX += value;
            private void MoveSouth(int value) => _posY -= value;
            private void MoveWest(int value) => _posX -= value;
            private void MoveForward(int value)
            {
                switch((Face)_currentFace)
                {
                    case Face.North:    _posY += value; break;
                    case Face.East:     _posX += value; break;
                    case Face.South:    _posY -= value; break;
                    case Face.West:     _posX -= value; break;
                    default: throw new NotImplementedException();
                }
            }
            private void TurnLeft(int value)
            {
                Console.WriteLine($"Current face is {_currentFace}. Turn Left by: {value}");
                
                _currentFace -= value;
                if (_currentFace >= 360)
                {
                    _currentFace %= 360;
                }
                else if (_currentFace < 0)
                {
                    _currentFace = 360 + _currentFace;
                }
            }
            private void TurnRight(int value)
            {
                Console.WriteLine($"Current face is {_currentFace}. Turn Right by: {value}");

                _currentFace = Math.Abs(_currentFace + value);
                if (_currentFace >= 360)
                {
                    _currentFace %= 360;
                }
                else if (_currentFace < 0)
                {
                    _currentFace = 360 + _currentFace;
                }
            }

            public (int posX, int posY) Navigate(List<NavigatorInstruction> instructions)
            {
                foreach(var instruction in instructions)
                {
                    switch(instruction.Action)
                    {
                        case 'N': MoveNorth(instruction.Value); break;
                        case 'S': MoveSouth(instruction.Value); break;
                        case 'E': MoveEast(instruction.Value); break;
                        case 'W': MoveWest(instruction.Value); break;
                        case 'L': TurnLeft(instruction.Value); break;
                        case 'R': TurnRight(instruction.Value); break;
                        case 'F': MoveForward(instruction.Value); break;
                        default: throw new NotImplementedException();
                    }
                }

                return (_posX, _posY);
            }

            public int GetManhattanDistance() => Math.Abs(_posX) + Math.Abs(_posY);
        }
    }
}
