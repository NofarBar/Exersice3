using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.IO;

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
        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";           // The Path of the Secnario

        // 
        // GET: /HelloWorld/Welcome/ 
        [HttpGet]
        public ActionResult display(string ip, int port,int time)
        {
            string txt = Request.Path;
            Console.WriteLine(ip);
            Console.WriteLine(port);
            Thread threadClient = new Thread(new ThreadStart(() =>
            {
                Client c = Client.Instance;
                c.connect(ip,port);
                c.readData();
            }));            Session["time"] = time;
            threadClient.Start();
            return View();
        }



        [HttpPost]
        public string GetValue()
        {
            var emp = Client.Instance.flightValueP;
            return ToXml(emp);
        }

        private string ToXml(Models.FlightValue flightValue)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Flight");

            flightValue.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
    }
}