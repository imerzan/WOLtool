using System;
// dotnet publish -c Release -r win-x64 --self-contained=false /p:PublishSingleFile=true
// dotnet publish -c Release -r linux-x64 --self-contained=false /p:PublishSingleFile=true
// dotnet publish -c Release -r linux-arm --self-contained=false /p:PublishSingleFile=true
// dotnet publish -c Release -r osx-x64 --self-contained=false /p:PublishSingleFile=true

namespace WOLtool
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0) // No args provided, get user input
            {
                Console.Write("Enter MAC Address: ");
                string szMacAddress = Console.ReadLine();
                return WOL.Send(szMacAddress);
            }
            else if (args.Length == 1) // arg1 = MAC Address
            {
                return WOL.Send(args[0]);
            }
            else // Handle unexpected startup
            {
                Console.WriteLine("Unexpected arguments.");
                return -1;
            }
        }
    }
}