using System.ComponentModel.DataAnnotations;


namespace Squares.Contracts.Lists.CreateList
{
    public class CreateListRequest : BaseRequest
    {
        [Required]
        public string ListName { get ; set; }
    }
}