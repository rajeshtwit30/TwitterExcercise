using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace Twitterexercise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          //  config.EnableCors();
            var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
        }
    }
}
