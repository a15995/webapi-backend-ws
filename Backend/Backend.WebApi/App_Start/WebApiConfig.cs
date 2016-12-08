using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Backend.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new NotFoundHandler()); // Sætter Exception Handler til NotFound (Exception.cs)
            //config.Filters.Add(new VersionCheckFilter()); // Aktivér denne for at bruge Action Filters (Filter.cs)
            config.MessageHandlers.Add(new VersionCheckHandler()); // Aktivér denne for at bruge Delegating Handlers (Filters.cs)

            // Web API routes
            config.MapHttpAttributeRoutes(); // Gør det muligt at tilføje egen URI [RoutePrefix]/[Route] samt metoden f.eks. [HttpGet] i controlleren

            /*config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/
        }
    }
}
