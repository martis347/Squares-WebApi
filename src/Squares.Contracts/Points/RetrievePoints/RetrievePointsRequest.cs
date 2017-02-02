using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Points.RetrievePoints
{
    public class RetrievePointsRequest : BaseRequest
    {
        [Required]
        public string ListName { get; set; }
        public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
    }
}