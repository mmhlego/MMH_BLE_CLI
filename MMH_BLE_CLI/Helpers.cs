using MMH_BLE_CLI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Bluetooth;
using Windows.UI.Xaml;

namespace MMH_BLE_CLI {
    class Helpers {
        public static void PrintVersion() { //TODO
            Console.WriteLine($"Current version: {Constants.VERSION}\n");
        }

        public static void PrintHelp() { //TODO
            Console.WriteLine("help");
        }

        internal static void PrintModels() {
            Console.WriteLine("BleDevice model:");
            Console.WriteLine(
                JsonConvert.SerializeObject(
                    new BleDevice() {
                        name = "string",
                        address = "string"
                    })
                );
            Console.WriteLine();
        }

        public static string GetDeviceAddress(BluetoothLEDevice device) {
            try {
                if (device == null) {
                    return "";
                }
                string tempMac = device.BluetoothAddress.ToString("X");
                string regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
                string replace = "$1:$2:$3:$4:$5:$6";
                return Regex.Replace(tempMac, regex, replace);
            } catch (Exception ex) {
                return "";
            }
        }
    }
}
