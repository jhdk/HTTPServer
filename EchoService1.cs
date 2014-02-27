using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace SocketConcurrent
{
    class EchoService1
    {
        private TcpClient connectionSocket;

        public EchoService1(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }
        public void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string answer;
            sw.Write("HTTP/1.0 200 OK \r\n");
            sw.Write("\r\n");
            sw.WriteLine("Message");
            //while (message != null && message != "")
            //{
            //    Console.WriteLine("Client: " + message);
            //    answer = message.ToUpper();
            //    sw.Write("HTTP/1.0 200 OK \r\n ");
            //    sw.Write("\r\n");
            //    sw.WriteLine("Message");
            //    message = sr.ReadLine();

            //}
            connectionSocket.Close();
        }

        public string answer { get; set; }
    }
}
