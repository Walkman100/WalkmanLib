using System;
using System.Collections.Generic;
using System.IO;

namespace Tests {
    static class Main {
        public static bool RunTests(string projectRoot, 
                                    string rootTestFolder = null, 
                                    bool haveGUIAccess = true, 
                                    bool runCustomMsgBoxTests = true, 
                                    bool runArgHandlerTests = true, 
                                    bool runUpdatesTests = true, 
                                    bool runWin32Tests = true, 
                                    bool runDotNetTests = true) {
            bool returnVal = true;

            if (rootTestFolder == null)
                rootTestFolder = Path.Combine(Directory.GetCurrentDirectory(), "tests");
            Directory.CreateDirectory(rootTestFolder);
            if (!GeneralFunctions.TestBoolean("CreateDirectory", Directory.Exists(rootTestFolder), true))
                returnVal = false;

            if (Directory.Exists(rootTestFolder)) {
                rootTestFolder = new DirectoryInfo(rootTestFolder).FullName;
            } else {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Test folder could not be created, skipping tests!");
                Console.ResetColor();
                return false;
            }

            if (runDotNetTests) {
                if (!Tests_GetFolderIconPath.Test_GetFolderIconPath1(rootTestFolder)) returnVal = false;
                if (!Tests_GetFolderIconPath.Test_GetFolderIconPath2(rootTestFolder)) returnVal = false;
                if (!Tests_GetFolderIconPath.Test_GetFolderIconPath3(rootTestFolder)) returnVal = false;
                if (!Tests_GetFolderIconPath.Test_GetFolderIconPath4(rootTestFolder)) returnVal = false;
                if (!Tests_GetFolderIconPath.Test_GetFolderIconPath5(rootTestFolder)) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutput1()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutput2()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutput3()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutput4()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutput5()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutput6()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutputThrows1()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutputThrows2()) returnVal = false;
                if (!Tests_RunAndGetOutput.Test_RunAndGetOutputThrows3()) returnVal = false;
                if (!Tests_IsAdmin.Test_IsAdmin1()) returnVal = false;
                if (!Tests_IsAdmin.Test_IsAdmin2(rootTestFolder)) returnVal = false;
                if (!Tests_IsFileOrDirectory.Test_IsFileOrDirectory1(rootTestFolder)) returnVal = false;
                if (!Tests_IsFileOrDirectory.Test_IsFileOrDirectory2(rootTestFolder)) returnVal = false;
                if (!Tests_IsFileOrDirectory.Test_IsFileOrDirectory3()) returnVal = false;
                if (!Tests_IsFileOrDirectory.Test_IsFileOrDirectory4()) returnVal = false;
                if (!Tests_IsFileOrDirectory.Test_IsFileOrDirectory5()) returnVal = false;
                if (!Tests_IsFileOrDirectory.Test_IsFileOrDirectory6()) returnVal = false;
                if (!Tests_IsFileOrDirectory.Test_IsFileOrDirectory7()) returnVal = false;
                if (!Tests_Attributes.Test_Attributes1(rootTestFolder)) returnVal = false;
                if (!Tests_Attributes.Test_Attributes2(rootTestFolder)) returnVal = false;
                if (!Tests_Attributes.Test_Attributes3(rootTestFolder)) returnVal = false;
                if (!Tests_Attributes.Test_Attributes4(rootTestFolder)) returnVal = false;
                if (!Tests_Attributes.Test_Attributes5()) returnVal = false;
                if (!Tests_Attributes.Test_Attributes6()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert1()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert2()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert3()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert4()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert5()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert6()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert7()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert8()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert9()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert10()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert11()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert12()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert13()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert14()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert15()) returnVal = false;
                if (!Tests_TimeConvert.Test_TimeConvert16()) returnVal = false;
                if (!Tests_StreamCopy.Test_StreamCopy1(rootTestFolder)) returnVal = false;
                if (!Tests_StreamCopy.Test_StreamCopy2(rootTestFolder)) returnVal = false;
                if (!Tests_StreamCopy.Test_StreamCopy3(rootTestFolder)) returnVal = false;
                if (!Tests_StreamCopy.Test_StreamCopyThrows1()) returnVal = false;
                if (!Tests_StreamCopy.Test_StreamCopyThrows2(rootTestFolder)) returnVal = false;
            }

            if (runWin32Tests) {
                if (!Tests_Shortcuts.Test_Shortcuts1()) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts2()) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts3()) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts4(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts5(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts6(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts7(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts8(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts9(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts10(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts11(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts12(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts13(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts14(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts15(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts16(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts17(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_Shortcuts18(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_ShortcutThrows1(rootTestFolder)) returnVal = false;
                if (!Tests_Shortcuts.Test_ShortcutThrows2()) returnVal = false;
                if (!Tests_Compression.Test_Compression1(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression2(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression3(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression4(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression5(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression6(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression7(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression8(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression9(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression10(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression11(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression12(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression13(rootTestFolder)) returnVal = false;
                if (!Tests_Compression.Test_Compression14()) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith1()) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith2(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith3(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith4(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith5(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith6(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith7(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith8(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith9(rootTestFolder)) returnVal = false;
                if (!Tests_GetOpenWith.Test_GetOpenWith10(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks1(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks2()) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks3(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks4(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks5(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks6(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks7(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks8(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks9()) returnVal = false;
                if (!Tests_Symlinks.Test_Symlinks10()) returnVal = false;
                if (!Tests_Symlinks.Test_SymlinkThrows1()) returnVal = false;
                if (!Tests_Symlinks.Test_SymlinkThrows2(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_SymlinkThrows3(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_SymlinkThrows4(rootTestFolder)) returnVal = false;
                if (!Tests_Symlinks.Test_SymlinkThrows5(rootTestFolder)) returnVal = false;
                if (!Tests_Junctions.Test_Junctions1(rootTestFolder)) returnVal = false;
                if (!Tests_Junctions.Test_Junctions2(rootTestFolder)) returnVal = false;
                if (!Tests_Junctions.Test_Junctions3(rootTestFolder)) returnVal = false;
                if (!Tests_Junctions.Test_Junctions4(rootTestFolder)) returnVal = false;
                if (!Tests_Junctions.Test_Junctions5(rootTestFolder)) returnVal = false;
                if (!Tests_Junctions.Test_JunctionThrows1(rootTestFolder)) returnVal = false;
                if (!Tests_Junctions.Test_JunctionThrows2(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlinks1(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlinks2(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlinks3(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlinks4(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlinks5(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows1(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows2(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows3(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows4(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlink6(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlink7(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlink8(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlink9(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_Hardlink10(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows5(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows6(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows7()) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows8()) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows9(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows10(rootTestFolder)) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows11()) returnVal = false;
                if (!Tests_Hardlinks.Test_HardlinkThrows12()) returnVal = false;
                if (!Tests_Icons.Test_ExtractIcon1(rootTestFolder, projectRoot)) returnVal = false;
                if (!Tests_Icons.Test_ExtractIcon2(rootTestFolder, projectRoot)) returnVal = false;
                if (!Tests_Icons.Test_ExtractIcon3(rootTestFolder)) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon1(rootTestFolder)) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon2(rootTestFolder)) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon3()) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon4()) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon5()) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon6()) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon7()) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon8()) returnVal = false;
                if (!Tests_GetFileIcon.Test_GetFileIcon9()) returnVal = false;
                if (!Tests_ContextMenu.Test_ContextMenu1(rootTestFolder)) returnVal = false;
                if (!Tests_ContextMenu.Test_ContextMenu2(rootTestFolder)) returnVal = false;
                if (!Tests_ContextMenu.Test_ContextMenu3(rootTestFolder)) returnVal = false;
                if (!Tests_ContextMenu.Test_ContextMenu4(rootTestFolder)) returnVal = false;
                if (!Tests_ContextMenu.Test_ContextMenu5(rootTestFolder)) returnVal = false;
                if (!Tests_ContextMenu.Test_ContextMenu6(rootTestFolder)) returnVal = false;

                if (haveGUIAccess) {
                    if (!Tests_ContextMenu.Test_ContextMenuUI1(rootTestFolder)) returnVal = false;
                    if (!Tests_ContextMenu.Test_ContextMenuUI2(rootTestFolder)) returnVal = false;
                    if (!Tests_ContextMenu.Test_ContextMenuUI3(rootTestFolder)) returnVal = false;
                    if (!Tests_Icons.Test_PickIconDialog1()) returnVal = false;
                    if (!Tests_Icons.Test_PickIconDialog2()) returnVal = false;
                    if (!Tests_Icons.Test_PickIconDialog3()) returnVal = false;
                    if (!Tests_ShowProperties.Test_ShowProperties1()) returnVal = false;
                    if (!Tests_ShowProperties.Test_ShowProperties2()) returnVal = false;
                    if (!Tests_ShowProperties.Test_ShowProperties3()) returnVal = false;
                    if (!Tests_ShowProperties.Test_ShowProperties4()) returnVal = false;
                    if (!Tests_WaitFor.Test_WaitForWindow1()) returnVal = false;
                    if (!Tests_WaitFor.Test_WaitForWindow2()) returnVal = false;
                    if (!Tests_Mouse.Test_Mouse1()) returnVal = false;
                    if (!Tests_Mouse.Test_Mouse2()) returnVal = false;
                } else {
                    GeneralFunctions.WriteTestSkipped(new List<string>(new[] { 
                            "ContextMenuUI1", "ContextMenuUI2", "ContextMenuUI3", 
                            "PickIconDialog1", "PickIconDialog2", "PickIconDialog3", 
                            "ShowProperties1", "ShowProperties2", "ShowProperties3", "ShowProperties4", 
                            "Mouse1", "Mouse2" 
                        }), "No Graphical Session Access");
                }
            }

            if (runUpdatesTests) {
                if (!Tests_Updates.Test_Updates1()) returnVal = false;
                if (!Tests_Updates.Test_Updates2()) returnVal = false;
                if (!Tests_Updates.Test_Updates3()) returnVal = false;
                if (!Tests_Updates.Test_Updates4()) returnVal = false;
                if (!Tests_Updates.Test_Updates5()) returnVal = false;
                if (!Tests_Updates.Test_Updates6()) returnVal = false;
                if (!Tests_Updates.Test_Updates7()) returnVal = false;
                if (!Tests_Updates.Test_Updates8()) returnVal = false;
                if (!Tests_Updates.Test_UpdateThrows1()) returnVal = false;
                if (!Tests_Updates.Test_UpdateThrows2()) returnVal = false;
                if (!Tests_Updates.Test_UpdateThrows3()) returnVal = false;
            }

            if (runArgHandlerTests) {
                if (!Tests_ArgHandler.Test_ArgHandler1()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler2()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler3()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler4()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler5()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler6()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler7()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler8()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler9()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler10()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler11()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler12()) returnVal = false;
                if (!Tests_ArgHandler.Test_ArgHandler13()) returnVal = false;
            }

            if (runCustomMsgBoxTests) {
                if (haveGUIAccess) {
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox1()) returnVal = false;
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox2()) returnVal = false;
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox3()) returnVal = false;
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox4()) returnVal = false;
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox5()) returnVal = false;
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox6()) returnVal = false;
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox7()) returnVal = false;
                    if (!Tests_CustomMsgBox.Test_CustomMsgBox8()) returnVal = false;
                } else {
                    GeneralFunctions.WriteTestSkipped(new List<string>(new[] { 
                            "CustomMsgBox1", "CustomMsgBox2", "CustomMsgBox3", "CustomMsgBox4", 
                            "CustomMsgBox5", "CustomMsgBox6", "CustomMsgBox7", "CustomMsgBox8" 
                        }), "No Graphical Session Access");
                }
            }

            return returnVal;
        }
    }

    static class GeneralFunctions {
        public static void DeleteFileIfExists(string path) {
            if (File.Exists(path)) {
                File.Delete(path);
            } else {
                Console.Write("[");
                WriteColour(ConsoleColor.DarkRed, "Warning");
                Console.WriteLine("] File " + path + " does not exist");
            }
        }

        public static bool TestString(string functionName, string input, string expected) {
            if (input == expected) {
                WriteTestOutput(functionName, true);
                return true;
            } else {
                WriteTestOutput(functionName, false, input, expected);
                return false;
            }
        }

        public static bool TestBoolean(string functionName, bool input, bool expected) {
            if (input == expected) {
                WriteTestOutput(functionName, true);
                return true;
            } else {
                WriteTestOutput(functionName, false, input.ToString(), expected.ToString());
                return false;
            }
        }

        public static bool TestNumber(string functionName, double input, double expected) {
            if (input == expected) {
                WriteTestOutput(functionName, true);
                return true;
            } else {
                WriteTestOutput(functionName, false, ConvertDouble(input), ConvertDouble(expected));
                return false;
            }
        }

        private const string formatString = "0.############################";
        // https://stackoverflow.com/a/9391762/2999220
        private static string ConvertDouble(double input) => input.ToString(formatString);

        public static bool TestType(string functionName, Type input, Type expected) {
            if (input == expected) {
                WriteTestOutput(functionName, true);
                return true;
            } else {
                WriteTestOutput(functionName, false, input.FullName, expected.FullName);
                return false;
            }
        }

        private static void WriteTestOutput(string functionName, bool succeeded, string input = null, string expected = null) {
            WriteColour(ConsoleColor.White, functionName);

            Console.Write(": [");
            if (succeeded) {
                WriteColour(ConsoleColor.Green, "Y");
                Console.Write("]");
            } else {
                WriteColour(ConsoleColor.Red, "N");

                Console.Write("]: in:");
                if (input == null) {
                    WriteColour(ConsoleColor.Magenta, "NULL");
                } else {
                    WriteColour(ConsoleColor.Magenta, input);
                }

                Console.Write(" expected:");
                if (expected == null) {
                    WriteColour(ConsoleColor.Cyan, "NULL");
                } else {
                    WriteColour(ConsoleColor.Cyan, expected);
                }
            }
            Console.WriteLine();
        }

        public static void WriteTestSkipped(List<string> functionNames, string reason) {
            foreach (string functionName in functionNames) {
                WriteColour(ConsoleColor.White, functionName);

                Console.Write(": ");
                WriteColour(ConsoleColor.DarkGray, string.Format("Skipped ({0})", reason));
                Console.WriteLine();
            }
        }

        private static void WriteColour(ConsoleColor colour, string input) {
            if (!Console.IsOutputRedirected)
                Console.ForegroundColor = colour;
            Console.Write(input);
            if (!Console.IsOutputRedirected)
                Console.ResetColor();
        }
    }
}
