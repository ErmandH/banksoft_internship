using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Server
{
    class Server
    {
        static Socket listenerSocket; 
        static List<ClientManager> clients;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting server");

            var hostName = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(hostName);
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint localEp = new IPEndPoint(ip, 4242);

            clients = new List<ClientManager>();
            listenerSocket = new Socket(ip.AddressFamily,SocketType.Stream ,ProtocolType.Tcp);
            listenerSocket.Bind(localEp);
            Thread listenerThread = new Thread(ListenThread);
            listenerThread.Start();
            Console.ReadLine();
        }

        static void ListenThread()
        {
            while (true)
            {
                listenerSocket.Listen(10);
                var clientSocket = listenerSocket.Accept();
                Console.WriteLine("A user has joined");
                clients.Add(new ClientManager(clientSocket));
            }
        }

        public static void  ReceiveData(object cliSocket)
        {
            Socket clientSocket = (Socket)cliSocket;
            string name = "";
            byte[] buffer;
            int readBytes;

            while (true)
            {
                try
                {
                    buffer = new byte[clientSocket.SendBufferSize];

                    readBytes = clientSocket.Receive(buffer);

                    if (readBytes > 0)
                    {
                        Packet packet = new Packet(buffer);
                        name = packet.data[0];
                        DataManager(packet);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(name + " has disconnected!");
                    break;
                }
            }
        }


        public static void  DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Chat:
                    Console.WriteLine(p.data[0] + ":" + p.data[1]);
                    foreach (var c in clients)
                        c.clientSocket.Send(p.ToBytes());
                    break;
            }
        }
    }

    class ClientManager
    {
        public  Socket clientSocket;
        private Thread clientThread;
        private string id;

        public  ClientManager(Socket cliSocket)
        {
            this.clientSocket = cliSocket;
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.ReceiveData);
            clientThread.Start(clientSocket);
            SendRegistrationPacket();
        }

        public void SendRegistrationPacket()
        {
            Packet p = new Packet(PacketType.Registration, id);
            p.data.Add(id);
            clientSocket.Send(p.ToBytes());
        }
    }
}
