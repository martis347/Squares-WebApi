using System.Collections.Generic;
using System.Linq;
using Squares.Contracts.Points;

namespace Squares.Contracts.Squares
{
    public class Square : Listable
    {
        public Square(string line) : base(line)
        {
            string[] values = line.Split('.');
            Points.Add(new Point(values[0]));
            Points.Add(new Point(values[1]));
            Points.Add(new Point(values[2]));
            Points.Add(new Point(values[3]));
        }

        public Square() : base("")
        {

        }

        public override string ToString()
        {
            return $"{Points[0]}.{Points[1]}.{Points[2]}.{Points[3]}";
        }

        public override int GetHashCode()
        {
            return Points.Sum(point => point.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return ToString() == obj?.ToString();
        }

        public IList<Point> Points { get; set; } = new List<Point>(4);
    }
}