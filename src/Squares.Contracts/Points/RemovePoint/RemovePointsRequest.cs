using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Points.RemovePoint
{
    public class RemovePointsRequest : BaseRequest
    {
        [Required]
        public string ListName { get; set; }
        [Required]
        public IList<Point> Points { get; set; }
    }
}