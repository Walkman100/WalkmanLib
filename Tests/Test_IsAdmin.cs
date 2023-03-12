using System;
using System.IO;

namespace Tests {
    static class Tests_IsAdmin {
        public static bool Test_IsAdmin1() {
            return GeneralFunctions.TestBoolean("IsAdmin1", WalkmanLib.IsAdmin(), false);
        }

        public static bool Test_IsAdmin2(string rootTestFolder) {
            string programPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            programPath = new Uri(programPath).LocalPath;
            string tmpOutPath = Path.Combine(rootTestFolder, "outTmp.txt");

            bool result = WalkmanLib.RunAsAdmin("cmd.exe", "/c \"" + programPath + "\" getAdmin > " + tmpOutPath);
            if (!result)
                return GeneralFunctions.TestString("IsAdmin2", "Admin Prompt Denied", "Admin Prompt Accepted");

            string runAsAdminOutput;
            try {
                runAsAdminOutput = File.ReadAllText(tmpOutPath);
            } catch (Exception ex) {
                runAsAdminOutput = "Error: " + ex.Message;
            }
            GeneralFunctions.DeleteFileIfExists(tmpOutPath);

            return GeneralFunctions.TestString("IsAdmin2", runAsAdminOutput, "True" + Environment.NewLine);
        }
    }
}
