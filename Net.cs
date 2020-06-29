using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;

namespace WOLtool
{
    public class Net
    {
        public static int WakeOnLan(string ipaddress, string macaddress)
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // Create socket
                sock.EnableBroadcast = true; // Enable broadcast, required for macOS compatibility 
                IPAddress ip = IPAddress.Parse(ipaddress);
                IPEndPoint ep1 = new IPEndPoint(ip, 7); // Port 7 common WOL port
                IPEndPoint ep2 = new IPEndPoint(ip, 9); // Port 9 common WOL port
                byte[] mp = BuildMagicPacket(macaddress); // Get magic packet byte array based on MAC Address
                if (mp == null)
                {
                    throw new NullReferenceException("BuildMagicPacket() returned null due to an exception. Please double check the MAC Address.");
                }
                sock.SendTo(mp, ep1); // Transmit Magic Packet on Port 7
                sock.SendTo(mp, ep2); // Transmit Magic Packet on Port 9
                sock.Close(); // Close socket
                Console.WriteLine("Success!");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
        private static byte[] BuildMagicPacket(string macaddress) // MacAddress in any standard HEX format
        {
            try
            {
                macaddress = Regex.Replace(macaddress, "[: -]", "");
                byte[] macBytes = new byte[6];
                for (int i = 0; i < 6; i++)
                {
                    macBytes[i] = Convert.ToByte(macaddress.Substring(i * 2, 2), 16);
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
                    return ms.ToArray(); // return 102 bytes magic packet
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
