using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Squares.RetrieveSquares
{
    public class RetrieveSquaresRequest : BaseRequest
    {
        [Required]
        public string ListName { get; set; }
    }
}