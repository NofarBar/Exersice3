using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Exersice3.Models
{
    public class FlightValue
    {
        public double longitude;
        public double latitude;
        public double Lon
        {
            get { return longitude; }
            set
            {
                this.longitude = value;
            }
        }
        public double Lat
        {
            get { return latitude; }
            set { this.latitude = value; }
        }
        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Values");
            writer.WriteElementString("Lon", this.longitude.ToString());
            writer.WriteElementString("Lat", this.latitude.ToString());
            writer.WriteEndElement();

        }
    }
}