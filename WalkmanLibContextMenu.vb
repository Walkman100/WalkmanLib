Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Partial Public Class WalkmanLib
    ' Public version of ContextMenu.MenuFlags
    <Flags>
    Enum AddItemFlags As UInteger
        ''' <summary>Disables the menu item and grays it so it cannot be selected.</summary>
        Disabled = &H1
        ''' <summary>Places a check mark next to the menu item.</summary>
        Checked = &H8
        ''' <summary>Places the menu item on a new column, separated from the old column by a vertical line.</summary>
        VerticalBreak = &H20
        ''' <summary>
        ''' Draws a horizontal dividing line. The line cannot be disabled. The <c>text</c> and <c>action</c> parameters are ignored.
        ''' </summary>
        Separator = &H800
    End Enum

    ' based on the articles in https://stackoverflow.com/a/456922/2999220
    ' lots of interface help from https://www.developerfusion.com/article/84363/into-the-iunknown/
    Class ContextMenu
        Implements IDisposable
#Region "Native Methods"

#Region "Enums"
        'https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shellexecuteinfow
        <Flags>
        Private Enum SeeMask As UInteger
            ''' <summary>Use default values.</summary>
            [Default] = &H0
            ''' <summary>
            ''' Use the class name given by the ShellExecuteInfo.lpClass member. If both <see cref="ClassKey"/> and
            ''' <see cref="ClassName"/> are set, the class key is used.
            ''' </summary>
            ClassName = &H1
            ''' <summary>
            ''' Use the class key given by the ShellExecuteInfo.hkeyClass member. If both <see cref="ClassKey"/> and
            ''' <see cref="ClassName"/> are set, the class key is used.
            ''' </summary>
            ClassKey = &H3
            ''' <summary>
            ''' Use the item identifier list given by the ShellExecuteInfo.lpIDList member. The ShellExecuteInfo.lpIDList member must point to an ITEMIDLIST structure.
            ''' </summary>
            IDList = &H4
            ''' <summary>
            ''' Use the <see cref="IContextMenu"/> interface of the selected item's shortcut menu handler. Use either ShellExecuteInfo.lpFile to identify
            ''' the item by its file system path or ShellExecuteInfo.lpIDList to identify the item by its PIDL. This flag allows applications to use
            ''' ShellExecuteEx to invoke verbs from shortcut menu extensions instead of the static verbs listed in the registry.
            ''' <br />Note: <see cref="InvokeIdList"/> overrides and implies <see cref="IDList"/>.
            ''' </summary>
            InvokeIdList = &HC
            ''' <summary>Use the icon given by the ShellExecuteInfo.hIcon member. This flag cannot be combined with <see cref="HMonitor"/>.
            ''' <br/>Note: This flag is used only in Windows XP and earlier. It is ignored as of Windows Vista.</summary>
            Icon = &H10
            ''' <summary>Use the keyboard shortcut given by the ShellExecuteInfo.dwHotKey member.</summary>
            HotKey = &H20
            ''' <summary>
            ''' Use to indicate that the ShellExecuteInfo.hProcess member receives the process handle. This handle is typically used to allow an application
            ''' to find out when a process created with ShellExecuteEx terminates. In some cases, such as when execution is satisfied through a DDE conversation,
            ''' no handle will be returned. The calling application is responsible for closing the handle when it is no longer needed.
            ''' </summary>
            NoCloseProcess = &H40
            ''' <summary>
            ''' Validate the share and connect to a drive letter. This enables reconnection of disconnected network drives. The ShellExecuteInfo.lpFile member
            ''' is a UNC path of a file on a network.
            ''' </summary>
            ConnectNetDrv = &H80
            ''' <summary>
            ''' Wait for the execute operation to complete before returning. This flag should be used by callers that are using ShellExecute forms that might result
            ''' in an async activation, for example DDE, and create a process that might be run on a background thread. (Note: ShellExecuteEx runs on a background
            ''' thread by default if the caller's threading model is not Apartment.) Calls to ShellExecuteEx from processes already running on background threads
            ''' should always pass this flag. Also, applications that exit immediately after calling ShellExecuteEx should specify this flag.
            ''' <br/>If the execute operation is performed on a background thread and the caller did not specify the <see cref="AsyncOK"/> flag, then the calling thread
            ''' waits until the new process has started before returning. This typically means that either CreateProcess has been called, the DDE communication has
            ''' completed, or that the custom execution delegate has notified ShellExecuteEx that it is done. If the <see cref="WaitForInputIdle"/> flag is specified,
            ''' then ShellExecuteEx calls WaitForInputIdle and waits for the new process to idle before returning, with a maximum timeout of 1 minute.
            ''' </summary>
            NoAsync = &H100
            ''' <summary>Expand any environment variables specified in the string given by the ShellExecuteInfo.lpDirectory or ShellExecuteInfo.lpFile member.</summary>
            DoEnvSubst = &H200
            ''' <summary>Do not display an error message box if an error occurs.</summary>
            FlagNoUI = &H400
            ''' <summary>Use this flag to indicate a Unicode application.</summary>
            Unicode = &H4000
            ''' <summary>Use to inherit the parent's console for the new process instead of having it create a new console. It is the opposite of using
            ''' a CREATE_NEW_CONSOLE flag with CreateProcess.</summary>
            NoConsole = &H8000
            'See https://reactos.org/archives/public/ros-diffs/2013-March/048151.html
            ' the following three values are not defined in shellapi.h - however they are referenced
            ' in ShObjIdl_code.h as flags for the CMInvokeCommandInfoEx structure.
            HasLinkName = &H10000
            HasTitle = &H20000
            FlagSepVDM = &H40000
            ''' <summary>
            ''' The execution can be performed on a background thread and the call should return immediately without waiting for the background thread to finish.
            ''' Note that in certain cases ShellExecuteEx ignores this flag and waits for the process to finish before returning.
            ''' </summary>
            AsyncOK = &H100000
            ''' <summary>
            ''' Use this flag when specifying a monitor on multi-monitor systems. The monitor is specified in the ShellExecuteInfo.hMonitor member.
            ''' This flag cannot be combined with <see cref="icon"/>.
            ''' </summary>
            HMonitor = &H200000
            ''' <summary>Introduced in Windows XP. Do not perform a zone check. This flag allows ShellExecuteEx to bypass zone checking put into place by IAttachmentExecute.</summary>
            NoZoneChecks = &H800000
            ''' <summary>Not used.</summary>
            NoQueryClassStore = &H1000000
            ''' <summary>After the new process is created, wait for the process to become idle before returning, with a one minute timeout.</summary>
            WaitForInputIdle = &H2000000
            ''' <summary>
            ''' Introduced in Windows XP. Keep track of the number of times this application has been launched.
            ''' Applications with sufficiently high counts appear in the Start Menu's list of most frequently used programs.
            ''' </summary>
            FlagLogUsage = &H4000000
            ''' <summary>
            ''' The ShellExecuteInfo.hInstApp member is used to specify the IUnknown of an object that implements IServiceProvider. This object will be
            ''' used as a site pointer. The site pointer is used to provide services to the ShellExecute function, the handler binding process, and invoked verb handlers.
            ''' </summary>
            FlagHInstIsSite = &H8000000
        End Enum

        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/ns-shobjidl_core-cminvokecommandinfoex
        <Flags>
        Private Enum CMICMask As UInteger
            None = 0
            ''' <summary>The <see cref="CMInvokeCommandInfoEx.dwHotKey"/> member is valid.</summary>
            Hotkey = SeeMask.HotKey
            ''' <summary>The <see cref="CMInvokeCommandInfoEx.hIcon"/> member is valid. As of Windows Vista this flag is not used.</summary>
            Icon = SeeMask.Icon
            ''' <summary>
            ''' The implementation of <see cref="IContextMenu.InvokeCommand"/> is prevented from displaying user
            ''' interface elements (for example, error messages) while carrying out a command.
            ''' </summary>
            FlagNoUI = SeeMask.FlagNoUI
            ''' <summary>
            ''' The shortcut menu handler should use lpVerbW, lpParametersW, lpDirectoryW, and lpTitleW members instead of their ANSI equivalents.
            ''' Because some shortcut menu handlers may not support Unicode, you should also pass valid ANSI strings in the lpVerb, lpParameters,
            ''' lpDirectory, and lpTitle members.
            ''' </summary>
            Unicode = SeeMask.Unicode
            ''' <summary>
            ''' If a shortcut menu handler needs to create a new process, it will normally create a new console. Setting the <see cref="NoConsole"/>
            ''' flag suppresses the creation of a new console.
            ''' </summary>
            NoConsole = SeeMask.NoConsole
            ''' <summary>The <see cref="CMInvokeCommandInfoEx.lpTitle"/> member contains a full path to a shortcut file. Use in conjunction with <see cref="HasTitle"/>.</summary>
            HasLinkName = SeeMask.HasLinkName
            ''' <summary>The <see cref="CMInvokeCommandInfoEx.lpTitle"/> member is valid.</summary>
            HasTitle = SeeMask.HasTitle
            ''' <summary>This flag is valid only when referring to a 16-bit Windows-based application. If set, the application that the shortcut points to runs in a private Virtual DOS Machine (VDM).</summary>
            FlagSepVDM = SeeMask.FlagSepVDM
            ''' <summary>
            ''' The implementation of <see cref="IContextMenu.InvokeCommand"/> can spin off a new thread or process to handle
            ''' the call and does not need to block on completion of the function being invoked. For example, if the verb is "delete" the 
            ''' <see cref="IContextMenu.InvokeCommand"/> call may return before all of the items have been deleted. Since this
            ''' is advisory, calling applications that specify this flag cannot guarantee that this request will be honored if they are not familiar with the
            ''' implementation of the verb that they are invoking.
            ''' </summary>
            AsyncOK = SeeMask.AsyncOK
            ''' <summary>
            ''' Windows Vista and later. The implementation of <see cref="IContextMenu.InvokeCommand"/> should be synchronous,
            ''' not returning before it is complete. Since this is recommended, calling applications that specify this flag cannot guarantee that this request
            ''' will be honored if they are not familiar with the implementation of the verb that they are invoking.
            ''' </summary>
            NoAsync = SeeMask.NoAsync
            ''' <summary>The SHIFT key is pressed. Use this instead of polling the current state of the keyboard that may have changed since the verb was invoked.</summary>
            ShiftDown = &H10000000
            ''' <summary>The CTRL key is pressed. Use this instead of polling the current state of the keyboard that may have changed since the verb was invoked.</summary>
            ControlDown = &H40000000
            ''' <summary>
            ''' Indicates that the implementation of <see cref="IContextMenu.InvokeCommand"/> might want to keep track of the
            ''' item being invoked for features like the "Recent documents" menu.
            ''' </summary>
            FlagLogUsage = SeeMask.FlagLogUsage
            ''' <summary>Do not perform a zone check. This flag allows ShellExecuteEx to bypass zone checking put into place by IAttachmentExecute.</summary>
            NoZoneChecks = SeeMask.NoZoneChecks
            ''' <summary>The ptInvoke member is valid.</summary>
            PTInvoke = &H20000000
        End Enum

        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-getcommandstring
        <Flags>
        Private Enum GetCommandStringFlags As UInteger
            ''' <summary>Sets pszName to an ANSI string containing the help text for the command.</summary>
            HelpTextA = &H1
            ''' <summary>Sets pszName to a Unicode string containing the help text for the command.</summary>
            HelpTextW = &H5
            ''' <summary>Returns S_OK if the menu item exists, or S_FALSE otherwise.</summary>
            ValidateA = &H2
            ''' <summary>Returns S_OK if the menu item exists, or S_FALSE otherwise.</summary>
            ValidateW = &H6
            ''' <summary>Sets pszName to an ANSI string containing the language-independent command name for the menu item.</summary>
            VerbA = &H0
            ''' <summary>Sets pszName to a Unicode string containing the language-independent command name for the menu item.</summary>
            VerbW = &H4
            ''' <summary>(From C++ header) icon string (unicode)</summary>
            VerbIconW = &H14
            ''' <summary>(From C++ header) for bit testing - Unicode string</summary>
            Unicode = &H4
        End Enum

        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-querycontextmenu
        <Flags>
        Private Enum QueryContextMenuFlags As UInteger
            ''' <summary>Indicates normal operation. A shortcut menu extension, namespace extension, or drag-and-drop handler can add all menu items.</summary>
            Normal = 0
            ''' <summary>
            ''' The user is activating the default action, typically by double-clicking. This flag provides a hint for the shortcut menu extension to add nothing
            ''' if it does not modify the default item in the menu. A shortcut menu extension or drag-and-drop handler should not add any menu items if this value
            ''' is specified. A namespace extension should at most add only the default item.
            ''' </summary>
            DefaultOnly = 1
            ''' <summary>The shortcut menu is that of a shortcut file (normally, a .lnk file). Shortcut menu handlers should ignore this value.</summary>
            VerbsOnly = 2
            ''' <summary>The Windows Explorer tree window is present.</summary>
            Explore = 4
            ''' <summary>This flag is set for items displayed in the Send To menu. Shortcut menu handlers should ignore this value.</summary>
            NoVerbs = 8
            ''' <summary>
            ''' The calling application supports renaming of items. A shortcut menu or drag-and-drop handler should ignore this flag. A namespace extension should
            ''' add a Rename item to the menu if applicable.
            ''' </summary>
            CanRename = &H10
            ''' <summary>
            ''' No item in the menu has been set as the default. A drag-and-drop handler should ignore this flag. A namespace extension should not set any of the
            ''' menu items as the default.
            ''' </summary>
            NoDefault = &H20
            ''' <summary>
            ''' This value is not available.
            ''' <br />Windows Server 2003 and Windows XP: A static menu is being constructed. Only the browser should use this flag; all other shortcut menu
            ''' extensions should ignore it.
            ''' </summary>
            IncludeStatic = &H40
            ''' <summary>
            ''' The calling application is invoking a shortcut menu on an item in the view (as opposed to the background of the view).
            ''' <br />Windows Server 2003 and Windows XP: This value is not available.
            ''' </summary>
            ItemMenu = &H80
            ''' <summary>
            ''' The calling application wants extended verbs. Normal verbs are displayed when the user right-clicks an object. To display extended verbs, the
            ''' user must right-click while pressing the Shift key.
            ''' </summary>
            ExtendedVerbs = &H100
            ''' <summary>The calling application intends to invoke verbs that are disabled, such as legacy menus.</summary>
            DisabledVerbs = &H200
            ''' <summary>
            ''' The verb state can be evaluated asynchronously.
            ''' <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            ''' </summary>
            AsyncVerbState = &H400
            ''' <summary>
            ''' Informs context menu handlers that do not support the invocation of a verb through a canonical verb name to bypass
            ''' <see cref="IContextMenu.QueryContextMenu"/> in their implementation.
            ''' <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            ''' </summary>
            OptimizeForInvoke = &H800
            ''' <summary>
            ''' Populate submenus synchronously.
            ''' <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            ''' </summary>
            SyncCascadeMenu = &H1000
            ''' <summary>
            ''' When no verb is explicitly specified, do not use a default verb in its place.
            ''' <br />Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not available.
            ''' </summary>
            DoNotPickDefault = &H2000
            ''' <summary>
            ''' This flag is a bitmask that specifies all bits that should not be used. This is to be used only as a mask. Do not pass this as a parameter value.
            ''' </summary>
            Reserved = &HFFFF0000UI
        End Enum

        'https://docs.microsoft.com/en-us/windows/win32/shell/sfgao
        'https://www.pinvoke.net/default.aspx/Enums/SFGAOF.html
        ''' <summary>IShellFolder::GetAttributesOf flags</summary>
        <Flags>
        Private Enum SFGAO As UInteger
            None = &H0
            ''' <summary>The specified items can be copied.</summary>
            CanCopy = &H1
            ''' <summary>The specified items can be moved.</summary>
            CanMove = &H2
            ''' <summary>
            ''' Shortcuts can be created for the specified items.
            ''' <br/>If a namespace extension returns this attribute, a Create Shortcut entry with a default handler is added to the shortcut menu that
            ''' is displayed during drag-and-drop operations. The extension can also implement its own handler for the link verb in place of the default.
            ''' If the extension does so, it is responsible for creating the shortcut.
            ''' <br/>A Create Shortcut item is also added to the Windows Explorer File menu and to normal shortcut menus.
            ''' <br/>If the item is selected, your application's <see cref="IContextMenu.InvokeCommand"/> method is invoked
            ''' with the <see cref="CMInvokeCommandInfoEx.lpVerb"/> member set to link. Your application is responsible for creating the link.
            ''' </summary>
            CanLink = &H4
            ''' <summary>The specified items can be bound to an IStorage object through IShellFolder::BindToObject.</summary>
            Storage = &H8
            ''' <summary>The specified items can be renamed. Note that this value is essentially a suggestion; not all namespace clients allow items to be renamed. However, those that do must have this attribute set.</summary>
            CanRename = &H10
            ''' <summary>The specified items can be deleted.</summary>
            CanDelete = &H20
            ''' <summary>The specified items have property sheets.</summary>
            HasPropsSheet = &H40
            ''' <summary>The specified items are drop targets.</summary>
            DropTarget = &H100
            ''' <summary>
            ''' This flag is a mask for the capability attributes: <see cref="CanCopy"/>, <see cref="CanMove"/>, <see cref="CanLink"/>, <see cref="CanRename"/>,
            ''' <see cref="CanDelete"/>, <see cref="HasPropsSheet"/>, and <see cref="DropTarget"/>. Callers normally do not use this value.
            ''' </summary>
            CapabilityMask = &H177
            ''' <summary>File or folder is not fully present and recalled on open or access</summary>
            Placeholder = &H800
            ''' <summary>Windows 7 and later. The specified items are system items.</summary>
            System = &H1000
            ''' <summary>The specified items are encrypted and might require special presentation.</summary>
            Encrypted = &H2000
            ''' <summary>Accessing the item is expected to be a slow operation. Applications should avoid accessing items flagged with <see cref="IsSlow"/>.</summary>
            IsSlow = &H4000
            ''' <summary>The specified items are shown as dimmed and unavailable to the user.</summary>
            Ghosted = &H8000
            ''' <summary>The specified items are shortcuts.</summary>
            Link = &H10000
            ''' <summary>The specified objects are shared.</summary>
            Share = &H20000
            ''' <summary>The specified items are read-only. In the case of folders, this means that new items cannot be created in those folders.</summary>
            [ReadOnly] = &H40000
            ''' <summary>The item is hidden and should not be displayed unless the Show hidden files and folders option is enabled in Folder Settings.</summary>
            Hidden = &H80000
            ''' <summary>Do not use.</summary>
            DisplayAttrMask = &HFC000
            ''' <summary>The items are nonenumerated items and should be hidden. They are not returned through an enumerator such as that created by the IShellFolder::EnumObjects method.</summary>
            NonEnumerated = &H100000
            ''' <summary>The items contain new content, as defined by the particular application.</summary>
            NewContent = &H200000
            ''' <summary>
            ''' Indicates that the item has a stream associated with it. That stream can be accessed through a call to IShellFolder::BindToObject or
            ''' IShellItem::BindToHandler with IID_IStream in the riid parameter.
            ''' </summary>
            Stream = &H400000
            ''' <summary>Children of this item are accessible through IStream or IStorage. Those children are flagged with <see cref="Storage"/> or <see cref="Stream"/>.</summary>
            StorageAncestor = &H800000
            ''' <summary>
            ''' When specified as input, instructs the folder to validate that the items contained in a folder or Shell item array exist. If one or more of those items
            ''' do not exist, IShellFolder::GetAttributesOf and IShellItemArray::GetAttributes return a failure code. This flag is never returned as an [out] value.
            ''' <br/>When used with the file system folder, instructs the folder to discard cached properties retrieved by clients of IShellFolder2::GetDetailsEx that
            ''' might have accumulated for the specified items.
            ''' </summary>
            Validate = &H1000000
            ''' <summary>The specified items are on removable media or are themselves removable devices.</summary>
            Removable = &H2000000
            ''' <summary>The specified items are compressed.</summary>
            Compressed = &H4000000
            ''' <summary>The specified items can be hosted inside a web browser or Windows Explorer frame.</summary>
            Browsable = &H8000000
            ''' <summary>The specified folders are either file system folders or contain at least one descendant (child, grandchild, or later) that is a file system (<see cref="Filesystem"/>) folder.</summary>
            FileSysAncestor = &H10000000
            ''' <summary>
            ''' The specified items are folders. Some items can be flagged with both <see cref="Stream"/> and <see cref="Folder"/>, such as a compressed file with a .zip
            ''' file name extension. Some applications might include this flag when testing for items that are both files and containers.
            ''' </summary>
            Folder = &H20000000
            ''' <summary>
            ''' The specified folders or files are part of the file system (that is, they are files, directories, or root directories). The parsed names of the items can
            ''' be assumed to be valid Win32 file system paths. These paths can be either UNC or drive-letter based.
            ''' </summary>
            Filesystem = &H40000000
            ''' <summary>
            ''' This flag is a mask for the storage capability attributes: <see cref="Storage"/>, <see cref="Link"/>, <see cref="[ReadOnly]"/>, <see cref="Stream"/>,
            ''' <see cref="StorageAncestor"/>, <see cref="FileSysAncestor"/>, <see cref="Folder"/>, and <see cref="Filesystem"/>. Callers normally do not use this value.
            ''' </summary>
            StorageCapMask = &H70C50008
            ''' <summary>
            ''' The specified folders have subfolders. This attribute is only advisory and might be returned by Shell folder implementations even if they do not contain
            ''' subfolders. Note, however, that the converse — failing to return <see cref="HasSubFolder"/> — definitively states that the folder objects do not have subfolders.
            ''' <br/>Returning <see cref="HasSubFolder"/> is recommended whenever a significant amount of time is required to determine whether any subfolders exist.
            ''' For example, the Shell always returns <see cref="HasSubFolder"/> when a folder is located on a network drive.
            ''' </summary>
            HasSubFolder = &H80000000UI
            ''' <summary>This flag is a mask for content attributes, at present only <see cref="HasSubFolder"/>. Callers normally do not use this value.</summary>
            ContentsMask = &H80000000UI
            ''' <summary>
            ''' Mask used by the PKEY_SFGAOFlags property to determine attributes that are considered to cause slow calculations or lack context:
            ''' <see cref="IsSlow"/>, <see cref="[ReadOnly]"/>, <see cref="HasSubFolder"/>, and <see cref="Validate"/>. Callers normally do not use this value.
            ''' </summary>
            PKeySFGAOMask = &H81044000UI
        End Enum

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-trackpopupmenuex
        <Flags>
        Private Enum TrackPopupMenuExFlags As UInteger
            ''' <summary>The user can select menu items with only the left mouse button.</summary>
            LeftButton = &H0
            ''' <summary>The user can select menu items with both the left and right mouse buttons.</summary>
            RightButton = &H2

            ''' <summary>Positions the shortcut menu so that its left side is aligned with the coordinate specified by the x parameter.</summary>
            LeftAlign = &H0
            ''' <summary>Centers the shortcut menu horizontally relative to the coordinate specified by the x parameter.</summary>
            CenterAlign = &H4
            ''' <summary>Positions the shortcut menu so that its right side is aligned with the coordinate specified by the x parameter.</summary>
            RightAlign = &H8

            ''' <summary>Positions the shortcut menu so that its top side is aligned with the coordinate specified by the y parameter.</summary>
            TopAlign = &H0
            ''' <summary>Centers the shortcut menu vertically relative to the coordinate specified by the y parameter.</summary>
            VCenterAlign = &H10
            ''' <summary>Positions the shortcut menu so that its bottom side is aligned with the coordinate specified by the y parameter.</summary>
            BottomAlign = &H20

            ''' <summary>
            ''' If the menu cannot be shown at the specified location without overlapping the excluded rectangle, the system tries to accommodate the
            ''' requested horizontal alignment before the requested vertical alignment.
            ''' </summary>
            Horizontal = &H0
            ''' <summary>
            ''' If the menu cannot be shown at the specified location without overlapping the excluded rectangle, the system tries to accommodate the
            ''' requested vertical alignment before the requested horizontal alignment.
            ''' </summary>
            Vertical = &H40

            ''' <summary>The function does not send notification messages when the user clicks a menu item.</summary>
            NoNotify = &H80
            ''' <summary>The function returns the menu item identifier of the user's selection in the return value.</summary>
            ReturnCmd = &H100

            ''' <summary>Use the this flag to display a menu when another menu is already displayed. This is intended to support context menus within a menu.</summary>
            Recurse = &H1

            ''' <summary>Animates the menu from left to right.</summary>
            HorPosAnimation = &H400
            ''' <summary>Animates the menu from right to left.</summary>
            HorNegAnimation = &H800
            ''' <summary>Animates the menu from top to bottom.</summary>
            VerPosAnimation = &H1000
            ''' <summary>Animates the menu from bottom to top.</summary>
            VerNegAnimation = &H2000
            ''' <summary>Displays menu without animation.</summary>
            NoAnimation = &H4000

            ''' <summary>For right-to-left text layout, use this flag. By default, the text layout is left-to-right.</summary>
            LayoutRTL = &H8000

            'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-calculatepopupwindowposition#tpm_workarea
            ''' <summary>
            ''' Restricts the pop-up window to within the work area. If this flag is not set, the pop-up window is restricted
            ''' to the work area only if the input point is within the work area.
            ''' </summary>
            WorkArea = &H10000
        End Enum

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        <Flags>
        Private Enum ShowWindowFlags As Integer
            ''' <summary>Hides the window and activates another window.</summary>
            Hide = 0
            ''' <summary>Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.</summary>
            ShowNormal = 1
            ''' <summary>Activates the window and displays it as a minimized window.</summary>
            ShowMinimized = 2
            ''' <summary>Activates the window and displays it as a maximized window.</summary>
            ShowMaximized = 3
            ''' <summary>Maximizes the specified window.</summary>
            Maximize = 3
            ''' <summary>Displays a window in its most recent size and position. This value is similar to <see cref="ShowNormal"/>, except that the window is not activated.</summary>
            ShowNormalNoActive = 4
            ''' <summary>Activates the window and displays it in its current size and position.</summary>
            Show = 5
            ''' <summary>Minimizes the specified window and activates the next top-level window in the Z order.</summary>
            Minimize = 6
            ''' <summary>Displays the window as a minimized window. This value is similar to <see cref="ShowMinimized"/>, except the window is not activated.</summary>
            ShowMinimizedNoActive = 7
            ''' <summary>Displays the window in its current size and position. This value is similar to <see cref="Show"/>, except that the window is not activated.</summary>
            ShowNoActive = 8
            ''' <summary>Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.</summary>
            Restore = 9
            ''' <summary>Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.</summary>
            ShowDefault = 10
            ''' <summary>Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.</summary>
            ForceMinimize = 11
        End Enum

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-insertmenuw
        <Flags>
        Private Enum MenuFlags As UInteger
            ''' <summary>
            ''' Indicates that the uPosition parameter gives the identifier of the menu item. This flag is the default if neither the
            ''' <see cref="ByCommand"/> nor <see cref="ByPosition"/> flag is specified.
            ''' </summary>
            ByCommand = &H0
            ''' <summary>
            ''' Indicates that the uPosition parameter gives the zero-based relative position of the new menu item.
            ''' If uPosition is -1, the new menu item is appended to the end of the menu.
            ''' </summary>
            ByPosition = &H400

            ''' <summary>Enables the menu item so that it can be selected and restores it from its grayed state.</summary>
            Enabled = &H0
            ''' <summary>Disables the menu item and grays it so it cannot be selected.</summary>
            Grayed = &H1
            ''' <summary>Disables the menu item so that it cannot be selected, but does not gray it.</summary>
            Disabled = &H2
            ''' <summary>Places a check mark next to the menu item. If the application provides check-mark bitmaps, this flag displays the check-mark bitmap next to the menu item.</summary>
            Checked = &H8
            ''' <summary>
            ''' Does not place a check mark next to the menu item (default). If the application supplies check-mark bitmaps,
            ''' this flag displays the clear bitmap next to the menu item.
            ''' </summary>
            Unchecked = &H0

            ''' <summary>Specifies that the menu item is a text string; the lpNewItem parameter is a pointer to the string.</summary>
            [String] = &H0
            ''' <summary>Uses a bitmap as the menu item. The lpNewItem parameter contains a handle to the bitmap.</summary>
            Bitmap = &H4
            ''' <summary>
            ''' Specifies that the item is an owner-drawn item. Before the menu is displayed for the first time, the window that owns the menu receives a
            ''' WM_MEASUREITEM message to retrieve the width and height of the menu item. The WM_DRAWITEM message is then sent to the window procedure of
            ''' the owner window whenever the appearance of the menu item must be updated.
            ''' </summary>
            OwnerDraw = &H100

            ''' <summary>
            ''' Specifies that the menu item opens a drop-down menu or submenu. The uIDNewItem parameter specifies a handle to the drop-down menu or submenu.
            ''' This flag is used to add a menu name to a menu bar or a menu item that opens a submenu to a drop-down menu, submenu, or shortcut menu.
            ''' </summary>
            Popup = &H10
            ''' <summary>
            ''' Functions the same as the <see cref="MenuBreak"/> flag for a menu bar. For a drop-down menu, submenu, or shortcut menu,
            ''' the new column is separated from the old column by a vertical line.
            ''' </summary>
            MenuBarBreak = &H20
            ''' <summary>Places the item on a new line (for menu bars) or in a new column (for a drop-down menu, submenu, or shortcut menu) without separating columns.</summary>
            MenuBreak = &H40
            ''' <summary>
            ''' Draws a horizontal dividing line. This flag is used only in a drop-down menu, submenu, or shortcut menu. The line cannot be grayed, disabled,
            ''' or highlighted. The lpNewItem and uIDNewItem parameters are ignored.
            ''' </summary>
            Separator = &H800
        End Enum

        'from ShlGuid.h - line 50 in Windows Kit 10.0.18362.0
        ' declared as a structure as Enums have to be of "Integral" types
        Private Structure IID
            Public Shared ReadOnly INewShortcutHookA As New Guid(&H214E1, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellBrowser As New Guid(&H214E2, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellView As New Guid(&H214E3, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IContextMenu As New Guid(&H214E4, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellIcon As New Guid(&H214E5, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellFolder As New Guid(&H214E6, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellExtInit As New Guid(&H214E8, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellPropSheetExt As New Guid(&H214E9, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IPersistFolder As New Guid(&H214EA, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IExtractIconA As New Guid(&H214EB, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellDetails As New Guid(&H214EC, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellLinkA As New Guid(&H214EE, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly ICopyHookA As New Guid(&H214EF, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IFileViewerA As New Guid(&H214F0, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly ICommDlgBrowser As New Guid(&H214F1, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IEnumIDList As New Guid(&H214F2, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IFileViewerSite As New Guid(&H214F3, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IContextMenu2 As New Guid(&H214F4, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellExecuteHookA As New Guid(&H214F5, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IPropSheetPage As New Guid(&H214F6, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly INewShortcutHookW As New Guid(&H214F7, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IFileViewerW As New Guid(&H214F8, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellLinkW As New Guid(&H214F9, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IExtractIconW As New Guid(&H214FA, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IShellExecuteHookW As New Guid(&H214FB, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly ICopyHookW As New Guid(&H214FC, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IRemoteComputer As New Guid(&H214FE, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IQueryInfo As New Guid(&H21500, 0, 0, &HC0, 0, 0, 0, 0, 0, 0, &H46)
            Public Shared ReadOnly IBriefcaseStg As New Guid(&H8BCE1FA1, &H921, &H101B, &HB1, &HFF, &H0, &HDD, &H1, &HC, &HCC, &H48)
            Public Shared ReadOnly IShellView2 As New Guid(&H88E39E80, &H3578, &H11CF, &HAE, &H69, &H8, &H0, &H2B, &H2E, &H12, &H62)
            Public Shared ReadOnly IURLSearchHook As New Guid(&HAC60F6A0, &HFD9, &H11D0, &H99, &HCB, &H0, &HC0, &H4F, &HD6, &H44, &H97)
            Public Shared ReadOnly ISearchContext As New Guid(&H9F656A2, &H41AF, &H480C, &H88, &HF7, &H16, &HCC, &HD, &H16, &H46, &H15)
            Public Shared ReadOnly IURLSearchHook2 As New Guid(&H5EE44DA4, &H6D32, &H46E3, &H86, &HBC, &H7, &H54, &HD, &HED, &HD0, &HE0)
            Public Shared ReadOnly IDefViewID As New Guid(&H985F64F0UI, &HD410, &H4E02, &HBE, &H22, &HDA, &H7, &HF2, &HB5, &HC5, &HE1)
            Public Shared ReadOnly IDockingWindowSite As New Guid(&H2A342FC, &H7B26, &H11D0, &H8C, &HA9, &H0, &HA0, &HC9, &H2D, &HBF, &HE8)
            Public Shared ReadOnly IDockingWindowFrame As New Guid(&H47D2657, &H7B27, &H11D0, &H8C, &HA9, &H0, &HA0, &HC9, &H2D, &HBF, &HE8)
            Public Shared ReadOnly IShellIconOverlay As New Guid(&H7D688A70, &HC613, &H11D0, &H99, &H9B, &H0, &HC0, &H4F, &HD6, &H55, &HE1)
            Public Shared ReadOnly IShellIconOverlayIdentifier As New Guid(&HC6C4200, &HC589, &H11D0, &H99, &H9A, &H0, &HC0, &H4F, &HD6, &H55, &HE1)
            Public Shared ReadOnly ICommDlgBrowser2 As New Guid(&H1033951, &H2894, &H11D2, &H90, &H39, &H0, &HC0, &H4F, &H8E, &HEB, &H3E)
            Public Shared ReadOnly IShellFolderViewCB As New Guid(&H2047E320, &HF2A9, &H11CE, &HAE, &H65, &H8, &H0, &H2B, &H2E, &H12, &H62)
            Public Shared ReadOnly IShellIconOverlayManager As New Guid(&HF10B5E34UI, &HDD3B, &H42A7, &HAA, &H7D, &H2F, &H4E, &HC5, &H4B, &HB0, &H9B)
            Public Shared ReadOnly IThumbnailCapture As New Guid(&H4EA3926, &H7211, &H409F, &HB6, &H22, &HF6, &H3D, &HBD, &H16, &HC5, &H33)
            Public Shared ReadOnly IShellImageStore As New Guid(&H48C8118, &HB924, &H11D1, &H98, &HD5, &H0, &HC0, &H4F, &HB6, &H87, &HDA)
            Public Shared ReadOnly IContextMenu3 As New Guid(&HBCFCE0A, &HEC17, &H11D0, &H8D, &H10, &H0, &HA0, &HC9, &HF, &H27, &H19)
            Public Shared ReadOnly IShellFolderBand As New Guid(&H7FE80CC, &HC247, &H11D0, &HB9, &H3A, &H0, &HA0, &HC9, &H3, &H12, &HE1)
            Public Shared ReadOnly IDefViewFrame As New Guid(&H710EB7A0, &H45ED, &H11D0, &H92, &H4A, &H0, &H20, &HAF, &HC7, &HAC, &H4D)
            Public Shared ReadOnly IDiscardableBrowserProperty As New Guid(&H49C3DE7, &HD329, &H11D0, &HAB, &H73, &H0, &HC0, &H4F, &HC3, &H3E, &H80)
            Public Shared ReadOnly IShellChangeNotify As New Guid(&HD82BE2B1, &H5764, &H11D0, &HA9, &H6E, &H0, &HC0, &H4F, &HD7, &H5, &HA2)
            Public Shared ReadOnly IObjMgr As New Guid(&HBB2761, &H6A77, &H11D0, &HA5, &H35, &H0, &HC0, &H4F, &HD7, &HD0, &H62)
            Public Shared ReadOnly IACList As New Guid(&H77A130B0, &H94FD, &H11D0, &HA5, &H44, &H0, &HC0, &H4F, &HD7, &HD0, &H62)
            Public Shared ReadOnly IACList2 As New Guid(&H470141A0, &H5186, &H11D2, &HBB, &HB6, &H0, &H60, &H97, &H7B, &H46, &H4C)
            Public Shared ReadOnly ICurrentWorkingDirectory As New Guid(&H91956D21UI, &H9276, &H11D1, &H92, &H1A, &H0, &H60, &H97, &HDF, &H5B, &HD4)
            Public Shared ReadOnly IProgressDialog As New Guid(&HEBBC7C0, &H315E, &H11D2, &HB6, &H2F, &H0, &H60, &H97, &HDF, &H5B, &HD4)
            Public Shared ReadOnly IActiveDesktop As New Guid(&HF490EB00, &H1240, &H11D1, &H98, &H88, &H0, &H60, &H97, &HDE, &HAC, &HF9)
            Public Shared ReadOnly IActiveDesktopP As New Guid(&H52502EE0, &HEC80, &H11D0, &H89, &HAB, &H0, &HC0, &H4F, &HC2, &H97, &H2D)
            Public Shared ReadOnly IADesktopP2 As New Guid(&HB22754E, &H4574, &H11D1, &H98, &H88, &H0, &H60, &H97, &HDE, &HAC, &HF9)
            Public Shared ReadOnly ISynchronizedCallBack As New Guid(&H74C2604, &H70D1, &H11D1, &HB7, &H5A, &H0, &HA0, &HC9, &H5, &H64, &HFE)
            Public Shared ReadOnly IQueryAssociations As New Guid(&HC46CA59, &H3C3F, &H11D2, &HBE, &HE6, &H0, &H0, &HF8, &H5, &HCA, &H57)
            Public Shared ReadOnly IColumnProvider As New Guid(&HE802500, &H1C42, &H11D2, &HBE, &H2C, &H0, &HA0, &HC9, &HA8, &H3D, &HA1)
            Public Shared ReadOnly INamedPropertyBag As New Guid(&HFB70043, &H952C, &H11D1, &H94, &H6F, &H0, &H0, &H0, &H0, &H0, &H0)
            Public Shared ReadOnly IShellFolder2 As New Guid(&H93F2F68, &H1D1B, &H11D3, &HA3, &HE, &H0, &HC0, &H4F, &H79, &HAB, &HD1)
            Public Shared ReadOnly IEnumExtraSearch As New Guid(&HE700BE, &H9DB6, &H11D1, &HA1, &HCE, &H0, &HC0, &H4F, &HD7, &H5D, &H13)
            Public Shared ReadOnly IDocViewSite As New Guid(&H87D605E, &HC511, &H11CF, &H89, &HA9, &H0, &HA0, &HC9, &H5, &H41, &H29)
            Public Shared ReadOnly CDefView As New Guid(&H4434FF8, &HEF4C, &H11CE, &HAE, &H65, &H8, &H0, &H2B, &H2E, &H12, &H62)
            Public Shared ReadOnly IBanneredBar As New Guid(&H596A9A9, &H13E, &H11D1, &H8D, &H34, &H0, &HA0, &HC9, &HF, &H27, &H19)
        End Structure
#End Region

#Region "Structs"
        <StructLayout(LayoutKind.Sequential)>
        Private Structure ComPoint
            Public x As Integer
            Public y As Integer

            Public Sub New(pos As Point)
                x = pos.X
                y = pos.Y
            End Sub
        End Structure

        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/ns-shobjidl_core-cminvokecommandinfoex
        ''' <summary>Contains extended information about a shortcut menu command.</summary>
        <StructLayout(LayoutKind.Sequential)>
        Private Structure CMInvokeCommandInfoEx
            ''' <summary>
            ''' The size of this structure, in bytes. This member should be filled in by callers of <see cref="IContextMenu.InvokeCommand"/> and tested by the
            ''' implementations to know that the structure is a <see cref="CMInvokeCommandInfoEx"/> structure rather than CMInvokeCommandInfo.
            ''' </summary>
            Public cbSize As UInteger
            ''' <summary>Zero, or one or more of the <see cref="CMICMask"/> flags are set to indicate desired behavior and indicate that other fields in the structure are to be used.</summary>
            Public fMask As CMICMask
            ''' <summary>
            ''' A handle to the window that is the owner of the shortcut menu. An extension can also use this handle as the owner of any message boxes or dialog boxes
            ''' it displays. Callers must specify a legitimate HWND that can be used as the owner window for any UI that may be displayed. Failing to specify an HWND
            ''' when calling from a UI thread (one with windows already created) will result in reentrancy and possible bugs in the implementation of a
            ''' <see cref="IContextMenu.InvokeCommand"/> call.
            ''' </summary>
            Public hwnd As IntPtr
            ''' <summary>
            ''' The address of a null-terminated string that specifies the language-independent name of the command to carry out. This member is typically a string
            ''' when a command is being activated by an application.
            ''' <br/>If a canonical verb exists and a menu handler does not implement the canonical verb, it must return a failure code to enable the next handler
            ''' to be able to handle this verb. Failing to do this will break functionality in the system including ShellExecute.
            ''' <br/>Alternatively, rather than a pointer, this parameter can be MAKEINTRESOURCE(offset) where offset is the menu-identifier offset of the command
            ''' to carry out. Implementations can use the IS_INTRESOURCE macro to detect that this alternative is being employed. The Shell uses this alternative
            ''' when the user chooses a menu command.
            ''' <br/><br/>.Net Implementation: This member is an IntPtr so indexes can be passed - use <see cref="Marshal.StringToHGlobalAnsi"/> to use a string.
            ''' </summary>
            Public lpVerb As IntPtr
            ''' <summary>Optional parameters. This member is always NULL for menu items inserted by a Shell extension.</summary>
            <MarshalAs(UnmanagedType.LPStr)>
            Public lpParameters As String
            ''' <summary>An optional working directory name. This member is always NULL for menu items inserted by a Shell extension.</summary>
            <MarshalAs(UnmanagedType.LPStr)>
            Public lpDirectory As String
            ''' <summary>A set of <see cref="ShowWindowFlags"/> values to pass to the ShowWindow function if the command displays a window or starts an application.</summary>
            Public nShow As ShowWindowFlags
            ''' <summary>
            ''' An optional keyboard shortcut to assign to any application activated by the command. If the <see cref="fMask"/> member does not specify
            ''' <see cref="CMICMask.Hotkey"/>, this member is ignored.
            ''' </summary>
            Public dwHotKey As UInteger
            ''' <summary>
            ''' An icon to use for any application activated by the command. If the <see cref="fMask"/> member does not specify
            ''' <see cref="CMICMask.Icon"/>, this member is ignored.
            ''' </summary>
            Public hIcon As IntPtr
            ''' <summary>An ASCII title.</summary>
            <MarshalAs(UnmanagedType.LPStr)>
            Public lpTitle As String
            ''' <summary>
            ''' A Unicode verb, for those commands that can use it.
            ''' <br/>This member is an IntPtr so indexes can be passed - use <see cref="Marshal.StringToHGlobalUni"/> to use a string.
            ''' </summary>
            Public lpVerbW As IntPtr
            ''' <summary>A Unicode parameters, for those commands that can use it.</summary>
            <MarshalAs(UnmanagedType.LPWStr)>
            Public lpParametersW As String
            ''' <summary>A Unicode directory, for those commands that can use it.</summary>
            <MarshalAs(UnmanagedType.LPWStr)>
            Public lpDirectoryW As String
            ''' <summary>A Unicode title.</summary>
            <MarshalAs(UnmanagedType.LPWStr)>
            Public lpTitleW As String
            ''' <summary>
            ''' The point where the command is invoked. If the <see cref="fMask"/> member does not specify <see cref="CMICMask.PTInvoke"/>,
            ''' this member is ignored. This member is not valid prior to Internet Explorer 4.0.
            ''' </summary>
            Public ptInvoke As ComPoint
        End Structure
#End Region

#Region "Interfaces"
        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-icontextmenu
        <ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E4-0000-0000-c000-000000000046")>
        Private Interface IContextMenu
            'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-querycontextmenu
            ''' <summary>Adds commands to a shortcut menu.</summary>
            ''' <param name="hMenu">A handle to the shortcut menu. The handler should specify this handle when adding menu items.</param>
            ''' <param name="indexMenu">The zero-based position at which to insert the first new menu item.</param>
            ''' <param name="idCmdFirst">The minimum value that the handler can specify for a menu item identifier.</param>
            ''' <param name="idCmdLast">The maximum value that the handler can specify for a menu item identifier.</param>
            ''' <param name="uFlags">Optional flags that specify how the shortcut menu can be changed.</param>
            ''' <returns>
            ''' If successful, returns an HRESULT value that has its severity value set to SEVERITY_SUCCESS and its code value set to the offset of the largest command
            ''' identifier that was assigned, plus one. For example, if <paramref name="idCmdFirst"/> is set to 5 and you add three items to the menu with command
            ''' identifiers of 5, 7, and 8, the return value should be MAKE_HRESULT(SEVERITY_SUCCESS, 0, 8 - 5 + 1). Otherwise, it returns a COM error value.
            ''' </returns>
            Sub QueryContextMenu(hMenu As IntPtr, indexMenu As UInteger, idCmdFirst As UInteger, idCmdLast As UInteger, uFlags As QueryContextMenuFlags)

            'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-invokecommand
            ''' <summary>Carries out the command associated with a shortcut menu item.</summary>
            ''' <param name="pici">A pointer to a CMINVOKECOMMANDINFO or CMINVOKECOMMANDINFOEX structure that contains specifics about the command.</param>
            ''' <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            Sub InvokeCommand(ByRef pici As CMInvokeCommandInfoEx)

            'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu-getcommandstring
            ''' <summary>
            ''' Gets information about a shortcut menu command, including the help string and the language-independent, or canonical, name for the command.
            ''' </summary>
            ''' <param name="idCmd">Menu command identifier offset.</param>
            ''' <param name="uType">Flags specifying the information to return.</param>
            ''' <param name="pReserved">Reserved. Applications must specify NULL when calling this method and handlers must ignore this parameter when called.</param>
            ''' <param name="pszName">The address of the buffer to receive the null-terminated string being retrieved.</param>
            ''' <param name="cchMax">Size of the buffer, in characters, to receive the null-terminated string.</param>
            ''' <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            Sub GetCommandString(
                idCmd As UIntPtr,
                uType As GetCommandStringFlags,
                ByRef pReserved As UInteger,
                pszName As IntPtr,
                cchMax As UInteger)
        End Interface

        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-icontextmenu2
        <ComImport, Guid("000214f4-0000-0000-c000-000000000046")>
        Private Interface IContextMenu2
            Inherits IContextMenu

            'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu2-handlemenumsg
            ''' <summary>Enables client objects of the <see cref="IContextMenu"/> interface to handle messages associated with owner-drawn menu items.</summary>
            ''' <param name="uMsg">The message to be processed. In the case of some messages, such as WM_INITMENUPOPUP, WM_DRAWITEM, WM_MENUCHAR, or WM_MEASUREITEM, the client object being called may provide owner-drawn menu items.</param>
            ''' <param name="wParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            ''' <param name="lParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            ''' <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            Sub HandleMenuMsg(uMsg As Integer, wParam As IntPtr, lParam As IntPtr)
        End Interface

        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-icontextmenu3
        <ComImport, Guid("BCFCE0A0-EC17-11d0-8D10-00A0C90F2719")>
        Private Interface IContextMenu3
            Inherits IContextMenu2

            'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-icontextmenu3-handlemenumsg2
            ''' <summary>Allows client objects of the <see cref="IContextMenu3"/> interface to handle messages associated with owner-drawn menu items.</summary>
            ''' <param name="uMsg">The message to be processed. In the case of some messages, such as WM_INITMENUPOPUP, WM_DRAWITEM, WM_MENUCHAR, or WM_MEASUREITEM, the client object being called may provide owner-drawn menu items.</param>
            ''' <param name="wParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            ''' <param name="lParam">Additional message information. The value of this parameter depends on the value of the <paramref name="uMsg"/> parameter.</param>
            ''' <param name="plResult">The address of an LRESULT value that the owner of the menu will return from the message. This parameter can be NULL.</param>
            ''' <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            Sub HandleMenuMsg2(uMsg As Integer, wParam As IntPtr, lParam As IntPtr, plResult As IntPtr)
        End Interface

        'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-ishellfolder
        'https://www.pinvoke.net/default.aspx/Interfaces/IShellFolder.html
        <ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E6-0000-0000-C000-000000000046")>
        Private Interface IShellFolder
            'interface members are stubs, because GetUIObjectOf is the only function needed - and there are lots of members of this interface.
            Sub ParseDisplayName()
            Sub EnumObjects()
            Sub BindToObject()
            Sub BindToStorage()
            Sub CompareIDs()
            Sub CreateViewObject()
            Sub GetAttributesOf()

            'https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishellfolder-getuiobjectof
            ''' <summary>Gets an object that can be used to carry out actions on the specified file objects or folders.</summary>
            ''' <param name="hwndOwner">A handle to the owner window that the client should specify if it displays a dialog box or message box.</param>
            ''' <param name="cIDList">The number of file objects or subfolders specified in the apidl parameter.</param>
            ''' <param name="apIDList">
            ''' The address of an array of pointers to ITEMIDLIST structures, each of which uniquely identifies a file object or subfolder relative to the
            ''' parent folder. Each item identifier list must contain exactly one SHITEMID structure followed by a terminating zero.
            ''' </param>
            ''' <param name="riid">A reference to the IID of the interface to retrieve through ppv. This can be any valid interface identifier that can be created for an item.</param>
            ''' <param name="rgfReserved">Reserved.</param>
            ''' <param name="ppv">When this method returns successfully, contains the interface pointer requested in <paramref name="riid"/>.</param>
            ''' <returns>If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            Sub GetUIObjectOf(
                hwndOwner As IntPtr,
                cIDList As UInteger,
                <MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=1)>
                apIDList As IntPtr(),
                ByRef riid As Guid,
                ByRef rgfReserved As UInteger,
                <MarshalAs(UnmanagedType.Interface)>
                ByRef ppv As Object)

            Sub GetDisplayNameOf()
            Sub SetNameOf()
        End Interface
#End Region

#Region "Methods"
        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createpopupmenu
        ''' <summary>
        ''' Creates a drop-down menu, submenu, or shortcut menu. The menu is initially empty. You can insert or append menu items by using the InsertMenuItem
        ''' function. You can also use the InsertMenu function to insert menu items and the AppendMenu function to append menu items.
        ''' </summary>
        ''' <returns>
        ''' If the function succeeds, the return value is a handle to the newly created menu.
        ''' If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function CreatePopupMenu() As IntPtr
        End Function

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroymenu
        ''' <summary>Destroys the specified menu and frees any memory that the menu occupies.</summary>
        ''' <param name="hMenu">A handle to the menu to be destroyed.</param>
        ''' <returns>
        ''' If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function DestroyMenu(hMenu As IntPtr) As Boolean
        End Function

        'https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shparsedisplayname
        ''' <summary>
        ''' Translates a Shell namespace object's display name into an item identifier list and returns the attributes of the object.
        ''' This function is the preferred method to convert a string to a pointer to an item identifier list (PIDL).
        ''' </summary>
        ''' <param name="pszName">A pointer to a zero-terminated wide string that contains the display name to parse.</param>
        ''' <param name="pbc">A bind context that controls the parsing operation. This parameter is normally set to NULL. Type: IBindCtx*</param>
        ''' <param name="ppIDList">The address of a pointer to a variable of type ITEMIDLIST that receives the item identifier list for the object. If an error occurs, then this parameter is set to NULL.</param>
        ''' <param name="sfgaoIn">A ULONG value that specifies the attributes to query. To query for one or more attributes, initialize this parameter with the flags that represent the attributes of interest.</param>
        ''' <param name="psfgaoOut">A pointer to a ULONG. On return, those attributes that are true for the object and were requested in <paramref name="sfgaoIn"/> are set. An object's attribute flags can be zero or a combination of SFGAO flags.</param>
        ''' <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        <DllImport("shell32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function SHParseDisplayName(
            <MarshalAs(UnmanagedType.LPWStr)>
            pszName As String,
            pbc As IntPtr,
            ByRef ppIDList As IntPtr,
            sfgaoIn As SFGAO,
            ByRef psfgaoOut As SFGAO) As Integer
        End Function

        'https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shbindtoparent
        ''' <summary>
        ''' Takes a pointer to a fully qualified item identifier list (<paramref name="pIDList"/>), and returns a specified interface pointer on the parent object.
        ''' </summary>
        ''' <param name="pIDList">The item's PIDList.</param>
        ''' <param name="riid">The REFIID of one of the interfaces exposed by the item's parent object.</param>
        ''' <param name="ppv">A pointer to the interface specified by <paramref name="riid"/>. You must release the object when you are finished.</param>
        ''' <param name="ppIDListLast">
        ''' The item's PIDList relative to the parent folder. This PIDList can be used with many of the methods supported by the parent folder's interfaces.
        ''' If you set <paramref name="ppIDListLast"/> to NULL, the PIDList is not returned.
        ''' <br/>Note: SHBindToParent does not allocate a new PIDList; it simply receives a pointer through this parameter.
        ''' Therefore, you are not responsible for freeing this resource.
        ''' </param>
        ''' <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        <DllImport("shell32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function SHBindToParent(
            pIDList As IntPtr,
            ByRef riid As Guid,
            <MarshalAs(UnmanagedType.Interface)>
            ByRef ppv As Object,
            ByRef ppIDListLast As IntPtr) As Integer
        End Function

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-insertmenuw
        ''' <summary>Inserts a new menu item into a menu, moving other items down the menu.</summary>
        ''' <param name="hMenu">A handle to the menu to be changed.</param>
        ''' <param name="uPosition">The menu item before which the new menu item is to be inserted, as determined by the <paramref name="uFlags"/> parameter.</param>
        ''' <param name="uFlags">Controls the interpretation of the <paramref name="uPosition"/> parameter and the content, appearance, and behavior of the new menu item.</param>
        ''' <param name="uIDNewItem">The identifier of the new menu item or, if the uFlags parameter has the <see cref="MenuFlags.Popup"/> flag set, a handle to the drop-down menu or submenu.</param>
        ''' <param name="lpNewItem">The content of the new menu item. The interpretation of <paramref name="lpNewItem"/> depends on whether the <paramref name="uFlags"/> parameter includes the <see cref="MenuFlags.Bitmap"/>, <see cref="MenuFlags.OwnerDraw"/>, or <see cref="MenuFlags.String"/> flag.</param>
        ''' <returns>
        ''' If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function InsertMenu(
            hMenu As IntPtr,
            uPosition As Integer,
            uFlags As MenuFlags,
            uIDNewItem As UIntPtr,
            <MarshalAs(UnmanagedType.LPWStr)>
            lpNewItem As String) As Boolean
        End Function

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-trackpopupmenuex
        ''' <summary>
        ''' Displays a shortcut menu at the specified location and tracks the selection of items on the shortcut menu. The shortcut menu can appear anywhere on the screen.
        ''' </summary>
        ''' <param name="hMenu">
        ''' A handle to the shortcut menu to be displayed. This handle can be obtained by calling the <see cref="CreatePopupMenu"/> function to create a new shortcut
        ''' menu or by calling the GetSubMenu function to retrieve a handle to a submenu associated with an existing menu item.
        ''' </param>
        ''' <param name="uFlags">Specifies function options.</param>
        ''' <param name="x">The horizontal location of the shortcut menu, in screen coordinates.</param>
        ''' <param name="y">The vertical location of the shortcut menu, in screen coordinates.</param>
        ''' <param name="hwnd">
        ''' A handle to the window that owns the shortcut menu. This window receives all messages from the menu. The window does not receive a WM_COMMAND
        ''' message from the menu until the function returns. If you specify <see cref="TrackPopupMenuExFlags.NoNotify"/> in the <paramref name="uFlags"/> parameter,
        ''' the function does not send messages to the window identified by <paramref name="hwnd"/>. However, you must still pass a window handle in <paramref name="hwnd"/>.
        ''' It can be any window handle from your application.
        ''' </param>
        ''' <param name="lptpm">A pointer to a TPMPARAMS structure that specifies an area of the screen the menu should not overlap. This parameter can be NULL.</param>
        ''' <returns>
        ''' If you specify <see cref="TrackPopupMenuExFlags.ReturnCmd"/> in the <paramref name="uFlags"/> parameter, the return value is the menu-item identifier of
        ''' the item that the user selected. If the user cancels the menu without making a selection, or if an error occurs, the return value is zero.
        ''' <br/>If you do not specify <see cref="TrackPopupMenuExFlags.ReturnCmd"/> in the <paramref name="uFlags"/> parameter, the return value is nonzero if the
        ''' function succeeds and zero if it fails. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function TrackPopupMenuEx(
            hMenu As IntPtr,
            uFlags As TrackPopupMenuExFlags,
            x As Integer,
            y As Integer,
            hwnd As IntPtr,
            lptpm As IntPtr) As Integer
        End Function
#End Region

#End Region

#Region "Shared Methods"
        Private Shared Function GetUIObjectOfFile(hwnd As IntPtr, pszPath As String, ByRef rIID As Guid) As Object
            Dim hr As Integer
            Dim pIDList As IntPtr
            Dim sfgao As SFGAO

            hr = SHParseDisplayName(pszPath, Nothing, pIDList, SFGAO.None, sfgao)
            If hr < 0 Then Marshal.ThrowExceptionForHR(hr)

            Try
                Dim pSF As Object = Nothing
                Dim pIDListChild As IntPtr

                hr = SHBindToParent(pIDList, IID.IShellFolder, pSF, pIDListChild)
                If hr < 0 Then Return hr

                Dim shellFolder As IShellFolder = DirectCast(pSF, IShellFolder)
                Dim ppV As Object = Nothing

                shellFolder.GetUIObjectOf(hwnd, 1, New IntPtr() {pIDListChild}, rIID, Nothing, ppV)
                Return ppV
            Finally
                Marshal.FreeCoTaskMem(pIDList)
            End Try
        End Function

        Private Shared Function IContextMenu_GetCommandString(contextMenu As IContextMenu, idCmd As UIntPtr, uFlags As GetCommandStringFlags, ByRef pwReserved As UInteger) As String
            ' Callers are expected to be using Unicode.
            If Not uFlags.HasFlag(GetCommandStringFlags.Unicode) Then Throw New ArgumentException("Unicode flag expected!", "uFlags")

            ' First try the Unicode message. Preset the output buffer with a known value because some handlers return S_OK without doing anything.
            Dim pszUnicode As IntPtr = Marshal.AllocHGlobal(MAX_FILE_PATH * Marshal.SizeOf(Of Int16))
            Try
                Try
                    Marshal.WriteInt16(pszUnicode, 0)

                    ' Some context menu handlers have off-by-one bugs and will overflow the output buffer. Specify less buffer size so a one-character overflow won't corrupt memory.
                    contextMenu.GetCommandString(idCmd, uFlags, pwReserved, pszUnicode, MAX_FILE_PATH - 1)

                    If Marshal.ReadInt16(pszUnicode) = 0 Then
                        ' Rats, a buggy IContextMenu handler that returned success even though it failed.
                        Marshal.ThrowExceptionForHR(&H80004001) ' E_NOTIMPL
                    End If

                    Return Marshal.PtrToStringUni(pszUnicode)
                Finally
                    Marshal.FreeHGlobal(pszUnicode)
                End Try
            Catch
                ' try again with ANSI
                Dim pszAnsi As IntPtr = Marshal.AllocHGlobal(MAX_FILE_PATH * Marshal.SizeOf(Of Byte))
                Try
                    Marshal.WriteByte(pszAnsi, 0)

                    contextMenu.GetCommandString(idCmd, uFlags And Not GetCommandStringFlags.Unicode, pwReserved, pszAnsi, MAX_FILE_PATH - 1)

                    If Marshal.ReadByte(pszAnsi) = 0 Then
                        Marshal.ThrowExceptionForHR(&H80004001) ' E_NOTIMPL
                    End If

                    Return Marshal.PtrToStringAnsi(pszAnsi)
                Finally
                    Marshal.FreeHGlobal(pszAnsi)
                End Try
            End Try
        End Function
#End Region

#Region "Instance Methods"
        Private _contextMenu As IntPtr
        Private _icontextMenu As IContextMenu
        Private _icontextMenu2 As IContextMenu2
        Private _icontextMenu3 As IContextMenu3

        Private Const _firstItem As UInteger = &H1
        Private Const _maxItems As UInteger = &H7FFF

        Private _lastItem As UInteger = _maxItems
        Private _customItemCount As UInteger = 0
        Private _customItemDict As New Dictionary(Of UInteger, Action)

        Private _isShown As Boolean = False
        Private _disposed As Boolean
        Public Event HelpTextChanged(text As String, ex As Exception)

        Public Function IsBuilt() As Boolean
            Return _icontextMenu IsNot Nothing AndAlso _contextMenu <> IntPtr.Zero
        End Function

        Public Function BuildMenu(frmHandle As IntPtr, itemPath As String, Optional allowSpaceFor As UInteger = 0) As ContextMenu
            If IsBuilt() Then DestroyMenu()

            Dim pCM As Object = GetUIObjectOfFile(frmHandle, itemPath, IID.IContextMenu)

            _contextMenu = CreatePopupMenu()
            If _contextMenu = IntPtr.Zero Then Throw New Win32Exception()

            ' set _icontextMenu after testing CreatePopupMenu, so interface can be freed if there was an error
            _icontextMenu = DirectCast(pCM, IContextMenu)

            Try
                _lastItem = _maxItems - allowSpaceFor
                _icontextMenu.QueryContextMenu(_contextMenu, 0, _firstItem, _lastItem, QueryContextMenuFlags.Normal)
            Catch
                ' if QueryContextMenu throws an error, destroy the popupmenu and free _icontextMenu before re-throwing.
                DestroyMenu(_contextMenu)
                _contextMenu = IntPtr.Zero
                _icontextMenu = Nothing
                Throw
            End Try

            Return Me
        End Function

        Public Sub AddItem(position As Integer, text As String, action As Action, Optional flags As AddItemFlags = 0)
            If Not IsBuilt() Then Throw New NotSupportedException("Menu hasn't been built!")

            If Not (_lastItem + _customItemCount) < _maxItems Then
                Throw New ArgumentOutOfRangeException("BuildMenu.allowSpaceFor", "No space allowed for custom menu item!")
            End If

            Dim menuFlags As MenuFlags = MenuFlags.ByPosition Or DirectCast(flags, MenuFlags)
            _customItemCount += 1UI
            Dim newItemID As UInteger = _lastItem + _customItemCount

            _customItemDict.Add(newItemID, action)
            InsertMenu(_contextMenu, position, menuFlags, CType(newItemID, UIntPtr), text)
        End Sub

        Private Sub RunItem(iCmd As Integer, frmHandle As IntPtr, pos As Point)
            If iCmd > _lastItem AndAlso _customItemDict.ContainsKey(CType(iCmd, UInteger)) Then
                Dim act As Action = _customItemDict.Item(CType(iCmd, UInteger))
                act()
            Else
                Dim pt As New ComPoint(pos)
                Dim info As CMInvokeCommandInfoEx = Nothing

                info.cbSize = CType(Marshal.SizeOf(info), UInteger)
                info.fMask = CMICMask.Unicode Or CMICMask.PTInvoke

                If My.Computer.Keyboard.CtrlKeyDown Then
                    info.fMask = info.fMask Or CMICMask.ControlDown
                End If
                If My.Computer.Keyboard.ShiftKeyDown Then
                    info.fMask = info.fMask Or CMICMask.ShiftDown
                End If

                info.hwnd = frmHandle
                info.lpVerb = CType(iCmd - _firstItem, IntPtr)
                info.lpVerbW = CType(iCmd - _firstItem, IntPtr)
                info.nShow = ShowWindowFlags.ShowNormal
                info.ptInvoke = pt
                _icontextMenu.InvokeCommand(info)
            End If
        End Sub

        Public Sub ShowMenu(frmHandle As IntPtr, pos As Point)
            If Not IsBuilt() Then Throw New NotSupportedException("Menu hasn't been built!")

            Dim iCmd As Integer

            _icontextMenu2 = TryCast(_icontextMenu, IContextMenu2) ' casting performs QueryInterface under the hood
            _icontextMenu3 = TryCast(_icontextMenu, IContextMenu3) ' TryCast returns Nothing if cast failed

            _isShown = True
            Try
                iCmd = TrackPopupMenuEx(_contextMenu, TrackPopupMenuExFlags.ReturnCmd, pos.X, pos.Y, frmHandle, IntPtr.Zero)
            Finally
                _isShown = False

                If _icontextMenu2 IsNot Nothing Then _icontextMenu2 = Nothing
                If _icontextMenu3 IsNot Nothing Then _icontextMenu3 = Nothing
            End Try

            If iCmd > 0 Then
                RunItem(iCmd, frmHandle, pos)
            Else
                If Marshal.GetLastWin32Error <> 0 Then
                    Throw New Win32Exception()
                End If
            End If
        End Sub

        Private Sub OnMenuSelect(item As UInteger)
            If IsBuilt() AndAlso _isShown Then
                If item >= _firstItem AndAlso item <= _lastItem Then
                    Try
                        RaiseEvent HelpTextChanged(
                            IContextMenu_GetCommandString(_icontextMenu, CType(item - _firstItem, UIntPtr), GetCommandStringFlags.HelpTextW, Nothing),
                            Nothing)
                    Catch ex As Exception
                        RaiseEvent HelpTextChanged(Nothing, ex)
                    End Try
                Else
                    RaiseEvent HelpTextChanged(Nothing, Nothing)
                End If
            End If
        End Sub

        Private Const WM_MENUSELECT As Integer = &H11F
        Public Sub HandleWindowMessage(ByRef m As Message)
            If IsBuilt() Then
                If m.Msg = WM_MENUSELECT Then
                    'simplified HANDLE_WM_MENUSELECT C++ macro
                    Dim wParamUInt As UInteger = CType(m.WParam.ToInt64, UInteger)
                    ' >>16 is equivalent to HIWORD C++ macro
                    If ((wParamUInt >> 16) And MenuFlags.Popup) <> 0 Then
                        OnMenuSelect(0)
                    Else
                        OnMenuSelect(wParamUInt And &HFFFFUI) ' & 0xFFFF is equivalent to LOWORD C++ macro
                    End If
                End If

                If _icontextMenu3 IsNot Nothing Then
                    Dim lres As IntPtr
                    Try
                        _icontextMenu3.HandleMenuMsg2(m.Msg, m.WParam, m.LParam, lres)
                        m.Result = lres
                        Return
                    Catch
                    End Try
                ElseIf _icontextMenu2 IsNot Nothing Then
                    Try
                        _icontextMenu2.HandleMenuMsg(m.Msg, m.WParam, m.LParam)
                        m.Result = IntPtr.Zero
                        Return
                    Catch
                    End Try
                End If
            End If
        End Sub

        Public Sub DestroyMenu()
            If IsBuilt() Then
                _customItemCount = 0
                _customItemDict.Clear()
                DestroyMenu(_contextMenu)
                _contextMenu = IntPtr.Zero
                _icontextMenu = Nothing ' removing references to an interface automatically calls Marshal.Release
            End If
        End Sub

#Region "IDisposable Implementation"
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    ' dispose managed state (managed objects)
                    _customItemCount = 0
                    _customItemDict.Clear()
                End If

                ' free unmanaged resources (unmanaged objects) and override finalizer
                If _contextMenu <> IntPtr.Zero Then
                    DestroyMenu(_contextMenu)
                    _contextMenu = IntPtr.Zero
                End If
                _icontextMenu = Nothing
                _disposed = True
            End If
        End Sub

        ' override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
        Protected Overrides Sub Finalize()
            ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            Dispose(disposing:=False)
            MyBase.Finalize()
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

#End Region
    End Class
End Class
