using System;

namespace Squares.Handlers
{
    public abstract class BaseHandler<TRequest, TResponse> : IHandler<TRequest, TResponse>
    {
        public TResponse Handle(TRequest request)
        {
            TResponse response;
            try
            {
                response = HandleCore(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return response;
        }

        public abstract TResponse HandleCore(TRequest request);
    }
}
