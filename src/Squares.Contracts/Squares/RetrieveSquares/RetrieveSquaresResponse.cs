using System.Collections.Generic;
using System.ComponentModel;

namespace Squares.Contracts.Squares.RetrieveSquares
{
    public class RetrieveSquaresResponse : BaseResponse
    {
        public IList<Square> Squares { get; set; }
        public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;
    }
}