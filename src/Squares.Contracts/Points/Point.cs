using System;
using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Points
{
    public class Point : Listable
    {
        [Required]
        [Range(-5000, 5000)]
        public int X { get; set; }

        [Required]
        [Range(-5000, 5000)]
        public int Y { get; set; }

        public Point(string line) : base(line)
        {
            string[] values = line.Split(' ');
            X = Int32.Parse(values[0]);
            Y = Int32.Parse(values[1]);
        }

        public Point() : base("")
        {
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        public override bool Equals(object obj)
        {
            var comp = (Point)obj;
            return comp?.X == X && comp.Y == Y;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override int CompareTo(object obj)
        {
            Point point = (Point) obj;

            if (point.ToString() == ToString())
            {
                return 0;
            }

            if (X > point.X || X.Equals(point.X) && Y > point.Y)
            {
                return 1;
            }

            return -1;
        }
    }
}