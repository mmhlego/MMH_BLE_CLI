using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Bluetooth;
using Windows.UI.Xaml;

namespace MMH_BLE_CLI
{
    class Helpers
    {
        static string VERSION = "alpha";
        static int DEFAULT_TIMEOUT = 5;

        public static void PrintVersion() { //TODO
            Console.WriteLine($"Current version: {VERSION}\n");
        }

        public static void PrintHelp() { //TODO
            Console.WriteLine("help");
        }

        public static int getTimeout(string[] args) {
            int index = Array.IndexOf(args, "-t")+1;

            if(index < 0 ) { // No timeout provided
                return DEFAULT_TIMEOUT;
            } else if (index >= args.Length) { // 
                return -1;
            }

            bool isNumeric = int.TryParse(args[index], out int t);
            if(isNumeric) { return t; }

            return -1;
        }

        public static string getMacAddress(BluetoothLEDevice device)
        {
            try
            {
                if (device == null)
                {
                    return "";
                }
                var tempMac = device.BluetoothAddress.ToString("X");
                var regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
                var replace = "$1:$2:$3:$4:$5:$6";
                return Regex.Replace(tempMac, regex, replace);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
