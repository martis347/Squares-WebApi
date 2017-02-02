using System.ComponentModel.DataAnnotations;

namespace Squares.Contracts.Lists.RemoveList
{
    public class RemoveListRequest : BaseRequest
    {
        [Required]
        public string ListName { get ; set; }
    }
}