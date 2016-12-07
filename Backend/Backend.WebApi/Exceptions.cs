using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Backend.WebApi
{
    public class NotFoundException : Exception // Vi lader vores undtagelse nedarve fra Exception (laver en kopi)
    { }

    public class NotFoundHandler : IExceptionHandler // Vi extender IExceptionHandler (interface) med NotFoundHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken) // Exception Handler, modul #7 slide 21
        {
            if (context.ExceptionContext.Exception is NotFoundException) // 6.b. Hvis context.ExceptionContext.Exception er af typen NotFoundException
            {
                StatusCodeResult NotFound = new StatusCodeResult(HttpStatusCode.NotFound, context.Request); // ...skal handleren sætte context.Result til en instans af StatusCodeResult med HttpStatusCode.NotFound.
                context.Result = NotFound;
            }
            return Task.FromResult("404 Not Found"); // FromResult vises ikke men der kræves en parameter
        }
    }
}