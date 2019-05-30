using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Exersice3.Models
{
    public class FlightValue
    {
        private double rudder;
        private double throttle;
        private double longitude;
        private double latitude;
        private string fileName;
        public double Lon
        {
            get {
                return longitude; }
            set
            {
                this.longitude = value;

            }
        }
        public double Lat
        {
            get {
                return latitude; }
            set { this.latitude = value; }
        }

        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                this.rudder = value;

            }
        }

        public double Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                this.throttle = value;
            }
        }
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                this.fileName = value;

            }
        }

        public void ToXml(XmlWriter writer)
        {
            Console.WriteLine(longitude);
            writer.WriteStartElement("Values");
            writer.WriteElementString("Lon", this.longitude.ToString());
            writer.WriteElementString("Lat", this.latitude.ToString());
            writer.WriteEndElement();

        }
    }
}