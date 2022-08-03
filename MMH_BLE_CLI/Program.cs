using System;

namespace MMH_BLE_CLI {
    class Program {
        static void Main(string[] args) {
            Ble.Initialize();

            while (true) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("└→ ");
                Console.ResetColor();

                string[] commands = Console.ReadLine().Split();
                commands[0] = commands[0].ToLower();


                if (commands.Length == 0) {
                    continue;

                } else if (commands[0] == "models") {
                    Helpers.PrintModels();

                } else if (commands[0] == "h" || commands[0] == "help") {
                    Helpers.PrintHelp();

                } else if (commands[0] == "v" || commands[0] == "version") {
                    Helpers.PrintVersion();

                } else if (commands[0] == "q" || commands[0] == "quit") {
                    Environment.Exit(0);

                } else {
                    Core.RunCommand(commands);
                }
            }
        }
    }
}
