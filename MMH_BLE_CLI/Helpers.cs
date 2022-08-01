using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace MMH_BLE_CLI
{
    class Helpers
    {
        static string VERSION = "alpha";
        public static void PrintVersion() { //TODO
            Console.WriteLine($"Current version: {VERSION}\n");
        }

    }
}
