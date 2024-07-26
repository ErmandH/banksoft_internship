using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        public class SyncSocketClient
        {
            public static void StartClient()
            {
                // gelicek olan mesaj için oluşturulan buffer
                byte[] buffer = new byte[1024];
                try
                {
                    var hostName = Dns.GetHostName();
                    IPHostEntry ipHost = Dns.GetHostEntry(hostName);
                    IPAddress ip = ipHost.AddressList[0];
                    IPEndPoint remoteEp = new IPEndPoint(ip, 43665);

                    // bağlanılacak olan soketi oluşturma
                    Socket sender = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        // sokete bağlanma
                        sender.Connect(remoteEp);
                        Console.WriteLine("Socket connected");
                        while (true)
                        {
                            Console.Write("Client: ");
                            string msgStr = Console.ReadLine();
                            byte[] msg = Encoding.ASCII.GetBytes(msgStr);

                            // gönderme işlemi
                            int bytesSent = sender.Send(msg);

                            if (msgStr.IndexOf("<EOF>") > -1)
                            {
                                Console.WriteLine("Ending client");
                                return;
                            }

                            int bytesReceived = sender.Receive(buffer);
                            string msgReceived = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                            Console.WriteLine("Server: " + msgReceived);
                        }                 

                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception e){
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("press any key to connect");
            Console.ReadLine();

            SyncSocketClient.StartClient();
            Console.Write("press any key to exit");
            Console.ReadLine();
        }
    }
}
