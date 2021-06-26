using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Tests {
    static class Tests_ContextMenu {
        public static bool Test_ContextMenu1(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenu1.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                cm.BuildMenu(IntPtr.Zero, new string[] {testFile});

                return GeneralFunctions.TestBoolean("ContextMenu1", cm.IsBuilt(), true);
            }
        }

        public static bool Test_ContextMenu2(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenu2.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                cm.BuildMenu(IntPtr.Zero, new string[] {testFile});
                cm.DestroyMenu();

                return GeneralFunctions.TestBoolean("ContextMenu2", cm.IsBuilt(), false);
            }
        }

        public static bool Test_ContextMenu3(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenu3.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                Exception ex = new NoException();
                try {
                    cm.AddItem(-1, "test", () => Console.WriteLine("test"));
                } catch (Exception ex2) {
                    ex = ex2;
                }

                return GeneralFunctions.TestType("ContextMenu3", ex.GetType(), typeof(NotSupportedException));
            }
        }

        public static bool Test_ContextMenu4(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenu4.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                cm.BuildMenu(IntPtr.Zero, new string[] {testFile});

                Exception ex = new NoException();
                try {
                    cm.AddItem(-1, "test", () => Console.WriteLine("test"));
                } catch (Exception ex2) {
                    ex = ex2;
                }

                return GeneralFunctions.TestType("ContextMenu4", ex.GetType(), typeof(ArgumentOutOfRangeException));
            }
        }

        public static bool Test_ContextMenu5(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenu5.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                cm.BuildMenu(IntPtr.Zero, new string[] {testFile}, 2);

                Exception ex = new NoException();
                try {
                    cm.AddItem(-1, "test", () => Console.WriteLine("test"));
                } catch (Exception ex2) {
                    ex = ex2;
                }

                return GeneralFunctions.TestType("ContextMenu5", ex.GetType(), typeof(NoException));
            }
        }

        public static bool Test_ContextMenu6(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenu6.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                cm.BuildMenu(IntPtr.Zero, new string[] {testFile});

                try {
                    cm.ShowMenu(IntPtr.Zero, new System.Drawing.Point(0, 0));
                } catch (Win32Exception ex) { // 1400 = ERROR_INVALID_WINDOW_HANDLE (0x578): Invalid window handle.
                    return GeneralFunctions.TestNumber("ContextMenu6", ex.NativeErrorCode, 1400);
                } catch (Exception ex) {
                    return GeneralFunctions.TestType("ContextMenu6", ex.GetType(), typeof(Win32Exception));
                }

                return GeneralFunctions.TestType("ContextMenu6", typeof(NoException), typeof(Win32Exception));
            }
        }

        private static bool renameCalled;
        public static void RenameCallback() {
            renameCalled = true;
        }
        public static bool Test_ContextMenuUI1(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenuUI1.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                var frm = new Form();

                renameCalled = false;
                cm.ItemRenamed += RenameCallback;

                System.Threading.Tasks.Task.Run(() => Application.Run(frm));

                cm.BuildMenu(frm.Handle, new string[] {testFile}, flags: WalkmanLib.ContextMenu.QueryContextMenuFlags.CanRename);
                frm.BringToFront();

                System.Threading.Tasks.Task.Run(() => {
                    Thread.Sleep(500);
                    SendKeys.SendWait("{UP 2}");
                    SendKeys.SendWait("{ENTER}");
                });
                frm.Invoke((Action)(() => cm.ShowMenu(frm.Handle, frm.PointToScreen(new System.Drawing.Point(0, 0)))));
                frm.Close();

                return GeneralFunctions.TestBoolean("ContextMenuUI1", renameCalled, true);
            }
        }

        public static bool Test_ContextMenuUI2(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenuUI2.txt")))
            using (var cm = new WalkmanLib.ContextMenu()) {
                var frm = new Form();

                System.Threading.Tasks.Task.Run(() => Application.Run(frm));

                cm.BuildMenu(frm.Handle, new string[] {testFile}, 10);
                frm.BringToFront();

                renameCalled = false;
                cm.AddItem(-1, "test", RenameCallback);

                System.Threading.Tasks.Task.Run(() => {
                    Thread.Sleep(500);
                    SendKeys.SendWait("{UP}");
                    SendKeys.SendWait("{ENTER}");
                });
                frm.Invoke((Action)(() => cm.ShowMenu(frm.Handle, frm.PointToScreen(new System.Drawing.Point(0, 0)))));
                frm.Close();

                return GeneralFunctions.TestBoolean("ContextMenuUI2", renameCalled, true);
            }
        }

        private static string helpText;
        public static void HelpCallback(string text, Exception ex) {
            if (ex != null) {
                helpText = ex.Message;
            } else if (!string.IsNullOrEmpty(text)) {
                helpText = text;
            }
        }
        public class HandleWMForm : Form {
            public WalkmanLib.ContextMenu cm;

            protected override void WndProc(ref Message m) {
                cm.HandleWindowMessage(ref m);
                base.WndProc(ref m);
            }
        }

        public static bool Test_ContextMenuUI3(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "contextMenuUI3.txt"))) {
                var frm = new HandleWMForm();

                frm.cm = new WalkmanLib.ContextMenu();
                helpText = null;
                frm.cm.HelpTextChanged += HelpCallback;

                System.Threading.Tasks.Task.Run(() => Application.Run(frm));

                frm.cm.BuildMenu(frm.Handle, new string[] {testFile});
                frm.BringToFront();

                System.Threading.Tasks.Task.Run(() => {
                    Thread.Sleep(500);
                    SendKeys.SendWait("{UP}");
                    SendKeys.SendWait("{ESC}");
                });
                frm.Invoke((Action)(() => frm.cm.ShowMenu(frm.Handle, frm.PointToScreen(new System.Drawing.Point(0, 0)))));
                frm.Close();
                frm.cm.DestroyMenu();

                return GeneralFunctions.TestString("ContextMenuUI3", helpText, "Displays the properties of the selected items.");
            }
        }
    }
}
