using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using OvidosProject.Models;

namespace OvidosProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //JSON veri yapısı ile çalışıldığından tüm veriler buna göre ayarlanıyor.
            //All datas to set same type because system works with Json type
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            // Web API routes
            config.MapHttpAttributeRoutes();
            
            config.Filters.Add(new AuthenticationFilter());
            //enable cors
            //Cors orijin başlatmak için kullanılıyor.
            //this code using for the enable cors orijin
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
