using System.Collections.Generic;

namespace Squares.Contracts.Lists.RetrieveLists
{
    public class RetrieveListsResponse : BaseResponse
    {
        public List<string> ListNames { get; set; }
    }
}