using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Backend.WebApi
{
    public class VersionCheckFilter : IActionFilter // Vores filter VersionCheckFilter nedarver fra systeminterfacet IActionFilter
    {
        public bool AllowMultiple { get { return false; } } // Ingen funktion i nærværende eksempel

        public async Task<HttpResponseMessage> // Dette er hovedbestanddelen i Action Filteret (modul #7, slide 26)
            ExecuteActionFilterAsync(
            HttpActionContext actionContext, // actionContext bruges til at læse Request
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation) // continuation bruges til at fortsætte kæden, dvs. her efter accept af version
        {
            HttpResponseMessage response = null; // Sæt response til null som udgangspunkt

            if (actionContext.Request.Headers.Contains("X-Version")) // Hvis der findes en custom header med navnet X-Version
            {
                var header = actionContext.Request.Headers.First(h => h.Key == "X-Version"); // ... så sæt h.Key (FirstOrDefault) til værdien af headeren

                var version = header.Value.FirstOrDefault(); // version sættes til key-værdien af headeren
                if (version != null && version == "42") // Hvis værdien ikke er null og lig med 42
                {
                    response = await continuation(); // ... så tillad at fortsætte kæden.
                }
            }

            if (response == null)
            {
                var result = new StatusCodeResult((HttpStatusCode)418, actionContext.Request); // Returnér 418 "I'm a teapot", hvis response fortsat er null
                response = await result.ExecuteAsync(cancellationToken); // Cancel forespørgslen (afbryd kæden)
            }

            return response;
        }
    }

    public class VersionCheckHandler : DelegatingHandler // Vores handler VersionCheckHandler nedarver fra systemhandleren DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> // Dette er hovedbestanddelen i Delegating Handleren (modul #7, slide 27)
            SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            if (request.Headers.Contains("X-Version")) // Læg mærke til, at vi nu spørger til request-variablen frem for actionContext.Request.
            {
                var header = request.Headers.First(h => h.Key == "X-Version");

                var version = header.Value.FirstOrDefault();
                if (version != null && version == "42")
                {
                    response = await base
                        .SendAsync(request, cancellationToken); // Tillad at kæden fortsætter
                }
            }

            if (response == null)
            {
                var result = new StatusCodeResult((HttpStatusCode)418, request); // Returnér 418 "I'm a teapot", hvis response fortsat er null
                response = await result.ExecuteAsync(cancellationToken);
            }

            return response;
        }
    }
}