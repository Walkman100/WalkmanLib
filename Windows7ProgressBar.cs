using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Windows7ProgressBar v1.0, created by Wyatt O'Day
// Visit: http://wyday.com/windows-7-progress-bar/
namespace wyDay.Controls {
    /// <summary>
    /// Represents the thumbnail progress bar state.
    /// </summary>
    public enum ThumbnailProgressState {
        /// <summary>
        /// No progress is displayed.
        /// </summary>
        NoProgress = 0,
        /// <summary>
        /// The progress is indeterminate (marquee).
        /// </summary>
        Indeterminate = 0x1,
        /// <summary>
        /// Normal progress is displayed.
        /// </summary>
        Normal = 0x2,
        /// <summary>
        /// An error occurred (red).
        /// </summary>
        Error = 0x4,
        /// <summary>
        /// The operation is paused (yellow).
        /// </summary>
        Paused = 0x8
    }

    /// <summary>
    /// The progress bar state for Windows Vista & 7
    /// </summary>
    public enum ProgressBarState {
        /// <summary>
        /// Indicates normal progress
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Indicates an error in the progress
        /// </summary>
        Error = 2,

        /// <summary>
        /// Indicates paused progress
        /// </summary>
        Pause = 3
    }

    // Based on Rob Jarett's wrappers for the desktop integration PDC demos.
    [ComImport]
    [Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList3 {
        // ITaskbarList
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);
        // ITaskbarList2
        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);
        // ITaskbarList3
        void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
        void SetProgressState(IntPtr hwnd, ThumbnailProgressState tbpFlags);
        // yadda, yadda - there's more to the interface, but we don't need it.
    }

    [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComImport]
    internal class CTaskbarList {
    }

    /// <summary>
    /// The primary coordinator of the Windows 7 taskbar-related activities.
    /// </summary>
    public sealed class Windows7Taskbar {
        private Windows7Taskbar() {
        }

        private static ITaskbarList3 _taskbarList;

        internal static ITaskbarList3 TaskbarList {
            get {
                if (_taskbarList is null) {
                    lock (typeof(Windows7Taskbar)) {
                        if (_taskbarList is null) {
                            _taskbarList = (ITaskbarList3)new CTaskbarList();
                            _taskbarList.HrInit();
                        }
                    }
                }

                return _taskbarList;
            }
        }

        private static readonly OperatingSystem osInfo = Environment.OSVersion;

        internal static bool Windows7OrGreater {
            get {
                return osInfo.Version.Major == 6 && osInfo.Version.Minor >= 1 || osInfo.Version.Major > 6;
            }
        }

        /// <summary>
        /// Sets the progress state of the specified window's
        /// taskbar button.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="state">The progress state.</param>
        public static void SetProgressState(IntPtr hwnd, ThumbnailProgressState state) {
            if (Windows7OrGreater) {
                TaskbarList.SetProgressState(hwnd, state);
            }
        }
        /// <summary>
        /// Sets the progress value of the specified window's
        /// taskbar button.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="current">The current value.</param>
        /// <param name="maximum">The maximum value.</param>
        public static void SetProgressValue(IntPtr hwnd, ulong current, ulong maximum) {
            if (Windows7OrGreater) {
                TaskbarList.SetProgressValue(hwnd, current, maximum);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    }

    /// <summary>
    /// A Windows progress bar control with Windows Vista & 7 functionality.
    /// </summary>
    [ToolboxBitmap(typeof(ProgressBar))]
    public class Windows7ProgressBar : ProgressBar {
        private bool m_showInTaskbar;
        private ProgressBarState m_State = ProgressBarState.Normal;
        private ContainerControl ownerForm;

        public Windows7ProgressBar() {
        }

        public Windows7ProgressBar(ContainerControl parentControl) {
            ContainerControl = parentControl;
        }

        public ContainerControl ContainerControl {
            get {
                return ownerForm;
            }

            set {
                ownerForm = value;
                if (!ownerForm.Visible) {
                    ((Form)ownerForm).Shown += Windows7ProgressBar_Shown;
                }
            }
        }

        public override ISite Site {
            get {
                return base.Site;
            }

            set {
                // Runs at design time, ensures designer initializes ContainerControl
                base.Site = value;
                if (value is null) {
                    return;
                }

                var service = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (service is null) {
                    return;
                }

                var rootComponent = service.RootComponent;
                ContainerControl = rootComponent as ContainerControl;
            }
        }

        private void Windows7ProgressBar_Shown(object sender, EventArgs e) {
            if (ShowInTaskbar) {
                if (Style != ProgressBarStyle.Marquee) {
                    SetValueInTB();
                }

                SetStateInTB();
            } ((Form)ownerForm).Shown -= Windows7ProgressBar_Shown;
        }

        /// <summary>
        /// Show progress in taskbar
        /// </summary>
        [DefaultValue(false)]
        public bool ShowInTaskbar {
            get {
                return m_showInTaskbar;
            }

            set {
                if (m_showInTaskbar != value) {
                    m_showInTaskbar = value;

                    // send signal to the taskbar.
                    if (ownerForm is object) {
                        if (Style != ProgressBarStyle.Marquee) {
                            SetValueInTB();
                        }

                        SetStateInTB();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the current position of the progress bar.
        /// </summary>
        /// <returns>The position within the range of the progress bar. The default is 0.</returns>
        public new int Value {
            get {
                return base.Value;
            }

            set {
                base.Value = value;

                // send signal to the taskbar.
                SetValueInTB();
            }
        }

        /// <summary>
        /// Gets or sets the manner in which progress should be indicated on the progress bar.
        /// </summary>
        /// <returns>One of the ProgressBarStyle values. The default is ProgressBarStyle.Blocks</returns>
        public new ProgressBarStyle Style {
            get {
                return base.Style;
            }

            set {
                base.Style = value;

                // set the style of the progress bar
                if (m_showInTaskbar && ownerForm is object) {
                    SetStateInTB();
                }
            }
        }

        /// <summary>
        /// The progress bar state for Windows Vista & 7
        /// </summary>
        [DefaultValue(ProgressBarState.Normal)]
        public ProgressBarState State {
            get {
                return m_State;
            }

            set {
                m_State = value;
                bool wasMarquee = Style == ProgressBarStyle.Marquee;
                if (wasMarquee) {
                    // sets the state to normal (and implicity calls SetStateInTB() )
                    Style = ProgressBarStyle.Blocks;
                }

                // set the progress bar state (Normal, Error, Paused)
                Windows7Taskbar.SendMessage(Handle, 0x410, (int)value, 0);
                if (wasMarquee) {
                    // the Taskbar PB value needs to be reset
                    SetValueInTB();
                } else {
                    // there wasn't a marquee, thus we need to update the taskbar
                    SetStateInTB();
                }
            }
        }

        /// <summary>
        /// Advances the current position of the progress bar by the specified amount.
        /// </summary>
        /// <param name="value">The amount by which to increment the progress bar's current position.</param>
        public new void Increment(int value) {
            base.Increment(value);

            // send signal to the taskbar.
            SetValueInTB();
        }

        /// <summary>
        /// Advances the current position of the progress bar by the amount of the System.Windows.Forms.ProgressBar.Step property.
        /// </summary>
        public new void PerformStep() {
            base.PerformStep();

            // send signal to the taskbar.
            SetValueInTB();
        }

        private void SetValueInTB() {
            if (m_showInTaskbar) {
                ulong _maximum = (ulong)(Maximum - Minimum);
                ulong _progress = (ulong)(Value - Minimum);
                Windows7Taskbar.SetProgressValue(ownerForm.Handle, _progress, _maximum);
            }
        }

        private void SetStateInTB() {
            if (ownerForm is null) {
                return;
            }

            var thmState = ThumbnailProgressState.Normal;
            if (!m_showInTaskbar) {
                thmState = ThumbnailProgressState.NoProgress;
            } else if (Style == ProgressBarStyle.Marquee) {
                thmState = ThumbnailProgressState.Indeterminate;
            } else if (m_State == ProgressBarState.Error) {
                thmState = ThumbnailProgressState.Error;
            } else if (m_State == ProgressBarState.Pause) {
                thmState = ThumbnailProgressState.Paused;
            }

            Windows7Taskbar.SetProgressState(ownerForm.Handle, thmState);
        }
    }
}
