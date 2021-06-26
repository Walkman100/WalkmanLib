using System.IO;

namespace Tests {
    static class Tests_IsFileOrDirectory {
        public static bool Test_IsFileOrDirectory1(string rootTestFolder) {
            using (var testDir = new DisposableDirectory(Path.Combine(rootTestFolder, "isFileOrDirectory1"))) {
                return GeneralFunctions.TestNumber("IsFileOrDirectory1", (int)WalkmanLib.IsFileOrDirectory(testDir), (int)(PathEnum.Exists | PathEnum.IsDirectory));
            }
        }

        public static bool Test_IsFileOrDirectory2(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "isFileOrDirectory2.txt"))) {
                return GeneralFunctions.TestNumber("IsFileOrDirectory2", (int)WalkmanLib.IsFileOrDirectory(testFile), (int)(PathEnum.Exists | PathEnum.IsFile));
            }
        }

        public static bool Test_IsFileOrDirectory3() {
            return GeneralFunctions.TestNumber("IsFileOrDirectory3", (int)WalkmanLib.IsFileOrDirectory(@"C:\"), (int)(PathEnum.Exists | PathEnum.IsDirectory | PathEnum.IsDrive));
        }

        public static bool Test_IsFileOrDirectory4() {
            return GeneralFunctions.TestNumber("IsFileOrDirectory4", (int)WalkmanLib.IsFileOrDirectory(@"C:\nonexistantpath"), (int)PathEnum.NotFound);
        }

        public static bool Test_IsFileOrDirectory5() {
            return GeneralFunctions.TestNumber("IsFileOrDirectory5", (int)WalkmanLib.IsFileOrDirectory(@"test:test\test"), (int)PathEnum.NotFound);
        }

        public static bool Test_IsFileOrDirectory6() {
            return GeneralFunctions.TestNumber("IsFileOrDirectory6", (int)WalkmanLib.IsFileOrDirectory(@"~!@#$%^&*(){}[]/?=+-_\|"), (int)PathEnum.NotFound);
        }

        public static bool Test_IsFileOrDirectory7() {
            return GeneralFunctions.TestNumber("IsFileOrDirectory7", (int)WalkmanLib.IsFileOrDirectory(@"M:\"), (int)(PathEnum.Exists | PathEnum.IsDrive));
        }
    }
}
