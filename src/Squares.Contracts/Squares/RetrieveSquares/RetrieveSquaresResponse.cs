using System.Collections.Generic;

namespace Squares.Contracts.Squares.RetrieveSquares
{
    public class RetrieveSquaresResponse : BaseResponse
    {
        public List<Square> Squares { get; set; }
    }
}