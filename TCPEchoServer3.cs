using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketConcurrent
{
    public class TcpEchoServer3
    {
        public static void Main(string[] args)
        {
            IPAddress.Parse("192.168.43.72");
            var serverSocket = new TcpListener(8080);

            serverSocket.Start();

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                
                Console.WriteLine("Server activated now!");
                var service = new EchoService1(connectionSocket);

                Task.Factory.StartNew(service.DoIt);
              
            }

            
            serverSocket.Stop();
        }

    }
}
