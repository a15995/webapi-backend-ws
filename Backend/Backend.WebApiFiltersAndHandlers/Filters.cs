using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Backend.WebApiFiltersAndHandlers
{
    public class MyFilter : IActionFilter
    {
        private HttpResponseMessage response;

        public bool AllowMultiple { get { return false; } }

        public async Task<HttpResponseMessage>
            ExecuteActionFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            HttpResponseMessage response = null;

            if (actionContext.Request.Headers.Contains("X-Version"))
            {
                var header = actionContext.Request.Headers.First(h => h.Key == "X-Version");

                var version = header.Value.FirstOrDefault();
                if (version != null && version == "42")
                {
                    response = await continuation();
                }
            }

            if (response == null)
            {
                var result = new StatusCodeResult((HttpStatusCode)418, actionContext.Request);
                response = await result.ExecuteAsync(cancellationToken);
            }

            return response;
        }
    }
}