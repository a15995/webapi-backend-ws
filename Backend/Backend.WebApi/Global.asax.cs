using Backend.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Backend.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start() // 7. Indsæt et kald til InitializeSampleData() i Global.asax.cs filens Application_Start metode (lav en ny instans af ProductsController først).
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ProductsController controller = new ProductsController();
            controller.InitializeSampleData();
        }
    }
}
