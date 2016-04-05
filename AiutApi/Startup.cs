using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Test;
using Owin;
using System.Web.Http;
using Test.App_Start;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Startup))]
namespace Test
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();

            WebApiConfig.Register(httpConfig);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);

        }
    }
}