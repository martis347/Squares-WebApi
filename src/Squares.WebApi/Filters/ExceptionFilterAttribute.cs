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
                HttpResponseMessage response;
                switch (storageException.Reason)
                {
                    case "listNotFound":
                        response = new HttpResponseMessage
                        {
                            Content =
                                new ObjectContent<object>(new {storageException.Message, storageException.Reason},
                                    new JsonMediaTypeFormatter()),
                            StatusCode = HttpStatusCode.NotFound
                        };
                        context.Response = response;
                        return;
                    case "listSizeExceeded":
                        response = new HttpResponseMessage
                        {
                            Content =
                                new ObjectContent<object>(new {storageException.Message, storageException.Reason},
                                    new JsonMediaTypeFormatter()),
                            StatusCode = HttpStatusCode.RequestEntityTooLarge
                        };
                        context.Response = response;
                        return;
                    case "pointsExist":
                    case "listExists":
                        response = new HttpResponseMessage
                        {
                            Content =
                                new ObjectContent<object>(new { storageException.Message, storageException.Reason },
                                    new JsonMediaTypeFormatter()),
                            StatusCode = HttpStatusCode.Conflict
                        };
                        context.Response = response;
                        return;
                }
            }
            var badRequestException = context.Exception as BadRequestException;
            if (badRequestException != null)
            {
                HttpResponseMessage response;
                switch (badRequestException.Reason)
                {
                    case "invalidProperties":
                    case "invalidFormat":
                        response = new HttpResponseMessage
                        {
                            Content =new ObjectContent<object>(new {badRequestException.Message, badRequestException.Reason}, new JsonMediaTypeFormatter()),
                            StatusCode = HttpStatusCode.BadRequest
                        };
                        context.Response = response;
                        return;
                }
            }
        }
    }
}