using Squares.Contracts.Points;

namespace Squares.Contracts.Squares
{
    public class Square : Listable
    {
        public Square(string line) : base(line)
        {
            string[] values = line.Split('.');
            Points[0] = new Point(values[0]);
            Points[1] = new Point(values[1]);
            Points[2] = new Point(values[2]);
            Points[3] = new Point(values[3]);
        }

        public Square() : base("")
        {
            
        }

        public Point[] Points { get; set; } = new Point[4];
    }
}