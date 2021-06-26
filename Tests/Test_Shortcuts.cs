using System;
using System.IO;

namespace Tests {
    static class Tests_Shortcuts {
        public static bool Test_Shortcuts1() {
            string shortcutPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), "Microsoft", 
                                               "Windows", "Start Menu", "Programs", "System Tools", 
                                               "Command Prompt.lnk");
            if (!File.Exists(shortcutPath)) {
                return GeneralFunctions.TestString("Shortcuts1", "System shortcut doesn't exist", "System shortcut exists");
            }

            return GeneralFunctions.TestString("Shortcuts1", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "%HOMEDRIVE%%HOMEPATH%");
        }

        public static bool Test_Shortcuts2() {
            string shortcutPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), "Microsoft", 
                                               "Windows", "Start Menu", "Programs", "System Tools", 
                                               "Command Prompt.lnk");
            if (!File.Exists(shortcutPath)) {
                return GeneralFunctions.TestString("Shortcuts2", "System shortcut doesn't exist", "System shortcut exists");
            }

            return GeneralFunctions.TestBoolean("Shortcuts2", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), false);
        }

        public static bool Test_Shortcuts3() {
            string shortcutPath = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Microsoft", 
                                               "Windows", "Start Menu", "Programs", "Accessories", "System Tools", 
                                               "Character Map.lnk");
            if (!File.Exists(shortcutPath)) {
                return GeneralFunctions.TestString("Shortcuts3", "System shortcut doesn't exist", "System shortcut exists");
            }

            return GeneralFunctions.TestString("Shortcuts3", WalkmanLib.GetShortcutInfo(shortcutPath).Description, 
                                               "Selects special characters and copies them to your document.");
        }

        public static bool Test_Shortcuts4(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "shortcuts4.lnk"), false, false)) {
                return GeneralFunctions.TestString("Shortcuts4", WalkmanLib.CreateShortcut(testFile), testFile);
            }
        }

        public static bool Test_Shortcuts5(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts5");
            try {
                return GeneralFunctions.TestString("Shortcuts5", WalkmanLib.CreateShortcut(shortcutPath), shortcutPath + ".lnk");
            } finally {
                if (File.Exists(shortcutPath))
                    File.Delete(shortcutPath); // this shouldn't exist - don't show warning
                GeneralFunctions.DeleteFileIfExists(shortcutPath + ".lnk");
            }
        }

        public static bool Test_Shortcuts6(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts6.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, targetPath: @"C:\Windows\notepad.exe");
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestString("Shortcuts6", WalkmanLib.GetShortcutInfo(shortcutPath).TargetPath, @"C:\Windows\notepad.exe");
            }
        }

        public static bool Test_Shortcuts7(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts7.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, arguments: "testArgument");
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestString("Shortcuts7", WalkmanLib.GetShortcutInfo(shortcutPath).Arguments, "testArgument");
            }
        }

        public static bool Test_Shortcuts8(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts8.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, workingDirectory: @"C:\Windows");
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestString("Shortcuts8", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, @"C:\Windows");
            }
        }

        public static bool Test_Shortcuts9(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts9.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, iconPath: @"C:\Windows\regedit.exe,0");
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestString("Shortcuts9", WalkmanLib.GetShortcutInfo(shortcutPath).IconLocation, @"C:\Windows\regedit.exe,0");
            }
        }

        public static bool Test_Shortcuts10(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts10.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, comment: "testComment");
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestString("Shortcuts10", WalkmanLib.GetShortcutInfo(shortcutPath).Description, "testComment");
            }
        }

        public static bool Test_Shortcuts11(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts11.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, shortcutKey: "CTRL+ALT+F");
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestString("Shortcuts11", WalkmanLib.GetShortcutInfo(shortcutPath).Hotkey, "Alt+Ctrl+F");
            }
        }

        public static bool Test_Shortcuts12(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts12.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, windowStyle: System.Windows.Forms.FormWindowState.Maximized);
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestNumber("Shortcuts12", WalkmanLib.GetShortcutInfo(shortcutPath).WindowStyle, 3); // 3 = Maximised. explained in the interface commentDoc
            }
        }

        public static bool Test_Shortcuts13(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts13.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath);
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                return GeneralFunctions.TestBoolean("Shortcuts13", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), false);
            }
        }

        public static bool Test_Shortcuts14(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts14.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath);
            using (var testFile = new DisposableFile(shortcutPath, false)) {
                WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, true);
                return GeneralFunctions.TestBoolean("Shortcuts14", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), true);
            }
        }

        public static bool Test_Shortcuts15(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts15.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, @"C:\Windows\notepad.exe", "testArgument", 
                                                     @"C:\Windows", @"C:\Windows\regedit.exe,0", "testComment", 
                                                     "CTRL+ALT+F", System.Windows.Forms.FormWindowState.Maximized);

            using (var testFile = new DisposableFile(shortcutPath, false)) {
                WalkmanLib.CreateShortcut(shortcutPath, workingDirectory: "%UserProfile%");
                return GeneralFunctions.TestString("Shortcuts15", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "%UserProfile%");
            }
        }

        public static bool Test_Shortcuts16(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts16.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, @"C:\Windows\notepad.exe", "testArgument", 
                                                     @"C:\Windows", @"C:\Windows\regedit.exe,0", "testComment", 
                                                     "CTRL+ALT+F", System.Windows.Forms.FormWindowState.Maximized);

            using (var testFile = new DisposableFile(shortcutPath, false)) {
                WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, true);
                return GeneralFunctions.TestString("Shortcuts16", WalkmanLib.GetShortcutInfo(shortcutPath).IconLocation, @"C:\Windows\regedit.exe,0");
            }
        }

        public static bool Test_Shortcuts17(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts17.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, @"C:\Windows\notepad.exe", "testArgument", 
                                                     @"C:\Windows", @"C:\Windows\regedit.exe,0", "testComment", 
                                                     "CTRL+ALT+F", System.Windows.Forms.FormWindowState.Maximized);

            using (var testFile = new DisposableFile(shortcutPath, false)) {
                WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, true);
                WalkmanLib.CreateShortcut(shortcutPath, workingDirectory: "%UserProfile%", iconPath: @"C:\Windows\explorer.exe");
                return GeneralFunctions.TestBoolean("Shortcuts17", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), true);
            }
        }

        public static bool Test_Shortcuts18(string rootTestFolder) {
            string shortcutPath = Path.Combine(rootTestFolder, "shortcuts18.lnk");
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, @"C:\Windows\notepad.exe", "testArgument", 
                                                     @"C:\Windows", @"C:\Windows\regedit.exe,0", "testComment", 
                                                     "CTRL+ALT+F", System.Windows.Forms.FormWindowState.Maximized);

            using (var testFile = new DisposableFile(shortcutPath, false)) {
                bool returnVal = true;

                var link = (IWshRuntimeLibrary.IWshShortcut)
                    new IWshRuntimeLibrary.WshShell().CreateShortcut(shortcutPath);

                if (!GeneralFunctions.TestString("Shortcuts18.1", link.TargetPath, @"C:\Windows\notepad.exe"))
                    returnVal = false;
                if (!GeneralFunctions.TestString("Shortcuts18.2", link.Arguments, "testArgument"))
                    returnVal = false;
                if (!GeneralFunctions.TestString("Shortcuts18.3", link.WorkingDirectory, @"C:\Windows"))
                    returnVal = false;
                if (!GeneralFunctions.TestString("Shortcuts18.4", link.IconLocation, @"C:\Windows\regedit.exe,0"))
                    returnVal = false;
                if (!GeneralFunctions.TestString("Shortcuts18.5", link.Description, "testComment"))
                    returnVal = false;
                if (!GeneralFunctions.TestString("Shortcuts18.6", link.Hotkey, "Alt+Ctrl+F"))
                    returnVal = false;
                if (!GeneralFunctions.TestNumber("Shortcuts18.7", link.WindowStyle, 3))
                    returnVal = false;
                if (!GeneralFunctions.TestString("Shortcuts18.8", link.FullName, shortcutPath))
                    returnVal = false;

                return returnVal;
            }
        }

        public static bool Test_ShortcutThrows1(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.CreateShortcut(Path.Combine(rootTestFolder, "shortcutThrows1.lnk"), shortcutKey: "TEST");
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("ShortcutThrows1", ex.GetType(), typeof(ArgumentException));
        }

        public static bool Test_ShortcutThrows2() {
            Exception ex = new NoException();
            try {
                WalkmanLib.CreateShortcut(Path.Combine(Environment.SystemDirectory, "shortcutThrows2.lnk"));
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("ShortcutThrows2", ex.GetType(), typeof(UnauthorizedAccessException));
        }
    }
}
