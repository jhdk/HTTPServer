using System;
using System.Collections.Generic;
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


            Console.WriteLine(message);

            string[] words = message.Split('/');
            message = words[1];
            words = message.Split(' ');
            message = words[0];

                Console.WriteLine(message);

                sw.Write("HTTP/1.0 200 OK \r\n");
                sw.Write("\r\n");
                sw.WriteLine("You requested: " + message);


            //while (message != null && message != "")
            //{
            //    Console.WriteLine(message);
            //    answer = message.ToUpper();
            //    message = sr.ReadLine();

            //}

            connectionSocket.Close();
        }

        public string answer { get; set; }
    }
}
