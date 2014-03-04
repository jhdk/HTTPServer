using System;
using System.Text;
using System.Net.Sockets;
using System.IO;
using log4net;
using log4net.Config;

namespace SocketConcurrent
{
    public class EchoService1
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EchoService1));
        private readonly TcpClient _connectionSocket;
        private const string RootCatalog = "c:/temp/";

        public EchoService1(TcpClient connectionSocket)
        {
            _connectionSocket = connectionSocket;
        }
        /// <summary>
        /// DoIt methods creates streams to write and recieve messages.
        /// It splits the message up to get the file name.
        /// If file exists, give HTTP OK response.
        /// If file doesn't exist, give HTTP Not Found response.
        /// </summary>
        public void DoIt()
        {
            try
            {
            var configFile = new FileInfo(@"..\..\logconfig.xml");
            XmlConfigurator.Configure(configFile);

            Stream ns = _connectionSocket.GetStream();
            var sr = new StreamReader(ns);
            var sw = new StreamWriter(ns) {AutoFlush = true};

            var message = sr.ReadLine();
            Logger.Info(message);

            if (message != null)
            {
                var words = message.Split('/');
                message = words[1];
                words = message.Split(' ');
                message = words[0];
            }

            Logger.Info(message);
            string contentType = GetContentType(message);
            Logger.Info("Content-Type: " + contentType);
            var path = RootCatalog + message;

            if (File.Exists(path))
            {
                var f = new FileInfo(path);
                sw.Write("HTTP/1.0 200 OK\r\n");
                sw.Write("Content-Type:"+ contentType +"\r\n");
                sw.Write("Content-Length:" + f.Length +"\r\n");
                sw.Write("\r\n");
                Logger.Info("OK");

                using (var fs = File.OpenRead(path))
                {
                    var b = new byte[1024];
                    var temp = new UTF8Encoding(true);
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        sw.WriteLine(temp.GetString(b));
                    }
                }
            }
            else
            {
                sw.Write("HTTP/1.0 404 Not Found\r\n");
                sw.Write("\r\n");
                sw.Write("Can't find the requested file");
                Logger.Info("Can't find the requested file");
            }

            _connectionSocket.Close();
            }
            catch (Exception ex)
            {
                Logger.Info("Error message: " + ex.Message);
            }
        }

        public static String GetContentType(String filename)
        {
            try
            {
            var s = filename;
            var found = s.IndexOf(".", StringComparison.Ordinal);
            var file = (s.Substring(found + 1));

            if (file == "html" || file == "htm")
            {
                return "text/html";
            }
            if (file == "gif")
            {
                return "image/gif";
            }
            if (file == "jpeg")
            {
                return "image/jpeg";
            }
            if (file == "pdf")
            {
                return "application/pdf";
            }
            if (file == "css")
            {
                return "text/css";
            }
                return "text/plain";
            }
            catch (Exception ex)
            {
                Logger.Info("Error message: " + ex.Message);
                return "text/plain"; 
            }

        }

        public string Answer { get; set; }
    }
}
