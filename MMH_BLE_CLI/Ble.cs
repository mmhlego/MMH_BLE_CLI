using MMH_BLE_CLI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Enumeration;
using Newtonsoft.Json;

namespace MMH_BLE_CLI {
    class Ble {
        static BluetoothLEAdvertisementWatcher BleWatcher;

        public static void Initialize() {
            BleWatcher = new BluetoothLEAdvertisementWatcher {
                ScanningMode = BluetoothLEScanningMode.Active
            };
        }

        static HashSet<BleDevice> ScanResults;

        public static void Scan(Arguments args) {    // TODO: Add result to a list and show it (json/regular)
            ScanResults = new HashSet<BleDevice>();

            BleWatcher.Received += PrintScanResults;
            BleWatcher.Stopped += OnWatcherError;
            BleWatcher.Start();
            Console.WriteLine("> Scan started");

            Thread.Sleep(args.timeout * 1000);

            BleWatcher.Stopped -= OnWatcherError;
            BleWatcher.Stop();
            BleWatcher.Received -= PrintScanResults;
            Console.WriteLine("> Scan stopped");

            // Filter results
            ArrayList FilteredResults = new ArrayList();
            foreach (BleDevice device in ScanResults) {
                if (device.name.ToLower().Contains(args.name.ToLower()) &&
                    device.address.ToLower().Contains(args.address.ToLower())) {
                    FilteredResults.Add(device);
                }
            }

            Console.WriteLine($"Found {FilteredResults.Count} devices:");
            if (args.json)
                Console.WriteLine(JsonConvert.SerializeObject(FilteredResults));
            else
                foreach (BleDevice device in FilteredResults) { Console.Write(device); }
        }

        static async void PrintScanResults(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs btAdv) {
            ScanResults.Add(BleDevice.Parse(await BluetoothLEDevice.FromBluetoothAddressAsync(btAdv.BluetoothAddress)));
        }

        public static async void PrintPairedDevices(Arguments args) {
            DeviceInformationCollection PairedBluetoothDevices = await DeviceInformation.FindAllAsync(BluetoothDevice.GetDeviceSelectorFromPairingState(true));

            // Filter results
            ArrayList FilteredResults = new ArrayList();
            foreach (DeviceInformation deviceInfo in PairedBluetoothDevices) {
                BleDevice device = BleDevice.Parse(deviceInfo);
                if (device.name.ToLower().Contains(args.name.ToLower()) &&
                    device.address.ToLower().Contains(args.address.ToLower())) {
                    FilteredResults.Add(device);
                }
            }

            Console.WriteLine($"List of paired devices:({FilteredResults.Count})");
            if (args.json)
                Console.WriteLine(JsonConvert.SerializeObject(FilteredResults));
            else
                foreach (BleDevice device in FilteredResults) { Console.Write(device); }
        }

        static void OnWatcherError(BluetoothLEAdvertisementWatcher s, BluetoothLEAdvertisementWatcherStoppedEventArgs e) {
            BleWatcher.Stop();
            Console.WriteLine("An error occured:");
            Console.WriteLine("\tScan stopped");
            Console.WriteLine($"\tScanning mode: {s.ScanningMode}");
            Console.WriteLine($"\tStatus: {s.Status}");
            Console.WriteLine($"\tError: {e.Error}");
            Console.WriteLine($"\tError type code: {e.Error.GetTypeCode()}");
            Console.WriteLine($"\tStopped event args type: {e.GetType()}");
            Console.WriteLine();
        }
    }
}
