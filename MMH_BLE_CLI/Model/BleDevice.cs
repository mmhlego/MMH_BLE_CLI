using System;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace MMH_BLE_CLI.Model {
    class BleDevice {
        public string name;
        public string address;

        public static BleDevice Parse(BluetoothLEDevice device) {
            return new BleDevice() {
                name = device.Name,
                address = Helpers.GetDeviceAddress(device),
            };
        }

        public static BleDevice Parse(DeviceInformation device) {
            return new BleDevice() {
                name = device.Name,
                address = device.Id,
            };
        }

        override public string ToString() {
            return $"Device Name: {name}\n"
                + $"\tMac Address: {address}\n";
        }

        override public bool Equals(object obj) {
            var item = obj as BleDevice;

            if (item == null) {
                return false;
            }

            return this.address.Equals(item.address);
        }

        override public int GetHashCode() {
            return address.GetHashCode();
        }
    }
}
