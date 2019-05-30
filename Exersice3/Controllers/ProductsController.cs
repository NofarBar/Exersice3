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

        // 
        // GET: /HelloWorld/Welcome/ 
        [HttpGet]
        public ActionResult display(string ip, int port,int time)
        {
            string txt = Request.Path;            Client c = Client.Instance;
            c.connect(ip, port);            Session["time"] = time;
            return View();
        }


        [HttpGet]
        public string GetValue()
        {
            Client.Instance.readData();
            Client.Instance.readForSave();
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

        [HttpGet]
        public ActionResult save(string ip, int port, int time,int timeOut, string name)
        {
            string txt = Request.Path;            Client c = Client.Instance;
            c.connect(ip, port);            Session["time"] = time;
            Session["timeOut"] = timeOut;
            c.flightValueP.FileName = name;
            return View();
        }
        [HttpGet]
        public string saveValue()
        {
                Client.Instance.readData();
                Client.Instance.readForSave();
                var emp = Client.Instance.flightValueP;
                toFile(emp.FileName);
                return ToXml(emp); 
            
        }
        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";           // The Path of the Secnario
        public void toFile(string fileName)
        {
            FileStream stream;
            string path = System.Web.HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            //string path = "/App_Data/" + fileName+".txt";
            // if (!File.Exists(path))
            if(!System.IO.File.Exists(path))
            {
                  stream = System.IO.File.Open(path, FileMode.OpenOrCreate);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true)) 
            //   using (System.IO.StreamWriter file = System.IO.File.AppendText(path))
            {
                Models.FlightValue flight = Client.Instance.flightValueP;
                file.Write(flight.Lat + ",");
                file.Write(flight.Lon + ",");
                file.Write(flight.Rudder + ",");
                file.WriteLine(flight.Throttle + ",");
            }
        }
    }


}