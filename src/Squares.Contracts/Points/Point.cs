using System;
using System.ComponentModel.DataAnnotations;

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

        public Point()
        {
        }

        [Required]
        [Range(-5000, 5000)]
        public int X { get; set; }
        [Required]
        [Range(-5000, 5000)]
        public int Y { get; set; }
    }
}