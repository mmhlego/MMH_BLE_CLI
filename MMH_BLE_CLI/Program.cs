using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace MMH_BLE_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("└→ ");
                Console.ResetColor();

                string[] commands = Console.ReadLine().Split();
                
                if (commands.Length == 0)
                {
                    continue;
                }

                switch (commands[0].ToLower())
                {
                    case "paired_devices":
                        Ble.GetPairedDevices();
                        break;

                    case "help":
                    case "--help":    // TODO: Show Help
                        Helpers.PrintHelp();
                        break;

                    case "--v":
                    case "version":
                    case "--version":    // Show app version
                        Helpers.PrintVersion();
                        break;

                    case "q":
                    case "quit":    // Quit console app
                        System.Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Unknown command. Use \"help\" to get list of commands.");
                        break;
                }
            }
        }
    }
}
