using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EMRWEBAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "EMRAPI001Controller",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "TestJsonController",
            //    routeTemplate:"api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "TestJsonController",
            //    routeTemplate: "{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
             
            config.Routes.MapHttpRoute(
                 name: "WebApi_IEMRController",
                 routeTemplate: "{controller}/{action}/{Date1}/{Time1}/{Date2}/{Time2}/{emrdbno}",
                 defaults: new { Date1 = RouteParameter.Optional, Time1 = RouteParameter.Optional, Date2 = RouteParameter.Optional, Time2 = RouteParameter.Optional, emrdbno = RouteParameter.Optional }
             );

            //config.Routes.MapHttpRoute(
            //    name: "TestWebApiController",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            // config.Routes.MapHttpRoute(
            //    name: "TestWebApi",
            //    routeTemplate: "{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //    //defaults: new { action = "Default", id = RouteParameter.Optional });
            //    //例如 GET 方法，將action name設定為Default： 
            //    //[HttpGet, ActionName("Default")]
            //);
        }
    }
}
