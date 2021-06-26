using System;
using System.IO;

namespace Tests {
    static class Tests_GetFolderIconPath {
        public static bool Test_GetFolderIconPath1(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath1"))) {
                File.WriteAllLines(Path.Combine(testDir, "desktop.ini"), new[] { 
                    "[.ShellClassInfo]", 
                    "IconResource=testIconPath,23" 
                });

                return GeneralFunctions.TestString("GetFolderIconPath1", WalkmanLib.GetFolderIconPath(testDir), testDir.dirPath + Path.DirectorySeparatorChar + "testIconPath,23");
            }
        }

        public static bool Test_GetFolderIconPath2(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath2"))) {
                File.WriteAllLines(Path.Combine(testDir, "desktop.ini"), new[] { 
                    "[.ShellClassInfo]", 
                    "IconFile=testIconPath", 
                    "IconIndex=23" 
                });

                return GeneralFunctions.TestString("GetFolderIconPath2", WalkmanLib.GetFolderIconPath(testDir), testDir.dirPath + Path.DirectorySeparatorChar + "testIconPath,23");
            }
        }

        public static bool Test_GetFolderIconPath3(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath3"))) {
                File.WriteAllLines(Path.Combine(testDir, "desktop.ini"), new[] { 
                    "[.ShellClassInfo]", 
                    @"IconResource=D:\test\testIconPath,23" 
                });

                return GeneralFunctions.TestString("GetFolderIconPath3", WalkmanLib.GetFolderIconPath(testDir), @"D:\test\testIconPath,23");
            }
        }

        public static bool Test_GetFolderIconPath4(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath4"))) {
                File.WriteAllLines(Path.Combine(testDir, "desktop.ini"), new[] {
                    "[.ShellClassInfo]",
                    @"IconResource=%SystemRoot%\system32\imageres.dll,-184"
                });

                return GeneralFunctions.TestString("GetFolderIconPath4", WalkmanLib.GetFolderIconPath(testDir), Environment.GetEnvironmentVariable("SystemRoot") + @"\system32\imageres.dll,-184");
            }
        }

        public static bool Test_GetFolderIconPath5(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath5"))) {
                File.WriteAllLines(Path.Combine(testDir, "desktop.ini"), new[] { 
                    "[.ShellClassInfo]" 
                });

                return GeneralFunctions.TestString("GetFolderIconPath5", WalkmanLib.GetFolderIconPath(testDir), "no icon found");
            }
        }
    }
}
