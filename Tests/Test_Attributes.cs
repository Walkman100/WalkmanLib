using System;
using System.IO;
using System.Threading;

namespace Tests {
    static class Tests_Attributes {
        private static FileAttributes TestGetAttributes(string path) {
            // have to clear Compressed attribute, as tests assume it isn't set, and decompressing files involves P/Invoke
            FileAttributes fAttr = File.GetAttributes(path);
            if (fAttr == FileAttributes.Compressed) {
                return FileAttributes.Normal;
            } else {
                return fAttr & ~FileAttributes.Compressed;
            }
        }

        public static bool Test_Attributes1(string rootTestFolder) {
            bool returnVal = true;
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest1.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                if (!GeneralFunctions.TestNumber("Attributes1.1", (int)TestGetAttributes(testFile), (int)FileAttributes.Normal))
                    returnVal = false;

                WalkmanLib.SetAttribute(testFile, FileAttributes.Hidden);
                if (!GeneralFunctions.TestNumber("Attributes1.2", (int)TestGetAttributes(testFile), (int)FileAttributes.Hidden))
                    returnVal = false;

                WalkmanLib.SetAttribute(testFile, TestGetAttributes(testFile) | FileAttributes.System);
                if (!GeneralFunctions.TestNumber("Attributes1.3", (int)TestGetAttributes(testFile), (int)(FileAttributes.Hidden | FileAttributes.System)))
                    returnVal = false;

                return returnVal;
            }
        }

        public static bool Test_Attributes2(string rootTestFolder) {
            bool returnVal = true;
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest2.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.Archive);

                WalkmanLib.AddAttribute(testFile, FileAttributes.Normal);
                if (!GeneralFunctions.TestNumber("Attributes2.1", (int)TestGetAttributes(testFile), (int)FileAttributes.Archive))
                    returnVal = false;

                WalkmanLib.AddAttribute(testFile, FileAttributes.Hidden);
                if (!GeneralFunctions.TestNumber("Attributes2.2", (int)TestGetAttributes(testFile), (int)(FileAttributes.Archive | FileAttributes.Hidden)))
                    returnVal = false;

                WalkmanLib.AddAttribute(testFile, FileAttributes.System);
                if (!GeneralFunctions.TestNumber("Attributes2.3", (int)TestGetAttributes(testFile), (int)(FileAttributes.Archive | FileAttributes.Hidden | FileAttributes.System)))
                    returnVal = false;

                return returnVal;
            }
        }

        public static bool Test_Attributes3(string rootTestFolder) {
            bool returnVal = true;

            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest3.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal | FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System);

                WalkmanLib.RemoveAttribute(testFile, FileAttributes.Normal);
                if (!GeneralFunctions.TestNumber("Attributes3.1", (int)TestGetAttributes(testFile), (int)(FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System)))
                    returnVal = false;

                WalkmanLib.RemoveAttribute(testFile, FileAttributes.Hidden);
                if (!GeneralFunctions.TestNumber("Attributes3.2", (int)TestGetAttributes(testFile), (int)(FileAttributes.ReadOnly | FileAttributes.System)))
                    returnVal = false;

                WalkmanLib.RemoveAttribute(testFile, FileAttributes.ReadOnly | FileAttributes.System);
                if (!GeneralFunctions.TestNumber("Attributes3.3", (int)TestGetAttributes(testFile), (int)FileAttributes.Normal))
                    returnVal = false;

                return returnVal;
            }
        }

        public static bool Test_Attributes4(string rootTestFolder) {
            bool returnVal = true;
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest4.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.System | FileAttributes.Hidden);

                WalkmanLib.ChangeAttribute(testFile, FileAttributes.Hidden, false);
                if (!GeneralFunctions.TestNumber("Attributes4.1", (int)TestGetAttributes(testFile), (int)FileAttributes.System))
                    returnVal = false;

                WalkmanLib.ChangeAttribute(testFile, FileAttributes.Hidden, true);
                if (!GeneralFunctions.TestNumber("Attributes4.2", (int)TestGetAttributes(testFile), (int)(FileAttributes.System | FileAttributes.Hidden)))
                    returnVal = false;

                WalkmanLib.ChangeAttribute(testFile, FileAttributes.Archive, true);
                if (!GeneralFunctions.TestNumber("Attributes4.3", (int)TestGetAttributes(testFile), (int)(FileAttributes.System | FileAttributes.Hidden | FileAttributes.Archive)))
                    returnVal = false;

                return returnVal;
            }
        }

        public static bool Test_Attributes5() {
            bool returnVal = true;

            string testPath = @"C:\Windows";

            if (!GeneralFunctions.TestBoolean("Attributes5.1", WalkmanLib.SetAttribute(testPath, FileAttributes.Hidden, Test_Attributes5_delegate), false))
                returnVal = false;
            if (!GeneralFunctions.TestBoolean("Attributes5.2", WalkmanLib.AddAttribute(testPath, FileAttributes.Hidden, Test_Attributes5_delegate), false))
                returnVal = false;
            if (!GeneralFunctions.TestBoolean("Attributes5.3", WalkmanLib.RemoveAttribute(testPath, FileAttributes.Hidden, Test_Attributes5_delegate), false))
                returnVal = false;
            if (!GeneralFunctions.TestBoolean("Attributes5.4", WalkmanLib.ChangeAttribute(testPath, FileAttributes.Hidden, true, Test_Attributes5_delegate), false))
                returnVal = false;
            return returnVal;
        }
        public static void Test_Attributes5_delegate(Exception ex) { }

        public static bool Test_Attributes6() {
            string testPath = @"C:\Windows";

            delegateHasBeenCalled = false;
            delegateEx = null;

            WalkmanLib.SetAttribute(testPath, FileAttributes.Hidden, Test_Attributes6_delegate);

            int count = 0;
            while (delegateHasBeenCalled != true) {
                Thread.Sleep(10);

                count += 1;
                if (count > 1000) {
                    break;
                }
            }

            return GeneralFunctions.TestType("Attributes6", delegateEx.GetType(), typeof(UnauthorizedAccessException));
        }

        private static bool delegateHasBeenCalled;
        private static Exception delegateEx;
        public static void Test_Attributes6_delegate(Exception ex) {
            delegateEx = ex;
            delegateHasBeenCalled = true;
        }
    }
}
