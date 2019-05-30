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
        public ActionResult Index()
        {

            return View();
        }
 
        [HttpGet]
        public ActionResult display(string ip, int port, int time)
        {
            string[] isIp = ip.Split('.');
            Info c = Info.Instance;
            if (isIp.Length != 4)
            {
                Session["read"] = true;
                Session["time"] = port;
                readFile(ip);
            }
            else
            {
                c.connect(ip, port);
                Session["time"] = time;
                Session["read"] = false;
            }
            return View();
        }


        [HttpGet]
        public string GetValue()
        {
            Info info = Info.Instance;

            var readFromFile = @Session["read"];
            bool read = Convert.ToBoolean(readFromFile);
            if (!read)
            {
                info.readData();

            }
            else
            {
                string[] linesValue = info.flightValueP.StringValue;
                if (linesValue.Length == 0|| linesValue[0] == "")
                {
                    return null;
                }
                string[] tempLine = linesValue[0].Split(',');
                info.flightValueP.Lat = Convert.ToDouble(tempLine[0]);
                info.flightValueP.Lon = Convert.ToDouble(tempLine[1]);
                info.flightValueP.StringValue = info.flightValueP.StringValue.Skip(1).ToArray();

            }
            var emp = info.flightValueP;
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
        public ActionResult save(string ip, int port, int time, int timeOut, string name)
        {
            string txt = Request.Path;
            Info c = Info.Instance;
            c.connect(ip, port);
            Session["time"] = time;
            Session["timeOut"] = timeOut;
            c.flightValueP.FileName = name;
            return View();
        }
        [HttpGet]
        public string saveValue()
        {
            Info.Instance.readData();
            Info.Instance.readForSave();
            var emp = Info.Instance.flightValueP;
            toFile(emp.FileName);
            return ToXml(emp);

        }
        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";           // The Path of the Secnario
        public void toFile(string fileName)
        {
            FileStream stream;
            string path = System.Web.HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            if (!System.IO.File.Exists(path))
            {
                stream = System.IO.File.Open(path, FileMode.OpenOrCreate);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                Models.FlightValue flight = Info.Instance.flightValueP;
                file.Write(flight.Lat + ",");
                file.Write(flight.Lon + ",");
                file.Write(flight.Rudder + ",");
                file.WriteLine(flight.Throttle + ",");

            }
        }
        public void readFile(string fileName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, fileName));
            string[] lines = System.IO.File.ReadAllLines(path);
            Info.Instance.flightValueP.StringValue = lines;
        }

    }

}