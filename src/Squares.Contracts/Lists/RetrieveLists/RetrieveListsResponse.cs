using System.Collections.Generic;

namespace Squares.Contracts.Lists.RetrieveLists
{
    public class RetrieveListsResponse : BaseResponse
    {
        public IList<string> ListNames { get; set; }
    }
}