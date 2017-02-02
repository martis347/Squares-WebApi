using System.Collections.Generic;

namespace Squares.Contracts.Points.RetrievePoints
{
    public class RetrievePointsResponse : BaseResponse
    {
        public IList<Point> Points { get; set; }
    }
}