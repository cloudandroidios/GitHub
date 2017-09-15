using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Papp1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id= RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "TestWebApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional}
                //defaults: new { action = "Default", id = RouteParameter.Optional });
                //例如 GET 方法，將action name設定為Default： 
                //[HttpGet, ActionName("Default")]
            );

            config.Routes.MapHttpRoute(
                name: "TestWebApi2P",
                routeTemplate: "{controller}/{action}/{id1}/{id2}",
                defaults: new { id1 = RouteParameter.Optional, id2 = RouteParameter.Optional },
                constraints: new { action = "xxx|yyy|ZZZ" } //=> 這邊設定指定的action必須是XXX or YYY or ZZZ or …
            );
            //http://huan-lin.blogspot.com/2013/01/aspnet-web-api-and-json.html
            //var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        }
    }
}
