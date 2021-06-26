using System;
using System.Net;
using System.Threading;

namespace Tests {
    static class Tests_Updates {
        private const string projectName = "PortActions";

        public static bool Test_Updates1() {
            return GeneralFunctions.TestString("Updates1", WalkmanLib.GetLatestVersionInfo(projectName).TagName, "v1.0");
        }

        public static bool Test_Updates2() {
            return GeneralFunctions.TestString("Updates2", WalkmanLib.GetLatestVersionInfo(projectName).Title, "Version 1.0 Initial Release");
        }

        public static bool Test_Updates3() {
            string expectedBody = "Everything is functional except for the <kbd>More Info</kbd> button, which does nothing.\n";
            return GeneralFunctions.TestString("Updates3", WalkmanLib.GetLatestVersionInfo(projectName).Body, expectedBody);
        }

        public static bool Test_Updates4() {
            string expectedLink = "https://github.com/Walkman100/PortActions/releases/download/v1.0/PortActions-Installer.exe";
            return GeneralFunctions.TestString("Updates4", WalkmanLib.GetLatestDownloadLink(projectName), expectedLink);
        }

        public static bool Test_Updates5() {
            return GeneralFunctions.TestString("Updates5", WalkmanLib.GetLatestVersion(projectName).ToString(), "1.0");
        }

        public static bool Test_Updates6() {
            return GeneralFunctions.TestBoolean("Updates6", WalkmanLib.CheckIfUpdateAvailable(projectName, new Version(1, 0)), false);
        }

        public static bool Test_Updates7() {
            return GeneralFunctions.TestBoolean("Updates7", WalkmanLib.CheckIfUpdateAvailable(projectName, new Version(0, 9, 9)), true);
        }

        public static bool Test_Updates8() {
            delegateCallComplete = false;
            delegateReturn = null;

            WalkmanLib.CheckIfUpdateAvailableInBackground(projectName, new Version(0, 9, 9), UpdateCheckReturn);

            int count = 0;
            while (!delegateCallComplete) {
                Thread.Sleep(100);

                count += 1;
                if (count > 100) {
                    break;
                }
            }

            return GeneralFunctions.TestString("Updates8", delegateReturn, "Update available: True");
        }

        private static bool delegateCallComplete;
        private static string delegateReturn;
        static void UpdateCheckReturn(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if (e.Error == null) {
                delegateReturn = "Update available: " + (bool)e.Result;
            } else {
                delegateReturn = "Error checking for updates: " + e.Error.Message;
            }
            delegateCallComplete = true;
        }

        public static bool Test_UpdateThrows1() {
            Exception ex = new NoException();
            try {
                WalkmanLib.GetLatestVersionInfo("NonExistantProject");
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("UpdateThrows1", ex.GetType(), typeof(WebException));
        }

        public static bool Test_UpdateThrows2() {
            try {
                WalkmanLib.GetLatestVersionInfo("NonExistantProject");
                return GeneralFunctions.TestType("UpdateThrows2", typeof(NoException), typeof(WebException));
            } catch (WebException ex) {
                if (ex.Response is HttpWebResponse) {
                    var response = (HttpWebResponse)ex.Response;
                    // expected result. all other Return Test* are unexpected.
                    return GeneralFunctions.TestNumber("UpdateThrows2", (int)response.StatusCode, (int)HttpStatusCode.NotFound);
                } else {
                    return GeneralFunctions.TestType("UpdateThrows2", ex.Response.GetType(), typeof(HttpWebResponse));
                }
            } catch (Exception ex) {
                return GeneralFunctions.TestType("UpdateThrows2", ex.GetType(), typeof(WebException));
            }
        }

        public static bool Test_UpdateThrows3() {
            delegateCallComplete = false;
            delegateReturn = null;

            WalkmanLib.CheckIfUpdateAvailableInBackground("NonExistantProject", new Version(0, 9, 9), UpdateCheckReturn);

            int count = 0;
            while (!delegateCallComplete) {
                Thread.Sleep(100);

                count += 1;
                if (count > 100) {
                    break;
                }
            }

            return GeneralFunctions.TestString("UpdateThrows3", delegateReturn, "Error checking for updates: The remote server returned an error: (404) Not Found.");
        }
    }
}
