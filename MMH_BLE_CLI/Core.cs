using MMH_BLE_CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MMH_BLE_CLI {
    class Core {
        public static Arguments Validate(string[] commands) {
            Arguments result = new Arguments();

            if (commands.Length == 0)
                result.is_vaild = false;

            result.timeout = GetTimeout(commands);
            result.name = GetName(commands);
            result.address = GetAddress(commands);
            result.json = Array.IndexOf(commands, "-json") > 0;

            result.is_vaild = result.is_vaild ||
                              result.timeout == -1 ||
                              result.name == null ||
                              result.address == null;

            return result;
        }

        // ===================================================== Run functions

        static List<string> Flags = new List<string>() {
            "-name", "-json", "-address", "-t",
        };

        public static void RunCommand(string[] commands) {
            Arguments args = Validate(commands);

            if (!args.is_vaild)
                Console.WriteLine("Invalid format."); //TODO

            else if (commands[0] == "sleep")
                Sleep(commands);

            else if (commands[0] == "paired_devices")
                Ble.PrintPairedDevices(args);

            else if (commands[0] == "scan")
                Ble.Scan(args);

            else
                Console.WriteLine("Unknown command. Use \"help\" to get list of commands.");

        }

        public static void Sleep(string[] commands) {
            if (commands.Length == 1) {
                Console.WriteLine("Sleeping for 1 second");
                Thread.Sleep(1000);
                return;
            }

            bool isNumeric = int.TryParse(commands[1], out int t);
            if (!isNumeric) {
                Console.WriteLine("Invalid parameter (seconds)");
            } else {
                Console.WriteLine($"Sleeping for {t} second(s)");
                Thread.Sleep(t * 1000);
            }
        }

        // ===================================================== Helper functions

        static int GetTimeout(string[] args) {
            int index = Array.IndexOf(args, "-t") + 1;

            if (index <= 1) {
                return Constants.DEFAULT_TIMEOUT;
            } else if (index >= args.Length) {
                return -1;
            }

            bool isNumeric = int.TryParse(args[index], out int t);
            if (isNumeric) { return t; }

            return -1;
        }

        static string GetAddress(string[] args) {
            int index = Array.IndexOf(args, "-address") + 1;

            if (index <= 1) { return ""; }
            if (index >= args.Length) { return null; }
            if (IsKeyword(args[index])) { return null; }

            string regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
            if (!Regex.IsMatch(args[index], regex)) { return null; }

            return args[index];
        }

        static string GetName(string[] args) {
            int index = Array.IndexOf(args, "-name") + 1;

            if (index <= 1) { return ""; }
            if (index >= args.Length) { return null; }
            if (IsKeyword(args[index])) { return null; }

            return args[index];
        }

        static bool IsKeyword(string keyword) {
            return Flags.Contains(keyword);
        }
    }
}
