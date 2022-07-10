using System;
using System.IO;

namespace Tests {
    static class Tests_GetOpenWith {
        public static bool Test_GetOpenWith1() {
            string testPath = Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "explorer.exe").ToLower();
            return GeneralFunctions.TestString("GetOpenWith1", WalkmanLib.GetOpenWith(testPath).ToLower(), testPath);
        }

        public static bool Is8Dot3Enabled(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "test file.txt"))) {
                return File.Exists(Path.Combine(rootTestFolder, "TESTFI~1.TXT"));
            }
        }

        public static bool Test_GetOpenWith2(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith2.bat"))) {
                if (Is8Dot3Enabled(rootTestFolder)) {
                    return GeneralFunctions.TestString("GetOpenWith2", WalkmanLib.GetOpenWith(testFile), Path.Combine(rootTestFolder, "TESTOP~1.BAT"));
                } else {
                    return GeneralFunctions.TestString("GetOpenWith2", WalkmanLib.GetOpenWith(testFile), testFile);
                }
            }
        }

        public static bool Test_GetOpenWith3(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith3.cmd"))) {
                if (Is8Dot3Enabled(rootTestFolder)) {
                    return GeneralFunctions.TestString("GetOpenWith3", WalkmanLib.GetOpenWith(testFile), Path.Combine(rootTestFolder, "TESTOP~1.CMD"));
                } else {
                    return GeneralFunctions.TestString("GetOpenWith3", WalkmanLib.GetOpenWith(testFile), testFile);
                }
            }
        }

        public static bool Test_GetOpenWith4(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith4.randomextension"))) {
                return GeneralFunctions.TestString("GetOpenWith4", WalkmanLib.GetOpenWith(testFile), string.Empty);
            }
        }

        public static bool Test_GetOpenWith5(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith5.txt"))) {
                return GeneralFunctions.TestString("GetOpenWith5", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                                   Path.Combine(Environment.SystemDirectory, "notepad.exe").ToLower());
            }
        }

        public static bool Test_GetOpenWith6(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith6.url"))) {
                return GeneralFunctions.TestString("GetOpenWith6", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                                   Path.Combine(Environment.SystemDirectory, "ieframe.dll").ToLower());
            }
        }

        public static bool Test_GetOpenWith7(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith7.vbs"))) {
                return GeneralFunctions.TestString("GetOpenWith7", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                                   Path.Combine(Environment.SystemDirectory, "wscript.exe").ToLower());
            }
        }

        public static bool Test_GetOpenWith8(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith8.cat"))) {
                return GeneralFunctions.TestString("GetOpenWith8", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                                   Path.Combine(Environment.SystemDirectory, "cryptext.dll").ToLower());
            }
        }

        public static bool Test_GetOpenWith9(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "testOpenWith9.cer"))) {
                return GeneralFunctions.TestString("GetOpenWith9", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                                   Path.Combine(Environment.SystemDirectory, "cryptext.dll").ToLower());
            }
        }

        public static bool Test_GetOpenWith10(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetOpenWith(Path.Combine(rootTestFolder, "testOpenWith10.txt"));
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("GetOpenWith10", ex.GetType(), typeof(FileNotFoundException));
        }
    }
}
