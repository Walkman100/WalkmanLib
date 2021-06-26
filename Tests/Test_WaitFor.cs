using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests {
    static class Tests_WaitFor {
        private const string shell32WindowName = "shell32.dll Properties";
        // https://gist.github.com/Memphizzz/ed69d2500c422019609c
        private const string shell32WindowClass = "#32770";

        public static bool Test_WaitForWindow1() {
            if (WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll")) == false) {
                return GeneralFunctions.TestString("WaitForWindow1", "ShowProperties returned False", "ShowProperties returned True");
            }

            Task.Run(() => {
                Thread.Sleep(700);
                SendKeys.SendWait("{ESC}");
            });

            Thread.Sleep(400); // give the window time to show

            bool result = WalkmanLib.WaitForWindow(shell32WindowName, shell32WindowClass, 10);

            return GeneralFunctions.TestBoolean("WaitForWindow1", result, false);
        }

        public static bool Test_WaitForWindow2() {
            if (WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll")) == false) {
                return GeneralFunctions.TestString("WaitForWindow2", "ShowProperties returned False", "ShowProperties returned True");
            }

            Task.Run(() => {
                Thread.Sleep(700);
                SendKeys.SendWait("{ESC}");
            });

            Thread.Sleep(400);

            bool waitDone = false;
            bool countExited = false;
            int waitTimeout = 40;
            if (!Console.IsOutputRedirected) {
                Task.Run(() => {
                    Console.Write("Waiting for Shell thread to exit. This is expected to take a while, please wait: ");
                    WalkmanLib.ConsoleProgress(0, waitTimeout, ref waitDone, ref countExited);
                });
            } else {
                countExited = true;
            }

            bool result = true;
            try {
                result = WalkmanLib.WaitForWindowByThread(shell32WindowName, shell32WindowClass, (uint)waitTimeout * 1000);
            } finally {
                waitDone = true;
                while (!countExited) {
                    Thread.Sleep(1);
                }
                if (!Console.IsOutputRedirected)
                    Console.WriteLine();
            }

            return GeneralFunctions.TestBoolean("WaitForWindow2", result, false);
        }
    }
}
