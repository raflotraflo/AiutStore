using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datebase;

namespace AiutApi.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        DB db = new DB(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        [HttpGet, Route("")]
        public string Test()
        {
            var p = db.GetProduct();
            //w założeniu: przekaznie listy produtków w formacie Json lub XML
            return "Show productList" + p.First().Name.ToString();
        }
    }
}
