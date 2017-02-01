using System;

namespace Squares.Contracts.Points
{
    public class Point
    {
        public Point(string line)
        {
            string[] values = line.Split(' ');
            X = Int32.Parse(values[0]);
            Y = Int32.Parse(values[1]);
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}