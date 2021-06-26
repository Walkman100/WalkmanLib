using System;
using System.Management;
using System.Runtime.InteropServices;

// Credits:
//  Main code: https://code.msdn.microsoft.com/windowsapps/Sample-to-demonstrate-how-495e69db
//  Additional version tables:
//   http://www.nirmaltv.com/2009/08/17/windows-os-version-numbers/
//    which links to https://www.msigeek.com/442/windows-os-version-numbers
//   https://stackoverflow.com/a/2819962/2999220
//   https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_osversioninfoexa
//   https://docs.microsoft.com/en-us/windows/desktop/SysInfo/operating-system-version

public enum WindowsVersion {
    Windows1Point0,
    Windows2Point0,
    Windows3Point0,
    WindowsNT3Point1,
    WindowsNT3Point11,
    WindowsNT3Point5,
    WindowsNT3Point51,
    Windows95,
    WindowsNT4Point0,
    Windows98,
    Windows98SE,
    WindowsME,
    Windows2000,
    WindowsXP,

    WindowsXPProX64,
    WindowsServer2003,
    WindowsServer2003R2,

    WindowsVista,
    WindowsServer2008,

    Windows7,
    WindowsServer2008R2,

    Windows8,
    WindowsServer2012,

    Windows8Point1,
    WindowsServer2012R2,

    Windows10,
    WindowsServer2016
}

public partial class WalkmanLib {
    /// <summary>Gets whether the current Operating System is a Windows Server version or not</summary>
    /// <returns>True if current environment is a Server version, and False for a standard Workstation version</returns>
    public static bool IsWindowsServer() {
        //                      add a reference to System.Management.dll
        using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")) {
            foreach (ManagementObject managementObject in searcher.Get()) {
                // ProductType will be one of:
                // 1: Workstation
                // 2: Domain Controller
                // 3: Server
                uint productType = (uint)managementObject.GetPropertyValue("ProductType");
                return productType != 1;
            }
        }

        return false;
    }

    // this helps distinguish between Windows Server 2003 and Windows Server 2003 R2
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetSystemMetrics(int smIndex);

    /// <summary>Gets the current Windows version. NOTE: To get an accurate version on Windows versions above 8, 
    /// you will need to embed a manifest as per https://msdn.microsoft.com/E7A1A16A-95B3-4B45-81AD-A19E33F15AE4
    /// (https://docs.microsoft.com/en-us/windows/desktop/SysInfo/targeting-your-application-at-windows-8-1)</summary>
    /// <returns>A Windows version of type <see cref="WindowsVersion"/></returns>
    public static WindowsVersion GetWindowsVersion() {
        Version currentVersion = Environment.OSVersion.Version;

        switch (currentVersion.Major) {
            case 1: {
                return WindowsVersion.Windows1Point0;
            }
            case 2: {
                return WindowsVersion.Windows2Point0;
            }
            case 3: {
                if (currentVersion.Minor == 0) {
                    return WindowsVersion.Windows3Point0;
                } else if (currentVersion.Minor == 10) {
                    return WindowsVersion.WindowsNT3Point1;
                } else if (currentVersion.Minor == 11) {
                    return WindowsVersion.WindowsNT3Point11;
                } else if (currentVersion.Minor == 5) {
                    return WindowsVersion.WindowsNT3Point5;
                } else if (currentVersion.Minor == 51) {
                    return WindowsVersion.WindowsNT3Point51;
                }

                break;
            }
            case 4: {
                if (currentVersion.Minor == 0) {
                    if (currentVersion.MinorRevision == 950) {
                        return WindowsVersion.Windows95;
                    } else if (currentVersion.MinorRevision == 1381) {
                        return WindowsVersion.WindowsNT4Point0;
                    }
                } else if (currentVersion.Minor == 1 || currentVersion.Minor == 10) {
                    if (currentVersion.MinorRevision == 1998) {
                        return WindowsVersion.Windows98;
                    } else if (currentVersion.MinorRevision == 2222) {
                        return WindowsVersion.Windows98SE;
                    }
                } else if (currentVersion.Minor == 90) {
                    return WindowsVersion.WindowsME;
                }

                break;
            }
            case 5: {
                if (currentVersion.Minor == 0) {
                    return WindowsVersion.Windows2000;
                } else if (currentVersion.Minor == 1) {
                    return WindowsVersion.WindowsXP;
                } else if (currentVersion.Minor == 2) {

                    if (IsWindowsServer()) {
                        if (GetSystemMetrics(89) == 0) {
                            return WindowsVersion.WindowsServer2003;
                        } else {
                            return WindowsVersion.WindowsServer2003R2;
                        }

                        // Possibly also Windows Home Server - see https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_osversioninfoexa
                    } else {
                        return WindowsVersion.WindowsXPProX64;
                    }
                }

                break;
            }
            case 6: {
                if (currentVersion.Minor == 0) {
                    if (IsWindowsServer()) {
                        return WindowsVersion.WindowsServer2008;
                    } else {
                        return WindowsVersion.WindowsVista;
                    }
                } else if (currentVersion.Minor == 1) {
                    if (IsWindowsServer()) {
                        return WindowsVersion.WindowsServer2008R2;
                    } else {
                        return WindowsVersion.Windows7;
                    }
                } else if (currentVersion.Minor == 2) {
                    if (IsWindowsServer()) {
                        return WindowsVersion.WindowsServer2012;
                    } else {
                        return WindowsVersion.Windows8;
                    }
                } else if (currentVersion.Minor == 3) {
                    if (IsWindowsServer()) {
                        return WindowsVersion.WindowsServer2012R2;
                    } else {
                        return WindowsVersion.Windows8Point1;
                    }
                }

                break;
            }
            case 10: {
                if (IsWindowsServer()) {
                    return WindowsVersion.WindowsServer2016;
                } else {
                    return WindowsVersion.Windows10;
                }
            }
        }

        throw new InvalidOperationException(string.Format("Unrecognised Windows Version!{0}{0}VersionString: {1}{0}Version.ToString: {2}",
                                                          Environment.NewLine, Environment.OSVersion.VersionString, currentVersion.ToString()));
    }
}
