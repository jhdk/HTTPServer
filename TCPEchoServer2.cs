using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketConcurrent
{
    public class TCPEchoServer2
    {
        public static void Main1(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 6789);

            //TcpListener serverSocket = new TcpListener(6789);
            serverSocket.Start();


            while (true)
            {
                Socket connectionSocket = serverSocket.AcceptSocket();
                Console.WriteLine("Server activated now");
                EchoService service = new EchoService(connectionSocket);
                Thread myThread = new Thread(new ThreadStart(service.DoIt));
                myThread.Start();

                //Task.Factory.StartNew(service.doIt);
                // or use delegates Task.Factory.StartNew() => service.DoIt();
             }


            serverSocket.Stop();
        } 
        
    }
}
