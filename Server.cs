using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarekDamikDungeon;

namespace TcpServer2
{
    public class Server
    {
        private TcpListener myServer;
        private bool isRunning;

        public Server(int port)
        {
            myServer = new TcpListener(System.Net.IPAddress.Any, port);
            myServer.Start();
            isRunning = true;
            ServerLoop();
        }

        /**
         * method that waits until client connects, then makes passes the connection to ClientLoop()
         */
        private void ServerLoop()
        {
            Console.WriteLine("Server byl spusten");
            while (isRunning)
            {
                TcpClient client = myServer.AcceptTcpClient();
                Thread thread = new Thread(new ParameterizedThreadStart(ClientLoop));
                thread.Start(client);
            }

        }
        
        /**
         * this method contains loop that keeps individual client connected
         * it reads data from client passes them to command design pattern in the GameLoop and then sends respond to player
         * @param obj is client object containing tcp connection
         */
        private void ClientLoop(object obj)
        {
            TcpClient client = (TcpClient)obj;
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);

            writer.WriteLine("Byl jsi pripojen");
            writer.Flush();
            bool clientConnect = true;
            string data = null;
            string[] args = null;
            string dataRecive = null;
            GameExec gameExec = new GameExec();
            while (clientConnect)
            {
                data = reader.ReadLine();
                data = data.ToLower();
                args = data.Split(' ');
                clientConnect = !gameExec.CommandFromClient(args);
                writer.WriteLine(gameExec.Result);
                writer.Flush();
            }
            writer.Flush();
            client.Close();
        }
    }
}
