using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;

namespace WOLtool
{
    public class Net
    {
        public static int WakeOnLan(string broadcastip, string macaddress)
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress ip = IPAddress.Parse(broadcastip);
                IPEndPoint ep1 = new IPEndPoint(ip, 7); // Port 7 common WOL port
                IPEndPoint ep2 = new IPEndPoint(ip, 9); // Port 9 common WOL port
                byte[] mp = BuildMagicPacket(macaddress);
                if (mp == null)
                {
                    throw new NullReferenceException("MagicPacket value is null, please check MAC Address.");
                }
                sock.SendTo(mp, ep1);
                sock.SendTo(mp, ep2);
                sock.Close();
                Console.WriteLine("Success!");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
        private static byte[] BuildMagicPacket(string macAddress) // MacAddress in any standard HEX format
        {
            try
            {
                macAddress = Regex.Replace(macAddress, "[: -]", "");
                byte[] macBytes = new byte[6];
                for (int i = 0; i < 6; i++)
                {
                    macBytes[i] = Convert.ToByte(macAddress.Substring(i * 2, 2), 16);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        for (int i = 0; i < 6; i++)  //First 6 times 0xff
                        {
                            bw.Write((byte)0xff);
                        }
                        for (int i = 0; i < 16; i++) // then 16 times MacAddress
                        {
                            bw.Write(macBytes);
                        }
                    }
                    return ms.ToArray(); // 102 bytes magic packet
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
