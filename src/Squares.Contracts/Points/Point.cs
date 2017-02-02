using System;
using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Points
{
    public class Point : Listable
    {
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

        [Required]
        [Range(-5000, 5000)]
        public int X { get; set; }
        [Required]
        [Range(-5000, 5000)]
        public int Y { get; set; }
    }
}