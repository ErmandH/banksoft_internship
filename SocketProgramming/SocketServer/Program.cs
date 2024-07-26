using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class Program
    {
        public class SyncServerSocket
        {
            public static string data = null;

            public static void StartListener()
            {   
                byte[] buffer = new byte[1024];
                int maxListenerCount = 10;
                var hostName = Dns.GetHostName();
                IPHostEntry ipHost = Dns.GetHostEntry(hostName);
                IPAddress ip = ipHost.AddressList[0];
                // serverın çalışacağı portun ayarlanması
                IPEndPoint localEp = new IPEndPoint(ip, 43665);

                Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    // bağlantı beklenecek olan endpointi sokete bağla
                    listener.Bind(localEp);

                    // serverın dinleme işlemini başlat
                    listener.Listen(maxListenerCount);
                    while (true)
                    {
                        Console.WriteLine("Waiting for a connection.....");
                        // bağlantı isteği geldiğinde kabul et
                        Socket handler = listener.Accept();
                        Console.WriteLine("------ New Socket Connected ------");
                        data = null;
                        // end of file görene kadar clientı dinlemeye devam et
                        while (true)
                        {
                            int byteRec = handler.Receive(buffer);
                            data = Encoding.ASCII.GetString(buffer, 0, byteRec);
                            Console.WriteLine("Client: " + data);
                            // gelen mesajda EOF varsa döngüyü bitir
                            if (data.IndexOf("<EOF>") > -1)
                            {
                                handler.Shutdown(SocketShutdown.Both);
                                handler.Close();
                                break;
                            }
                            Console.Write("Server: ");
                            string msgStr = Console.ReadLine();
                            byte[] msg = Encoding.ASCII.GetBytes(msgStr);
                            handler.Send(msg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("press any to exit program");
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            SyncServerSocket.StartListener();
        }
    }
}
