using System;
using System.IO;

namespace Tests {
    static class Tests_Symlinks {
        public static bool Test_Symlinks1(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinks1Source"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks1");

                string mklinkOutput = WalkmanLib.RunAndGetOutput("cmd", arguments: "/c mklink " + symlinkPath + " symlinks1Source", workingDirectory: rootTestFolder);

                using (new DisposableFile(symlinkPath, false)) {
                    return GeneralFunctions.TestString("Symlinks1", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource);
                }
            }
        }

        public static bool Test_Symlinks2() {
            string symlinkPath = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Documents");
            return GeneralFunctions.TestString("Symlinks2", WalkmanLib.GetSymlinkTarget(symlinkPath), Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), "Documents"));
        }

        public static bool Test_Symlinks3(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinks3Source.txt"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks3.txt");
                
                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3Source.txt", false);

                using (new DisposableFile(symlinkPath, false)) {
                    return GeneralFunctions.TestString("Symlinks3", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource);
                }
            }
        }

        public static bool Test_Symlinks4(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks4Source"))) {
                string symlinkPath = Path.Combine(rootTestFolder, "symlinks4");

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks4Source", true);

                using (new DisposableDirectory(symlinkPath, false)) {
                    return GeneralFunctions.TestString("Symlinks4", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource);
                }
            }
        }

        public static bool Test_Symlinks5(string rootTestFolder) {
            string testDirSource = Path.GetPathRoot(rootTestFolder);
            string symlinkPath = Path.Combine(rootTestFolder, "symlinks5");

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, true);

            using (new DisposableDirectory(symlinkPath, false)) {
                return GeneralFunctions.TestString("Symlinks5", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource);
            }
        }

        public static bool Test_Symlinks6(string rootTestFolder) {
            string testDirSource = Path.Combine("..", Path.GetFileName(rootTestFolder));
            string symlinkPath = Path.Combine(rootTestFolder, "symlinks6");

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, true);

            using (new DisposableDirectory(symlinkPath, false)) {
                return GeneralFunctions.TestString("Symlinks6", WalkmanLib.GetSymlinkTarget(symlinkPath), rootTestFolder);
            }
        }

        public static bool Test_Symlinks7(string rootTestFolder) {
            using (var testDirRoot = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7Root")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirRoot, "symlinks7Source.txt"))) {

                string symlinkPath = Path.Combine(rootTestFolder, "symlinks7.txt");
                string symlinkTarget = Path.Combine(Path.GetFileName(testDirRoot), Path.GetFileName(testFileSource));

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, false);

                using (new DisposableFile(symlinkPath, false)) {
                    return GeneralFunctions.TestString("Symlinks7", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource);
                }
            }
        }

        public static bool Test_Symlinks8(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8Source")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirSource, "symlinks8Source.txt")))
            using (var testDirTarget = new DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8Target"))) {

                string symlinkPath = Path.Combine(testDirTarget, "symlinks8.txt");
                string symlinkTarget = Path.Combine("..", Path.GetFileName(testDirSource), Path.GetFileName(testFileSource));

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, false);

                using (new DisposableFile(symlinkPath, false)) {
                    return GeneralFunctions.TestString("Symlinks8", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource);
                }
            }
        }

        public static bool Test_Symlinks9() {
            string testPath = Environment.GetEnvironmentVariable("WinDir").ToLower();
            return GeneralFunctions.TestString("Symlinks9", WalkmanLib.GetSymlinkTarget(testPath).ToLower(), testPath);
        }

        public static bool Test_Symlinks10() {
            string testPath = Path.Combine(Environment.SystemDirectory, "shell32.dll").ToLower();
            return GeneralFunctions.TestString("Symlinks10", WalkmanLib.GetSymlinkTarget(testPath).ToLower(), testPath);
        }

        public static bool Test_SymlinkThrows1() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetSymlinkTarget("nonExistantFile");
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("SymlinkThrows1", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_SymlinkThrows2(string rootTestFolder) {
            string symlinkPath = Path.Combine(rootTestFolder, "symlinkThrows2.txt");
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows2Source.txt"))) {
                WalkmanLib.CreateSymLink(symlinkPath, testFileSource, false);
            }

            if (!File.Exists(symlinkPath)) {
                return GeneralFunctions.TestString("SymlinkThrows2", "Test symlink doesn't exist", "Test symlink exists");
            }

            Exception ex = new NoException();
            try {
                WalkmanLib.GetSymlinkTarget(symlinkPath);
            } catch (Exception ex2) {
                ex = ex2;
            }
            File.Delete(symlinkPath);
            return GeneralFunctions.TestType("SymlinkThrows2", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_SymlinkThrows3(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows3Source.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateSymLink(Path.Combine(rootTestFolder, "nonExistantFolder", "symlinkThrows3Source.txt"), testFile, false);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("SymlinkThrows3", ex.GetType(), typeof(DirectoryNotFoundException));
            }
        }

        public static bool Test_SymlinkThrows4(string rootTestFolder) {
            using (var symlinkPath = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows4.txt")))
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows4Source.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateSymLink(symlinkPath, testFile, false);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("SymlinkThrows4", ex.GetType(), typeof(IOException));
            }
        }

        public static bool Test_SymlinkThrows5(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows5.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateSymLink(Path.Combine(Environment.SystemDirectory, "symlinkThrows5.txt"), testFile, false);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("SymlinkThrows5", ex.GetType(), typeof(UnauthorizedAccessException));
            }
        }
    }
}
