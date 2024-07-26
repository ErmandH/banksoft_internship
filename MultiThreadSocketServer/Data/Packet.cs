using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
namespace Data
{
    [Serializable]
    public class Packet
    {
       public List<string> data;
       public string senderID;
       public PacketType packetType;

       public Packet(PacketType type, string senderID)
       {
            this.data = new List<string>();
            this.senderID = senderID;
            this.packetType = type;
       }


       public Packet(byte[] packetBytes)
       {
           BinaryFormatter bf = new BinaryFormatter();
           using (MemoryStream ms = new MemoryStream(packetBytes))
           {
               Packet p = (Packet)bf.Deserialize(ms);
               this.data = p.data;
               senderID = p.senderID;
               packetType = p.packetType;
           }
       }

       public byte[] ToBytes()
       {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                byte[] bytes = ms.ToArray();
                return bytes;
            }
       }
    }

    public enum PacketType
    {
        Registration,
        Chat
    }
}
