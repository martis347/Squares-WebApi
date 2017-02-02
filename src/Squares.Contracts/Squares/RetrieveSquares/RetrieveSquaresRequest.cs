using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Squares.RetrieveSquares
{
    public class RetrieveSquaresRequest : BaseRequest
    {
        [Required]
        public string ListName { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
    }
}