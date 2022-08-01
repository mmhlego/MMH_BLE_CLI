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
        BluetoothLEAdvertisementWatcher BleWatcher = new BluetoothLEAdvertisementWatcher
        {
            ScanningMode = BluetoothLEScanningMode.Active
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                string[] commands = Console.ReadLine().Split();
                
                if (commands.Length == 0)
                {
                    continue;
                }

                switch (commands[0].ToLower())
                {
                    case "q":
                    case "quit":    // Quit console app
                        System.Environment.Exit(0);
                        break;

                    case "--help":    // TODO: Show Help

                        break;

                    case "--v":
                    case "--version":    // Show app version
                        Helpers.PrintVersion();
                        break;

                    default:
                        break;
                }


                /*if (commands[0].ToLower() == "q" || commands[0].ToLower() == "quit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command. Use \"help\" to get list of commands.");
                }*/
            }
        }
    }
}
