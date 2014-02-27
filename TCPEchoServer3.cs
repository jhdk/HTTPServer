﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketConcurrent
{
    public class TCPEchoServer3
    {
        public static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("10.154.1.119" );
            TcpListener serverSocket = new TcpListener(8080);

            //TcpListener serverSocket = new TcpListener(8080);
            serverSocket.Start();

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated now");
                EchoService1 service = new EchoService1(connectionSocket);
                Thread myThread = new Thread(new ThreadStart(service.DoIt));
                myThread.Start();

                //Task.Factory.StartNew(service.doIt);
                // or use delegates Task.Factory.StartNew() => service.DoIt();
            }

            
            serverSocket.Stop();
        }

    }
}
