using System.Collections.Generic;

namespace Squares.Contracts.Squares.RetrieveSquares
{
    public class RetrieveSquaresResponse : BaseResponse
    {
        public IList<Square> Squares { get; set; }
        public int SquaresCount { get; set; }
    }
}