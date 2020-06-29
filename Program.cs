using System;
// dotnet publish -c Release -r win-x64 --self-contained=false /p:PublishSingleFile=true
// dotnet publish -c Release -r linux-x64 --self-contained=false /p:PublishSingleFile=true
// dotnet publish -c Release -r osx-x64 --self-contained=false /p:PublishSingleFile=true

namespace WOLtool
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0) // No args provided, get user input
            {
                Console.Write("Enter Broadcast IP: ");
                string szBroadcastIP = Console.ReadLine();
                Console.Write("Enter MAC Address: ");
                string szMacAddress = Console.ReadLine();
                return Net.WakeOnLan(szBroadcastIP, szMacAddress);
            }
            else if (args.Length == 2) // arg1 = Broadcast IP, arg2 = MAC Address
            {
                return Net.WakeOnLan(args[0], args[1]);
            }
            else // Handle unexpected startup
            {
                Console.WriteLine("Unexpected number of arguments.");
                return -1;
            }
        }
    }
}