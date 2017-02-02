using System.Collections.Generic;
using Squares.Contracts.Points;

namespace Squares.Contracts.Lists.RetrieveLists
{
    public class RetrieveListsResponse : BaseResponse
    {
        public IList<string> ListNames { get; set; }
        public IList<Point> Points { get; set; }
    }
}