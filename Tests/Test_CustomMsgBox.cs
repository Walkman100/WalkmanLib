using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests {
    static class Tests_CustomMsgBox {
        public static bool Test_CustomMsgBox1() {
            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ENTER}");
            });

            DialogResult result = WalkmanLib.CustomMsgBox("test");

            return GeneralFunctions.TestNumber("CustomMsgBox1", (int)result, (int)DialogResult.OK);
        }

        public static bool Test_CustomMsgBox2() {
            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ENTER}");
            });

            DialogResult result = WalkmanLib.CustomMsgBox("test", buttons: MessageBoxButtons.YesNoCancel);

            return GeneralFunctions.TestNumber("CustomMsgBox2", (int)result, (int)DialogResult.Yes);
        }

        public static bool Test_CustomMsgBox3() {
            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ESC}");
            });

            DialogResult result = WalkmanLib.CustomMsgBox("test", buttons: MessageBoxButtons.YesNoCancel);

            return GeneralFunctions.TestNumber("CustomMsgBox3", (int)result, (int)DialogResult.Cancel);
        }

        public static bool Test_CustomMsgBox4() {
            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ENTER}");
            });

            DialogResult result = WalkmanLib.CustomMsgBox("test", buttons: MessageBoxButtons.AbortRetryIgnore);

            return GeneralFunctions.TestNumber("CustomMsgBox4", (int)result, (int)DialogResult.Abort);
        }

        public static bool Test_CustomMsgBox5() {
            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ESC}");
            });

            DialogResult result = WalkmanLib.CustomMsgBox("test", buttons: MessageBoxButtons.AbortRetryIgnore);

            return GeneralFunctions.TestNumber("CustomMsgBox5", (int)result, (int)DialogResult.Ignore);
        }

        public static bool Test_CustomMsgBox6() {
            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ENTER}");
            });

            string result = WalkmanLib.CustomMsgBox("test", null, "Test Button One", "Test Button Two");

            return GeneralFunctions.TestString("CustomMsgBox6", result, "Test Button One");
        }

        public static bool Test_CustomMsgBox7() {
            Task.Run(() => {
                Thread.Sleep(700);
                SendKeys.SendWait("{ESC}");
            });

            string result = WalkmanLib.CustomMsgBox("test", null, "Test Button One", "Test Button Two", "Test Button Three");

            return GeneralFunctions.TestString("CustomMsgBox7", result, "Test Button Three");
        }

        public static bool Test_CustomMsgBox8() {
            Task.Run(() => WalkmanLib.CustomMsgBox("test", "TestTitle"));

            Thread.Sleep(700);
            string result = ShowPropertiesTestsHelper.GetActiveWindowText();
            SendKeys.SendWait("{ENTER}");

            return GeneralFunctions.TestString("CustomMsgBox8", result, "TestTitle");
        }
    }
}
