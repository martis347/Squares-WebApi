using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Points.RetrievePoints
{
    public class RetrievePointsRequest : BaseRequest
    {
        [Required]
        public string ListName { get; set; }
    }
}