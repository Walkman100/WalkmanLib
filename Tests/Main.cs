using System;
using System.Collections.Generic;
using System.Reflection;

static class Program {
    private static Dictionary<string, WalkmanLib.FlagInfo> flagDict = new Dictionary<string, WalkmanLib.FlagInfo>() {
        { "help", new WalkmanLib.FlagInfo() {
            shortFlag = 'h',
            description = "Show Help",
            hasArgs = true,
            optionalArgs = true,
            argsInfo = "[flag]",
            action = input => {
                Console.WriteLine("WalkmanLib Tests. Long options are case-insensitive. All options are True by default, except GUI access." + Environment.NewLine);
                WalkmanLib.EchoHelp(flagDict, input);
                Environment.Exit(0);
                return true;
            }
        } },
        { "TestDir", new WalkmanLib.FlagInfo() {
            shortFlag = 'f',
            description = "Folder to use to write test files to. Defaults to the current directory",
            hasArgs = true,
            argsInfo = "<folderPath>",
            action = input => ((rootTestFolder = input) != null) || true
        } },
        { "GUI", new WalkmanLib.FlagInfo() {
            shortFlag = 'g',
            description = "Set to true if GUI access is available",
            hasArgs = true,
            optionalArgs = true,
            argsInfo = "[true|false]",
            action = input => (haveGUIAccess = getBool(input, true)) || true
        } },
        { "DotNet", new WalkmanLib.FlagInfo() {
            shortFlag = 'd',
            description = "Set to false to skip running DotNet tests",
            hasArgs = true,
            optionalArgs = true,
            argsInfo = "[true|false]",
            action = input => (runDotNetTests = getBool(input)) || true
        } },
        { "Win32", new WalkmanLib.FlagInfo() {
            shortFlag = 'w',
            description = "Set to false to skip running Win32 tests",
            hasArgs = true,
            optionalArgs = true,
            argsInfo = "[true|false]",
            action = input => (runWin32Tests = getBool(input)) || true
        } },
        { "Updates", new WalkmanLib.FlagInfo() {
            shortFlag = 'u',
            description = "Set to false to skip running Updates tests",
            hasArgs = true,
            optionalArgs = true,
            argsInfo = "[true|false]",
            action = input => (runUpdatesTests = getBool(input)) || true
        } },
        { "ArgHandler", new WalkmanLib.FlagInfo() {
            shortFlag = 'a',
            description = "Set to false to skip running ArgHandler tests",
            hasArgs = true,
            optionalArgs = true,
            argsInfo = "[true|false]",
            action = input => (runArgHandlerTests = getBool(input)) || true
        } },
        { "CustomMsgBox", new WalkmanLib.FlagInfo() {
            shortFlag = 'c',
            description = "Set to false to skip running CustomMsgBox tests",
            hasArgs = true,
            optionalArgs = true,
            argsInfo = "[true|false]",
            action = input => (runCustomMsgBoxTests = getBool(input)) || true
        } }
    };

    private static string rootTestFolder = null;
    private static bool haveGUIAccess = false;
    private static bool runDotNetTests = true;
    private static bool runWin32Tests = true;
    private static bool runUpdatesTests = true;
    private static bool runArgHandlerTests = true;
    private static bool runCustomMsgBoxTests = true;

    private static bool getBool(string input, bool @default = false) {
        if (string.IsNullOrEmpty(input))
            return @default;
        if (bool.TryParse(input, out bool rtn)) {
            return rtn;
        } else {
            ExitE("\"{0}\" is not True or False!", input);
            Environment.Exit(1);
            Environment.Exit(0); // vb.net version uses `End` here, but there is no c# equivalent
            return false;
        }
    }

    public static void Main(string[] args) {
        WalkmanLib.ResultInfo res = WalkmanLib.ProcessArgs(args, flagDict, true);

        if (res.gotError) {
            ExitE(res.errorInfo);
        } else if (res.extraParams.Count == 1 && res.extraParams[0] == "getAdmin") {
            Console.WriteLine(WalkmanLib.IsAdmin());
            Environment.Exit(0);
        } else if (res.extraParams.Count > 0) {
            ExitE("Unknown parameter(s) supplied!");
        }

        // folder where CustomMsgBox.resx is in, used for Test_Icons
        string projectRoot = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
        projectRoot = new System.IO.FileInfo(projectRoot).Directory.Parent.Parent.Parent.FullName;

        Console.WriteLine(Environment.NewLine + "All tests completed successfully: " + Tests.Main.RunTests(projectRoot, rootTestFolder,
            haveGUIAccess, runCustomMsgBoxTests, runArgHandlerTests, runUpdatesTests, runWin32Tests, runDotNetTests));

        if (!Console.IsOutputRedirected) {
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }

    static void ExitE(string msg, string formatItem = null) {
        if (formatItem != null) {
            msg = string.Format(msg, formatItem);
        }
        Console.WriteLine(msg);
        Environment.Exit(1);
    }
}
