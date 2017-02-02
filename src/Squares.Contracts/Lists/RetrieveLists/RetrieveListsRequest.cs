using System.ComponentModel;

namespace Squares.Contracts.Lists.RetrieveLists
{
    public class RetrieveListsRequest : BaseRequest
    {
        public string ListName { get; set; }
        public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;
    }
}