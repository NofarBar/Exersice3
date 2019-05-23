using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exersice3.Controllers
{
    public class ProductsController : Controller
    {
        // 
        // GET: /HelloWorld/

        public ActionResult Index()
        {
            
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        [HttpGet]
        public string display(string ip, int port)
        {
            string txt = Request.Path;
            Console.WriteLine(ip);
            Console.WriteLine(port);
            return "1";
          //  return View();
        }
    }
}