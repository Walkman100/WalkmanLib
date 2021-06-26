using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests {
    internal class ShowPropertiesTestsHelper {
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
        // https://www.pinvoke.net/default.aspx/user32/GetForegroundWindow.html
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr GetForegroundWindow();

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw
        // https://www.pinvoke.net/default.aspx/user32/GetWindowText.html
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow
        // https://www.pinvoke.net/default.aspx/user32/GetWindow.html
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        internal static string GetActiveWindowText(bool getChildText = false) {
            IntPtr windowHandle;
            windowHandle = GetForegroundWindow();
            if (Marshal.GetLastWin32Error() != 0)
                throw new System.ComponentModel.Win32Exception();

            if (getChildText) {
                windowHandle = GetWindow(windowHandle, 5); // GW_CHILD = 5
                if (Marshal.GetLastWin32Error() != 0)
                    throw new System.ComponentModel.Win32Exception();
            }

            var stringBuilderTarget = new System.Text.StringBuilder(1024);
            int result = GetWindowText(windowHandle, stringBuilderTarget, 1024);
            if (result == 0)
                throw new System.ComponentModel.Win32Exception();
            return stringBuilderTarget.ToString();
        }
    }

    static class Tests_ShowProperties {
        public static bool Test_ShowProperties1() {
            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ENTER}");
            });

            bool result = WalkmanLib.ShowProperties("nonExistantFile.txt");

            return GeneralFunctions.TestBoolean("ShowProperties1", result, false);
        }

        public static bool Test_ShowProperties2() {
            bool result = WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll"));

            Thread.Sleep(600); // ShowProperties is Async when it succeeds
            SendKeys.SendWait("{ESC}");
            Thread.Sleep(10);  // wait for window to close else next functions don't work

            return GeneralFunctions.TestBoolean("ShowProperties2", result, true);
        }

        public static bool Test_ShowProperties3() {
            try {
                bool result = WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll"));
                if (!result) {
                    return GeneralFunctions.TestBoolean("ShowProperties3", result, true);
                }

                Thread.Sleep(700); // wait for window to show
                return GeneralFunctions.TestString("ShowProperties3", ShowPropertiesTestsHelper.GetActiveWindowText(), "shell32.dll Properties");
            } finally {
                SendKeys.SendWait("{ESC}");
                Thread.Sleep(10);
            }
        }

        public static bool Test_ShowProperties4() {
            try {
                bool result = WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll"), "Details");
                if (!result) {
                    return GeneralFunctions.TestBoolean("ShowProperties4", result, true);
                }

                Thread.Sleep(700);

                string tabName;
                try {
                    tabName = ShowPropertiesTestsHelper.GetActiveWindowText(true);
                } catch (Exception ex) {
                    tabName = ex.ToString();
                }

                return GeneralFunctions.TestString("ShowProperties4", tabName, "Details");
            } finally {
                SendKeys.SendWait("{ESC}");
                Thread.Sleep(10);
            }
        }
    }
}
