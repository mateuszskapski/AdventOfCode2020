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

            SeatManager seatManager = new SeatManager(input);
            seatManager.ArrangeSeats(4);
            int occupiedSeats = seatManager.CountOccupiedSeats();

            Console.WriteLine($"Part one answer: {occupiedSeats}");
        }
    }

    internal struct Seat
    {
        public int Row;
        public int Column;
    }

    internal class SeatManager
    {
        private List<string> _currentSeatsLayout;
        private List<string> _newSeatLayout;
        private readonly int _maxRows;
        private readonly int _maxRowSeats;

        public SeatManager(List<string> seatLayout)
        {
            _currentSeatsLayout = new List<string>(seatLayout);
            _newSeatLayout = new List<string>();
            _maxRows = seatLayout.Count;
            _maxRowSeats = seatLayout[0].Length;
        }

        private bool IsOccupied(Seat seat) => _currentSeatsLayout[seat.Row][seat.Column].Equals('#');

        private int CountOccupiedAdjacentSeats(Seat seat)
        {
            int occupiedSeats = 0;

            for (int row = seat.Row > 0 ? seat.Row - 1 : 0;
                seat.Row >= 0 && row <= seat.Row + 1 && row >= seat.Row - 1 && row < _maxRows;
                row++)
            {
                for (int col = seat.Column > 0 ? seat.Column - 1 : 0;
                    seat.Column >= 0 && col <= seat.Column + 1 && col >= seat.Column - 1 && col < _maxRowSeats;
                    col++)
                {
                    if (row == seat.Row && col == seat.Column)
                    {
                        continue;
                    }

                    if (IsOccupied(new Seat { Row = row, Column = col }))
                    {
                        occupiedSeats++;
                        //System.Diagnostics.Debug.WriteLine($"Seat row: {seat.Row}, col: {seat.Column}: Occupied: row: {row}, col {col}");
                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine($"R: {seat.Row}, C: {seat.Column} is occupied by {occupiedSeats} seats");

            return occupiedSeats;
        }

        public void ArrangeSeats(int tolerance)
        {
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

                    int occupiedAdjacentSeats = CountOccupiedAdjacentSeats(new Seat { Row = row, Column = col });
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
                ArrangeSeats(tolerance);
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
