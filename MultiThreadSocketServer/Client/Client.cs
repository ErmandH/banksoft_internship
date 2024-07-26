using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        public static Socket socket;
        public static string name;
        public static string id;

        static void Main(string[] args)
        {
            Console.Write("Enter your name:");
            name = Console.ReadLine();
            
            A:Console.Clear();
            var hostName = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(hostName);
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint serverEp = new IPEndPoint(ip, 4242);

            socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(serverEp);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to host: " + e.Message);
                Thread.Sleep(1000);
                goto A;
            }

            Thread t = new Thread(ReceiveData);
            t.Start();

            while (true)
            {
                string input = Console.ReadLine();

                Packet p = new Packet(PacketType.Chat, id);
                p.data.Add(name);
                p.data.Add(input);
                socket.Send(p.ToBytes());
                Thread.Sleep(300);
            }
        }

        static void ReceiveData()
        {
            try
            {
                byte[] buffer;
                int readBytes;

                while (true)
                {
                    buffer = new byte[socket.SendBufferSize];
                    readBytes = socket.Receive(buffer);
                    if (readBytes > 0)
                    {
                        Packet packet = new Packet(buffer);
                        DataManager(packet);
                    }
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Server has terminated!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        // data manager
        public static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Registration:
                    id = p.data[0];      
                    break;
                case PacketType.Chat:
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(p.data[0] + ": " + p.data[1]);
                    Console.ForegroundColor = c;
                    break;
            }
        }
    }
}
