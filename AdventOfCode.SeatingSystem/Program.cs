using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.SeatingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(Path.Combine(PathHelper.ProjectRootFolder(), "Input.txt")).Split(Environment.NewLine).ToList();

            SeatManager seatManagerPartOne = new SeatManager(input);
            seatManagerPartOne.ArrangeSeats(seatManagerPartOne.CountOccupiedAdjacentSeats, 4);
            int occupiedSeatsOne = seatManagerPartOne.CountOccupiedSeats();
            Console.WriteLine($"Part one answer: {occupiedSeatsOne}");

            SeatManager seatManagerPartTwo = new SeatManager(input);
            seatManagerPartTwo.ArrangeSeats(seatManagerPartTwo.CountOccupiedVisibleSeats, 5);
            int occupiedSeatsTwo = seatManagerPartTwo.CountOccupiedSeats();
            Console.WriteLine($"Part two answer: {occupiedSeatsTwo}");
        }
    }

    internal class SeatManager
    {
        private List<string> _currentSeatsLayout;
        private List<string> _newSeatLayout;
        private readonly int _maxRows;
        private readonly int _maxRowSeats;
        private int _arrangeCount = 0;

        public SeatManager(List<string> seatLayout)
        {
            _currentSeatsLayout = new List<string>(seatLayout);
            _newSeatLayout = new List<string>();
            _maxRows = seatLayout.Count;
            _maxRowSeats = seatLayout[0].Length;
        }

        private bool IsOccupied(int row, int column) => _currentSeatsLayout[row][column].Equals('#');

        private bool IsFloor(int row, int column) => _currentSeatsLayout[row][column].Equals('.');

        public int CountOccupiedAdjacentSeats(int row, int column)
        {
            int occupiedSeats = 0;

            for (int seatRow = row > 0 ? row - 1 : 0;
                row >= 0 && seatRow <= row + 1 && row >= row - 1 && seatRow < _maxRows;
                seatRow++)
            {
                for (int seatCol = column > 0 ? column - 1 : 0;
                    column >= 0 && seatCol <= column + 1 && seatCol >= column - 1 && seatCol < _maxRowSeats;
                    seatCol++)
                {
                    if (row == seatRow && column == seatCol)
                    {
                        continue;
                    }

                    if (IsOccupied(seatRow, seatCol))
                    {
                        occupiedSeats++;
                    }
                }
            }

            return occupiedSeats;
        }

        public int CountOccupiedVisibleSeats(int row, int column)
        {
            int occupiedSeat = 0;

            for (int r = row > 0 ? row - 1 : 0;
                row >= 0 && r <= row + 1 && row >= row - 1 && r < _maxRows;
                r++)
            {
                for (int c = column > 0 ? column - 1 : 0;
                    column >= 0 && c <= column + 1 && c >= column - 1 && c < _maxRowSeats;
                    c++)
                {
                    if (r == row && c == column) continue;

                    if (IsFloor(r,c))
                    {
                        (int row, int column) nextSit = (r, c);
                        while (IsFloor(nextSit.row, nextSit.column))
                        {
                            nextSit = GetAvailableNextSit(row, column, nextSit.row, nextSit.column);
                            if (nextSit.row >= _maxRows || nextSit.column >= _maxRowSeats || nextSit.row < 0 || nextSit.column < 0)
                            {
                                break;
                            }

                            if (IsOccupied(nextSit.row, nextSit.column))
                            {
                                occupiedSeat++;
                            }
                        }
                    } else if (IsOccupied(r, c))
                    {
                        occupiedSeat++;
                    }
                }
            }

            return occupiedSeat;
        }

        private (int row, int column) GetAvailableNextSit(int row, int column, int nextRow, int nextCol)
        {
            if (nextRow == row)
            {
                if (nextCol > column)
                {
                    return (nextRow, ++nextCol);
                }
                else if  (nextCol < column)
                {
                    return (nextRow, --nextCol);
                }
                else
                {
                    throw new ArgumentException("Positions are the same.");
                }
            }
            else
            {
                if (nextCol == column)
                {
                    if (nextRow > row)
                    {
                        return (++nextRow, nextCol);
                    }
                    else if (nextRow < row)
                    {
                        return (--nextRow, nextCol);
                    }
                    else
                    {
                        throw new ArgumentException("Positions are the same");
                    }
                }
                else
                {
                    if (nextRow > row && nextCol > column)
                    {
                        return (++nextRow, ++nextCol);
                    }
                    else if (nextRow > row && nextCol < column)
                    {
                        return (++nextRow, --nextCol);
                    }
                    else if (nextRow < row && nextCol > column)
                    {
                        return (--nextRow, ++nextCol);
                    }
                    else if (nextRow < row && nextCol < column)
                    {
                        return (--nextRow, --nextCol);
                    }
                    else
                        throw new ArgumentException("Not supported position");

                }
            }
        }

        public void ArrangeSeats(Func<int, int, int> countSeats, int tolerance)
        {
            _arrangeCount++;

            _newSeatLayout.Clear();

            for (int row = 0; row < _maxRows; row++)
            {
                StringBuilder newRow = new StringBuilder(_maxRowSeats);
                for (int col = 0; col < _maxRowSeats; col++)
                {
                    var seatStatus = _currentSeatsLayout[row][col];
                    if (seatStatus == '.')
                    {
                        newRow.Append(".");
                        continue;
                    }

                    int occupiedAdjacentSeats = countSeats(row, col);
                    if (seatStatus.Equals('L') && occupiedAdjacentSeats == 0)
                    {
                        newRow.Append('#');
                    }
                    else if (occupiedAdjacentSeats >= tolerance)
                    {
                        newRow.Append('L');
                    }
                    else
                    {
                        newRow.Append(seatStatus);
                    }
                }

                _newSeatLayout.Insert(row, newRow.ToString());
            }

            if (!Enumerable.SequenceEqual(_currentSeatsLayout, _newSeatLayout))
            {
                _currentSeatsLayout = new List<string>(_newSeatLayout);
                ArrangeSeats(countSeats, tolerance);
            }
            else
                return;
        }

        public int CountOccupiedSeats()
        {
            int occupiedSeats = 0;
            for (int row = 0; row < _maxRows; row++)
            {
                for (int col = 0; col < _maxRowSeats; col++)
                {
                    if (_newSeatLayout[row][col] == '#')
                        occupiedSeats++;
                }
            }

            return occupiedSeats;
        }
    }
}
