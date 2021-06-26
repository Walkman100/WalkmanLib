using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests {
    static class Tests_Hardlinks {
        public static bool Test_Hardlinks1(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks1Source.txt"))) {
                File.WriteAllText(testFileSource, "testText");
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks1.txt");
                Environment.CurrentDirectory = rootTestFolder;

                WalkmanLib.CreateHardLink(hardlinkPath, "hardlinks1Source.txt");

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    return GeneralFunctions.TestString("Hardlinks1", File.ReadAllText(hardlinkPath), "testText");
                }
            }
        }

        public static bool Test_Hardlinks2(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks2Source.txt"))) {
                File.WriteAllText(testFileSource, "testText");
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks2.txt");

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource);

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    return GeneralFunctions.TestString("Hardlinks2", File.ReadAllText(hardlinkPath), "testText");
                }
            }
        }

        public static bool Test_Hardlinks3(string rootTestFolder) {
            using (var testDirRoot = new DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks3Root")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirRoot, "hardlinks3Source.txt"))) {

                File.WriteAllText(testFileSource, "testText");
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks3.txt");
                string hardlinkTarget = Path.Combine(Path.GetFileName(testDirRoot), Path.GetFileName(testFileSource));
                Environment.CurrentDirectory = rootTestFolder;

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget);

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    return GeneralFunctions.TestString("Hardlinks3", File.ReadAllText(hardlinkPath), "testText");
                }
            }
        }

        public static bool Test_Hardlinks4(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks4Source.txt")))
            using (var testDirTarget = new DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks4Target"))) {

                File.WriteAllText(testFileSource, "testText");
                string hardlinkPath = Path.Combine(testDirTarget, "hardlinks4.txt");
                string hardlinkTarget = Path.Combine("..", Path.GetFileName(testFileSource));
                Environment.CurrentDirectory = testDirTarget;

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget);

                Environment.CurrentDirectory = rootTestFolder; // allow testDirTarget to be deleted
                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    return GeneralFunctions.TestString("Hardlinks4", File.ReadAllText(hardlinkPath), "testText");
                }
            }
        }

        public static bool Test_Hardlinks5(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks5Source")))
            using (var testFileSource = new DisposableFile(Path.Combine(testDirSource, "hardlinks5Source.txt")))
            using (var testDirTarget = new DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks5Target"))) {

                File.WriteAllText(testFileSource, "testText");
                string hardlinkPath = Path.Combine(testDirTarget, "hardlinks5.txt");
                string hardlinkTarget = Path.Combine("..", Path.GetFileName(testDirSource), Path.GetFileName(testFileSource));
                Environment.CurrentDirectory = testDirTarget;

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget);

                Environment.CurrentDirectory = rootTestFolder; // allow testDirTarget to be deleted
                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    return GeneralFunctions.TestString("Hardlinks5", File.ReadAllText(hardlinkPath), "testText");
                }
            }
        }

        public static bool Test_HardlinkThrows1(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.CreateHardLink(Path.Combine(rootTestFolder, "hardlinkThrows1.txt"), "nonExistantFile.txt");
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows1", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_HardlinkThrows2(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows2Source.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateHardLink(Path.Combine(rootTestFolder, "nonExistantFolder", "hardlinkThrows2.txt"), testFile);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("HardlinkThrows2", ex.GetType(), typeof(DirectoryNotFoundException));
            }
        }

        public static bool Test_HardlinkThrows3(string rootTestFolder) {
            using (var testSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows3Source.txt")))
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows3.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateHardLink(testFile, testSource);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("HardlinkThrows3", ex.GetType(), typeof(IOException));
            }
        }

        public static bool Test_HardlinkThrows4(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows4Source.txt"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateHardLink(Path.Combine(Environment.SystemDirectory, "symlinkThrows3.txt"), testFile);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("HardlinkThrows4", ex.GetType(), typeof(UnauthorizedAccessException));
            }
        }

        public static bool Test_Hardlink6(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks6Source.txt"))) {
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks6.txt");

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource);

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    return GeneralFunctions.TestNumber("Hardlinks6", WalkmanLib.GetHardlinkCount(hardlinkPath), 2);
                }
            }
        }

        public static bool Test_Hardlink7(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks7Source.txt"))) {
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks7.txt");

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource);

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    return GeneralFunctions.TestNumber("Hardlinks7", WalkmanLib.GetHardlinkCount(testFileSource), 2);
                }
            }
        }

        public static bool Test_Hardlink8(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks8Source.txt"))) {
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks8.txt");

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource);

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    List<string> lstLinks = WalkmanLib.GetHardlinkLinks(hardlinkPath).ToList();

                    return GeneralFunctions.TestNumber("Hardlinks8", lstLinks.Count, 2);
                }
            }
        }

        public static bool Test_Hardlink9(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks9Source.txt"))) {
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks9.txt");

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource);

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    List<string> lstLinks = WalkmanLib.GetHardlinkLinks(hardlinkPath).ToList();
                    string linkPath = lstLinks.First((string x) => x.EndsWith("Source.txt"));

                    return GeneralFunctions.TestString("Hardlinks9", linkPath, testFileSource);
                }
            }
        }

        public static bool Test_Hardlink10(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "hardlinks10Source.txt"))) {
                string hardlinkPath = Path.Combine(rootTestFolder, "hardlinks10.txt");

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource);

                using (var testHardlink = new DisposableFile(hardlinkPath, false)) {
                    List<string> lstLinks = WalkmanLib.GetHardlinkLinks(testFileSource).ToList();
                    string linkPath = lstLinks.First((string x) => x.EndsWith("Source.txt"));

                    return GeneralFunctions.TestString("Hardlinks10", linkPath, testFileSource);
                }
            }
        }

        public static bool Test_HardlinkThrows5(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkCount(Path.Combine(rootTestFolder, "nonExistantFile.txt"));
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows5", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_HardlinkThrows6(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkCount(Path.Combine(rootTestFolder, "nonExistantFolder", "nonExistantFile.txt"));
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows6", ex.GetType(), typeof(DirectoryNotFoundException));
        }

        public static bool Test_HardlinkThrows7() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkCount(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "Temp", "MpCmdRun.log"));
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows7", ex.GetType(), typeof(UnauthorizedAccessException));
        }

        public static bool Test_HardlinkThrows8() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkCount(Path.Combine(Environment.GetEnvironmentVariable("SystemDrive") + Path.DirectorySeparatorChar, "pagefile.sys"));
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows8", ex.GetType(), typeof(IOException));
        }

        public static bool Test_HardlinkThrows9(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkLinks(Path.Combine(rootTestFolder, "nonExistantFile.txt")).ToList();
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows9", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_HardlinkThrows10(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkLinks(Path.Combine(rootTestFolder, "nonExistantFolder", "nonExistantFile.txt")).ToList();
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows10", ex.GetType(), typeof(DirectoryNotFoundException));
        }

        public static bool Test_HardlinkThrows11() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkLinks(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "Temp", "MpCmdRun.log")).ToList();
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows11", ex.GetType(), typeof(UnauthorizedAccessException));
        }

        public static bool Test_HardlinkThrows12() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetHardlinkLinks(Path.Combine(Environment.GetEnvironmentVariable("SystemDrive") + Path.DirectorySeparatorChar, "pagefile.sys")).ToList();
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("HardlinkThrows12", ex.GetType(), typeof(IOException));
        }
    }
}
