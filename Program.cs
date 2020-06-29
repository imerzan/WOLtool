using System;

namespace WOLtool
{
    class Program
    {
        private static string szBroadcastIP, szMacAddress;
        static int Main(string[] args)
        {
            if (args.Length == 0) // No args, get user input
            {
                Console.Write("Enter Broadcast IP: ");
                szBroadcastIP = Console.ReadLine();
                Console.Write("Enter MAC Address: ");
                szMacAddress = Console.ReadLine();
                return Net.WakeOnLan(szBroadcastIP, szMacAddress);
            }
            else if (args.Length == 2) // arg1 = Broadcast IP, arg2 = MAC Address
            {
                return Net.WakeOnLan(args[0], args[1]);
            }
            else
            {
                Console.WriteLine("Unexpected number of arguments.");
                return -1;
            }
        }
    }
}