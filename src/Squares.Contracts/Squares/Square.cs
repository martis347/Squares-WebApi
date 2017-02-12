using System.Collections.Generic;
using System.Linq;
using Squares.Contracts.Points;

namespace Squares.Contracts.Squares
{
    public class Square : Listable
    {
        public IList<Point> Points { get; set; }

        public Square(string line) : base(line)
        {
            string[] values = line.Split('.');
            Points = new List<Point>
            {
                new Point(values[0]),
                new Point(values[1]),
                new Point(values[2]),
                new Point(values[3])
            };
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
            var square = obj as Square;

            return square?.Points.SequenceEqual(Points) ?? false;
        }

        public override int CompareTo(object obj)
        {
            Square square = (Square) obj;

            int mathces = 0;
            while (square.Points[mathces].Equals(Points[mathces]))
            {
                mathces++;
                if (mathces == 4)
                    return 0;
            }

            var rez = Points[mathces].CompareTo(square.Points[mathces]);
            return rez;
        }

        public class SquareComparer : IComparer<IList<Point>>
        {
            public int Compare(IList<Point> x, IList<Point> y)
            {
                int mathces = 0;
                while (x[mathces].Equals(y[mathces]))
                {
                    mathces++;
                    if (mathces == 4)
                        return 0;
                }

                var rez = x[mathces].CompareTo(y[mathces]);
                return rez;
            }
        }
    }
}