using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Backend.WebApi
{
    public interface IExceptionHandler
    {
        Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken);
    }

    public class ExceptionHandlerContext
    {
        public ExceptionContext ExceptionContext { get; set; }
        //public IHttpActionResult Result { get; set; }
    }

    public class ExceptionContext
    {
        public HttpResponseMessage Response { get; set; }
    }
}