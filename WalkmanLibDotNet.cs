using System;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using static System.IO.File;

// used for IsFileOrDirectory
[Flags]
public enum PathEnum {
    NotFound = 0,
    Exists = 1,
    IsDirectory = 2,
    IsFile = 4,
    IsDrive = 8
}

public partial class WalkmanLib {
    public enum OS {
        Other,
        Windows,
        Linux,
        MacOS
    }

    public static OS GetOS() {
#if NETCOREAPP
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
            return OS.Windows;
        } else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux)) {
            return OS.Linux;
        } else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX)) {
            return OS.MacOS;
        }
#else
        if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
            return OS.Windows;
        } else if (Environment.OSVersion.Platform == PlatformID.Unix) {
            string unameOutput = RunAndGetOutput("uname", mergeStdErr: true).StandardOutput;
            if (unameOutput == "Linux")
                return OS.Linux;
            else if (unameOutput == "Darwin")
                return OS.MacOS;
        }
#endif

        return OS.Other;
    }

    /// <summary>Opens the Open With dialog box for a file path.</summary>
    /// <param name="path">The file to open with a program.</param>
    public static void OpenWith(string path) {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
            FileName = "rundll32",
            Arguments = "shell32.dll,OpenAs_RunDLL " + path
        }).WaitForExit(500);
    }

#if NETCOREAPP
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
    /// <summary>Checks whether the current process is elevated (running with administrator permissions)</summary>
    /// <returns>true if running with administrator permissions, false if not</returns>
    public static bool IsAdmin() {
        return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }

    // Link: https://www.howtogeek.com/howto/windows-vista/add-take-ownership-to-explorer-right-click-menu-in-vista/
    /// <summary>Runs the Take Ownership commands for a path.</summary>
    /// <param name="path">Path of file to take ownership of, or directory to recursively take ownership of.</param>
    public static void TakeOwnership(string path) {
        PathEnum pathInfo = IsFileOrDirectory(path);
        if (pathInfo.HasFlag(PathEnum.IsFile))
            RunAsAdmin("cmd.exe", "/c takeown /f \"" + path + "\" & icacls \"" + path + "\" /grant administrators:F & pause");
        else if (pathInfo.HasFlag(PathEnum.IsDirectory))
            RunAsAdmin("cmd.exe", "/c takeown /f \"" + path + "\" /r /d y & icacls \"" + path + "\" /grant administrators:F /t & pause");
        else
            throw new ArgumentException("File or Directory at specified path does not exist!", "path");
    }

    /// <summary>Starts a program with a set of command-line arguments as an administrator.</summary>
    /// <param name="programPath">Path of the program to run as administrator.</param>
    /// <param name="arguments">Optional. Command-line arguments to pass when starting the process. Surround whitespaces with quotes as usual.</param>
    /// <param name="workingDirectory">Optional. Working Directory for the launched program, and strangely also the first search path if <paramref name="programPath"/> is not absolute.</param>
    /// <returns>Whether the request for Admin was approved.</returns>
    public static bool RunAsAdmin(string programPath, string arguments = null, string workingDirectory = null) {
        try {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                Verb = "runas",
                UseShellExecute = true,
                FileName = programPath,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
            });
            return true;
        } catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223) { // ERROR_CANCELLED: The operation was canceled by the user.
            return false;
        }
    }

    /// <summary>Adds or removes the specified System.IO.FileAttributes to the file at the specified path, with a try..catch block.</summary>
    /// <param name="path">The path to the file.</param>
    /// <param name="fileAttribute">The FileAttributes to add or remove.</param>
    /// <param name="addOrRemoveAttribute">true to add the specified attribute, false to remove it.</param>
    /// <param name="accessDeniedSub">Create a void with the signature (Exception ex) to run it when access is denied and the program can be elevated.</param>
    /// <returns>Whether setting the attribute was successful or not.</returns>
    public static bool ChangeAttribute(string path, FileAttributes fileAttribute, bool addOrRemoveAttribute, Action<Exception> accessDeniedSub = null) =>
        addOrRemoveAttribute
            ? AddAttribute(path, fileAttribute, accessDeniedSub)
            : RemoveAttribute(path, fileAttribute, accessDeniedSub);

    /// <summary>Adds the specified System.IO.FileAttributes to the file at the specified path, with a try..catch block.</summary>
    /// <param name="path">The path to the file.</param>
    /// <param name="fileAttribute">The FileAttributes to add.</param>
    /// <param name="accessDeniedSub">Create a void with the signature (Exception ex) to run it when access is denied and the program can be elevated.</param>
    /// <returns>Whether adding the attribute was successful or not.</returns>
    public static bool AddAttribute(string path, FileAttributes fileAttribute, Action<Exception> accessDeniedSub = null) =>
        SetAttribute(path, GetAttributes(path) | fileAttribute, accessDeniedSub);

    /// <summary>Removes the specified System.IO.FileAttributes from the file at the specified path, with a try..catch block.</summary>
    /// <param name="path">The path to the file.</param>
    /// <param name="fileAttribute">The FileAttributes to remove.</param>
    /// <param name="accessDeniedSub">Create a void with the signature (Exception ex) to run it when access is denied and the program can be elevated.</param>
    /// <returns>Whether removing the attribute was successful or not.</returns>
    public static bool RemoveAttribute(string path, FileAttributes fileAttribute, Action<Exception> accessDeniedSub = null) =>
        SetAttribute(path, GetAttributes(path) & ~fileAttribute, accessDeniedSub);

    /// <summary>Sets the specified System.IO.FileAttributes of the file on the specified path, with a try..catch block.</summary>
    /// <param name="path">The path to the file.</param>
    /// <param name="fileAttributes">A bitwise combination of the enumeration values.</param>
    /// <param name="accessDeniedSub">Create a void with the signature (Exception ex) to run it when access is denied and the program can be elevated.</param>
    /// <returns>Whether setting the attribute was successful or not.</returns>
    public static bool SetAttribute(string path, FileAttributes fileAttributes, Action<Exception> accessDeniedSub = null) {
        try {
            SetAttributes(path, fileAttributes);
            return true;
        } catch (UnauthorizedAccessException ex) when (!IsAdmin() && accessDeniedSub != null) {
            accessDeniedSub(ex);
            return false;
        } catch (Exception ex) {
            ErrorDialog(ex);
            return false;
        }
    }

    // Example code to use the access denied sub return:
    //void Main() {
    //    WalkmanLib.SetAttribute(@"C:\ProgramData", FileAttributes.Hidden, accessDeniedSub);
    //}
    //void accessDeniedSub(Exception ex) {
    //    Application.EnableVisualStyles(); // affects when in a console app
    //    if (MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Try launching <programName> As Administrator?",
    //                        "Access denied!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
    //        WalkmanLib.RunAsAdmin(Application.StartupPath + @"\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", "<arguments>");
    //        Environment.Exit(0); Application.Exit(); // depending on whether running in Console or WinForms app, respectively
    //    }
    //}

    // Link: https://stackoverflow.com/a/25958432/2999220
    // Link: https://stackoverflow.com/a/11166160/2999220
    private const byte RunAsAdminByte  = 0x15; // Decimal 21
    private const byte RunAsAdminOnBit = 0x20; // Decimal 32

    /// <summary>Gets whether a shortcut's "Run as Administrator" checkbox is checked.</summary>
    /// <param name="shortcutPath">Path to the shortcut file. Shortcuts end in ".lnk".</param>
    /// <returns>State of the Admin flag. <see langword="true"/> = Set, i.e. will attempt to run as admin.</returns>
    public static bool GetShortcutRunAsAdmin(string shortcutPath) {
        byte[] shortcutBytes = ReadAllBytes(shortcutPath);
        return (shortcutBytes[RunAsAdminByte] & RunAsAdminOnBit) == RunAsAdminOnBit;
    }

    /// <summary>
    /// Sets a shortcut's "Run as Administrator" checkbox state.
    /// Note that this uses bit-flipping to change the documented RunAsAdmin bit.
    /// If the Shortcut (.lnk) specification ever changes, this will corrupt shortcuts.
    /// </summary>
    /// <param name="shortcutPath">Path to the shortcut file. Shortcuts end in ".lnk".</param>
    /// <param name="flagState">State to set the Admin flag to. <see langword="true"/> = Set, i.e. will attempt to run as admin.</param>
    public static void SetShortcutRunAsAdmin(string shortcutPath, bool flagState) {
        byte[] shortcutBytes = ReadAllBytes(shortcutPath);
        shortcutBytes[RunAsAdminByte] = flagState
            ? (byte)(shortcutBytes[RunAsAdminByte] |  RunAsAdminOnBit)
            : (byte)(shortcutBytes[RunAsAdminByte] & ~RunAsAdminOnBit);

        WriteAllBytes(shortcutPath, shortcutBytes);
    }

    /// <summary>Writes text to console to report a progress count</summary>
    /// <param name="start">Number to start at. Must be less than <paramref name="end"/>.</param>
    /// <param name="end">Number to end at. Must be more than <paramref name="start"/>.</param>
    /// <param name="stop">ref boolean used to stop the count from an external method.</param>
    /// <param name="stopped">out boolean used to signal that counting has stopped.</param>
    /// <returns><see langword="true"/> if the count reached <paramref name="end"/>, <see langword="false"/> if <paramref name="stop"/> was used to cancel.</returns>
    public static bool ConsoleProgress(int start, int end, ref bool stop, out bool stopped) {
        int currentCount = start;
        int stopLength = end.ToString().Length; // could also use: (int)(Math.Floor(Math.Log10(end) + 1))

        Console.Write(currentCount.ToString().PadLeft(stopLength) + " / " + end);
        Console.CursorLeft -= stopLength + 3;

        while (!stop && currentCount != end) {
            System.Threading.Thread.Sleep(1000);
            currentCount++;

            Console.CursorLeft -= stopLength;
            Console.Write(currentCount.ToString().PadLeft(stopLength));
        }

        stopped = true;
        return currentCount == end;
    }

    /// <summary>
    /// Gets whether a specified <paramref name="path"/> exists. `NotFound` is returned on invalid chars.
    /// Return value will contain `Exists` if <paramref name="path"/> refers to a valid drive,
    /// and `IsDirectory` will be set if the drive is accessible (ready).
    /// </summary>
    /// <param name="path">Path to the item to get exists info for</param>
    /// <returns>PathEnum: either NotFound, or Exists with IsFile | IsDirectory, and IsDrive if <paramref name="path"/> points to a drive mountpoint</returns>
    public static PathEnum IsFileOrDirectory(string path) {
        if (path.IndexOfAny(Path.GetInvalidPathChars()) != -1) {
            // invalid chars
            return PathEnum.NotFound;
        }

        var rtn = new PathEnum();
        if (File.Exists(path))
            rtn = PathEnum.Exists | PathEnum.IsFile;
        else if (Directory.Exists(path))
            rtn = PathEnum.Exists | PathEnum.IsDirectory;

        try {
            // path can be a Directory and a Drive, or just a Drive...
            // will have IsDirectory if the drive can be accessed
            if (new DriveInfo(path).Name == new FileInfo(path).FullName)
                rtn = rtn | PathEnum.Exists | PathEnum.IsDrive;
        } catch (ArgumentException) {
            // New DriveInfo() and New FileInfo() throw ArgumentException on invalid path sequence
        } catch (NotSupportedException) {
            // NotSupportedException is thrown for AlternateDataStreams
        }

        return rtn;
    }

    /// <summary>Sets clipboard to specified text, with optional success message and checks for errors.</summary>
    /// <param name="text">Text to copy.</param>
    /// <param name="successMessage">Message to show on success. If left out no message will be shown, if "default" is supplied then the default message will be shown.</param>
    /// <param name="showErrors">Whether to show a message on copy error or not. Default: <see langword="true"/></param>
    /// <returns>Whether setting the clipboard was successful or not.</returns>
    public static bool SafeSetText(string text, string successMessage = null, bool showErrors = true) {
        try {
            Clipboard.SetText(text, TextDataFormat.UnicodeText);
            if (!string.IsNullOrEmpty(successMessage)) {
                Application.EnableVisualStyles(); // affects when in a console app
                if (successMessage == "default")
                    MessageBox.Show(text + Environment.NewLine + "Succesfully copied!", "Succesfully copied!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(successMessage, "Succesfully copied!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        } catch (Exception ex) {
            if (showErrors) {
                Application.EnableVisualStyles(); // affects when in a console app
                MessageBox.Show("Copy failed!" + Environment.NewLine + "Error: \"" + ex.ToString() + "\"", "Copy failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

#if NETCOREAPP
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
    /// <summary>Checks whether the user has enabled the Win10+ setting "Default App Mode" (Light/Dark)</summary>
    /// <returns><see langword="true"/> if Dark mode is enabled, <see langword="false"/> if disabled. <see langword="null"/> if the Personalize key does not exist in the registry.</returns>
    public static bool? GetDarkThemeEnabled() {
        // HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize AppsUseLightTheme
        string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        var localKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser,
                                                               Environment.Is64BitOperatingSystem ? Microsoft.Win32.RegistryView.Registry64 : Microsoft.Win32.RegistryView.Registry32);
        localKey = localKey.OpenSubKey(keyPath);

        if (localKey != null) {
            int? appsUseLightTheme = (int?)localKey.GetValue("AppsUseLightTheme");
            if (appsUseLightTheme == null)
                return false;

            return appsUseLightTheme != 1;
        }

        return null;
    }

#if NETCOREAPP
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
    /// <summary>
    /// Gets path to a WalkmanUtils install. First tries in the same folder as the running application,
    /// then the location saved in the registry, then a default fallback in Program Files.
    /// <br />Throws <see cref="DirectoryNotFoundException"/> on no install found.
    /// </summary>
    /// <param name="minimumVersion">
    /// Optional version to check against installed version.
    /// Only checked with installed WalkmanUtils, and only presents a Continue message to the user. Throws <see cref="OperationCanceledException"/> on user abort.
    /// </param>
    /// <returns>Directory containing a WalkmanUtils installation. Module presence is not checked.</returns>
    public static string GetWalkmanUtilsPath(Version minimumVersion = null) {
        // first check startup path
        string rtn = Path.Combine(Application.StartupPath, "WalkmanUtils");
        if (Directory.Exists(rtn))
            return rtn;

        // then registry
        // if 64-bit app: HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\WalkmanUtils InstallLocation
        // if 32-bit app: HKLM\Software            \Microsoft\Windows\CurrentVersion\Uninstall\WalkmanUtils InstallLocation
        string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\WalkmanUtils\";

        // always use 32-bit view
        var localKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine,
                                                               Microsoft.Win32.RegistryView.Registry32);
        localKey = localKey.OpenSubKey(keyPath);

        if (localKey != null && localKey.GetValue("InstallLocation") != null) {
            rtn = localKey.GetValue("InstallLocation").ToString();
            if (Directory.Exists(rtn)) {
                if (minimumVersion != null) {
                    if (Version.TryParse(localKey.GetValue("DisplayVersion").ToString(), out Version gotVersion)) {
                        if (gotVersion < minimumVersion && MessageBox.Show("Currently Installed WalkmanUtils version is out of date! Use anyway?",
                                                                           "WalkmanUtils Version", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                            throw new OperationCanceledException();
                    } else if (MessageBox.Show("Got Invalid Version from WalkmanUtils install info! Continue anyway?",
                                               "WalkmanUtils Version", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) {
                        throw new OperationCanceledException();
                    }
                }

                return rtn.TrimEnd(Path.DirectorySeparatorChar);
            }
        }

        // fall back to check %ProgramFiles%\WalkmanOSS\WalkmanUtils
        rtn = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles"), "WalkmanOSS", "WalkmanUtils");
        if (Directory.Exists(rtn))
            return rtn;
        //                and %ProgramFiles(x86)%\WalkmanOSS\WalkmanUtils
        rtn = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), "WalkmanOSS", "WalkmanUtils");
        if (Directory.Exists(rtn))
            return rtn;
        //              ... and %ProgramW6432%\WalkmanOSS\WalkmanUtils
        rtn = Path.Combine(Environment.GetEnvironmentVariable("ProgramW6432"), "WalkmanOSS", "WalkmanUtils");
        if (Directory.Exists(rtn))
            return rtn;

        throw new DirectoryNotFoundException("WalkmanUtils path not found in application's path, Installed location or default folder in Program Files");
    }

    /// <summary>Shows an error message for an exception, and asks the user if they want to display the full error in a copyable window.</summary>
    /// <param name="ex">The <see cref="Exception"/> to show details about.</param>
    /// <param name="errorMessage">Optional error message to show instead of the default "There was an error! Error message: "</param>
    /// <param name="showMsgBox"><see langword="true"/> to show the error message prompt to show the full stacktrace or not, <see langword="false"/> to just show the window immediately</param>
    /// <param name="showErrorBlockingWindow"><see langword="true"/> to use <see cref="Form.ShowDialog"/> to show error dialog, <see langword="false"/> to use <see cref="Control.Show"/>, <see langword="null"/> to show on separate thread.</param>
    public static void ErrorDialog(Exception ex, string errorMessage = "There was an error! Error message: ", bool showMsgBox = true, bool? showErrorBlockingWindow = null) {
        Application.EnableVisualStyles(); // affects when in a console app
        if (showMsgBox && MessageBox.Show($"{errorMessage}{ex.Message}{Environment.NewLine}Show full stacktrace? (For sending to developer/making bugreport)",
                                          "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;

        var frmBugReport = new Form() {
            Width = 600,
            Height = 525,
            StartPosition = FormStartPosition.CenterParent,
            ShowIcon = false,
            Text = "Full error trace"
        };
        var txtBugReport = new TextBox() {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Dock = DockStyle.Fill
        };
        frmBugReport.Controls.Add(txtBugReport);
        try {
            txtBugReport.Text = "";
            while (ex != null) {
                if (ex.ToString() != null)          txtBugReport.Text += $"ToString:{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}";
                if (ex.GetBaseException() != null)  txtBugReport.Text += $"BaseException:{Environment.NewLine}{ex.GetBaseException()}{Environment.NewLine}{Environment.NewLine}";
                if (ex.GetType() != null)           txtBugReport.Text += $"Type: {ex.GetType()}{Environment.NewLine}";
                if (ex.Message != null)             txtBugReport.Text += $"Message: {ex.Message}{Environment.NewLine}{Environment.NewLine}";
                if (ex.StackTrace != null)          txtBugReport.Text += $"StackTrace:{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{Environment.NewLine}";
                if (ex is System.ComponentModel.Win32Exception win32ex) {
                                                    txtBugReport.Text += $"ErrorCode: 0x{win32ex.ErrorCode.ToString("X")}{Environment.NewLine}";
                                                    txtBugReport.Text += $"NativeErrorCode: 0x{win32ex.NativeErrorCode.ToString("X")}{Environment.NewLine}";
                }
                if (ex is FileNotFoundException fileNotFoundEx) {
                                                    txtBugReport.Text += $"FileName: {fileNotFoundEx.FileName}{Environment.NewLine}";
                                                    txtBugReport.Text += $"FusionLog: {fileNotFoundEx.FusionLog}{Environment.NewLine}";
                }
                if (ex.Source != null)              txtBugReport.Text += $"Source: {ex.Source}{Environment.NewLine}";
                if (ex.TargetSite != null)          txtBugReport.Text += $"TargetSite: {ex.TargetSite}{Environment.NewLine}";
                                                    txtBugReport.Text += $"HashCode: 0x{ex.GetHashCode().ToString("X")}{Environment.NewLine}";
                                                    txtBugReport.Text += $"HResult: 0x{ex.HResult.ToString("X")}{Environment.NewLine}{Environment.NewLine}";
                foreach (object key in ex.Data.Keys) {
                                                    txtBugReport.Text += $"Data({key}): {ex.Data[key]}{Environment.NewLine}";
                }
                if (ex.InnerException != null)      txtBugReport.Text += $"{Environment.NewLine}InnerException:{Environment.NewLine}";
                ex = ex.InnerException;
            }
        } catch (Exception ex2) {
            txtBugReport.Text += $"Error getting exception data!{Environment.NewLine}{Environment.NewLine}{ex2}";
        }

        try {
            if (showErrorBlockingWindow.HasValue && showErrorBlockingWindow.Value)
                frmBugReport.ShowDialog();
            else if (showErrorBlockingWindow.HasValue)
                frmBugReport.Show();
            else
                System.Threading.Tasks.Task.Run(() => frmBugReport.ShowDialog());
        } catch (Exception ex2) {
            MessageBox.Show($"Error showing window: {ex2}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }


    // fix for no optional ref/out parameters in C#
    public struct RunAndGetOutputReturn {
        public RunAndGetOutputReturn(string standardOutput, string standardError, int exitCode) {
            StandardOutput = standardOutput;
            StandardError = standardError;
            ExitCode = exitCode;
        }
        public string StandardOutput;
        public string StandardError;
        public int ExitCode;
    }
    public static string RunAndGetOutput(string fileName, string arguments, string workingDirectory, bool mergeStdErr, out string stdErrReturn, out int exitCode) {
        var rtn = RunAndGetOutput(fileName, arguments, workingDirectory, mergeStdErr);
        stdErrReturn = rtn.StandardError;
        exitCode = rtn.ExitCode;
        return rtn.StandardOutput;
    }
    // Link: https://stackoverflow.com/a/10072082/2999220
    /// <summary>Runs an executable without showing a window and returns its output.</summary>
    /// <param name="fileName">The path to the program executable.</param>
    /// <param name="arguments">Any arguments to run the program with. Default: <see langword="null"/></param>
    /// <param name="workingDirectory">Working directory for the process to be started. Default: <see langword="null"/></param>
    /// <param name="mergeStdErr">Whether to merge StandardError with StandardError in the <see cref="RunAndGetOutputReturn.StandardOutput"/>. Default: <see langword="false"/></param>
    /// <returns>If <paramref name="mergeStdErr"/> is <see langword="true"/> and the process outputs data to StandardError, <see cref="RunAndGetOutputReturn.StandardOutput"/> will be appended with "StdErr:", <see cref="RunAndGetOutputReturn.StandardError"/>, "ExitCode:", and the process's Exit Code.
    /// <br />To merge StandardOutput and StandardError in the order they are output, use "cmd.exe" as the fileName, "/c actual_program.exe actual_arguments 2>&amp;1" as the arguments (replace actual_* with real values), and set <paramref name="mergeStdErr"/> to <see langword="false"/>.</returns>
    public static RunAndGetOutputReturn RunAndGetOutput(string fileName, string arguments = null, string workingDirectory = null, bool mergeStdErr = false) {
        var process = new System.Diagnostics.Process() {
            StartInfo = new System.Diagnostics.ProcessStartInfo() {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                CreateNoWindow = true,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            }
        };

        var stdOutput = new System.Text.StringBuilder();
        process.OutputDataReceived += (sender, args) => stdOutput.AppendLine(args.Data);
        // Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.

        string stdError;
        process.Start();
        process.BeginOutputReadLine();
        stdError = process.StandardError.ReadToEnd();
        process.WaitForExit();

        string returnString = stdOutput.ToString().Trim();
        stdError = stdError?.Trim();
        if (mergeStdErr && !string.IsNullOrEmpty(stdError)) {
            if (!string.IsNullOrEmpty(returnString))
                returnString += Environment.NewLine;

            returnString += "StdErr: " + stdError;
            returnString += $"{Environment.NewLine}ExitCode: {process.ExitCode}";
        }

        return new RunAndGetOutputReturn(returnString, stdError, process.ExitCode);
    }

    /// <summary>Gets path to the folder icon, or "no icon found" if none is set.</summary>
    /// <param name="folderPath">The folder path to get the icon path for.</param>
    /// <returns>The icon path.</returns>
    public static string GetFolderIconPath(string folderPath) {
        bool gotIcon = false, lookingForIconIndex, isAbsolute;
        string parsedIconPath = folderPath;

        if (folderPath.EndsWith($"{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}")) {
            if (Exists(Path.Combine(folderPath, "Autorun.inf"))) {
                foreach (string line in ReadLines(Path.Combine(folderPath, "Autorun.inf"))) {
                    if (line.StartsWith("Icon=", true, null)) {
                        parsedIconPath = line.Substring(5);
                        gotIcon = true;
                    }
                }
            }
        } else {
            if (Exists(Path.Combine(folderPath, "desktop.ini"))) {
                gotIcon = false;
                lookingForIconIndex = false;
                foreach (string line in ReadLines(Path.Combine(folderPath, "desktop.ini"))) {
                    if (line.StartsWith("IconResource=", true, null)) {
                        parsedIconPath = line.Substring(13);
                        gotIcon = true;
                    } else if (line.StartsWith("IconFile=", true, null) && gotIcon == false) {
                        parsedIconPath = line.Substring(9);
                        lookingForIconIndex = true;
                        gotIcon = true;
                    } else if (line.StartsWith("IconIndex=", true, null) && lookingForIconIndex) {
                        parsedIconPath = parsedIconPath + "," + line.Substring(10);
                        lookingForIconIndex = false;
                    }
                }
            }
        }

        if (gotIcon) {
            isAbsolute = false;
            if (parsedIconPath.StartsWith("%")) {
                isAbsolute = true;
                parsedIconPath = Environment.ExpandEnvironmentVariables(parsedIconPath);
            } else {
                for (int i = 1; i <= 26; i++) { // The Convert.ToChar() below will give all letters from A to Z
                    if (parsedIconPath.StartsWith($"{Convert.ToChar(i + 64)}{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}", true, null)) {
                        isAbsolute = true;
                        break;
                    }
                }
            }

            if (parsedIconPath.EndsWith(",0"))
                parsedIconPath = parsedIconPath.Remove(parsedIconPath.Length - 2);

            return isAbsolute ? parsedIconPath : Path.Combine(folderPath, parsedIconPath);
        } else {
            return "no icon found";
        }
    }
}
