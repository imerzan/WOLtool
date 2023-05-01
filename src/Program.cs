using System;

namespace WOLtool
{
    class Program
    {
        static int Main(string[] args)
        {
            using var wol = new WakeOnLan();
            if (args.Length == 0) // No args provided, get console input
            {
                while (true) // Continue prompting user until no more input is received
                {
                    Console.Write("Enter MAC Address: ");
                    string mac = Console.ReadLine().Trim();
                    if (mac == String.Empty) break; // User is done, exit
                    try
                    {
                        wol.Send(mac);
                        Console.WriteLine($"{mac} [OK]");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{mac} [FAIL] {ex}");
                    }
                }
                return 0;
            }
            else // Args provided
            {
                int numFailed = 0;
                foreach (string arg in args) // iterate all arguments
                {
                    try
                    {
                        wol.Send(arg);
                        Console.WriteLine($"{arg} [OK]");
                    }
                    catch (Exception ex)
                    {
                        numFailed--;
                        Console.WriteLine($"{arg} [FAIL] {ex}");
                    }
                }
                return numFailed;
            }
        }
    }
}