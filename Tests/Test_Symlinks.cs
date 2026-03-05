using System;
using System.IO;

namespace Tests {
    static class Tests_Symlinks {
        public static bool Test_Symlinks1_Final(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinks1fSource"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks1f");

                string mklinkOutput = WalkmanLib.RunAndGetOutput("cmd", arguments: "/c mklink " + symlinkPath + " symlinks1fSource", workingDirectory: rootTestFolder, mergeStdErr: true).StandardOutput;

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks1_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource);
            }
        }
        public static bool Test_Symlinks1_Target(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinks1tSource"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks1t");

                string mklinkOutput = WalkmanLib.RunAndGetOutput("cmd", arguments: "/c mklink " + symlinkPath + " symlinks1tSource", workingDirectory: rootTestFolder, mergeStdErr: true).StandardOutput;

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks1_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), "symlinks1tSource");
            }
        }

        public static bool Test_Symlinks2_Final() {
            string symlinkPath = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Documents");
            return GeneralFunctions.TestString("Symlinks2_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), "Documents"));
        }
        public static bool Test_Symlinks2_Target() {
            string symlinkPath = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Documents");
            return GeneralFunctions.TestString("Symlinks2_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), "Documents"));
        }

        public static bool Test_Symlinks3_Final(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinks3fSource.txt"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks3f.txt");

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3fSource.txt", false);

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks3_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource);
            }
        }
        public static bool Test_Symlinks3_Target(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinks3tSource.txt"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks3t.txt");

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3tSource.txt", false);

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks3_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), "symlinks3tSource.txt");
            }
        }

        public static bool Test_Symlinks4_Final(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks4fSource"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks4f");

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks4fSource", true);

                using (new DisposableDirectory(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks4_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testDirSource);
            }
        }
        public static bool Test_Symlinks4_Target(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks4tSource"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks4t");

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks4tSource", true);

                using (new DisposableDirectory(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks4_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), "symlinks4tSource");
            }
        }

        public static bool Test_Symlinks5_Final(string rootTestFolder) {
            string testDirSource = Path.GetPathRoot(rootTestFolder);
            string symlinkPath = Path.Combine(rootTestFolder, "symlinks5f");

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, true);

            using (new DisposableDirectory(symlinkPath, false))
                return GeneralFunctions.TestString("Symlinks5_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testDirSource);
        }
        public static bool Test_Symlinks5_Target(string rootTestFolder) {
            string testDirSource = Path.GetPathRoot(rootTestFolder);
            string symlinkPath = Path.Combine(rootTestFolder, "symlinks5t");

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, true);

            using (new DisposableDirectory(symlinkPath, false))
                return GeneralFunctions.TestString("Symlinks5_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource);
        }

        public static bool Test_Symlinks6_Final(string rootTestFolder) {
            string testDirSource = Path.Combine("..", Path.GetFileName(rootTestFolder));
            string symlinkPath = Path.Combine(rootTestFolder, "symlinks6f");

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, true);

            using (new DisposableDirectory(symlinkPath, false))
                return GeneralFunctions.TestString("Symlinks6_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), rootTestFolder);
        }
        public static bool Test_Symlinks6_Target(string rootTestFolder) {
            string testDirSource = Path.Combine("..", Path.GetFileName(rootTestFolder));
            string symlinkPath = Path.Combine(rootTestFolder, "symlinks6t");

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, true);

            using (new DisposableDirectory(symlinkPath, false))
                return GeneralFunctions.TestString("Symlinks6_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource);
        }

        public static bool Test_Symlinks7_Final(string rootTestFolder) {
            using (var testDirRoot = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7fRoot")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirRoot, "symlinks7fSource.txt"))) {

                string symlinkPath = Path.Combine(rootTestFolder, "symlinks7f.txt");
                string symlinkTarget = Path.Combine(Path.GetFileName(testDirRoot), Path.GetFileName(testFileSource));

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, false);

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks7_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource);
            }
        }
        public static bool Test_Symlinks7_Target(string rootTestFolder) {
            using (var testDirRoot = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7tRoot")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirRoot, "symlinks7tSource.txt"))) {

                string symlinkPath = Path.Combine(rootTestFolder, "symlinks7t.txt");
                string symlinkTarget = Path.Combine(Path.GetFileName(testDirRoot), Path.GetFileName(testFileSource));

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, false);

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks7_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), symlinkTarget);
            }
        }

        public static bool Test_Symlinks8_Final(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8fSource")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirSource, "symlinks8fSource.txt")))
            using (var testDirTarget = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8fTarget"))) {

                string symlinkPath = Path.Combine(testDirTarget, "symlinks8f.txt");
                string symlinkTarget = Path.Combine("..", Path.GetFileName(testDirSource), Path.GetFileName(testFileSource));

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, false);

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks8_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource);
            }
        }
        public static bool Test_Symlinks8_Target(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8tSource")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirSource, "symlinks8tSource.txt")))
            using (var testDirTarget = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8tTarget"))) {

                string symlinkPath = Path.Combine(testDirTarget, "symlinks8t.txt");
                string symlinkTarget = Path.Combine("..", Path.GetFileName(testDirSource), Path.GetFileName(testFileSource));

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, false);

                using (new DisposableFile(symlinkPath, false))
                    return GeneralFunctions.TestString("Symlinks8_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), symlinkTarget);
            }
        }

        public static bool Test_Symlinks9() {
            string testPath = Environment.GetEnvironmentVariable("WinDir").ToLower();
            return GeneralFunctions.TestString("Symlinks9", WalkmanLib.GetSymlinkFinalPath(testPath).ToLower(), testPath);
        }

        public static bool Test_Symlinks10() {
            string testPath = Path.Combine(Environment.SystemDirectory, "shell32.dll").ToLower();
            return GeneralFunctions.TestString("Symlinks10", WalkmanLib.GetSymlinkFinalPath(testPath).ToLower(), testPath);
        }

        public static bool Test_Symlinks11(string rootTestFolder) {
            string symlinkPath = Path.Combine(rootTestFolder, "symlinks11.txt");
            string symlinkTarget = Path.Combine(rootTestFolder, "symlinks11Source.txt");
            using (var testFileSource = new DisposableFile(symlinkTarget)) {
                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, false);
            }

            using (new DisposableFile(symlinkPath, false))
                return GeneralFunctions.TestString("Symlinks11", WalkmanLib.GetSymlinkTarget(symlinkPath), symlinkTarget);
        }

        public static bool Test_SymlinkThrows1(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows1Source.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateSymLink(Path.Combine(rootTestFolder, "nonExistantFolder", "symlinkThrows1Source.txt"), testFile, false);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("SymlinkThrows1", ex.GetType(), typeof(DirectoryNotFoundException));
            }
        }

        public static bool Test_SymlinkThrows2(string rootTestFolder) {
            using (var symlinkPath = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows2.txt")))
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows2Source.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateSymLink(symlinkPath, testFile, false);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("SymlinkThrows2", ex.GetType(), typeof(IOException));
            }
        }

        public static bool Test_SymlinkThrows3(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows3.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateSymLink(Path.Combine(Environment.SystemDirectory, "symlinkThrows3.txt"), testFile, false);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("SymlinkThrows3", ex.GetType(), typeof(UnauthorizedAccessException));
            }
        }

        public static bool Test_SymlinkThrows4() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetSymlinkFinalPath("nonExistantFile");
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("SymlinkThrows4", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_SymlinkThrows5() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetSymlinkTarget("nonExistantFile");
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("SymlinkThrows5", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_SymlinkThrows6() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetSymlinkTarget(Environment.GetEnvironmentVariable("WinDir"));
            } catch (Exception ex2) {
                ex = ex2;
            }

            if (ex is not System.ComponentModel.Win32Exception)
                return GeneralFunctions.TestType("SymlinkThrows6", ex.GetType(), typeof(System.ComponentModel.Win32Exception));
            return GeneralFunctions.TestNumber("SymlinkThrows6", ((System.ComponentModel.Win32Exception)ex).NativeErrorCode, (int)WalkmanLib.NativeErrorCode.ERROR_NOT_A_REPARSE_POINT);
        }

        public static bool Test_SymlinkThrows7() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetSymlinkTarget(Path.Combine(Environment.SystemDirectory, "shell32.dll"));
            } catch (Exception ex2) {
                ex = ex2;
            }

            if (ex is not System.ComponentModel.Win32Exception)
                return GeneralFunctions.TestType("SymlinkThrows7", ex.GetType(), typeof(System.ComponentModel.Win32Exception));
            return GeneralFunctions.TestNumber("SymlinkThrows7", ((System.ComponentModel.Win32Exception)ex).NativeErrorCode, (int)WalkmanLib.NativeErrorCode.ERROR_NOT_A_REPARSE_POINT);
        }

        public static bool Test_SymlinkThrows8(string rootTestFolder) {
            string symlinkPath = Path.Combine(rootTestFolder, "symlinkThrows8.txt");
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows8Source.txt"))) {
                WalkmanLib.CreateSymLink(symlinkPath, testFileSource, false);
            }

            using (new DisposableFile(symlinkPath, false)) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.GetSymlinkFinalPath(symlinkPath);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("SymlinkThrows8", ex.GetType(), typeof(FileNotFoundException));
            }
        }
    }
}
