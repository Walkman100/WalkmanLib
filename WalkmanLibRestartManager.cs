// Get locking processes: RestartManager method
//https://stackoverflow.com/a/3504251/2999220
//https://stackoverflow.com/a/20623311/2999220
//https://stackoverflow.com/a/20623302/2999220
//https://gist.github.com/mlaily/9423f1855bb176d52a327f5874915a97
//https://docs.microsoft.com/en-us/archive/msdn-magazine/2007/april/net-matters-restart-manager-and-generic-method-compilation
//https://devblogs.microsoft.com/oldnewthing/?p=8283

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

public partial class WalkmanLib {
    /// <summary>
    /// Contains methods to get processes using a specified file, using the Windows RestartManager APIs
    /// </summary>
    public sealed class RestartManager {
        private const int CCH_RM_MAX_APP_NAME = 0xFF;
        private const int CCH_RM_MAX_SVC_NAME = 0x3F;
        private const int ERROR_MORE_DATA = 0xEA;

        // https://docs.microsoft.com/en-us/windows/win32/api/restartmanager/ns-restartmanager-rm_process_info
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct ProcessInfo {
            public UniqueProcess Process;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_APP_NAME + 1)]
            public string AppName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_RM_MAX_SVC_NAME + 1)]
            public string ServiceShortName;

            public AppType ApplicationType;
            public uint AppStatus;
            public uint TSSessionId;
            [MarshalAs(UnmanagedType.Bool)]
            public bool Restartable;
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/restartmanager/ns-restartmanager-rm_unique_process
        [StructLayout(LayoutKind.Sequential)]
        public struct UniqueProcess {
            public uint ProcessID;
            private System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/restartmanager/ne-restartmanager-rm_app_type
        // values: https://github.com/microsoft/msbuild/blob/2791d9d93e88325011eb6907579d6fdac0b1b62e/src/Tasks/LockCheck.cs#L101
        public enum AppType {
            RmUnknownApp = 0,
            RmMainWindow = 1,
            RmOtherWindow = 2,
            RmService = 3,
            RmExplorer = 4,
            RmConsole = 5,
            RmCritical = 1000
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/restartmanager/nf-restartmanager-rmregisterresources
        [DllImport("rstrtmgr.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int RmRegisterResources(
            uint pSessionHandle,
            uint nFiles,
            string[] rgsFilenames,
            uint nApplications,
            [In] UniqueProcess[] rgApplications,
            uint nServices,
            string[] rgsServiceNames);

        // https://docs.microsoft.com/en-us/windows/win32/api/restartmanager/nf-restartmanager-rmstartsession
        [DllImport("rstrtmgr.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int RmStartSession(
            out uint pSessionHandle,
            int dwSessionFlags,
            string strSessionKey);

        // https://docs.microsoft.com/en-us/windows/win32/api/restartmanager/nf-restartmanager-rmendsession
        [DllImport("rstrtmgr.dll", SetLastError = true)]
        private static extern int RmEndSession(
            uint pSessionHandle);

        // https://docs.microsoft.com/en-us/windows/win32/api/restartmanager/nf-restartmanager-rmgetlist
        [DllImport("rstrtmgr.dll", SetLastError = true)]
        private static extern int RmGetList(
            uint dwSessionHandle,
            ref uint pnProcInfoNeeded,
            ref uint pnProcInfo,
            [In, Out] ProcessInfo[] rgAffectedApps,
            ref uint lpdwRebootReasons);

        public static ProcessInfo[] GetLockingProcessInfos(string path) {
            uint handle;
            if (RmStartSession(out handle, 0, Guid.NewGuid().ToString()) != 0) {
                throw new Exception("Could not begin session. Unable to determine file lockers.", new Win32Exception());
            }

            try {
                uint ArrayLengthNeeded = 0;
                uint ArrayLength = 0;
                uint lpdwRebootReasons = 0; // RmRebootReasonNone

                string[] resources = new[] {path}; // Just checking on one resource.

                if (RmRegisterResources(handle, (uint)resources.Length, resources, 0, null, 0, null) != 0) {
                    throw new Exception("Could not register resource.", new Win32Exception());
                }

                switch (RmGetList(handle, ref ArrayLengthNeeded, ref ArrayLength, null, ref lpdwRebootReasons)) {
                    case ERROR_MORE_DATA: {
                        var processInfos = new ProcessInfo[(int)ArrayLengthNeeded];
                        ArrayLength = ArrayLengthNeeded;

                        if (RmGetList(handle, ref ArrayLengthNeeded, ref ArrayLength, processInfos, ref lpdwRebootReasons) != 0) {
                            throw new Exception("Could not list processes locking resource.", new Win32Exception());
                        }

                        return processInfos;
                    }
                    case 0: {
                        return new ProcessInfo[] { };
                    }
                    default: {
                        throw new Exception("Could not list processes locking resource. Failed to get size of result.", new Win32Exception());
                    }
                }
            } finally {
                RmEndSession(handle);
            }
        }

        /// <summary>
        /// Returns a list of Diagnostics.Process that are currently using the specified <paramref name="path" />.
        /// </summary>
        /// <param name="path">Path to the file to get processes for</param>
        /// <returns>Collections.Generic.List(Of Process) that are using the file</returns>
        public static IEnumerable<Process> GetLockingProcesses(string path) {
            foreach (ProcessInfo pI in GetLockingProcessInfos(path)) {
                Process processToAdd = null;
                try {
                    processToAdd = Process.GetProcessById((int)pI.Process.ProcessID);
                } catch (ArgumentException) { }
                if (processToAdd != null) yield return processToAdd;
            }
        }
    }

    /// <summary>
    /// Returns a list of Diagnostics.Process that are currently using the specified <paramref name="path" />, using the RestartManager method.
    /// </summary>
    /// <param name="path">Path to the file to get processes for</param>
    /// <returns>Collections.Generic.List(Of Process) that are using the file</returns>
    public static IEnumerable<Process> GetLockingProcessesRM(string path) {
        return RestartManager.GetLockingProcesses(path);
    }
}
