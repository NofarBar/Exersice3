﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Exersice3
{
    public class Client
    {
       
        private string ip;
        private int port;
        private IPEndPoint ep;
        private TcpClient client;
        private bool isConnect = false;
        private static Client instance = null;
        private Models.FlightValue flightValue;
        private Client() {        
            flightValue = new Models.FlightValue();
        }
        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                }
                return instance;
            }
        }

        public Models.FlightValue flightValueP
        {
            get
            {
                return this.flightValue;
            }
            set
            {
                this.flightValue = value;
            }
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";           // The Path of the Secnario

 
            public void connect(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            Console.WriteLine(this.ip);
            Console.WriteLine(this.port);
            if (!isConnect)
            {
                try
                {
                    ep = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
                    client = new TcpClient();
                    client.Connect(ep);
                    Console.WriteLine("Command - You are connected");
                    isConnect = true;
                }
                catch (System.Exception) { }
            }
        }
        // The function return true if client connect and false otherwise
        public bool isConnection()
        {
            return isConnect;
        }
        // The function stop the connection of client 
        public void disconnect()
        {
            if (isConnect)
            {
                client.Close();
                isConnect = false;
            }
        }

        // The function sent values to flight gear
        public string readData()
        {
            string result = "";
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                { // read from flight gear while connect
                    while (isConnection())
                    {
                        string commandWrite = "get /position/longitude-deg";
                        writer.WriteLine(commandWrite);
                        writer.Flush();
                        result = reader.ReadLine();
                        string[] splitValues = result.Split('\'');
                        flightValue.Lon = Convert.ToDouble(splitValues[1]);
                        commandWrite = "get /position/latitude-deg";
                        writer.WriteLine(commandWrite);
                        writer.Flush();
                        result = reader.ReadLine();
                        splitValues = result.Split('\'');
                        flightValue.Lat = Convert.ToDouble(splitValues[1]);
                    }
                }
            }
            catch (System.Exception)
            {

            }
            return result;
            
        }
    }
}