using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public partial class WalkmanLib {
    // public version of ContextMenu.MenuFlags
    [Flags]
    public enum AddItemFlags : uint {
        /// <summary>Disables the menu item and grays it so it cannot be selected.</summary>
        Disabled = 0x1,
        /// <summary>Places a check mark next to the menu item.</summary>
        Checked = 0x8,
        /// <summary>Places the menu item on a new column, separated from the old column by a vertical line.</summary>
        VerticalBreak = 0x20,
        /// <summary>
        /// Draws a horizontal dividing line. The line cannot be disabled. The <c>text</c> and <c>action</c> parameters are ignored.
        /// </summary>
        Separator = 0x800
    }

    // based on the articles in https://stackoverflow.com/a/456922/2999220
    // lots of interface help from https://www.developerfusion.com/article/84363/into-the-iunknown/
    public class ContextMenu : IDisposable {
        #region Native Methods

        #region Enums
        // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shellexecuteinfow
        [Flags]
        private enum SeeMask : uint {
            /// <summary>Use default values.</summary>
            Default = 0x0,
            /// <summary>
            /// Use the class name given by the ShellExecuteInfo.lpClass member. If both <see cref="ClassKey"/> and
            /// <see cref="ClassName"/> are set, the class key is used.
            /// </summary>
            ClassName = 0x1,
            /// <summary>
            /// Use the class key given by the ShellExecuteInfo.hkeyClass member. If both <see cref="ClassKey"/> and
            /// <see cref="ClassName"/> are set, the class key is used.
            /// </summary>
            ClassKey = 0x3,
            /// <summary>
            /// Use the item identifier list given by the ShellExecuteInfo.lpIDList member. The ShellExecuteInfo.lpIDList member must point to an ITEMIDLIST structure.
            /// </summary>
            IDList = 0x4,
            /// <summary>
            /// Use the <see cref="IContextMenu"/> interface of the selected item's shortcut menu handler. Use either ShellExecuteInfo.lpFile to identify
            /// the item by its file system path or ShellExecuteInfo.lpIDList to identify the item by its PIDL. This flag allows applications to use
            /// ShellExecuteEx to invoke verbs from shortcut menu extensions instead of the static verbs listed in the registry.
            /// <br />Note: <see cref="InvokeIdList"/> overrides and implies <see cref="IDList"/>.
            /// </summary>
            InvokeIdList = 0xC,
            /// <summary>Use the icon given by the ShellExecuteInfo.hIcon member. This flag cannot be combined with <see cref="HMonitor"/>.
            /// <br/>Note: This flag is used only in Windows XP and earlier. It is ignored as of Windows Vista.</summary>
            Icon = 0x10,
            /// <summary>Use the keyboard shortcut given by the ShellExecuteInfo.dwHotKey member.</summary>
            HotKey = 0x20,
            /// <summary>
            /// Use to indicate that the ShellExecuteInfo.hProcess member receives the process handle. This handle is typically used to allow an application
            /// to find out when a process created with ShellExecuteEx terminates. In some cases, such as when execution is satisfied through a DDE conversation,
            /// no handle will be returned. The calling application is responsible for closing the handle when it is no longer needed.
            /// </summary>
            NoCloseProcess = 0x40,
            /// <summary>
            /// Validate the share and connect to a drive letter. This enables reconnection of disconnected network drives. The ShellExecuteInfo.lpFile member
            /// is a UNC path of a file on a network.
            /// </summary>
            ConnectNetDrv = 0x80,
            /// <summary>
            /// Wait for the execute operation to complete before returning. This flag should be used by callers that are using ShellExecute forms that might result
            /// in an async activation, for example DDE, and create a process that might be run on a background thread. (Note: ShellExecuteEx runs on a background
            /// thread by default if the caller's threading model is not Apartment.) Calls to ShellExecuteEx from processes already running on background threads
            /// should always pass this flag. Also, applications that exit immediately after calling ShellExecuteEx should specify this flag.
            /// <br/>If the execute operation is performed on a background thread and the caller did not specify the <see cref="AsyncOK"/> flag, then the calling thread
            /// waits until the new process has started before returning. This typically means that either CreateProcess has been called, the DDE communication has
            /// completed, or that the custom execution delegate has notified ShellExecuteEx that it is done. If the <see cref="WaitForInputIdle"/> flag is specified,
            /// then ShellExecuteEx calls WaitForInputIdle and waits for the new process to idle before returning, with a maximum timeout of 1 minute.
            /// </summary>
            NoAsync = 0x100,
            /// <summary>Expand any environment variables specified in the string given by the ShellExecuteInfo.lpDirectory or ShellExecuteInfo.lpFile member.</summary>
            DoEnvSubst = 0x200,
            /// <summary>Do not display an error message box if an error occurs.</summary>
            FlagNoUI = 0x400,
            /// <summary>Use this flag to indicate a Unicode application.</summary>
            Unicode = 0x4000,
            /// <summary>Use to inherit the parent's console for the new process instead of having it create a new console. It is the opposite of using
            /// a CREATE_NEW_CONSOLE flag with CreateProcess.</summary>
            NoConsole = 0x8000,
            //See https://reactos.org/archives/public/ros-diffs/2013-March/048151.html
            // the following three values are not defined in shellapi.h - however they are referenced
            // in ShObjIdl_code.h as flags for the CMInvokeCommandInfoEx structure.
            HasLinkName = 0x10000,
            HasTitle = 0x20000,
            FlagSepVDM = 0x40000,
            /// <summary>
            /// The execution can be performed on a background thread and the call should return immediately without waiting for the background thread to finish.
            /// Note that in certain cases ShellExecuteEx ignores this flag and waits for the process to finish before returning.
            /// </summary>
            AsyncOK = 0x100000,
            /// <summary>
            /// Use this flag when specifying a monitor on multi-monitor systems. The monitor is specified in the ShellExecuteInfo.hMonitor member.
            /// This flag cannot be combined with <see cref="icon"/>.
            /// </summary>
            HMonitor = 0x200000,
            /// <summary>Introduced in Windows XP. Do not perform a zone check. This flag allows ShellExecuteEx to bypass zone checking put into place by IAttachmentExecute.</summary>
            NoZoneChecks = 0x800000,
            /// <summary>Not used.</summary>
            NoQueryClassStore = 0x1000000,
            /// <summary>After the new process is created, wait for the process to become idle before returning, with a one minute timeout.</summary>
            WaitForInputIdle = 0x2000000,
            /// <summary>
            /// Introduced in Windows XP. Keep track of the number of times this application has been launched.
            /// Applications with sufficiently high counts appear in the Start Menu's list of most frequently used programs.
            /// </summary>
            FlagLogUsage = 0x4000000,
            /// <summary>
            /// The ShellExecuteInfo.hInstApp member is used to specify the IUnknown of an object that implements IServiceProvider. This object will be
            /// used as a site pointer. The site pointer is used to provide services to the ShellExecute function, the handler binding process, and invoked verb handlers.
            /// </summary>
            FlagHInstIsSite = 0x8000000U
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/ns-shobjidl_core-cminvokecommandinfoex
        [Flags]
        private enum CMICMask : uint {
            None = 0,
            /// <summary>The <see cref="CMInvokeCommandInfoEx.dwHotKey"/> member is valid.</summary>
            Hotkey = SeeMask.HotKey,
            /// <summary>The <see cref="CMInvokeCommandInfoEx.hIcon"/> member is valid. As of Windows Vista this flag is not used.</summary>
            Icon = SeeMask.Icon,
            /// <summary>
            /// The implementation of <see cref="IContextMenu.InvokeCommand"/> is prevented from displaying user
            /// interface elements (for example, error messages) while carrying out a command.
            /// </summary>
            FlagNoUI = SeeMask.FlagNoUI,
            /// <summary>
            /// The shortcut menu handler should use lpVerbW, lpParametersW, lpDirectoryW, and lpTitleW members instead of their ANSI equivalents.
            /// Because some shortcut menu handlers may not support Unicode, you should also pass valid ANSI strings in the lpVerb, lpParameters,
            /// lpDirectory, and lpTitle members.
            /// </summary>
            Unicode = SeeMask.Unicode,
            /// <summary>
            /// If a shortcut menu handler needs to create a new process, it will normally create a new console. Setting the <see cref="NoConsole"/>
            /// flag suppresses the creation of a new console.
            /// </summary>
            NoConsole = SeeMask.NoConsole,
            /// <summary>The <see cref="CMInvokeCommandInfoEx.lpTitle"/> member contains a full path to a shortcut file. Use in conjunction with <see cref="HasTitle"/>.</summary>
            HasLinkName = SeeMask.HasLinkName,
            /// <summary>The <see cref="CMInvokeCommandInfoEx.lpTitle"/> member is valid.</summary>
            HasTitle = SeeMask.HasTitle,
            /// <summary>This flag is valid only when referring to a 16-bit Windows-based application. If set, the application that the shortcut points to runs in a private Virtual DOS Machine (VDM).</summary>
            FlagSepVDM = SeeMask.FlagSepVDM,
            /// <summary>
            /// The implementation of <see cref="IContextMenu.InvokeCommand"/> can spin off a new thread or process to handle
            /// the call and does not need to block on completion of the function being invoked. For example, if the verb is "delete" the
            /// <see cref="IContextMenu.InvokeCommand"/> call may return before all of the items have been deleted. Since this
            /// is advisory, calling applications that specify this flag cannot guarantee that this request will be honored if they are not familiar with the
            /// implementation of the verb that they are invoking.
            /// </summary>
            AsyncOK = SeeMask.AsyncOK,
            /// <summary>
            /// Windows Vista and later. The implementation of <see cref="IContextMenu.InvokeCommand"/> should be synchronous,
            /// not returning before it is complete. Since this is recommended, calling applications that specify this flag cannot guarantee that this request
            /// will be honored if they are not familiar with the implementation of the verb that they are invoking.
            /// </summary>
            NoAsync = SeeMask.NoAsync,
            /// <summary>The SHIFT key is pressed. Use this instead of polling the current state of the keyboard that may have changed since the verb was invoked.</summary>
            ShiftDown = 0x10000000,
            /// <summary>The CTRL key is pressed. Use this instead of polling the current state of the keyboard that may have changed since the verb was invoked.</summary>
            ControlDown = 0x40000000,
            /// <summary>
            /// Indicates that the implementation of <see cref="IContextMenu.InvokeCommand"/> might want to keep track of the
            /// item being invoked for features like the "Recent documents" menu.
            /// </summary>
            FlagLogUsage = SeeMask.FlagLogUsage,
            /// <summary>Do not perform a zone check. This flag allows ShellExecuteEx to bypass zone checking put into place by IAttachmentExecute.</summary>
            NoZoneChecks = SeeMask.NoZoneChecks,
            /// <summary>The ptInvoke member is valid.</summary>
            PTInvoke = 0x20000000
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-getcommandstring
        [Flags]
        private enum GetCommandStringFlags : uint {
            /// <summary>Sets pszName to an ANSI string containing the help text for the command.</summary>
            HelpTextA = 0x1,
            /// <summary>Sets pszName to a Unicode string containing the help text for the command.</summary>
            HelpTextW = 0x5,
            /// <summary>Returns S_OK if the menu item exists, or S_FALSE otherwise.</summary>
            ValidateA = 0x2,
            /// <summary>Returns S_OK if the menu item exists, or S_FALSE otherwise.</summary>
            ValidateW = 0x6,
            /// <summary>Sets pszName to an ANSI string containing the language-independent command name for the menu item.</summary>
            VerbA = 0x0,
            /// <summary>Sets pszName to a Unicode string containing the language-independent command name for the menu item.</summary>
            VerbW = 0x4,
            /// <summary>(From C++ header) icon string (unicode)</summary>
            VerbIconW = 0x14,
            /// <summary>(From C++ header) for bit testing - Unicode string</summary>
            Unicode = 0x4
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-querycontextmenu
        [Flags]
        public enum QueryContextMenuFlags : uint {
            /// <summary>Indicates normal operation. A shortcut menu extension, namespace extension, or drag-and-drop handler can add all menu items.</summary>
            Normal = 0,
            /// <summary>
            /// The user is activating the default action, typically by double-clicking. This flag provides a hint for the shortcut menu extension to add nothing
            /// if it does not modify the default item in the menu. A shortcut menu extension or drag-and-drop handler should not add any menu items if this value
            /// is specified. A namespace extension should at most add only the default item.
            /// </summary>
            DefaultOnly = 1,
            /// <summary>The shortcut menu is that of a shortcut file (normally, a .lnk file). Shortcut menu handlers should ignore this value.</summary>
            VerbsOnly = 2,
            /// <summary>The Windows Explorer tree window is present.</summary>
            Explore = 4,
            /// <summary>This flag is set for items displayed in the Send To menu. Shortcut menu handlers should ignore this value.</summary>
            NoVerbs = 8,
            /// <summary>
            /// The calling application supports renaming of items. A shortcut menu or drag-and-drop handler should ignore this flag. A namespace extension should
            /// add a Rename item to the menu if applicable.
            /// </summary>
            CanRename = 0x10,
            /// <summary>
            /// No item in the menu has been set as the default. A drag-and-drop handler should ignore this flag. A namespace extension should not set any of the
            /// menu items as the default.
            /// </summary>
            NoDefault = 0x20,
            /// <summary>
            /// This value is not available.
            /// <br />Windows Server 2003 and Windows XP: A static menu is being constructed. Only the browser should use this flag; all other shortcut menu
            /// extensions should ignore it.
            /// </summary>
            IncludeStatic = 0x40,
            /// <summary>
            /// The calling application is invoking a shortcut menu on an item in the view (as opposed to the background of the view).
            /// <br />Windows Server 2003 and Windows XP: This value is not available.
            /// </summary>
            ItemMenu = 0x80,
            /// <summary>
            /// The calling application wants extended verbs. Normal verbs are displayed when the user right-clicks an object. To display extended verbs, the
            /// user must right-click while pressing the Shift key.
            /// </summary>
            ExtendedVerbs = 0x100,
            /// <summary>The calling application intends to invoke verbs that are disabled, such as legacy menus.</summary>
            DisabledVerbs = 0x200,
            /// <summary>
            /// The verb state can be evaluated asynchronously.
            /// <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            /// </summary>
            AsyncVerbState = 0x400,
            /// <summary>
            /// Informs context menu handlers that do not support the invocation of a verb through a canonical verb name to bypass
            /// <see cref="IContextMenu.QueryContextMenu"/> in their implementation.
            /// <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            /// </summary>
            OptimizeForInvoke = 0x800,
            /// <summary>
            /// Populate submenus synchronously.
            /// <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            /// </summary>
            SyncCascadeMenu = 0x1000,
            /// <summary>
            /// When no verb is explicitly specified, do not use a default verb in its place.
            /// <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            /// </summary>
            DoNotPickDefault = 0x2000,
            /// <summary>
            /// This flag is a bitmask that specifies all bits that should not be used. This is to be used only as a mask. Do not pass this as a parameter value.
            /// </summary>
            Reserved = 0xFFFF0000
        }

        // https://docs.microsoft.com/en-us/windows/win32/shell/sfgao
        // https://www.pinvoke.net/default.aspx/Enums/SFGAOF.html
        /// <summary>IShellFolder::GetAttributesOf flags</summary>
        [Flags]
        private enum SFGAO : uint {
            None = 0x0,
            /// <summary>The specified items can be copied.</summary>
            CanCopy = 0x1,
            /// <summary>The specified items can be moved.</summary>
            CanMove = 0x2,
            /// <summary>
            /// Shortcuts can be created for the specified items.
            /// <br/>If a namespace extension returns this attribute, a Create Shortcut entry with a default handler is added to the shortcut menu that
            /// is displayed during drag-and-drop operations. The extension can also implement its own handler for the link verb in place of the default.
            /// If the extension does so, it is responsible for creating the shortcut.
            /// <br/>A Create Shortcut item is also added to the Windows Explorer File menu and to normal shortcut menus.
            /// <br/>If the item is selected, your application's <see cref="IContextMenu.InvokeCommand"/> method is invoked
            /// with the <see cref="CMInvokeCommandInfoEx.lpVerb"/> member set to link. Your application is responsible for creating the link.
            /// </summary>
            CanLink = 0x4,
            /// <summary>The specified items can be bound to an IStorage object through IShellFolder::BindToObject.</summary>
            Storage = 0x8,
            /// <summary>The specified items can be renamed. Note that this value is essentially a suggestion; not all namespace clients allow items to be renamed. However, those that do must have this attribute set.</summary>
            CanRename = 0x10,
            /// <summary>The specified items can be deleted.</summary>
            CanDelete = 0x20,
            /// <summary>The specified items have property sheets.</summary>
            HasPropsSheet = 0x40,
            /// <summary>The specified items are drop targets.</summary>
            DropTarget = 0x100,
            /// <summary>
            /// This flag is a mask for the capability attributes: <see cref="CanCopy"/>, <see cref="CanMove"/>, <see cref="CanLink"/>, <see cref="CanRename"/>,
            /// <see cref="CanDelete"/>, <see cref="HasPropsSheet"/>, and <see cref="DropTarget"/>. Callers normally do not use this value.
            /// </summary>
            CapabilityMask = 0x177,
            /// <summary>File or folder is not fully present and recalled on open or access</summary>
            Placeholder = 0x800,
            /// <summary>Windows 7 and later. The specified items are system items.</summary>
            System = 0x1000,
            /// <summary>The specified items are encrypted and might require special presentation.</summary>
            Encrypted = 0x2000,
            /// <summary>Accessing the item is expected to be a slow operation. Applications should avoid accessing items flagged with <see cref="IsSlow"/>.</summary>
            IsSlow = 0x4000,
            /// <summary>The specified items are shown as dimmed and unavailable to the user.</summary>
            Ghosted = 0x8000,
            /// <summary>The specified items are shortcuts.</summary>
            Link = 0x10000,
            /// <summary>The specified objects are shared.</summary>
            Share = 0x20000,
            /// <summary>The specified items are read-only. In the case of folders, this means that new items cannot be created in those folders.</summary>
            ReadOnly = 0x40000,
            /// <summary>The item is hidden and should not be displayed unless the Show hidden files and folders option is enabled in Folder Settings.</summary>
            Hidden = 0x80000,
            /// <summary>Do not use.</summary>
            DisplayAttrMask = 0xFC000,
            /// <summary>The items are nonenumerated items and should be hidden. They are not returned through an enumerator such as that created by the IShellFolder::EnumObjects method.</summary>
            NonEnumerated = 0x100000,
            /// <summary>The items contain new content, as defined by the particular application.</summary>
            NewContent = 0x200000,
            /// <summary>
            /// Indicates that the item has a stream associated with it. That stream can be accessed through a call to IShellFolder::BindToObject or
            /// IShellItem::BindToHandler with IID_IStream in the riid parameter.
            /// </summary>
            Stream = 0x400000,
            /// <summary>Children of this item are accessible through IStream or IStorage. Those children are flagged with <see cref="Storage"/> or <see cref="Stream"/>.</summary>
            StorageAncestor = 0x800000,
            /// <summary>
            /// When specified as input, instructs the folder to validate that the items contained in a folder or Shell item array exist. If one or more of those items
            /// do not exist, IShellFolder::GetAttributesOf and IShellItemArray::GetAttributes return a failure code. This flag is never returned as an [out] value.
            /// <br/>When used with the file system folder, instructs the folder to discard cached properties retrieved by clients of IShellFolder2::GetDetailsEx that
            /// might have accumulated for the specified items.
            /// </summary>
            Validate = 0x1000000,
            /// <summary>The specified items are on removable media or are themselves removable devices.</summary>
            Removable = 0x2000000,
            /// <summary>The specified items are compressed.</summary>
            Compressed = 0x4000000,
            /// <summary>The specified items can be hosted inside a web browser or Windows Explorer frame.</summary>
            Browsable = 0x8000000,
            /// <summary>The specified folders are either file system folders or contain at least one descendant (child, grandchild, or later) that is a file system (<see cref="Filesystem"/>) folder.</summary>
            FileSysAncestor = 0x10000000,
            /// <summary>
            /// The specified items are folders. Some items can be flagged with both <see cref="Stream"/> and <see cref="Folder"/>, such as a compressed file with a .zip
            /// file name extension. Some applications might include this flag when testing for items that are both files and containers.
            /// </summary>
            Folder = 0x20000000,
            /// <summary>
            /// The specified folders or files are part of the file system (that is, they are files, directories, or root directories). The parsed names of the items can
            /// be assumed to be valid Win32 file system paths. These paths can be either UNC or drive-letter based.
            /// </summary>
            Filesystem = 0x40000000,
            /// <summary>
            /// This flag is a mask for the storage capability attributes: <see cref="Storage"/>, <see cref="Link"/>, <see cref="ReadOnly"/>, <see cref="Stream"/>,
            /// <see cref="StorageAncestor"/>, <see cref="FileSysAncestor"/>, <see cref="Folder"/>, and <see cref="Filesystem"/>. Callers normally do not use this value.
            /// </summary>
            StorageCapMask = 0x70C50008,
            /// <summary>
            /// The specified folders have subfolders. This attribute is only advisory and might be returned by Shell folder implementations even if they do not contain
            /// subfolders. Note, however, that the converse — failing to return <see cref="HasSubFolder"/> — definitively states that the folder objects do not have subfolders.
            /// <br/>Returning <see cref="HasSubFolder"/> is recommended whenever a significant amount of time is required to determine whether any subfolders exist.
            /// For example, the Shell always returns <see cref="HasSubFolder"/> when a folder is located on a network drive.
            /// </summary>
            HasSubFolder = 0x80000000,
            /// <summary>This flag is a mask for content attributes, at present only <see cref="HasSubFolder"/>. Callers normally do not use this value.</summary>
            ContentsMask = 0x80000000,
            /// <summary>
            /// Mask used by the PKEY_SFGAOFlags property to determine attributes that are considered to cause slow calculations or lack context:
            /// <see cref="IsSlow"/>, <see cref="ReadOnly"/>, <see cref="HasSubFolder"/>, and <see cref="Validate"/>. Callers normally do not use this value.
            /// </summary>
            PKeySFGAOMask = 0x81044000
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-trackpopupmenuex
        [Flags]
        private enum TrackPopupMenuExFlags : uint {
            /// <summary>The user can select menu items with only the left mouse button.</summary>
            LeftButton = 0x0,
            /// <summary>The user can select menu items with both the left and right mouse buttons.</summary>
            RightButton = 0x2,

            /// <summary>Positions the shortcut menu so that its left side is aligned with the coordinate specified by the x parameter.</summary>
            LeftAlign = 0x0,
            /// <summary>Centers the shortcut menu horizontally relative to the coordinate specified by the x parameter.</summary>
            CenterAlign = 0x4,
            /// <summary>Positions the shortcut menu so that its right side is aligned with the coordinate specified by the x parameter.</summary>
            RightAlign = 0x8,

            /// <summary>Positions the shortcut menu so that its top side is aligned with the coordinate specified by the y parameter.</summary>
            TopAlign = 0x0,
            /// <summary>Centers the shortcut menu vertically relative to the coordinate specified by the y parameter.</summary>
            VCenterAlign = 0x10,
            /// <summary>Positions the shortcut menu so that its bottom side is aligned with the coordinate specified by the y parameter.</summary>
            BottomAlign = 0x20,

            /// <summary>
            /// If the menu cannot be shown at the specified location without overlapping the excluded rectangle, the system tries to accommodate the
            /// requested horizontal alignment before the requested vertical alignment.
            /// </summary>
            Horizontal = 0x0,
            /// <summary>
            /// If the menu cannot be shown at the specified location without overlapping the excluded rectangle, the system tries to accommodate the
            /// requested vertical alignment before the requested horizontal alignment.
            /// </summary>
            Vertical = 0x40,

            /// <summary>The function does not send notification messages when the user clicks a menu item.</summary>
            NoNotify = 0x80,
            /// <summary>The function returns the menu item identifier of the user's selection in the return value.</summary>
            ReturnCmd = 0x100,

            /// <summary>Use the this flag to display a menu when another menu is already displayed. This is intended to support context menus within a menu.</summary>
            Recurse = 0x1,

            /// <summary>Animates the menu from left to right.</summary>
            HorPosAnimation = 0x400,
            /// <summary>Animates the menu from right to left.</summary>
            HorNegAnimation = 0x800,
            /// <summary>Animates the menu from top to bottom.</summary>
            VerPosAnimation = 0x1000,
            /// <summary>Animates the menu from bottom to top.</summary>
            VerNegAnimation = 0x2000,
            /// <summary>Displays menu without animation.</summary>
            NoAnimation = 0x4000,

            /// <summary>For right-to-left text layout, use this flag. By default, the text layout is left-to-right.</summary>
            LayoutRTL = 0x8000,

            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-calculatepopupwindowposition#tpm_workarea
            /// <summary>
            /// Restricts the pop-up window to within the work area. If this flag is not set, the pop-up window is restricted
            /// to the work area only if the input point is within the work area.
            /// </summary>
            WorkArea = 0x10000
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        [Flags]
        private enum ShowWindowFlags : int {
            /// <summary>Hides the window and activates another window.</summary>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.</summary>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position. This value is similar to <see cref="ShowNormal"/>, except that the window is not activated.</summary>
            ShowNormalNoActive = 4,
            /// <summary>Activates the window and displays it in its current size and position.</summary>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next top-level window in the Z order.</summary>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is similar to <see cref="ShowMinimized"/>, except the window is not activated.</summary>
            ShowMinimizedNoActive = 7,
            /// <summary>Displays the window in its current size and position. This value is similar to <see cref="Show"/>, except that the window is not activated.</summary>
            ShowNoActive = 8,
            /// <summary>Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.</summary>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.</summary>
            ShowDefault = 10,
            /// <summary>Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.</summary>
            ForceMinimize = 11
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-insertmenuw
        [Flags]
        private enum MenuFlags : uint {
            /// <summary>
            /// Indicates that the uPosition parameter gives the identifier of the menu item. This flag is the default if neither the
            /// <see cref="ByCommand"/> nor <see cref="ByPosition"/> flag is specified.
            /// </summary>
            ByCommand = 0x0,
            /// <summary>
            /// Indicates that the uPosition parameter gives the zero-based relative position of the new menu item.
            /// If uPosition is -1, the new menu item is appended to the end of the menu.
            /// </summary>
            ByPosition = 0x400,

            /// <summary>Enables the menu item so that it can be selected and restores it from its grayed state.</summary>
            Enabled = 0x0,
            /// <summary>Disables the menu item and grays it so it cannot be selected.</summary>
            Grayed = 0x1,
            /// <summary>Disables the menu item so that it cannot be selected, but does not gray it.</summary>
            Disabled = 0x2,
            /// <summary>Places a check mark next to the menu item. If the application provides check-mark bitmaps, this flag displays the check-mark bitmap next to the menu item.</summary>
            Checked = 0x8,
            /// <summary>
            /// Does not place a check mark next to the menu item (default). If the application supplies check-mark bitmaps,
            /// this flag displays the clear bitmap next to the menu item.
            /// </summary>
            Unchecked = 0x0,

            /// <summary>Specifies that the menu item is a text string; the lpNewItem parameter is a pointer to the string.</summary>
            String = 0x0,
            /// <summary>Uses a bitmap as the menu item. The lpNewItem parameter contains a handle to the bitmap.</summary>
            Bitmap = 0x4,
            /// <summary>
            /// Specifies that the item is an owner-drawn item. Before the menu is displayed for the first time, the window that owns the menu receives a
            /// WM_MEASUREITEM message to retrieve the width and height of the menu item. The WM_DRAWITEM message is then sent to the window procedure of
            /// the owner window whenever the appearance of the menu item must be updated.
            /// </summary>
            OwnerDraw = 0x100,

            /// <summary>
            /// Specifies that the menu item opens a drop-down menu or submenu. The uIDNewItem parameter specifies a handle to the drop-down menu or submenu.
            /// This flag is used to add a menu name to a menu bar or a menu item that opens a submenu to a drop-down menu, submenu, or shortcut menu.
            /// </summary>
            Popup = 0x10,
            /// <summary>
            /// Functions the same as the <see cref="MenuBreak"/> flag for a menu bar. For a drop-down menu, submenu, or shortcut menu,
            /// the new column is separated from the old column by a vertical line.
            /// </summary>
            MenuBarBreak = 0x20,
            /// <summary>Places the item on a new line (for menu bars) or in a new column (for a drop-down menu, submenu, or shortcut menu) without separating columns.</summary>
            MenuBreak = 0x40,
            /// <summary>
            /// Draws a horizontal dividing line. This flag is used only in a drop-down menu, submenu, or shortcut menu. The line cannot be grayed, disabled,
            /// or highlighted. The lpNewItem and uIDNewItem parameters are ignored.
            /// </summary>
            Separator = 0x800
        }

        // from ShlGuid.h - line 50 in Windows Kit 10.0.18362.0
        // declared as a structure as Enums have to be of "Integral" types
        private struct IID {
            public static readonly Guid INewShortcutHookA = new(0x214E1, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellBrowser = new(0x214E2, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellView = new(0x214E3, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IContextMenu = new(0x214E4, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellIcon = new(0x214E5, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellFolder = new(0x214E6, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellExtInit = new(0x214E8, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellPropSheetExt = new(0x214E9, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IPersistFolder = new(0x214EA, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IExtractIconA = new(0x214EB, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellDetails = new(0x214EC, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellLinkA = new(0x214EE, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid ICopyHookA = new(0x214EF, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IFileViewerA = new(0x214F0, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid ICommDlgBrowser = new(0x214F1, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IEnumIDList = new(0x214F2, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IFileViewerSite = new(0x214F3, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IContextMenu2 = new(0x214F4, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellExecuteHookA = new(0x214F5, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IPropSheetPage = new(0x214F6, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid INewShortcutHookW = new(0x214F7, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IFileViewerW = new(0x214F8, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellLinkW = new(0x214F9, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IExtractIconW = new(0x214FA, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IShellExecuteHookW = new(0x214FB, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid ICopyHookW = new(0x214FC, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IRemoteComputer = new(0x214FE, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IQueryInfo = new(0x21500, 0, 0, 0xC0, 0, 0, 0, 0, 0, 0, 0x46);
            public static readonly Guid IBriefcaseStg = new(0x8BCE1FA1, 0x921, 0x101B, 0xB1, 0xFF, 0x0, 0xDD, 0x1, 0xC, 0xCC, 0x48);
            public static readonly Guid IShellView2 = new(0x88E39E80, 0x3578, 0x11CF, 0xAE, 0x69, 0x8, 0x0, 0x2B, 0x2E, 0x12, 0x62);
            public static readonly Guid IURLSearchHook = new(0xAC60F6A0, 0xFD9, 0x11D0, 0x99, 0xCB, 0x0, 0xC0, 0x4F, 0xD6, 0x44, 0x97);
            public static readonly Guid ISearchContext = new(0x9F656A2, 0x41AF, 0x480C, 0x88, 0xF7, 0x16, 0xCC, 0xD, 0x16, 0x46, 0x15);
            public static readonly Guid IURLSearchHook2 = new(0x5EE44DA4, 0x6D32, 0x46E3, 0x86, 0xBC, 0x7, 0x54, 0xD, 0xED, 0xD0, 0xE0);
            public static readonly Guid IDefViewID = new(0x985F64F0, 0xD410, 0x4E02, 0xBE, 0x22, 0xDA, 0x7, 0xF2, 0xB5, 0xC5, 0xE1);
            public static readonly Guid IDockingWindowSite = new(0x2A342FC, 0x7B26, 0x11D0, 0x8C, 0xA9, 0x0, 0xA0, 0xC9, 0x2D, 0xBF, 0xE8);
            public static readonly Guid IDockingWindowFrame = new(0x47D2657, 0x7B27, 0x11D0, 0x8C, 0xA9, 0x0, 0xA0, 0xC9, 0x2D, 0xBF, 0xE8);
            public static readonly Guid IShellIconOverlay = new(0x7D688A70, 0xC613, 0x11D0, 0x99, 0x9B, 0x0, 0xC0, 0x4F, 0xD6, 0x55, 0xE1);
            public static readonly Guid IShellIconOverlayIdentifier = new(0xC6C4200, 0xC589, 0x11D0, 0x99, 0x9A, 0x0, 0xC0, 0x4F, 0xD6, 0x55, 0xE1);
            public static readonly Guid ICommDlgBrowser2 = new(0x1033951, 0x2894, 0x11D2, 0x90, 0x39, 0x0, 0xC0, 0x4F, 0x8E, 0xEB, 0x3E);
            public static readonly Guid IShellFolderViewCB = new(0x2047E320, 0xF2A9, 0x11CE, 0xAE, 0x65, 0x8, 0x0, 0x2B, 0x2E, 0x12, 0x62);
            public static readonly Guid IShellIconOverlayManager = new(0xF10B5E34, 0xDD3B, 0x42A7, 0xAA, 0x7D, 0x2F, 0x4E, 0xC5, 0x4B, 0xB0, 0x9B);
            public static readonly Guid IThumbnailCapture = new(0x4EA3926, 0x7211, 0x409F, 0xB6, 0x22, 0xF6, 0x3D, 0xBD, 0x16, 0xC5, 0x33);
            public static readonly Guid IShellImageStore = new(0x48C8118, 0xB924, 0x11D1, 0x98, 0xD5, 0x0, 0xC0, 0x4F, 0xB6, 0x87, 0xDA);
            public static readonly Guid IContextMenu3 = new(0xBCFCE0A, 0xEC17, 0x11D0, 0x8D, 0x10, 0x0, 0xA0, 0xC9, 0xF, 0x27, 0x19);
            public static readonly Guid IShellFolderBand = new(0x7FE80CC, 0xC247, 0x11D0, 0xB9, 0x3A, 0x0, 0xA0, 0xC9, 0x3, 0x12, 0xE1);
            public static readonly Guid IDefViewFrame = new(0x710EB7A0, 0x45ED, 0x11D0, 0x92, 0x4A, 0x0, 0x20, 0xAF, 0xC7, 0xAC, 0x4D);
            public static readonly Guid IDiscardableBrowserProperty = new(0x49C3DE7, 0xD329, 0x11D0, 0xAB, 0x73, 0x0, 0xC0, 0x4F, 0xC3, 0x3E, 0x80);
            public static readonly Guid IShellChangeNotify = new(0xD82BE2B1, 0x5764, 0x11D0, 0xA9, 0x6E, 0x0, 0xC0, 0x4F, 0xD7, 0x5, 0xA2);
            public static readonly Guid IObjMgr = new(0xBB2761, 0x6A77, 0x11D0, 0xA5, 0x35, 0x0, 0xC0, 0x4F, 0xD7, 0xD0, 0x62);
            public static readonly Guid IACList = new(0x77A130B0, 0x94FD, 0x11D0, 0xA5, 0x44, 0x0, 0xC0, 0x4F, 0xD7, 0xD0, 0x62);
            public static readonly Guid IACList2 = new(0x470141A0, 0x5186, 0x11D2, 0xBB, 0xB6, 0x0, 0x60, 0x97, 0x7B, 0x46, 0x4C);
            public static readonly Guid ICurrentWorkingDirectory = new(0x91956D21, 0x9276, 0x11D1, 0x92, 0x1A, 0x0, 0x60, 0x97, 0xDF, 0x5B, 0xD4);
            public static readonly Guid IProgressDialog = new(0xEBBC7C0, 0x315E, 0x11D2, 0xB6, 0x2F, 0x0, 0x60, 0x97, 0xDF, 0x5B, 0xD4);
            public static readonly Guid IActiveDesktop = new(0xF490EB00, 0x1240, 0x11D1, 0x98, 0x88, 0x0, 0x60, 0x97, 0xDE, 0xAC, 0xF9);
            public static readonly Guid IActiveDesktopP = new(0x52502EE0, 0xEC80, 0x11D0, 0x89, 0xAB, 0x0, 0xC0, 0x4F, 0xC2, 0x97, 0x2D);
            public static readonly Guid IADesktopP2 = new(0xB22754E, 0x4574, 0x11D1, 0x98, 0x88, 0x0, 0x60, 0x97, 0xDE, 0xAC, 0xF9);
            public static readonly Guid ISynchronizedCallBack = new(0x74C2604, 0x70D1, 0x11D1, 0xB7, 0x5A, 0x0, 0xA0, 0xC9, 0x5, 0x64, 0xFE);
            public static readonly Guid IQueryAssociations = new(0xC46CA59, 0x3C3F, 0x11D2, 0xBE, 0xE6, 0x0, 0x0, 0xF8, 0x5, 0xCA, 0x57);
            public static readonly Guid IColumnProvider = new(0xE802500, 0x1C42, 0x11D2, 0xBE, 0x2C, 0x0, 0xA0, 0xC9, 0xA8, 0x3D, 0xA1);
            public static readonly Guid INamedPropertyBag = new(0xFB70043, 0x952C, 0x11D1, 0x94, 0x6F, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0);
            public static readonly Guid IShellFolder2 = new(0x93F2F68, 0x1D1B, 0x11D3, 0xA3, 0xE, 0x0, 0xC0, 0x4F, 0x79, 0xAB, 0xD1);
            public static readonly Guid IEnumExtraSearch = new(0xE700BE, 0x9DB6, 0x11D1, 0xA1, 0xCE, 0x0, 0xC0, 0x4F, 0xD7, 0x5D, 0x13);
            public static readonly Guid IDocViewSite = new(0x87D605E, 0xC511, 0x11CF, 0x89, 0xA9, 0x0, 0xA0, 0xC9, 0x5, 0x41, 0x29);
            public static readonly Guid CDefView = new(0x4434FF8, 0xEF4C, 0x11CE, 0xAE, 0x65, 0x8, 0x0, 0x2B, 0x2E, 0x12, 0x62);
            public static readonly Guid IBanneredBar = new(0x596A9A9, 0x13E, 0x11D1, 0x8D, 0x34, 0x0, 0xA0, 0xC9, 0xF, 0x27, 0x19);
        }
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct ComPoint {
            public int x;
            public int y;

            public ComPoint(Point pos) {
                x = pos.X;
                y = pos.Y;
            }
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/ns-shobjidl_core-cminvokecommandinfoex
        /// <summary>Contains extended information about a shortcut menu command.</summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct CMInvokeCommandInfoEx {
            /// <summary>
            /// The size of this structure, in bytes. This member should be filled in by callers of <see cref="IContextMenu.InvokeCommand"/> and tested by the
            /// implementations to know that the structure is a <see cref="CMInvokeCommandInfoEx"/> structure rather than CMInvokeCommandInfo.
            /// </summary>
            public uint cbSize;
            /// <summary>Zero, or one or more of the <see cref="CMICMask"/> flags are set to indicate desired behavior and indicate that other fields in the structure are to be used.</summary>
            public CMICMask fMask;
            /// <summary>
            /// A handle to the window that is the owner of the shortcut menu. An extension can also use this handle as the owner of any message boxes or dialog boxes
            /// it displays. Callers must specify a legitimate HWND that can be used as the owner window for any UI that may be displayed. Failing to specify an HWND
            /// when calling from a UI thread (one with windows already created) will result in reentrancy and possible bugs in the implementation of a
            /// <see cref="IContextMenu.InvokeCommand"/> call.
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// The address of a null-terminated string that specifies the language-independent name of the command to carry out. This member is typically a string
            /// when a command is being activated by an application.
            /// <br/>If a canonical verb exists and a menu handler does not implement the canonical verb, it must return a failure code to enable the next handler
            /// to be able to handle this verb. Failing to do this will break functionality in the system including ShellExecute.
            /// <br/>Alternatively, rather than a pointer, this parameter can be MAKEINTRESOURCE(offset) where offset is the menu-identifier offset of the command
            /// to carry out. Implementations can use the IS_INTRESOURCE macro to detect that this alternative is being employed. The Shell uses this alternative
            /// when the user chooses a menu command.
            /// <br/><br/>.Net Implementation: This member is an IntPtr so indexes can be passed - use <see cref="Marshal.StringToHGlobalAnsi"/> to use a string.
            /// </summary>
            public IntPtr lpVerb;
            /// <summary>Optional parameters. This member is always NULL for menu items inserted by a Shell extension.</summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpParameters;
            /// <summary>An optional working directory name. This member is always NULL for menu items inserted by a Shell extension.</summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpDirectory;
            /// <summary>A set of <see cref="ShowWindowFlags"/> values to pass to the ShowWindow function if the command displays a window or starts an application.</summary>
            public ShowWindowFlags nShow;
            /// <summary>
            /// An optional keyboard shortcut to assign to any application activated by the command. If the <see cref="fMask"/> member does not specify
            /// <see cref="CMICMask.Hotkey"/>, this member is ignored.
            /// </summary>
            public uint dwHotKey;
            /// <summary>
            /// An icon to use for any application activated by the command. If the <see cref="fMask"/> member does not specify
            /// <see cref="CMICMask.Icon"/>, this member is ignored.
            /// </summary>
            public IntPtr hIcon;
            /// <summary>An ASCII title.</summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpTitle;
            /// <summary>
            /// A Unicode verb, for those commands that can use it.
            /// <br/>This member is an IntPtr so indexes can be passed - use <see cref="Marshal.StringToHGlobalUni"/> to use a string.
            /// </summary>
            public IntPtr lpVerbW;
            /// <summary>A Unicode parameters, for those commands that can use it.</summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpParametersW;
            /// <summary>A Unicode directory, for those commands that can use it.</summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpDirectoryW;
            /// <summary>A Unicode title.</summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpTitleW;
            /// <summary>
            /// The point where the command is invoked. If the <see cref="fMask"/> member does not specify <see cref="CMICMask.PTInvoke"/>,
            /// this member is ignored. This member is not valid prior to Internet Explorer 4.0.
            /// </summary>
            public ComPoint ptInvoke;
        }
        #endregion

        #region Interfaces
        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-icontextmenu
        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E4-0000-0000-c000-000000000046")]
        private interface IContextMenu {
            // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-querycontextmenu
            /// <summary>Adds commands to a shortcut menu.</summary>
            /// <param name="hMenu">A handle to the shortcut menu. The handler should specify this handle when adding menu items.</param>
            /// <param name="indexMenu">The zero-based position at which to insert the first new menu item.</param>
            /// <param name="idCmdFirst">The minimum value that the handler can specify for a menu item identifier.</param>
            /// <param name="idCmdLast">The maximum value that the handler can specify for a menu item identifier.</param>
            /// <param name="uFlags">Optional flags that specify how the shortcut menu can be changed.</param>
            /// <returns>
            /// If successful, returns an HRESULT value that has its severity value set to SEVERITY_SUCCESS and its code value set to the offset of the largest command
            /// identifier that was assigned, plus one. For example, if <paramref name="idCmdFirst"/> is set to 5 and you add three items to the menu with command
            /// identifiers of 5, 7, and 8, the return value should be MAKE_HRESULT(SEVERITY_SUCCESS, 0, 8 - 5 + 1). Otherwise, it returns a COM error value.
            /// </returns>
            void QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, QueryContextMenuFlags uFlags);

            // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-invokecommand
            /// <summary>Carries out the command associated with a shortcut menu item.</summary>
            /// <param name="pici">A pointer to a CMINVOKECOMMANDINFO or CMINVOKECOMMANDINFOEX structure that contains specifics about the command.</param>
            /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            void InvokeCommand(ref CMInvokeCommandInfoEx pici);

            // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-getcommandstring
            /// <summary>
            /// Gets information about a shortcut menu command, including the help string and the language-independent, or canonical, name for the command.
            /// </summary>
            /// <param name="idCmd">Menu command identifier offset.</param>
            /// <param name="uType">Flags specifying the information to return.</param>
            /// <param name="pReserved">Reserved. Applications must specify NULL when calling this method and handlers must ignore this parameter when called.</param>
            /// <param name="pszName">The address of the buffer to receive the null-terminated string being retrieved.</param>
            /// <param name="cchMax">Size of the buffer, in characters, to receive the null-terminated string.</param>
            /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            void GetCommandString(
                UIntPtr idCmd,
                GetCommandStringFlags uType,
                out uint pReserved,
                IntPtr pszName,
                uint cchMax);
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-icontextmenu2
        [ComImport, Guid("000214f4-0000-0000-c000-000000000046")]
        private interface IContextMenu2 : IContextMenu {
            // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu2-handlemenumsg
            /// <summary>Enables client objects of the <see cref="IContextMenu"/> interface to handle messages associated with owner-drawn menu items.</summary>
            /// <param name="uMsg">The message to be processed. In the case of some messages, such as WM_INITMENUPOPUP, WM_DRAWITEM, WM_MENUCHAR, or WM_MEASUREITEM, the client object being called may provide owner-drawn menu items.</param>
            /// <param name="wParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            /// <param name="lParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            void HandleMenuMsg(int uMsg, IntPtr wParam, IntPtr lParam);
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-icontextmenu3
        [ComImport, Guid("BCFCE0A0-EC17-11d0-8D10-00A0C90F2719")]
        private interface IContextMenu3 : IContextMenu2 {
            // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu3-handlemenumsg2
            /// <summary>Allows client objects of the <see cref="IContextMenu3"/> interface to handle messages associated with owner-drawn menu items.</summary>
            /// <param name="uMsg">The message to be processed. In the case of some messages, such as WM_INITMENUPOPUP, WM_DRAWITEM, WM_MENUCHAR, or WM_MEASUREITEM, the client object being called may provide owner-drawn menu items.</param>
            /// <param name="wParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            /// <param name="lParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            /// <param name="plResult">The address of an LRESULT value that the owner of the menu will return from the message. This parameter can be NULL.</param>
            /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            void HandleMenuMsg2(int uMsg, IntPtr wParam, IntPtr lParam, IntPtr plResult);
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-ishellfolder
        // https://www.pinvoke.net/default.aspx/Interfaces/IShellFolder.html
        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E6-0000-0000-C000-000000000046")]
        private interface IShellFolder {
            // interface members are stubs, because GetUIObjectOf is the only function needed - and there are lots of members of this interface.
            void ParseDisplayName();
            void EnumObjects();
            void BindToObject();
            void BindToStorage();
            void CompareIDs();
            void CreateViewObject();
            void GetAttributesOf();

            // https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishellfolder-getuiobjectof
            /// <summary>Gets an object that can be used to carry out actions on the specified file objects or folders.</summary>
            /// <param name="hwndOwner">A handle to the owner window that the client should specify if it displays a dialog box or message box.</param>
            /// <param name="cIDList">The number of file objects or subfolders specified in the apidl parameter.</param>
            /// <param name="apIDList">
            /// The address of an array of pointers to ITEMIDLIST structures, each of which uniquely identifies a file object or subfolder relative to the
            /// parent folder. Each item identifier list must contain exactly one SHITEMID structure followed by a terminating zero.
            /// </param>
            /// <param name="riid">A reference to the IID of the interface to retrieve through ppv. This can be any valid interface identifier that can be created for an item.</param>
            /// <param name="rgfReserved">Reserved.</param>
            /// <param name="ppv">When this method returns successfully, contains the interface pointer requested in <paramref name="riid"/>.</param>
            /// <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            void GetUIObjectOf(
                IntPtr hwndOwner,
                uint cIDList,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
                IntPtr[] apIDList,
                ref Guid riid,
                out uint rgfReserved,
                [MarshalAs(UnmanagedType.Interface)]
                out object ppv);

            void GetDisplayNameOf();
            void SetNameOf();
        }
        #endregion

        #region Methods
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createpopupmenu
        /// <summary>
        /// Creates a drop-down menu, submenu, or shortcut menu. The menu is initially empty. You can insert or append menu items by using the InsertMenuItem
        /// function. You can also use the InsertMenu function to insert menu items and the AppendMenu function to append menu items.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the newly created menu.
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreatePopupMenu();

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroymenu
        /// <summary>Destroys the specified menu and frees any memory that the menu occupies.</summary>
        /// <param name="hMenu">A handle to the menu to be destroyed.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool DestroyMenu(IntPtr hMenu);

        // https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shparsedisplayname
        /// <summary>
        /// Translates a Shell namespace object's display name into an item identifier list and returns the attributes of the object.
        /// This function is the preferred method to convert a string to a pointer to an item identifier list (PIDL).
        /// </summary>
        /// <param name="pszName">A pointer to a zero-terminated wide string that contains the display name to parse.</param>
        /// <param name="pbc">A bind context that controls the parsing operation. This parameter is normally set to NULL. Type: IBindCtx*</param>
        /// <param name="ppIDList">The address of a pointer to a variable of type ITEMIDLIST that receives the item identifier list for the object. If an error occurs, then this parameter is set to NULL.</param>
        /// <param name="sfgaoIn">A ULONG value that specifies the attributes to query. To query for one or more attributes, initialize this parameter with the flags that represent the attributes of interest.</param>
        /// <param name="psfgaoOut">A pointer to a ULONG. On return, those attributes that are true for the object and were requested in <paramref name="sfgaoIn"/> are set. An object's attribute flags can be zero or a combination of SFGAO flags.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SHParseDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] 
            string pszName,
            IntPtr pbc,
            out IntPtr ppIDList,
            SFGAO sfgaoIn,
            out SFGAO psfgaoOut);

        // https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shbindtoparent
        /// <summary>
        /// Takes a pointer to a fully qualified item identifier list (<paramref name="pIDList"/>), and returns a specified interface pointer on the parent object.
        /// </summary>
        /// <param name="pIDList">The item's PIDList.</param>
        /// <param name="riid">The REFIID of one of the interfaces exposed by the item's parent object.</param>
        /// <param name="ppv">A pointer to the interface specified by <paramref name="riid"/>. You must release the object when you are finished.</param>
        /// <param name="ppIDListLast">
        /// The item's PIDList relative to the parent folder. This PIDList can be used with many of the methods supported by the parent folder's interfaces.
        /// If you set <paramref name="ppIDListLast"/> to NULL, the PIDList is not returned.
        /// <br/>Note: SHBindToParent does not allocate a new PIDList; it simply receives a pointer through this parameter.
        /// Therefore, you are not responsible for freeing this resource.
        /// </param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SHBindToParent(
            IntPtr pIDList,
            [In] ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] 
            out object ppv,
            out IntPtr ppIDListLast);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-insertmenuw
        /// <summary>Inserts a new menu item into a menu, moving other items down the menu.</summary>
        /// <param name="hMenu">A handle to the menu to be changed.</param>
        /// <param name="uPosition">The menu item before which the new menu item is to be inserted, as determined by the <paramref name="uFlags"/> parameter.</param>
        /// <param name="uFlags">Controls the interpretation of the <paramref name="uPosition"/> parameter and the content, appearance, and behavior of the new menu item.</param>
        /// <param name="uIDNewItem">The identifier of the new menu item or, if the uFlags parameter has the <see cref="MenuFlags.Popup"/> flag set, a handle to the drop-down menu or submenu.</param>
        /// <param name="lpNewItem">The content of the new menu item. The interpretation of <paramref name="lpNewItem"/> depends on whether the <paramref name="uFlags"/> parameter includes the <see cref="MenuFlags.Bitmap"/>, <see cref="MenuFlags.OwnerDraw"/>, or <see cref="MenuFlags.String"/> flag.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool InsertMenu(
            IntPtr hMenu,
            int uPosition,
            MenuFlags uFlags,
            UIntPtr uIDNewItem,
            [MarshalAs(UnmanagedType.LPWStr)] 
            string lpNewItem);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-trackpopupmenuex
        /// <summary>
        /// Displays a shortcut menu at the specified location and tracks the selection of items on the shortcut menu. The shortcut menu can appear anywhere on the screen.
        /// </summary>
        /// <param name="hMenu">
        /// A handle to the shortcut menu to be displayed. This handle can be obtained by calling the <see cref="CreatePopupMenu"/> function to create a new shortcut
        /// menu or by calling the GetSubMenu function to retrieve a handle to a submenu associated with an existing menu item.
        /// </param>
        /// <param name="uFlags">Specifies function options.</param>
        /// <param name="x">The horizontal location of the shortcut menu, in screen coordinates.</param>
        /// <param name="y">The vertical location of the shortcut menu, in screen coordinates.</param>
        /// <param name="hwnd">
        /// A handle to the window that owns the shortcut menu. This window receives all messages from the menu. The window does not receive a WM_COMMAND
        /// message from the menu until the function returns. If you specify <see cref="TrackPopupMenuExFlags.NoNotify"/> in the <paramref name="uFlags"/> parameter,
        /// the function does not send messages to the window identified by <paramref name="hwnd"/>. However, you must still pass a window handle in <paramref name="hwnd"/>.
        /// It can be any window handle from your application.
        /// </param>
        /// <param name="lptpm">A pointer to a TPMPARAMS structure that specifies an area of the screen the menu should not overlap. This parameter can be NULL.</param>
        /// <returns>
        /// If you specify <see cref="TrackPopupMenuExFlags.ReturnCmd"/> in the <paramref name="uFlags"/> parameter, the return value is the menu-item identifier of
        /// the item that the user selected. If the user cancels the menu without making a selection, or if an error occurs, the return value is zero.
        /// <br/>If you do not specify <see cref="TrackPopupMenuExFlags.ReturnCmd"/> in the <paramref name="uFlags"/> parameter, the return value is nonzero if the
        /// function succeeds and zero if it fails. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int TrackPopupMenuEx(
            IntPtr hMenu,
            TrackPopupMenuExFlags uFlags,
            int x,
            int y,
            IntPtr hwnd,
            IntPtr lptpm);
        #endregion

        #endregion

        #region Shared Methods
        /// <summary>Helper function to get an interface associated with files given as paths</summary>
        /// <param name="hwnd">A handle to the owner window that the client should specify if it displays a dialog box or message box</param>
        /// <param name="paths">Full paths to items to get an interface for. These must be in the same parent directory</param>
        /// <param name="rIID"><see cref="IID"/> of the interface to get</param>
        /// <returns>Interface of <paramref name="rIID"/> associated with the requested items. Cast it to the requested interface.</returns>
        private static object GetUIObjectOfFiles(IntPtr hwnd, string[] paths, Guid rIID) {
            int hr;
            var apIDList = new List<IntPtr>();
            try {
                foreach (string path in paths) {
                    IntPtr pIDList;
                    SFGAO sfgao;

                    hr = SHParseDisplayName(path, default, out pIDList, SFGAO.None, out sfgao);
                    if (hr < 0) Marshal.ThrowExceptionForHR(hr);

                    apIDList.Add(pIDList);
                }

                object pSF = null;
                var apIDListChild = new List<IntPtr>();
                foreach (IntPtr pIDList in apIDList) {
                    object pSFlocal = null;
                    IntPtr pIDListChild;

                    Guid tmpGuidCopy = IID.IShellFolder;
                    hr = SHBindToParent(pIDList, ref tmpGuidCopy, out pSFlocal, out pIDListChild);
                    if (hr < 0) Marshal.ThrowExceptionForHR(hr);

                    if (pSF == null) pSF = pSFlocal;
                    apIDListChild.Add(pIDListChild);
                }

                var shellFolder = (IShellFolder)pSF;
                object ppV;

                shellFolder.GetUIObjectOf(hwnd, (uint)apIDListChild.Count, apIDListChild.ToArray(), ref rIID, out _, out ppV);
                return ppV;
            } finally {
                foreach (IntPtr pIDList in apIDList) {
                    Marshal.FreeCoTaskMem(pIDList);
                }
            }
        }

        /// <summary>Helper function to handle the bugs in context menu item implementations</summary>
        /// <param name="contextMenu">Interface to operate on</param>
        /// <param name="idCmd">ID of the item to operate on</param>
        /// <param name="uFlags">The type of requested Unicode string</param>
        /// <returns>The Unicode string requested</returns>
        private static string IContextMenu_GetCommandString(IContextMenu contextMenu, UIntPtr idCmd, GetCommandStringFlags uFlags) {
            // Callers are expected to be using Unicode.
            if (!uFlags.HasFlag(GetCommandStringFlags.Unicode)) throw new ArgumentException("Unicode flag expected!", "uFlags");

            // First try the Unicode message. Preset the output buffer with a known value because some handlers return S_OK without doing anything.
            IntPtr pszUnicode = Marshal.AllocHGlobal(MAX_FILE_PATH * Marshal.SizeOf<Int16>());
            try {
                try {
                    Marshal.WriteInt16(pszUnicode, 0);

                    // Some context menu handlers have off-by-one bugs and will overflow the output buffer. Specify less buffer size so a one-character overflow won't corrupt memory.
                    contextMenu.GetCommandString(idCmd, uFlags, out _, pszUnicode, MAX_FILE_PATH - 1);

                    if (Marshal.ReadInt16(pszUnicode) == 0) {
                        // Rats, a buggy IContextMenu handler that returned success even though it failed.
                        Marshal.ThrowExceptionForHR(unchecked((int)0x80004001)); // E_NOTIMPL
                    }

                    return Marshal.PtrToStringUni(pszUnicode);
                } finally {
                    Marshal.FreeHGlobal(pszUnicode);
                }
            } catch {
                // try again with ANSI
                IntPtr pszAnsi = Marshal.AllocHGlobal(MAX_FILE_PATH * Marshal.SizeOf<byte>());
                try {
                    Marshal.WriteByte(pszAnsi, 0);

                    contextMenu.GetCommandString(idCmd, uFlags & ~GetCommandStringFlags.Unicode, out _, pszAnsi, MAX_FILE_PATH - 1);

                    if (Marshal.ReadByte(pszAnsi) == 0) {
                        Marshal.ThrowExceptionForHR(unchecked((int)0x80004001)); // E_NOTIMPL
                    }

                    return Marshal.PtrToStringAnsi(pszAnsi);
                } finally {
                    Marshal.FreeHGlobal(pszAnsi);
                }
            }
        }

        /// <summary>Helper function to run GetCommandString, without checking the ANSI string, and returning <see langword="null"/> if there was an error</summary>
        /// <param name="contextMenu">Interface to operate on</param>
        /// <param name="idCmd">ID of the item to operate on</param>
        /// <param name="uFlags">The type of requested Unicode string</param>
        /// <returns>The Unicode string requested, or <see langword="null"/></returns>
        private static string IContextMenu_GetCommandStringOrNull(IContextMenu contextMenu, UIntPtr idCmd, GetCommandStringFlags uFlags) {
            IntPtr pszUnicode = Marshal.AllocHGlobal(MAX_FILE_PATH * Marshal.SizeOf<Int16>());
            try {
                contextMenu.GetCommandString(idCmd, uFlags, out _, pszUnicode, MAX_FILE_PATH - 1);
                return Marshal.PtrToStringUni(pszUnicode);
            } catch {
                return null;
            } finally {
                Marshal.FreeHGlobal(pszUnicode);
            }
        }
        #endregion

        #region Instance Methods
        private IntPtr _contextMenu;
        private IContextMenu _icontextMenu;
        private IContextMenu2 _icontextMenu2;
        private IContextMenu3 _icontextMenu3;

        private const uint _firstItem = 0x1;
        private const uint _maxItems = 0x7FFF;

        private uint _lastItem = _maxItems;
        private uint _customItemCount = 0;
        private Dictionary<uint, Action> _customItemDict = new();

        private bool _isShown = false;
        private bool _disposed;

        /// <summary>Event raised when the help text is changed. Use this to update a statusbar or other info pane</summary>
        /// <param name="text">Help text, or <see langword="null"/> if there was an error or no help text</param>
        /// <param name="ex">Error if any. Nothing if no error</param>
        public event HelpTextChangedEventHandler HelpTextChanged;
        public delegate void HelpTextChangedEventHandler(string text, Exception ex);
        /// <summary>Event raised when the <c>Rename</c> item is selected</summary>
        public event ItemRenamedEventHandler ItemRenamed;
        public delegate void ItemRenamedEventHandler();

        /// <summary>Gets whether the Context Menu has been built with <see cref="BuildMenu"/></summary>
        /// <returns>true if the menu has been built, false if it has been destroyed or not built</returns>
        public bool IsBuilt() {
            return _icontextMenu != null && _contextMenu != IntPtr.Zero;
        }

        /// <summary>Builds the context menu for a set of items</summary>
        /// <param name="frmHandle">Handle to the form that the menu will be shown on</param>
        /// <param name="itemPaths">Full paths to items to build a context menu for. These must be in the same parent directory</param>
        /// <param name="allowSpaceFor">Amount of space to be kept for Custom Items. The maximum amount of items is 0x7FFF</param>
        /// <param name="flags">Flags to use when building the menu, e.g. <see cref="QueryContextMenuFlags.CanRename"/>. <see cref="QueryContextMenuFlags.Explore"/> is recommended</param>
        /// <returns>The current instance of <see cref="ContextMenu"/>. Useful to build the context menu in a <see langword="using"/> block</returns>
        public ContextMenu BuildMenu(IntPtr frmHandle, string[] itemPaths, uint allowSpaceFor = 0, QueryContextMenuFlags flags = 0) {
            if (IsBuilt()) DestroyMenu();

            object pCM = GetUIObjectOfFiles(frmHandle, itemPaths, IID.IContextMenu);

            _contextMenu = CreatePopupMenu();
            if (_contextMenu == IntPtr.Zero) throw new Win32Exception();

            // set _icontextMenu after testing CreatePopupMenu, so interface can be freed if there was an error
            _icontextMenu = (IContextMenu)pCM;

            try {
                _lastItem = _maxItems - allowSpaceFor;
                _icontextMenu.QueryContextMenu(_contextMenu, 0, _firstItem, _lastItem, QueryContextMenuFlags.Normal | flags);

                return this;
            } catch {
                // if QueryContextMenu throws an error, destroy the popupmenu and free _icontextMenu before re-throwing.
                DestroyMenu(_contextMenu);
                _contextMenu = IntPtr.Zero;
                _icontextMenu = null;
                throw;
            }
        }

        /// <summary>Adds a custom entry to the currently built context menu. Sufficient space must have been specified in the BuildMenu call</summary>
        /// <param name="position">Index of the position in the context menu to add the item. Use -1 for the end of the menu</param>
        /// <param name="text">Text to display on the custem entry</param>
        /// <param name="action">Action to execute when the user selects the item</param>
        /// <param name="flags">
        /// Flags to apply to the entry. If <see cref="AddItemFlags.Separator"/> is used, <see langword="null"/>
        /// can be supplied for <paramref name="text"/> and <paramref name="action"/>
        /// </param>
        public void AddItem(int position, string text, Action action, AddItemFlags flags = 0) {
            if (!IsBuilt()) throw new NotSupportedException("Menu hasn't been built!");

            if (!(_lastItem + _customItemCount < _maxItems)) {
                throw new ArgumentOutOfRangeException("BuildMenu.allowSpaceFor", "No space allowed for custom menu item!");
            }

            MenuFlags menuFlags = MenuFlags.ByPosition | (MenuFlags)flags;
            _customItemCount += 1;
            uint newItemID = _lastItem + _customItemCount;

            _customItemDict.Add(newItemID, action);
            InsertMenu(_contextMenu, position, menuFlags, (UIntPtr)newItemID, text);
        }

        /// <summary>Performs the user selected entry's action, using the currently built Context Menu</summary>
        /// <param name="iCmd">Index of the entry</param>
        /// <param name="frmHandle">Handle of the form to associate with the action</param>
        /// <param name="pos">Position the Context Menu was displayed at</param>
        private void RunItem(int iCmd, IntPtr frmHandle, Point pos) {
            if (iCmd > _lastItem && _customItemDict.ContainsKey((uint)iCmd)) {
                Action act = _customItemDict[(uint)iCmd];
                act();
            } else {
                string itemVerb = IContextMenu_GetCommandStringOrNull(_icontextMenu, (UIntPtr)(iCmd - _firstItem), GetCommandStringFlags.VerbW);
                if (itemVerb == "rename") {
                    ItemRenamed?.Invoke();
                    return;
                }

                CMInvokeCommandInfoEx info = default;

                info.cbSize = (uint)Marshal.SizeOf(info);
                info.fMask = CMICMask.Unicode | CMICMask.PTInvoke;

                if (Control.ModifierKeys.HasFlag(Keys.Control)) {
                    info.fMask = info.fMask | CMICMask.ControlDown;
                }
                if (Control.ModifierKeys.HasFlag(Keys.Shift)) {
                    info.fMask = info.fMask | CMICMask.ShiftDown;
                }

                info.hwnd = frmHandle;
                info.lpVerb = (IntPtr)(iCmd - _firstItem);
                info.lpVerbW = (IntPtr)(iCmd - _firstItem);
                info.nShow = ShowWindowFlags.ShowNormal;
                info.ptInvoke = new ComPoint(pos);
                _icontextMenu.InvokeCommand(ref info);
            }
        }

        /// <summary>Shows the currently built context menu to the user, at the specified point</summary>
        /// <param name="frmHandle">Handle to the form to show the menu for</param>
        /// <param name="pos">Screen position to show the menu. Use <see cref="Control.PointToScreen"/> on the control to convert event mouse position to screen position</param>
        public void ShowMenu(IntPtr frmHandle, Point pos) {
            if (!IsBuilt()) throw new NotSupportedException("Menu hasn't been built!");

            int iCmd;

            _icontextMenu2 = _icontextMenu as IContextMenu2; // casting performs QueryInterface under the hood
            _icontextMenu3 = _icontextMenu as IContextMenu3; // .. as T returns null if cast failed

            _isShown = true;
            try {
                iCmd = TrackPopupMenuEx(_contextMenu, TrackPopupMenuExFlags.ReturnCmd, pos.X, pos.Y, frmHandle, IntPtr.Zero);
            } finally {
                _isShown = false;

                if (_icontextMenu2 is object) _icontextMenu2 = null;
                if (_icontextMenu3 is object) _icontextMenu3 = null;
            }

            if (iCmd > 0) {
                RunItem(iCmd, frmHandle, pos);
            } else {
                if (Marshal.GetLastWin32Error() != 0) {
                    throw new Win32Exception();
                }
            }
        }

        /// <summary>Handles menu select events, if the current context menu is built</summary>
        /// <param name="item">Index of the item that was selected</param>
        private void OnMenuSelect(uint item) {
            if (IsBuilt() && _isShown) {
                if (item >= _firstItem && item <= _lastItem) {
                    try {
                        HelpTextChanged?.Invoke(
                            IContextMenu_GetCommandString(_icontextMenu, (UIntPtr)(item - _firstItem), GetCommandStringFlags.HelpTextW), 
                            null);
                    } catch (Exception ex) {
                        HelpTextChanged?.Invoke(null, ex);
                    }
                } else {
                    HelpTextChanged?.Invoke(null, null);
                }
            }
        }

        private const int WM_MENUSELECT = 0x11F;
        /// <summary>Handles window messages related to the currently built context menu. Doesn't do anything if the menu isn't built or shown</summary>
        /// <param name="m">The message to handle</param>
        public void HandleWindowMessage(ref Message m) {
            if (IsBuilt()) {
                if (m.Msg == WM_MENUSELECT) {
                    // simplified HANDLE_WM_MENUSELECT C++ macro
                    uint wParamUInt = (uint)m.WParam.ToInt64();
                    // >>16 is equivalent to HIWORD C++ macro
                    if ((wParamUInt >> 16 & (uint)MenuFlags.Popup) != 0) {
                        OnMenuSelect(0);
                    } else {
                        OnMenuSelect(wParamUInt & 0xFFFF); // & 0xFFFF is equivalent to LOWORD C++ macro
                    }
                }

                if (_icontextMenu3 != null) {
                    IntPtr lres = default;
                    try {
                        _icontextMenu3.HandleMenuMsg2(m.Msg, m.WParam, m.LParam, lres);
                        m.Result = lres;
                        return;
                    } catch { }
                } else if (_icontextMenu2 != null) {
                    try {
                        _icontextMenu2.HandleMenuMsg(m.Msg, m.WParam, m.LParam);
                        m.Result = IntPtr.Zero;
                        return;
                    } catch { }
                }
            }
        }

        /// <summary>Explicitly destroys the currently built menu. Use this to free up resources if the context menu won't be rebuilt soon</summary>
        public void DestroyMenu() {
            if (IsBuilt()) {
                _customItemCount = 0;
                _customItemDict.Clear();
                DestroyMenu(_contextMenu);
                _contextMenu = IntPtr.Zero;
                _icontextMenu = null; // removing references to an interface automatically calls Marshal.Release
            }
        }

        #region IDisposable Implementation
        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    // dispose managed state (managed objects)
                    _customItemCount = 0;
                    _customItemDict.Clear();
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                if (_contextMenu != IntPtr.Zero) {
                    DestroyMenu(_contextMenu);
                    _contextMenu = IntPtr.Zero;
                }
                _icontextMenu = null;
                _disposed = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~ContextMenu() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }
}
