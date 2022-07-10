using System;
using System.IO;

namespace Tests {
    static class Tests_Junctions {
        public static bool Test_Junctions1(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "junctions1Source"))) {
                string junctionPath = Path.Combine(rootTestFolder, "junctions1");

                string mklinkOutput = WalkmanLib.RunAndGetOutput("cmd", arguments: "/c mklink /J " + junctionPath + " junctions1Source", workingDirectory: rootTestFolder, mergeStdErr: true).StandardOutput;

                using (new DisposableDirectory(junctionPath, false)) {
                    return GeneralFunctions.TestString("Junctions1", WalkmanLib.GetSymlinkTarget(junctionPath), testDirSource);
                }
            }
        }

        public static bool Test_Junctions2(string rootTestFolder) {
            using (var testDirSource = new DisposableDirectory(Path.Combine(rootTestFolder, "junctions2Source"))) {
                string junctionPath = Path.Combine(rootTestFolder, "junctions2");

                WalkmanLib.CreateJunction(junctionPath, testDirSource);
 
                using (new DisposableDirectory(junctionPath, false)) {
                    return GeneralFunctions.TestString("Junctions2", WalkmanLib.GetSymlinkTarget(junctionPath), testDirSource);
                }
            }
        }

        public static bool Test_Junctions3(string rootTestFolder) {
            string testDirSource = Path.GetPathRoot(rootTestFolder);
            string junctionPath = Path.Combine(rootTestFolder, "junctions3");

            WalkmanLib.CreateJunction(junctionPath, testDirSource);

            using (new DisposableDirectory(junctionPath, false)) {
                return GeneralFunctions.TestString("Junctions3", WalkmanLib.GetSymlinkTarget(junctionPath), testDirSource);
            }
        }

        public static bool Test_Junctions4(string rootTestFolder) {
            string testDirSource = Path.Combine("..", Path.GetFileName(rootTestFolder));
            string junctionPath = Path.Combine(rootTestFolder, "junctions4");
            Environment.CurrentDirectory = rootTestFolder;

            WalkmanLib.CreateJunction(junctionPath, testDirSource);

            using (new DisposableDirectory(junctionPath, false)) {
                return GeneralFunctions.TestString("Junctions4", WalkmanLib.GetSymlinkTarget(junctionPath), rootTestFolder);
            }
        }

        public static bool Test_Junctions5(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "junctions5Source")))
            using (var parentDir = new DisposableDirectory(Path.Combine(rootTestFolder, "nonExistantDirectory"), false))
            using (var junctionPath = new DisposableDirectory(Path.Combine(parentDir, "junction5Target"), false)) {

                WalkmanLib.CreateJunction(junctionPath, testDir);

                return GeneralFunctions.TestString("Junctions5", WalkmanLib.GetSymlinkTarget(junctionPath), testDir);
            }
        }

        public static bool Test_JunctionThrows1(string rootTestFolder) {
            using (var junctionPath = new DisposableDirectory(Path.Combine(rootTestFolder, "junctionThrows1")))
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "junctionThrows1Source"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateJunction(junctionPath, testDir);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("JunctionThrows1", ex.GetType(), typeof(IOException));
            }
        }

        public static bool Test_JunctionThrows2(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "junctionThrows2"))) {
                Exception ex = new NoException();
                try {
                    WalkmanLib.CreateJunction(Path.Combine(Environment.SystemDirectory, "junctionThrows2"), testDir);
                } catch (Exception ex2) {
                    ex = ex2;
                }
                return GeneralFunctions.TestType("JunctionThrows2", ex.GetType(), typeof(UnauthorizedAccessException));
            }
        }
    }
}
