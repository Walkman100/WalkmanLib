using System;
using System.ComponentModel;
using System.IO;

namespace Tests {
    static class Tests_RunAndGetOutput {
        public static bool Test_RunAndGetOutput1() {
            var rtn = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo hi", mergeStdErr: true);

            return GeneralFunctions.TestString("RunAndGetOutput1", rtn.StandardOutput, "hi");
        }

        public static bool Test_RunAndGetOutput2() {
            var rtn = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo %CD%", mergeStdErr: true);

            return GeneralFunctions.TestString("RunAndGetOutput2", rtn.StandardOutput, Environment.CurrentDirectory);
        }

        public static bool Test_RunAndGetOutput3() {
            var rtn = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo %CD%", workingDirectory: Environment.GetEnvironmentVariable("WinDir"), mergeStdErr: true);

            return GeneralFunctions.TestString("RunAndGetOutput3", rtn.StandardOutput, Environment.GetEnvironmentVariable("WinDir"));
        }

        public static bool Test_RunAndGetOutput4() {
            var rtn = WalkmanLib.RunAndGetOutput("cmd.exe", arguments: "/c exit 5", mergeStdErr: true);

            return GeneralFunctions.TestNumber("RunAndGetOutput4", rtn.ExitCode, 5);
        }

        public static bool Test_RunAndGetOutput5() {
            var rtn = WalkmanLib.RunAndGetOutput("cmd.exe", arguments: "/c echo hi >&2", mergeStdErr: false);

            return GeneralFunctions.TestString("RunAndGetOutput5", rtn.StandardError, "hi");
        }

        public static bool Test_RunAndGetOutput6() {
            string mergedStdOut = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo hi >&2 & echo hi", mergeStdErr: true).StandardOutput;

            return GeneralFunctions.TestString("RunAndGetOutput6", mergedStdOut,
                "hi" + Environment.NewLine +
                "StdErr: hi" + Environment.NewLine +
                "ExitCode: 0");
        }

        public static bool Test_RunAndGetOutputThrows1() {
            try {
                WalkmanLib.RunAndGetOutput("nonExistantFile");
                return GeneralFunctions.TestType("RunAndGetOutputThrows1", typeof(NoException), typeof(Win32Exception));
            } catch (Win32Exception ex) { // expected. none of the other Test* should run.
                return GeneralFunctions.TestNumber("RunAndGetOutputThrows1", ex.NativeErrorCode, 2); // 0x2: ERROR_FILE_NOT_FOUND: The system cannot find the file specified.
            } catch (Exception ex) {
                return GeneralFunctions.TestType("RunAndGetOutputThrows1", ex.GetType(), typeof(Win32Exception));
            }
        }

        public static bool Test_RunAndGetOutputThrows2() {
            try {
                WalkmanLib.RunAndGetOutput(Environment.GetEnvironmentVariable("WinDir"));
                return GeneralFunctions.TestType("RunAndGetOutputThrows2", typeof(NoException), typeof(Win32Exception));
            } catch (Win32Exception ex) { // expected. none of the other Test* should run.
                return GeneralFunctions.TestNumber("RunAndGetOutputThrows2", ex.NativeErrorCode, 5); // 0x5: ERROR_ACCESS_DENIED: Access is denied.
            } catch (Exception ex) {
                return GeneralFunctions.TestType("RunAndGetOutputThrows2", ex.GetType(), typeof(Win32Exception));
            }
        }

        public static bool Test_RunAndGetOutputThrows3() {
            try {
                WalkmanLib.RunAndGetOutput(Path.Combine(Environment.SystemDirectory, "shell32.dll"));
                return GeneralFunctions.TestType("RunAndGetOutputThrows3", typeof(NoException), typeof(Win32Exception));
            } catch (Win32Exception ex) { // expected. none of the other Test* should run.
                return GeneralFunctions.TestNumber("RunAndGetOutputThrows3", ex.NativeErrorCode, 193); // 0xC1: ERROR_BAD_EXE_FORMAT: %1 is not a valid Win32 application.
            } catch (Exception ex) {
                return GeneralFunctions.TestType("RunAndGetOutputThrows3", ex.GetType(), typeof(Win32Exception));
            }
        }
    }
}
