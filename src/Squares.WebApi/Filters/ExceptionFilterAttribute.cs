using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;
using Squares.Contracts.Exceptions;

namespace Squares.WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var storageException = context.Exception as FileStorageException;
            if (storageException != null)
            {
                switch (storageException.Reason)
                {
                    case "listNotFound":
                        var response = new HttpResponseMessage
                        {
                            Content = new ObjectContent<object>(new { storageException.Message, storageException.Reason }, new JsonMediaTypeFormatter())
                        };
                        context.Response = response;
                        break;
                    case "listSizeExceeded":
                        context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        break;

                }
            }
        }
    }
}