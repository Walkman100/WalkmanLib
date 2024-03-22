using System;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests {
    static class Tests_Icons {
        private static void ExtractResXIcon(string resXpath, string resourceName, string outputPath) {
            using (var resXset = new ResXResourceSet(resXpath)) {
                object resXobject = resXset.GetObject(resourceName);
                if (!(resXobject is Icon)) {
                    throw new InvalidDataException("ResX Object was not of type Icon. Got type: " + resXobject.GetType().FullName);
                }
                var resXicon = (Icon)resXobject;

                using (var fs = new FileStream(outputPath, FileMode.Create)) {
                    resXicon.Save(fs);
                }
            }
        }

        private static string Sha1File(string filePath) {
            using (var fs = new FileStream(filePath, FileMode.Open))
            using (var sha1 = SHA1.Create()) {
                byte[] hash = sha1.ComputeHash(fs);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        public static bool Test_ExtractIcon1(string rootTestFolder, string projectRoot) {
            string resXlocation = Path.Combine(projectRoot, "CustomMsgBox.resx");
            if (!File.Exists(resXlocation)) {
                return GeneralFunctions.TestString("ExtractIcon1", "CustomMsgBox.resx doesn't exist", "CustomMsgBox.resx exists");
            }

            using (var extractedIcon = new DisposableFile(Path.Combine(rootTestFolder, "extractIcon1.ico"), false, false)) {
                try {
                    ExtractResXIcon(resXlocation, "Win7_Information", extractedIcon);
                } catch (Exception ex) {
                    return GeneralFunctions.TestType("ExtractIcon1", ex.GetType(), typeof(NoException));
                }

                return GeneralFunctions.TestString("ExtractIcon1", Sha1File(extractedIcon), "f61833aa0ecdcecc32911c2346fe93821683acff");
            }
        }

        public static bool Test_ExtractIcon2(string rootTestFolder, string projectRoot) {
            string resXlocation = Path.Combine(projectRoot, "CustomMsgBox.resx");
            if (!File.Exists(resXlocation)) {
                return GeneralFunctions.TestString("ExtractIcon2", "CustomMsgBox.resx doesn't exist", "CustomMsgBox.resx exists");
            }

            using (var resXextractedIcon = new DisposableFile(Path.Combine(rootTestFolder, "extractIcon2.ico"), false, false)) {
                try {
                    ExtractResXIcon(resXlocation, "Win7_Information", resXextractedIcon);
                } catch (Exception ex) {
                    return GeneralFunctions.TestType("ExtractIcon2", ex.GetType(), typeof(NoException));
                }

                Icon extractedIcon;
                try {
                    extractedIcon = WalkmanLib.ExtractIconByIndex(resXextractedIcon, 0, 256);
                } catch (Exception ex) {
                    return GeneralFunctions.TestType("ExtractIcon2", ex.GetType(), typeof(NoException));
                }

                using (var extractedIconByIndex = new DisposableFile(Path.Combine(rootTestFolder, "extractIcon2ByIndex.png"), false, false)) {
                    extractedIcon.ToBitmap().Save(extractedIconByIndex);

                    return GeneralFunctions.TestString("ExtractIcon2", Sha1File(extractedIconByIndex), "bb1495a09780ca0cda8abd8957fe99fbe724a4d7");
                }
            }
        }

        public static bool Test_ExtractIcon3(string rootTestFolder) {
            Icon extractedIcon;
            try {
                extractedIcon = WalkmanLib.ExtractIconByIndex(Path.Combine(Environment.SystemDirectory, "shell32.dll"), 32);
            } catch (Exception ex) {
                return GeneralFunctions.TestType("ExtractIcon3", ex.GetType(), typeof(NoException));
            }

            using (var targetIcon = new DisposableFile(Path.Combine(rootTestFolder, "extractIcon3.png"), false, false)) {
                extractedIcon.ToBitmap().Save(targetIcon);

                return GeneralFunctions.TestString("ExtractIcon3", Sha1File(targetIcon), "b1419f59aea45aa1b1aee38d3d77cdaae9ff3916");
            }
        }

        public static bool Test_PickIconDialog1() {
            string filePath = Path.Combine(Environment.SystemDirectory, "shell32.dll");
            int iconIndex = 32;

            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ESC}");
            });

            bool result = WalkmanLib.PickIconDialogShow(ref filePath, ref iconIndex);

            return GeneralFunctions.TestBoolean("PickIconDialog1", result, false);
        }

        public static bool Test_PickIconDialog2() {
            string filePath = Path.Combine(Environment.SystemDirectory, "shell32.dll");
            int iconIndex = 32;

            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{ENTER}");
            });
            bool result = WalkmanLib.PickIconDialogShow(ref filePath, ref iconIndex);

            if (!result) {
                return GeneralFunctions.TestBoolean("PickIconDialog2", result, true);
            }

            return GeneralFunctions.TestString("PickIconDialog2", filePath, Path.Combine(Environment.SystemDirectory, "shell32.dll"));
        }

        public static bool Test_PickIconDialog3() {
            string filePath = Path.Combine(Environment.SystemDirectory, "shell32.dll");
            int iconIndex = 32;

            Task.Run(() => {
                Thread.Sleep(600);
                SendKeys.SendWait("{TAB 2}{DOWN}{ENTER}");
            });
            bool result = WalkmanLib.PickIconDialogShow(ref filePath, ref iconIndex);

            if (!result) {
                return GeneralFunctions.TestBoolean("PickIconDialog3", result, true);
            }

            return GeneralFunctions.TestNumber("PickIconDialog3", iconIndex, 33);
        }
    }
}
