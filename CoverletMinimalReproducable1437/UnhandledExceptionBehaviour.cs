using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverletMinimalReproducable1437;

public class UnhandledExceptionBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public UnhandledExceptionBehaviour()
    {
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await next().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            LogUnhandledException(request, ex);

            throw;
        }
    }

    private void LogUnhandledException(TRequest request, Exception ex)
    {
        var requestName = typeof(TRequest).Name;
        Console.WriteLine("Unhandled exception for request");
    }
}