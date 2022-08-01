using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Enumeration;

namespace MMH_BLE_CLI
{
    class Ble
    {
        static BluetoothLEAdvertisementWatcher BleWatcher = new BluetoothLEAdvertisementWatcher
        {
            ScanningMode = BluetoothLEScanningMode.Active
        };

        public static async void Scan(int timeout)
        {
            // TODO
        }

        public static async void GetPairedDevices()
        {
            DeviceInformationCollection PairedBluetoothDevices = await DeviceInformation.FindAllAsync(BluetoothDevice.GetDeviceSelectorFromPairingState(true));

            Console.WriteLine($"\tList of paired devices:({PairedBluetoothDevices.Count})");

            foreach (DeviceInformation deviceInfo in PairedBluetoothDevices)
            {
                //BluetoothLEDevice device = await BluetoothLEDevice.FromIdAsync(deviceInfo.Id);

                Console.WriteLine($"\t\tDevice Name:\t{deviceInfo.Name}");
                Console.WriteLine($"\t\tMac Address:\t{deviceInfo.Id}"); //TODO
                Console.WriteLine();
            }
        }

        public static async void PairDevice()
        {
            // TODO
        }
    }
}
