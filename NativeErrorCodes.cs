public partial class WalkmanLib {
    public enum NativeErrorCode : int {
        /// <summary>The operation completed successfully.</summary>
        ERROR_SUCCESS = 0x0,
        /// <summary>Incorrect function.</summary>
        ERROR_INVALID_FUNCTION = 0x1,
        /// <summary>The system cannot find the file specified.</summary>
        ERROR_FILE_NOT_FOUND = 0x2,
        /// <summary>The system cannot find the path specified.</summary>
        ERROR_PATH_NOT_FOUND = 0x3,
        /// <summary>The system cannot open the file.</summary>
        ERROR_TOO_MANY_OPEN_FILES = 0x4,
        /// <summary>Access is denied.</summary>
        ERROR_ACCESS_DENIED = 0x5,
        /// <summary>The handle is invalid.</summary>
        ERROR_INVALID_HANDLE = 0x6,
        /// <summary>The storage control blocks were destroyed.</summary>
        ERROR_ARENA_TRASHED = 0x7,
        /// <summary>Not enough memory resources are available to process this command.</summary>
        ERROR_NOT_ENOUGH_MEMORY = 0x8,
        /// <summary>The storage control block address is invalid.</summary>
        ERROR_INVALID_BLOCK = 0x9,
        /// <summary>[10] The environment is incorrect.</summary>
        ERROR_BAD_ENVIRONMENT = 0xA,
        /// <summary>[11] An attempt was made to load a program with an incorrect format.</summary>
        ERROR_BAD_FORMAT = 0xB,
        /// <summary>[12] The access code is invalid.</summary>
        ERROR_INVALID_ACCESS = 0xC,
        /// <summary>[13] The data is invalid.</summary>
        ERROR_INVALID_DATA = 0xD,
        /// <summary>[14] Not enough storage is available to complete this operation.</summary>
        ERROR_OUTOFMEMORY = 0xE,
        /// <summary>[15] The system cannot find the drive specified.</summary>
        ERROR_INVALID_DRIVE = 0xF,
        /// <summary>[16] The directory cannot be removed.</summary>
        ERROR_CURRENT_DIRECTORY = 0x10,
        /// <summary>[17] The system cannot move the file to a different disk drive.</summary>
        ERROR_NOT_SAME_DEVICE = 0x11,
        /// <summary>[18] There are no more files.</summary>
        ERROR_NO_MORE_FILES = 0x12,
        /// <summary>[19] The media is write protected.</summary>
        ERROR_WRITE_PROTECT = 0x13,
        /// <summary>[20] The system cannot find the device specified.</summary>
        ERROR_BAD_UNIT = 0x14,
        /// <summary>[21] The device is not ready.</summary>
        ERROR_NOT_READY = 0x15,
        /// <summary>[22] The device does not recognize the command.</summary>
        ERROR_BAD_COMMAND = 0x16,
        /// <summary>[23] Data error (cyclic redundancy check).</summary>
        ERROR_CRC = 0x17,
        /// <summary>[24] The program issued a command but the command length is incorrect.</summary>
        ERROR_BAD_LENGTH = 0x18,
        /// <summary>[25] The drive cannot locate a specific area or track on the disk.</summary>
        ERROR_SEEK = 0x19,
        /// <summary>[26] The specified disk or diskette cannot be accessed.</summary>
        ERROR_NOT_DOS_DISK = 0x1A,
        /// <summary>[27] The drive cannot find the sector requested.</summary>
        ERROR_SECTOR_NOT_FOUND = 0x1B,
        /// <summary>[28] The printer is out of paper.</summary>
        ERROR_OUT_OF_PAPER = 0x1C,
        /// <summary>[29] The system cannot write to the specified device.</summary>
        ERROR_WRITE_FAULT = 0x1D,
        /// <summary>[30] The system cannot read from the specified device.</summary>
        ERROR_READ_FAULT = 0x1E,
        /// <summary>[31] A device attached to the system is not functioning.</summary>
        ERROR_GEN_FAILURE = 0x1F,
        /// <summary>[32] The process cannot access the file because it is being used by another process.</summary>
        ERROR_SHARING_VIOLATION = 0x20,
        /// <summary>[33] The process cannot access the file because another process has locked a portion of the file.</summary>
        ERROR_LOCK_VIOLATION = 0x21,
        /// <summary>[34] The wrong diskette is in the drive. Insert %2 (Volume Serial Number: %3) into drive %1.</summary>
        ERROR_WRONG_DISK = 0x22,
        /// <summary>[36] Too many files opened for sharing.</summary>
        ERROR_SHARING_BUFFER_EXCEEDED = 0x24,
        /// <summary>[38] Reached the end of the file.</summary>
        ERROR_HANDLE_EOF = 0x26,
        /// <summary>[39] The disk is full.</summary>
        ERROR_HANDLE_DISK_FULL = 0x27,
        /// <summary>[50] The request is not supported.</summary>
        ERROR_NOT_SUPPORTED = 0x32,
        /// <summary>[51] Windows cannot find the network path. Verify that the network path is correct and the destination computer is not busy or turned off. If Windows still cannot find the network path, contact your network administrator.</summary>
        ERROR_REM_NOT_LIST = 0x33,
        /// <summary>[52] You were not connected because a duplicate name exists on the network. If joining a domain, go to System in Control Panel to change the computer name and try again. If joining a workgroup, choose another workgroup name.</summary>
        ERROR_DUP_NAME = 0x34,
        /// <summary>[53] The network path was not found.</summary>
        ERROR_BAD_NETPATH = 0x35,
        /// <summary>[54] The network is busy.</summary>
        ERROR_NETWORK_BUSY = 0x36,
        /// <summary>[55] The specified network resource or device is no longer available.</summary>
        ERROR_DEV_NOT_EXIST = 0x37,
        /// <summary>[56] The network BIOS command limit has been reached.</summary>
        ERROR_TOO_MANY_CMDS = 0x38,
        /// <summary>[57] A network adapter hardware error occurred.</summary>
        ERROR_ADAP_HDW_ERR = 0x39,
        /// <summary>[58] The specified server cannot perform the requested operation.</summary>
        ERROR_BAD_NET_RESP = 0x3A,
        /// <summary>[59] An unexpected network error occurred.</summary>
        ERROR_UNEXP_NET_ERR = 0x3B,
        /// <summary>[60] The remote adapter is not compatible.</summary>
        ERROR_BAD_REM_ADAP = 0x3C,
        /// <summary>[61] The printer queue is full.</summary>
        ERROR_PRINTQ_FULL = 0x3D,
        /// <summary>[62] Space to store the file waiting to be printed is not available on the server.</summary>
        ERROR_NO_SPOOL_SPACE = 0x3E,
        /// <summary>[63] Your file waiting to be printed was deleted.</summary>
        ERROR_PRINT_CANCELLED = 0x3F,
        /// <summary>[64] The specified network name is no longer available.</summary>
        ERROR_NETNAME_DELETED = 0x40,
        /// <summary>[65] Network access is denied.</summary>
        ERROR_NETWORK_ACCESS_DENIED = 0x41,
        /// <summary>[66] The network resource type is not correct.</summary>
        ERROR_BAD_DEV_TYPE = 0x42,
        /// <summary>[67] The network name cannot be found.</summary>
        ERROR_BAD_NET_NAME = 0x43,
        /// <summary>[68] The name limit for the local computer network adapter card was exceeded.</summary>
        ERROR_TOO_MANY_NAMES = 0x44,
        /// <summary>[69] The network BIOS session limit was exceeded.</summary>
        ERROR_TOO_MANY_SESS = 0x45,
        /// <summary>[70] The remote server has been paused or is in the process of being started.</summary>
        ERROR_SHARING_PAUSED = 0x46,
        /// <summary>[71] No more connections can be made to this remote computer at this time because there are already as many connections as the computer can accept.</summary>
        ERROR_REQ_NOT_ACCEP = 0x47,
        /// <summary>[72] The specified printer or disk device has been paused.</summary>
        ERROR_REDIR_PAUSED = 0x48,
        /// <summary>[80] The file exists.</summary>
        ERROR_FILE_EXISTS = 0x50,
        /// <summary>[82] The directory or file cannot be created.</summary>
        ERROR_CANNOT_MAKE = 0x52,
        /// <summary>[83] Fail on INT 24.</summary>
        ERROR_FAIL_I24 = 0x53,
        /// <summary>[84] Storage to process this request is not available.</summary>
        ERROR_OUT_OF_STRUCTURES = 0x54,
        /// <summary>[85] The local device name is already in use.</summary>
        ERROR_ALREADY_ASSIGNED = 0x55,
        /// <summary>[86] The specified network password is not correct.</summary>
        ERROR_INVALID_PASSWORD = 0x56,
        /// <summary>[87] The parameter is incorrect.</summary>
        ERROR_INVALID_PARAMETER = 0x57,
        /// <summary>[88] A write fault occurred on the network.</summary>
        ERROR_NET_WRITE_FAULT = 0x58,
        /// <summary>[89] The system cannot start another process at this time.</summary>
        ERROR_NO_PROC_SLOTS = 0x59,
        /// <summary>[100] Cannot create another system semaphore.</summary>
        ERROR_TOO_MANY_SEMAPHORES = 0x64,
        /// <summary>[101] The exclusive semaphore is owned by another process.</summary>
        ERROR_EXCL_SEM_ALREADY_OWNED = 0x65,
        /// <summary>[102] The semaphore is set and cannot be closed.</summary>
        ERROR_SEM_IS_SET = 0x66,
        /// <summary>[103] The semaphore cannot be set again.</summary>
        ERROR_TOO_MANY_SEM_REQUESTS = 0x67,
        /// <summary>[104] Cannot request exclusive semaphores at interrupt time.</summary>
        ERROR_INVALID_AT_INTERRUPT_TIME = 0x68,
        /// <summary>[105] The previous ownership of this semaphore has ended.</summary>
        ERROR_SEM_OWNER_DIED = 0x69,
        /// <summary>[106] Insert the diskette for drive %1.</summary>
        ERROR_SEM_USER_LIMIT = 0x6A,
        /// <summary>[107] The program stopped because an alternate diskette was not inserted.</summary>
        ERROR_DISK_CHANGE = 0x6B,
        /// <summary>[108] The disk is in use or locked by another process.</summary>
        ERROR_DRIVE_LOCKED = 0x6C,
        /// <summary>[109] The pipe has been ended.</summary>
        ERROR_BROKEN_PIPE = 0x6D,
        /// <summary>[110] The system cannot open the device or file specified.</summary>
        ERROR_OPEN_FAILED = 0x6E,
        /// <summary>[111] The file name is too long.</summary>
        ERROR_BUFFER_OVERFLOW = 0x6F,
        /// <summary>[112] There is not enough space on the disk.</summary>
        ERROR_DISK_FULL = 0x70,
        /// <summary>[113] No more internal file identifiers available.</summary>
        ERROR_NO_MORE_SEARCH_HANDLES = 0x71,
        /// <summary>[114] The target internal file identifier is incorrect.</summary>
        ERROR_INVALID_TARGET_HANDLE = 0x72,
        /// <summary>[117] The IOCTL call made by the application program is not correct.</summary>
        ERROR_INVALID_CATEGORY = 0x75,
        /// <summary>[118] The verify-on-write switch parameter value is not correct.</summary>
        ERROR_INVALID_VERIFY_SWITCH = 0x76,
        /// <summary>[119] The system does not support the command requested.</summary>
        ERROR_BAD_DRIVER_LEVEL = 0x77,
        /// <summary>[120] This function is not supported on this system.</summary>
        ERROR_CALL_NOT_IMPLEMENTED = 0x78,
        /// <summary>[121] The semaphore timeout period has expired.</summary>
        ERROR_SEM_TIMEOUT = 0x79,
        /// <summary>[122] The data area passed to a system call is too small.</summary>
        ERROR_INSUFFICIENT_BUFFER = 0x7A,
        /// <summary>[123] The filename, directory name, or volume label syntax is incorrect.</summary>
        ERROR_INVALID_NAME = 0x7B,
        /// <summary>[124] The system call level is not correct.</summary>
        ERROR_INVALID_LEVEL = 0x7C,
        /// <summary>[125] The disk has no volume label.</summary>
        ERROR_NO_VOLUME_LABEL = 0x7D,
        /// <summary>[126] The specified module could not be found.</summary>
        ERROR_MOD_NOT_FOUND = 0x7E,
        /// <summary>[127] The specified procedure could not be found.</summary>
        ERROR_PROC_NOT_FOUND = 0x7F,
        /// <summary>[128] There are no child processes to wait for.</summary>
        ERROR_WAIT_NO_CHILDREN = 0x80,
        /// <summary>[129] The %1 application cannot be run in Win32 mode.</summary>
        ERROR_CHILD_NOT_COMPLETE = 0x81,
        /// <summary>[130] Attempt to use a file handle to an open disk partition for an operation other than raw disk I/O.</summary>
        ERROR_DIRECT_ACCESS_HANDLE = 0x82,
        /// <summary>[131] An attempt was made to move the file pointer before the beginning of the file.</summary>
        ERROR_NEGATIVE_SEEK = 0x83,
        /// <summary>[132] The file pointer cannot be set on the specified device or file.</summary>
        ERROR_SEEK_ON_DEVICE = 0x84,
        /// <summary>[133] A JOIN or SUBST command cannot be used for a drive that contains previously joined drives.</summary>
        ERROR_IS_JOIN_TARGET = 0x85,
        /// <summary>[134] An attempt was made to use a JOIN or SUBST command on a drive that has already been joined.</summary>
        ERROR_IS_JOINED = 0x86,
        /// <summary>[135] An attempt was made to use a JOIN or SUBST command on a drive that has already been substituted.</summary>
        ERROR_IS_SUBSTED = 0x87,
        /// <summary>[136] The system tried to delete the JOIN of a drive that is not joined.</summary>
        ERROR_NOT_JOINED = 0x88,
        /// <summary>[137] The system tried to delete the substitution of a drive that is not substituted.</summary>
        ERROR_NOT_SUBSTED = 0x89,
        /// <summary>[138] The system tried to join a drive to a directory on a joined drive.</summary>
        ERROR_JOIN_TO_JOIN = 0x8A,
        /// <summary>[139] The system tried to substitute a drive to a directory on a substituted drive.</summary>
        ERROR_SUBST_TO_SUBST = 0x8B,
        /// <summary>[140] The system tried to join a drive to a directory on a substituted drive.</summary>
        ERROR_JOIN_TO_SUBST = 0x8C,
        /// <summary>[141] The system tried to SUBST a drive to a directory on a joined drive.</summary>
        ERROR_SUBST_TO_JOIN = 0x8D,
        /// <summary>[142] The system cannot perform a JOIN or SUBST at this time.</summary>
        ERROR_BUSY_DRIVE = 0x8E,
        /// <summary>[143] The system cannot join or substitute a drive to or for a directory on the same drive.</summary>
        ERROR_SAME_DRIVE = 0x8F,
        /// <summary>[144] The directory is not a subdirectory of the root directory.</summary>
        ERROR_DIR_NOT_ROOT = 0x90,
        /// <summary>[145] The directory is not empty.</summary>
        ERROR_DIR_NOT_EMPTY = 0x91,
        /// <summary>[146] The path specified is being used in a substitute.</summary>
        ERROR_IS_SUBST_PATH = 0x92,
        /// <summary>[147] Not enough resources are available to process this command.</summary>
        ERROR_IS_JOIN_PATH = 0x93,
        /// <summary>[148] The path specified cannot be used at this time.</summary>
        ERROR_PATH_BUSY = 0x94,
        /// <summary>[149] An attempt was made to join or substitute a drive for which a directory on the drive is the target of a previous substitute.</summary>
        ERROR_IS_SUBST_TARGET = 0x95,
        /// <summary>[150] System trace information was not specified in your CONFIG.SYS file, or tracing is disallowed.</summary>
        ERROR_SYSTEM_TRACE = 0x96,
        /// <summary>[151] The number of specified semaphore events for DosMuxSemWait is not correct.</summary>
        ERROR_INVALID_EVENT_COUNT = 0x97,
        /// <summary>[152] DosMuxSemWait did not execute; too many semaphores are already set.</summary>
        ERROR_TOO_MANY_MUXWAITERS = 0x98,
        /// <summary>[153] The DosMuxSemWait list is not correct.</summary>
        ERROR_INVALID_LIST_FORMAT = 0x99,
        /// <summary>[154] The volume label you entered exceeds the label character limit of the target file system.</summary>
        ERROR_LABEL_TOO_LONG = 0x9A,
        /// <summary>[155] Cannot create another thread.</summary>
        ERROR_TOO_MANY_TCBS = 0x9B,
        /// <summary>[156] The recipient process has refused the signal.</summary>
        ERROR_SIGNAL_REFUSED = 0x9C,
        /// <summary>[157] The segment is already discarded and cannot be locked.</summary>
        ERROR_DISCARDED = 0x9D,
        /// <summary>[158] The segment is already unlocked.</summary>
        ERROR_NOT_LOCKED = 0x9E,
        /// <summary>[159] The address for the thread ID is not correct.</summary>
        ERROR_BAD_THREADID_ADDR = 0x9F,
        /// <summary>[160] One or more arguments are not correct.</summary>
        ERROR_BAD_ARGUMENTS = 0xA0,
        /// <summary>[161] The specified path is invalid.</summary>
        ERROR_BAD_PATHNAME = 0xA1,
        /// <summary>[162] A signal is already pending.</summary>
        ERROR_SIGNAL_PENDING = 0xA2,
        /// <summary>[164] No more threads can be created in the system.</summary>
        ERROR_MAX_THRDS_REACHED = 0xA4,
        /// <summary>[167] Unable to lock a region of a file.</summary>
        ERROR_LOCK_FAILED = 0xA7,
        /// <summary>[170] The requested resource is in use.</summary>
        ERROR_BUSY = 0xAA,
        /// <summary>[171] Device's command support detection is in progress.</summary>
        ERROR_DEVICE_SUPPORT_IN_PROGRESS = 0xAB,
        /// <summary>[173] A lock request was not outstanding for the supplied cancel region.</summary>
        ERROR_CANCEL_VIOLATION = 0xAD,
        /// <summary>[174] The file system does not support atomic changes to the lock type.</summary>
        ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 0xAE,
        /// <summary>[180] The system detected a segment number that was not correct.</summary>
        ERROR_INVALID_SEGMENT_NUMBER = 0xB4,
        /// <summary>[182] The operating system cannot run %1.</summary>
        ERROR_INVALID_ORDINAL = 0xB6,
        /// <summary>[183] Cannot create a file when that file already exists.</summary>
        ERROR_ALREADY_EXISTS = 0xB7,
        /// <summary>[186] The flag passed is not correct.</summary>
        ERROR_INVALID_FLAG_NUMBER = 0xBA,
        /// <summary>[187] The specified system semaphore name was not found.</summary>
        ERROR_SEM_NOT_FOUND = 0xBB,
        /// <summary>[188] The operating system cannot run %1.</summary>
        ERROR_INVALID_STARTING_CODESEG = 0xBC,
        /// <summary>[189] The operating system cannot run %1.</summary>
        ERROR_INVALID_STACKSEG = 0xBD,
        /// <summary>[190] The operating system cannot run %1.</summary>
        ERROR_INVALID_MODULETYPE = 0xBE,
        /// <summary>[191] Cannot run %1 in Win32 mode.</summary>
        ERROR_INVALID_EXE_SIGNATURE = 0xBF,
        /// <summary>[192] The operating system cannot run %1.</summary>
        ERROR_EXE_MARKED_INVALID = 0xC0,
        /// <summary>[193] %1 is not a valid Win32 application.</summary>
        ERROR_BAD_EXE_FORMAT = 0xC1,
        /// <summary>[194] The operating system cannot run %1.</summary>
        ERROR_ITERATED_DATA_EXCEEDS_64k = 0xC2,
        /// <summary>[195] The operating system cannot run %1.</summary>
        ERROR_INVALID_MINALLOCSIZE = 0xC3,
        /// <summary>[196] The operating system cannot run this application program.</summary>
        ERROR_DYNLINK_FROM_INVALID_RING = 0xC4,
        /// <summary>[197] The operating system is not presently configured to run this application.</summary>
        ERROR_IOPL_NOT_ENABLED = 0xC5,
        /// <summary>[198] The operating system cannot run %1.</summary>
        ERROR_INVALID_SEGDPL = 0xC6,
        /// <summary>[199] The operating system cannot run this application program.</summary>
        ERROR_AUTODATASEG_EXCEEDS_64k = 0xC7,
        /// <summary>[200] The code segment cannot be greater than or equal to 64K.</summary>
        ERROR_RING2SEG_MUST_BE_MOVABLE = 0xC8,
        /// <summary>[201] The operating system cannot run %1.</summary>
        ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 0xC9,
        /// <summary>[202] The operating system cannot run %1.</summary>
        ERROR_INFLOOP_IN_RELOC_CHAIN = 0xCA,
        /// <summary>[203] The system could not find the environment option that was entered.</summary>
        ERROR_ENVVAR_NOT_FOUND = 0xCB,
        /// <summary>[205] No process in the command subtree has a signal handler.</summary>
        ERROR_NO_SIGNAL_SENT = 0xCD,
        /// <summary>[206] The filename or extension is too long.</summary>
        ERROR_FILENAME_EXCED_RANGE = 0xCE,
        /// <summary>[207] The ring 2 stack is in use.</summary>
        ERROR_RING2_STACK_IN_USE = 0xCF,
        /// <summary>[208] The global filename characters, * or ?, are entered incorrectly or too many global filename characters are specified.</summary>
        ERROR_META_EXPANSION_TOO_LONG = 0xD0,
        /// <summary>[209] The signal being posted is not correct.</summary>
        ERROR_INVALID_SIGNAL_NUMBER = 0xD1,
        /// <summary>[210] The signal handler cannot be set.</summary>
        ERROR_THREAD_1_INACTIVE = 0xD2,
        /// <summary>[212] The segment is locked and cannot be reallocated.</summary>
        ERROR_LOCKED = 0xD4,
        /// <summary>[214] Too many dynamic-link modules are attached to this program or dynamic-link module.</summary>
        ERROR_TOO_MANY_MODULES = 0xD6,
        /// <summary>[215] Cannot nest calls to LoadModule.</summary>
        ERROR_NESTING_NOT_ALLOWED = 0xD7,
        /// <summary>[216] This version of %1 is not compatible with the version of Windows you're running. Check your computer's system information and then contact the software publisher.</summary>
        ERROR_EXE_MACHINE_TYPE_MISMATCH = 0xD8,
        /// <summary>[217] The image file %1 is signed, unable to modify.</summary>
        ERROR_EXE_CANNOT_MODIFY_SIGNED_BINARY = 0xD9,
        /// <summary>[218] The image file %1 is strong signed, unable to modify.</summary>
        ERROR_EXE_CANNOT_MODIFY_STRONG_SIGNED_BINARY = 0xDA,
        /// <summary>[220] This file is checked out or locked for editing by another user.</summary>
        ERROR_FILE_CHECKED_OUT = 0xDC,
        /// <summary>[221] The file must be checked out before saving changes.</summary>
        ERROR_CHECKOUT_REQUIRED = 0xDD,
        /// <summary>[222] The file type being saved or retrieved has been blocked.</summary>
        ERROR_BAD_FILE_TYPE = 0xDE,
        /// <summary>[223] The file size exceeds the limit allowed and cannot be saved.</summary>
        ERROR_FILE_TOO_LARGE = 0xDF,
        /// <summary>[224] Access Denied. Before opening files in this location, you must first add the web site to your trusted sites list, browse to the web site, and select the option to login automatically.</summary>
        ERROR_FORMS_AUTH_REQUIRED = 0xE0,
        /// <summary>[225] Operation did not complete successfully because the file contains a virus or potentially unwanted software.</summary>
        ERROR_VIRUS_INFECTED = 0xE1,
        /// <summary>[226] This file contains a virus or potentially unwanted software and cannot be opened. Due to the nature of this virus or potentially unwanted software, the file has been removed from this location.</summary>
        ERROR_VIRUS_DELETED = 0xE2,
        /// <summary>[229] The pipe is local.</summary>
        ERROR_PIPE_LOCAL = 0xE5,
        /// <summary>[230] The pipe state is invalid.</summary>
        ERROR_BAD_PIPE = 0xE6,
        /// <summary>[231] All pipe instances are busy.</summary>
        ERROR_PIPE_BUSY = 0xE7,
        /// <summary>[232] The pipe is being closed.</summary>
        ERROR_NO_DATA = 0xE8,
        /// <summary>[233] No process is on the other end of the pipe.</summary>
        ERROR_PIPE_NOT_CONNECTED = 0xE9,
        /// <summary>[234] More data is available.</summary>
        ERROR_MORE_DATA = 0xEA,
        /// <summary>[240] The session was canceled.</summary>
        ERROR_VC_DISCONNECTED = 0xF0,
        /// <summary>[254] The specified extended attribute name was invalid.</summary>
        ERROR_INVALID_EA_NAME = 0xFE,
        /// <summary>[255] The extended attributes are inconsistent.</summary>
        ERROR_EA_LIST_INCONSISTENT = 0xFF,
        /// <summary>[258] The wait operation timed out.</summary>
        WAIT_TIMEOUT = 0x102,
        /// <summary>[259] No more data is available.</summary>
        ERROR_NO_MORE_ITEMS = 0x103,
        /// <summary>[266] The copy functions cannot be used.</summary>
        ERROR_CANNOT_COPY = 0x10A,
        /// <summary>[267] The directory name is invalid.</summary>
        ERROR_DIRECTORY = 0x10B,
        /// <summary>[275] The extended attributes did not fit in the buffer.</summary>
        ERROR_EAS_DIDNT_FIT = 0x113,
        /// <summary>[276] The extended attribute file on the mounted file system is corrupt.</summary>
        ERROR_EA_FILE_CORRUPT = 0x114,
        /// <summary>[277] The extended attribute table file is full.</summary>
        ERROR_EA_TABLE_FULL = 0x115,
        /// <summary>[278] The specified extended attribute handle is invalid.</summary>
        ERROR_INVALID_EA_HANDLE = 0x116,
        /// <summary>[282] The mounted file system does not support extended attributes.</summary>
        ERROR_EAS_NOT_SUPPORTED = 0x11A,
        /// <summary>[288] Attempt to release mutex not owned by caller.</summary>
        ERROR_NOT_OWNER = 0x120,
        /// <summary>[298] Too many posts were made to a semaphore.</summary>
        ERROR_TOO_MANY_POSTS = 0x12A,
        /// <summary>[299] Only part of a ReadProcessMemory or WriteProcessMemory request was completed.</summary>
        ERROR_PARTIAL_COPY = 0x12B,
        /// <summary>[300] The oplock request is denied.</summary>
        ERROR_OPLOCK_NOT_GRANTED = 0x12C,
        /// <summary>[301] An invalid oplock acknowledgment was received by the system.</summary>
        ERROR_INVALID_OPLOCK_PROTOCOL = 0x12D,
        /// <summary>[302] The volume is too fragmented to complete this operation.</summary>
        ERROR_DISK_TOO_FRAGMENTED = 0x12E,
        /// <summary>[303] The file cannot be opened because it is in the process of being deleted.</summary>
        ERROR_DELETE_PENDING = 0x12F,
        /// <summary>[304] Short name settings may not be changed on this volume due to the global registry setting.</summary>
        ERROR_INCOMPATIBLE_WITH_GLOBAL_SHORT_NAME_REGISTRY_SETTING = 0x130,
        /// <summary>[305] Short names are not enabled on this volume.</summary>
        ERROR_SHORT_NAMES_NOT_ENABLED_ON_VOLUME = 0x131,
        /// <summary>[306] The security stream for the given volume is in an inconsistent state. Please run CHKDSK on the volume.</summary>
        ERROR_SECURITY_STREAM_IS_INCONSISTENT = 0x132,
        /// <summary>[307] A requested file lock operation cannot be processed due to an invalid byte range.</summary>
        ERROR_INVALID_LOCK_RANGE = 0x133,
        /// <summary>[308] The subsystem needed to support the image type is not present.</summary>
        ERROR_IMAGE_SUBSYSTEM_NOT_PRESENT = 0x134,
        /// <summary>[309] The specified file already has a notification GUID associated with it.</summary>
        ERROR_NOTIFICATION_GUID_ALREADY_DEFINED = 0x135,
        /// <summary>[310] An invalid exception handler routine has been detected.</summary>
        ERROR_INVALID_EXCEPTION_HANDLER = 0x136,
        /// <summary>[311] Duplicate privileges were specified for the token.</summary>
        ERROR_DUPLICATE_PRIVILEGES = 0x137,
        /// <summary>[312] No ranges for the specified operation were able to be processed.</summary>
        ERROR_NO_RANGES_PROCESSED = 0x138,
        /// <summary>[313] Operation is not allowed on a file system internal file.</summary>
        ERROR_NOT_ALLOWED_ON_SYSTEM_FILE = 0x139,
        /// <summary>[314] The physical resources of this disk have been exhausted.</summary>
        ERROR_DISK_RESOURCES_EXHAUSTED = 0x13A,
        /// <summary>[315] The token representing the data is invalid.</summary>
        ERROR_INVALID_TOKEN = 0x13B,
        /// <summary>[316] The device does not support the command feature.</summary>
        ERROR_DEVICE_FEATURE_NOT_SUPPORTED = 0x13C,
        /// <summary>[317] The system cannot find message text for message number 0x%1 in the message file for %2.</summary>
        ERROR_MR_MID_NOT_FOUND = 0x13D,
        /// <summary>[318] The scope specified was not found.</summary>
        ERROR_SCOPE_NOT_FOUND = 0x13E,
        /// <summary>[319] The Central Access Policy specified is not defined on the target machine.</summary>
        ERROR_UNDEFINED_SCOPE = 0x13F,
        /// <summary>[320] The Central Access Policy obtained from Active Directory is invalid.</summary>
        ERROR_INVALID_CAP = 0x140,
        /// <summary>[321] The device is unreachable.</summary>
        ERROR_DEVICE_UNREACHABLE = 0x141,
        /// <summary>[322] The target device has insufficient resources to complete the operation.</summary>
        ERROR_DEVICE_NO_RESOURCES = 0x142,
        /// <summary>[323] A data integrity checksum error occurred. Data in the file stream is corrupt.</summary>
        ERROR_DATA_CHECKSUM_ERROR = 0x143,
        /// <summary>[324] An attempt was made to modify both a KERNEL and normal Extended Attribute (EA) in the same operation.</summary>
        ERROR_INTERMIXED_KERNEL_EA_OPERATION = 0x144,
        /// <summary>[326] Device does not support file-level TRIM.</summary>
        ERROR_FILE_LEVEL_TRIM_NOT_SUPPORTED = 0x146,
        /// <summary>[327] The command specified a data offset that does not align to the device's granularity/alignment.</summary>
        ERROR_OFFSET_ALIGNMENT_VIOLATION = 0x147,
        /// <summary>[328] The command specified an invalid field in its parameter list.</summary>
        ERROR_INVALID_FIELD_IN_PARAMETER_LIST = 0x148,
        /// <summary>[329] An operation is currently in progress with the device.</summary>
        ERROR_OPERATION_IN_PROGRESS = 0x149,
        /// <summary>[330] An attempt was made to send down the command via an invalid path to the target device.</summary>
        ERROR_BAD_DEVICE_PATH = 0x14A,
        /// <summary>[331] The command specified a number of descriptors that exceeded the maximum supported by the device.</summary>
        ERROR_TOO_MANY_DESCRIPTORS = 0x14B,
        /// <summary>[332] Scrub is disabled on the specified file.</summary>
        ERROR_SCRUB_DATA_DISABLED = 0x14C,
        /// <summary>[333] The storage device does not provide redundancy.</summary>
        ERROR_NOT_REDUNDANT_STORAGE = 0x14D,
        /// <summary>[334] An operation is not supported on a resident file.</summary>
        ERROR_RESIDENT_FILE_NOT_SUPPORTED = 0x14E,
        /// <summary>[335] An operation is not supported on a compressed file.</summary>
        ERROR_COMPRESSED_FILE_NOT_SUPPORTED = 0x14F,
        /// <summary>[336] An operation is not supported on a directory.</summary>
        ERROR_DIRECTORY_NOT_SUPPORTED = 0x150,
        /// <summary>[337] The specified copy of the requested data could not be read.</summary>
        ERROR_NOT_READ_FROM_COPY = 0x151,
        /// <summary>[350] No action was taken as a system reboot is required.</summary>
        ERROR_FAIL_NOACTION_REBOOT = 0x15E,
        /// <summary>[351] The shutdown operation failed.</summary>
        ERROR_FAIL_SHUTDOWN = 0x15F,
        /// <summary>[352] The restart operation failed.</summary>
        ERROR_FAIL_RESTART = 0x160,
        /// <summary>[353] The maximum number of sessions has been reached.</summary>
        ERROR_MAX_SESSIONS_REACHED = 0x161,
        /// <summary>[400] The thread is already in background processing mode.</summary>
        ERROR_THREAD_MODE_ALREADY_BACKGROUND = 0x190,
        /// <summary>[401] The thread is not in background processing mode.</summary>
        ERROR_THREAD_MODE_NOT_BACKGROUND = 0x191,
        /// <summary>[402] The process is already in background processing mode.</summary>
        ERROR_PROCESS_MODE_ALREADY_BACKGROUND = 0x192,
        /// <summary>[403] The process is not in background processing mode.</summary>
        ERROR_PROCESS_MODE_NOT_BACKGROUND = 0x193,
        /// <summary>[487] Attempt to access invalid address.</summary>
        ERROR_INVALID_ADDRESS = 0x1E7,
        /// <summary>[500] User profile cannot be loaded.</summary>
        ERROR_USER_PROFILE_LOAD = 0x1F4,
        /// <summary>[534] Arithmetic result exceeded 32 bits.</summary>
        ERROR_ARITHMETIC_OVERFLOW = 0x216,
        /// <summary>[535] There is a process on other end of the pipe.</summary>
        ERROR_PIPE_CONNECTED = 0x217,
        /// <summary>[536] Waiting for a process to open the other end of the pipe.</summary>
        ERROR_PIPE_LISTENING = 0x218,
        /// <summary>[537] Application verifier has found an error in the current process.</summary>
        ERROR_VERIFIER_STOP = 0x219,
        /// <summary>[538] An error occurred in the ABIOS subsystem.</summary>
        ERROR_ABIOS_ERROR = 0x21A,
        /// <summary>[539] A warning occurred in the WX86 subsystem.</summary>
        ERROR_WX86_WARNING = 0x21B,
        /// <summary>[540] An error occurred in the WX86 subsystem.</summary>
        ERROR_WX86_ERROR = 0x21C,
        /// <summary>[541] An attempt was made to cancel or set a timer that has an associated APC and the subject thread is not the thread that originally set the timer with an associated APC routine.</summary>
        ERROR_TIMER_NOT_CANCELED = 0x21D,
        /// <summary>[542] Unwind exception code.</summary>
        ERROR_UNWIND = 0x21E,
        /// <summary>[543] An invalid or unaligned stack was encountered during an unwind operation.</summary>
        ERROR_BAD_STACK = 0x21F,
        /// <summary>[544] An invalid unwind target was encountered during an unwind operation.</summary>
        ERROR_INVALID_UNWIND_TARGET = 0x220,
        /// <summary>[545] Invalid Object Attributes specified to NtCreatePort or invalid Port Attributes specified to NtConnectPort</summary>
        ERROR_INVALID_PORT_ATTRIBUTES = 0x221,
        /// <summary>[546] Length of message passed to NtRequestPort or NtRequestWaitReplyPort was longer than the maximum message allowed by the port.</summary>
        ERROR_PORT_MESSAGE_TOO_LONG = 0x222,
        /// <summary>[547] An attempt was made to lower a quota limit below the current usage.</summary>
        ERROR_INVALID_QUOTA_LOWER = 0x223,
        /// <summary>[548] An attempt was made to attach to a device that was already attached to another device.</summary>
        ERROR_DEVICE_ALREADY_ATTACHED = 0x224,
        /// <summary>[549] An attempt was made to execute an instruction at an unaligned address and the host system does not support unaligned instruction references.</summary>
        ERROR_INSTRUCTION_MISALIGNMENT = 0x225,
        /// <summary>[550] Profiling not started.</summary>
        ERROR_PROFILING_NOT_STARTED = 0x226,
        /// <summary>[551] Profiling not stopped.</summary>
        ERROR_PROFILING_NOT_STOPPED = 0x227,
        /// <summary>[552] The passed ACL did not contain the minimum required information.</summary>
        ERROR_COULD_NOT_INTERPRET = 0x228,
        /// <summary>[553] The number of active profiling objects is at the maximum and no more may be started.</summary>
        ERROR_PROFILING_AT_LIMIT = 0x229,
        /// <summary>[554] Used to indicate that an operation cannot continue without blocking for I/O.</summary>
        ERROR_CANT_WAIT = 0x22A,
        /// <summary>[555] Indicates that a thread attempted to terminate itself by default (called NtTerminateThread with NULL) and it was the last thread in the current process.</summary>
        ERROR_CANT_TERMINATE_SELF = 0x22B,
        /// <summary>[556] If an MM error is returned which is not defined in the standard FsRtl filter, it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost, however, the filter correctly handles the exception.</summary>
        ERROR_UNEXPECTED_MM_CREATE_ERR = 0x22C,
        /// <summary>[557] If an MM error is returned which is not defined in the standard FsRtl filter, it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost, however, the filter correctly handles the exception.</summary>
        ERROR_UNEXPECTED_MM_MAP_ERROR = 0x22D,
        /// <summary>[558] If an MM error is returned which is not defined in the standard FsRtl filter, it is converted to one of the following errors which is guaranteed to be in the filter. In this case information is lost, however, the filter correctly handles the exception.</summary>
        ERROR_UNEXPECTED_MM_EXTEND_ERR = 0x22E,
        /// <summary>[559] A malformed function table was encountered during an unwind operation.</summary>
        ERROR_BAD_FUNCTION_TABLE = 0x22F,
        /// <summary>[560] Indicates that an attempt was made to assign protection to a file system file or directory and one of the SIDs in the security descriptor could not be translated into a GUID that could be stored by the file system. This causes the protection attempt to fail, which may cause a file creation attempt to fail.</summary>
        ERROR_NO_GUID_TRANSLATION = 0x230,
        /// <summary>[561] Indicates that an attempt was made to grow an LDT by setting its size, or that the size was not an even number of selectors.</summary>
        ERROR_INVALID_LDT_SIZE = 0x231,
        /// <summary>[563] Indicates that the starting value for the LDT information was not an integral multiple of the selector size.</summary>
        ERROR_INVALID_LDT_OFFSET = 0x233,
        /// <summary>[564] Indicates that the user supplied an invalid descriptor when trying to set up Ldt descriptors.</summary>
        ERROR_INVALID_LDT_DESCRIPTOR = 0x234,
        /// <summary>[565] Indicates a process has too many threads to perform the requested action. For example, assignment of a primary token may only be performed when a process has zero or one threads.</summary>
        ERROR_TOO_MANY_THREADS = 0x235,
        /// <summary>[566] An attempt was made to operate on a thread within a specific process, but the thread specified is not in the process specified.</summary>
        ERROR_THREAD_NOT_IN_PROCESS = 0x236,
        /// <summary>[567] Page file quota was exceeded.</summary>
        ERROR_PAGEFILE_QUOTA_EXCEEDED = 0x237,
        /// <summary>[568] The Netlogon service cannot start because another Netlogon service running in the domain conflicts with the specified role.</summary>
        ERROR_LOGON_SERVER_CONFLICT = 0x238,
        /// <summary>[569] The SAM database on a Windows Server is significantly out of synchronization with the copy on the Domain Controller. A complete synchronization is required.</summary>
        ERROR_SYNCHRONIZATION_REQUIRED = 0x239,
        /// <summary>[570] The NtCreateFile API failed. This error should never be returned to an application, it is a place holder for the Windows Lan Manager Redirector to use in its internal error mapping routines.</summary>
        ERROR_NET_OPEN_FAILED = 0x23A,
        /// <summary>[571] {Privilege Failed} The I/O permissions for the process could not be changed.</summary>
        ERROR_IO_PRIVILEGE_FAILED = 0x23B,
        /// <summary>[572] {Application Exit by CTRL+C} The application terminated as a result of a CTRL+C.</summary>
        ERROR_CONTROL_C_EXIT = 0x23C,
        /// <summary>[573] {Missing System File} The required system file %hs is bad or missing.</summary>
        ERROR_MISSING_SYSTEMFILE = 0x23D,
        /// <summary>[574] {Application Error} The exception %s (0x%08lx) occurred in the application at location 0x%08lx.</summary>
        ERROR_UNHANDLED_EXCEPTION = 0x23E,
        /// <summary>[575] {Application Error} The application was unable to start correctly (0x%lx). Click OK to close the application.</summary>
        ERROR_APP_INIT_FAILURE = 0x23F,
        /// <summary>[576] {Unable to Create Paging File} The creation of the paging file %hs failed (%lx). The requested size was %ld.</summary>
        ERROR_PAGEFILE_CREATE_FAILED = 0x240,
        /// <summary>[577] Windows cannot verify the digital signature for this file. A recent hardware or software change might have installed a file that is signed incorrectly or damaged, or that might be malicious software from an unknown source.</summary>
        ERROR_INVALID_IMAGE_HASH = 0x241,
        /// <summary>[578] {No Paging File Specified} No paging file was specified in the system configuration.</summary>
        ERROR_NO_PAGEFILE = 0x242,
        /// <summary>[579] {EXCEPTION} A real-mode application issued a floating-point instruction and floating-point hardware is not present.</summary>
        ERROR_ILLEGAL_FLOAT_CONTEXT = 0x243,
        /// <summary>[580] An event pair synchronization operation was performed using the thread specific client/server event pair object, but no event pair object was associated with the thread.</summary>
        ERROR_NO_EVENT_PAIR = 0x244,
        /// <summary>[581] A Windows Server has an incorrect configuration.</summary>
        ERROR_DOMAIN_CTRLR_CONFIG_ERROR = 0x245,
        /// <summary>[582] An illegal character was encountered. For a multi-byte character set this includes a lead byte without a succeeding trail byte. For the Unicode character set this includes the characters 0xFFFF and 0xFFFE.</summary>
        ERROR_ILLEGAL_CHARACTER = 0x246,
        /// <summary>[583] The Unicode character is not defined in the Unicode character set installed on the system.</summary>
        ERROR_UNDEFINED_CHARACTER = 0x247,
        /// <summary>[584] The paging file cannot be created on a floppy diskette.</summary>
        ERROR_FLOPPY_VOLUME = 0x248,
        /// <summary>[585] The system BIOS failed to connect a system interrupt to the device or bus for which the device is connected.</summary>
        ERROR_BIOS_FAILED_TO_CONNECT_INTERRUPT = 0x249,
        /// <summary>[586] This operation is only allowed for the Primary Domain Controller of the domain.</summary>
        ERROR_BACKUP_CONTROLLER = 0x24A,
        /// <summary>[587] An attempt was made to acquire a mutant such that its maximum count would have been exceeded.</summary>
        ERROR_MUTANT_LIMIT_EXCEEDED = 0x24B,
        /// <summary>[588] A volume has been accessed for which a file system driver is required that has not yet been loaded.</summary>
        ERROR_FS_DRIVER_REQUIRED = 0x24C,
        /// <summary>[589] {Registry File Failure} The registry cannot load the hive (file): %hs or its log or alternate. It is corrupt, absent, or not writable.</summary>
        ERROR_CANNOT_LOAD_REGISTRY_FILE = 0x24D,
        /// <summary>[590] {Unexpected Failure in DebugActiveProcess} An unexpected failure occurred while processing a DebugActiveProcess API request. You may choose OK to terminate the process, or Cancel to ignore the error.</summary>
        ERROR_DEBUG_ATTACH_FAILED = 0x24E,
        /// <summary>[591] {Fatal System Error} The %hs system process terminated unexpectedly with a status of 0x%08x (0x%08x 0x%08x). The system has been shut down.</summary>
        ERROR_SYSTEM_PROCESS_TERMINATED = 0x24F,
        /// <summary>[592] {Data Not Accepted} The TDI client could not handle the data received during an indication.</summary>
        ERROR_DATA_NOT_ACCEPTED = 0x250,
        /// <summary>[593] NTVDM encountered a hard error.</summary>
        ERROR_VDM_HARD_ERROR = 0x251,
        /// <summary>[594] {Cancel Timeout} The driver %hs failed to complete a cancelled I/O request in the allotted time.</summary>
        ERROR_DRIVER_CANCEL_TIMEOUT = 0x252,
        /// <summary>[595] {Reply Message Mismatch} An attempt was made to reply to an LPC message, but the thread specified by the client ID in the message was not waiting on that message.</summary>
        ERROR_REPLY_MESSAGE_MISMATCH = 0x253,
        /// <summary>[596] {Delayed Write Failed} Windows was unable to save all the data for the file %hs. The data has been lost. This error may be caused by a failure of your computer hardware or network connection. Please try to save this file elsewhere.</summary>
        ERROR_LOST_WRITEBEHIND_DATA = 0x254,
        /// <summary>[597] The parameter(s) passed to the server in the client/server shared memory window were invalid. Too much data may have been put in the shared memory window.</summary>
        ERROR_CLIENT_SERVER_PARAMETERS_INVALID = 0x255,
        /// <summary>[598] The stream is not a tiny stream.</summary>
        ERROR_NOT_TINY_STREAM = 0x256,
        /// <summary>[599] The request must be handled by the stack overflow code.</summary>
        ERROR_STACK_OVERFLOW_READ = 0x257,
        /// <summary>[600] Internal OFS status codes indicating how an allocation operation is handled. Either it is retried after the containing onode is moved or the extent stream is converted to a large stream.</summary>
        ERROR_CONVERT_TO_LARGE = 0x258,
        /// <summary>[601] The attempt to find the object found an object matching by ID on the volume but it is out of the scope of the handle used for the operation.</summary>
        ERROR_FOUND_OUT_OF_SCOPE = 0x259,
        /// <summary>[602] The bucket array must be grown. Retry transaction after doing so.</summary>
        ERROR_ALLOCATE_BUCKET = 0x25A,
        /// <summary>[603] The user/kernel marshalling buffer has overflowed.</summary>
        ERROR_MARSHALL_OVERFLOW = 0x25B,
        /// <summary>[604] The supplied variant structure contains invalid data.</summary>
        ERROR_INVALID_VARIANT = 0x25C,
        /// <summary>[605] The specified buffer contains ill-formed data.</summary>
        ERROR_BAD_COMPRESSION_BUFFER = 0x25D,
        /// <summary>[606] {Audit Failed} An attempt to generate a security audit failed.</summary>
        ERROR_AUDIT_FAILED = 0x25E,
        /// <summary>[607] The timer resolution was not previously set by the current process.</summary>
        ERROR_TIMER_RESOLUTION_NOT_SET = 0x25F,
        /// <summary>[608] There is insufficient account information to log you on.</summary>
        ERROR_INSUFFICIENT_LOGON_INFO = 0x260,
        /// <summary>[609] {Invalid DLL Entrypoint} The dynamic link library %hs is not written correctly. The stack pointer has been left in an inconsistent state. The entrypoint should be declared as WINAPI or STDCALL. Select YES to fail the DLL load. Select NO to continue execution. Selecting NO may cause the application to operate incorrectly.</summary>
        ERROR_BAD_DLL_ENTRYPOINT = 0x261,
        /// <summary>[610] {Invalid Service Callback Entrypoint} The %hs service is not written correctly. The stack pointer has been left in an inconsistent state. The callback entrypoint should be declared as WINAPI or STDCALL. Selecting OK will cause the service to continue operation. However, the service process may operate incorrectly.</summary>
        ERROR_BAD_SERVICE_ENTRYPOINT = 0x262,
        /// <summary>[611] There is an IP address conflict with another system on the network.</summary>
        ERROR_IP_ADDRESS_CONFLICT1 = 0x263,
        /// <summary>[612] There is an IP address conflict with another system on the network.</summary>
        ERROR_IP_ADDRESS_CONFLICT2 = 0x264,
        /// <summary>[613] {Low On Registry Space} The system has reached the maximum size allowed for the system part of the registry. Additional storage requests will be ignored.</summary>
        ERROR_REGISTRY_QUOTA_LIMIT = 0x265,
        /// <summary>[614] A callback return system service cannot be executed when no callback is active.</summary>
        ERROR_NO_CALLBACK_ACTIVE = 0x266,
        /// <summary>[615] The password provided is too short to meet the policy of your user account. Please choose a longer password.</summary>
        ERROR_PWD_TOO_SHORT = 0x267,
        /// <summary>[616] The policy of your user account does not allow you to change passwords too frequently. This is done to prevent users from changing back to a familiar, but potentially discovered, password. If you feel your password has been compromised then please contact your administrator immediately to have a new one assigned.</summary>
        ERROR_PWD_TOO_RECENT = 0x268,
        /// <summary>[617] You have attempted to change your password to one that you have used in the past. The policy of your user account does not allow this. Please select a password that you have not previously used.</summary>
        ERROR_PWD_HISTORY_CONFLICT = 0x269,
        /// <summary>[618] The specified compression format is unsupported.</summary>
        ERROR_UNSUPPORTED_COMPRESSION = 0x26A,
        /// <summary>[619] The specified hardware profile configuration is invalid.</summary>
        ERROR_INVALID_HW_PROFILE = 0x26B,
        /// <summary>[620] The specified Plug and Play registry device path is invalid.</summary>
        ERROR_INVALID_PLUGPLAY_DEVICE_PATH = 0x26C,
        /// <summary>[621] The specified quota list is internally inconsistent with its descriptor.</summary>
        ERROR_QUOTA_LIST_INCONSISTENT = 0x26D,
        /// <summary>[622] {Windows Evaluation Notification} The evaluation period for this installation of Windows has expired. This system will shutdown in 1 hour. To restore access to this installation of Windows, please upgrade this installation using a licensed distribution of this product.</summary>
        ERROR_EVALUATION_EXPIRATION = 0x26E,
        /// <summary>[623] {Illegal System DLL Relocation} The system DLL %hs was relocated in memory. The application will not run properly. The relocation occurred because the DLL %hs occupied an address range reserved for Windows system DLLs. The vendor supplying the DLL should be contacted for a new DLL.</summary>
        ERROR_ILLEGAL_DLL_RELOCATION = 0x26F,
        /// <summary>[624] {DLL Initialization Failed} The application failed to initialize because the window station is shutting down.</summary>
        ERROR_DLL_INIT_FAILED_LOGOFF = 0x270,
        /// <summary>[625] The validation process needs to continue on to the next step.</summary>
        ERROR_VALIDATE_CONTINUE = 0x271,
        /// <summary>[626] There are no more matches for the current index enumeration.</summary>
        ERROR_NO_MORE_MATCHES = 0x272,
        /// <summary>[627] The range could not be added to the range list because of a conflict.</summary>
        ERROR_RANGE_LIST_CONFLICT = 0x273,
        /// <summary>[628] The server process is running under a SID different than that required by client.</summary>
        ERROR_SERVER_SID_MISMATCH = 0x274,
        /// <summary>[629] A group marked use for deny only cannot be enabled.</summary>
        ERROR_CANT_ENABLE_DENY_ONLY = 0x275,
        /// <summary>[630] {EXCEPTION} Multiple floating point faults.</summary>
        ERROR_FLOAT_MULTIPLE_FAULTS = 0x276,
        /// <summary>[631] {EXCEPTION} Multiple floating point traps.</summary>
        ERROR_FLOAT_MULTIPLE_TRAPS = 0x277,
        /// <summary>[632] The requested interface is not supported.</summary>
        ERROR_NOINTERFACE = 0x278,
        /// <summary>[633] {System Standby Failed} The driver %hs does not support standby mode. Updating this driver may allow the system to go to standby mode.</summary>
        ERROR_DRIVER_FAILED_SLEEP = 0x279,
        /// <summary>[634] The system file %1 has become corrupt and has been replaced.</summary>
        ERROR_CORRUPT_SYSTEM_FILE = 0x27A,
        /// <summary>[635] {Virtual Memory Minimum Too Low} Your system is low on virtual memory. Windows is increasing the size of your virtual memory paging file. During this process, memory requests for some applications may be denied. For more information, see Help.</summary>
        ERROR_COMMITMENT_MINIMUM = 0x27B,
        /// <summary>[636] A device was removed so enumeration must be restarted.</summary>
        ERROR_PNP_RESTART_ENUMERATION = 0x27C,
        /// <summary>[637] {Fatal System Error} The system image %s is not properly signed. The file has been replaced with the signed file. The system has been shut down.</summary>
        ERROR_SYSTEM_IMAGE_BAD_SIGNATURE = 0x27D,
        /// <summary>[638] Device will not start without a reboot.</summary>
        ERROR_PNP_REBOOT_REQUIRED = 0x27E,
        /// <summary>[639] There is not enough power to complete the requested operation.</summary>
        ERROR_INSUFFICIENT_POWER = 0x27F,
        /// <summary>[640] ERROR_MULTIPLE_FAULT_VIOLATION</summary>
        ERROR_MULTIPLE_FAULT_VIOLATION = 0x280,
        /// <summary>[641] The system is in the process of shutting down.</summary>
        ERROR_SYSTEM_SHUTDOWN = 0x281,
        /// <summary>[642] An attempt to remove a processes DebugPort was made, but a port was not already associated with the process.</summary>
        ERROR_PORT_NOT_SET = 0x282,
        /// <summary>[643] This version of Windows is not compatible with the behavior version of directory forest, domain or domain controller.</summary>
        ERROR_DS_VERSION_CHECK_FAILURE = 0x283,
        /// <summary>[644] The specified range could not be found in the range list.</summary>
        ERROR_RANGE_NOT_FOUND = 0x284,
        /// <summary>[646] The driver was not loaded because the system is booting into safe mode.</summary>
        ERROR_NOT_SAFE_MODE_DRIVER = 0x286,
        /// <summary>[647] The driver was not loaded because it failed its initialization call.</summary>
        ERROR_FAILED_DRIVER_ENTRY = 0x287,
        /// <summary>[648] The "%hs" encountered an error while applying power or reading the device configuration. This may be caused by a failure of your hardware or by a poor connection.</summary>
        ERROR_DEVICE_ENUMERATION_ERROR = 0x288,
        /// <summary>[649] The create operation failed because the name contained at least one mount point which resolves to a volume to which the specified device object is not attached.</summary>
        ERROR_MOUNT_POINT_NOT_RESOLVED = 0x289,
        /// <summary>[650] The device object parameter is either not a valid device object or is not attached to the volume specified by the file name.</summary>
        ERROR_INVALID_DEVICE_OBJECT_PARAMETER = 0x28A,
        /// <summary>[651] A Machine Check Error has occurred. Please check the system eventlog for additional information.</summary>
        ERROR_MCA_OCCURED = 0x28B,
        /// <summary>[652] There was error [%2] processing the driver database.</summary>
        ERROR_DRIVER_DATABASE_ERROR = 0x28C,
        /// <summary>[653] System hive size has exceeded its limit.</summary>
        ERROR_SYSTEM_HIVE_TOO_LARGE = 0x28D,
        /// <summary>[654] The driver could not be loaded because a previous version of the driver is still in memory.</summary>
        ERROR_DRIVER_FAILED_PRIOR_UNLOAD = 0x28E,
        /// <summary>[655] {Volume Shadow Copy Service} Please wait while the Volume Shadow Copy Service prepares volume %hs for hibernation.</summary>
        ERROR_VOLSNAP_PREPARE_HIBERNATE = 0x28F,
        /// <summary>[656] The system has failed to hibernate (The error code is %hs). Hibernation will be disabled until the system is restarted.</summary>
        ERROR_HIBERNATION_FAILURE = 0x290,
        /// <summary>[657] The password provided is too long to meet the policy of your user account. Please choose a shorter password.</summary>
        ERROR_PWD_TOO_LONG = 0x291,
        /// <summary>[665] The requested operation could not be completed due to a file system limitation.</summary>
        ERROR_FILE_SYSTEM_LIMITATION = 0x299,
        /// <summary>[668] An assertion failure has occurred.</summary>
        ERROR_ASSERTION_FAILURE = 0x29C,
        /// <summary>[669] An error occurred in the ACPI subsystem.</summary>
        ERROR_ACPI_ERROR = 0x29D,
        /// <summary>[670] WOW Assertion Error.</summary>
        ERROR_WOW_ASSERTION = 0x29E,
        /// <summary>[671] A device is missing in the system BIOS MPS table. This device will not be used. Please contact your system vendor for system BIOS update.</summary>
        ERROR_PNP_BAD_MPS_TABLE = 0x29F,
        /// <summary>[672] A translator failed to translate resources.</summary>
        ERROR_PNP_TRANSLATION_FAILED = 0x2A0,
        /// <summary>[673] A IRQ translator failed to translate resources.</summary>
        ERROR_PNP_IRQ_TRANSLATION_FAILED = 0x2A1,
        /// <summary>[674] Driver %2 returned invalid ID for a child device (%3).</summary>
        ERROR_PNP_INVALID_ID = 0x2A2,
        /// <summary>[675] {Kernel Debugger Awakened} the system debugger was awakened by an interrupt.</summary>
        ERROR_WAKE_SYSTEM_DEBUGGER = 0x2A3,
        /// <summary>[676] {Handles Closed} Handles to objects have been automatically closed as a result of the requested operation.</summary>
        ERROR_HANDLES_CLOSED = 0x2A4,
        /// <summary>[677] {Too Much Information} The specified access control list (ACL) contained more information than was expected.</summary>
        ERROR_EXTRANEOUS_INFORMATION = 0x2A5,
        /// <summary>[678] This warning level status indicates that the transaction state already exists for the registry sub-tree, but that a transaction commit was previously aborted. The commit has NOT been completed, but has not been rolled back either (so it may still be committed if desired).</summary>
        ERROR_RXACT_COMMIT_NECESSARY = 0x2A6,
        /// <summary>[679] {Media Changed} The media may have changed.</summary>
        ERROR_MEDIA_CHECK = 0x2A7,
        /// <summary>[680] {GUID Substitution} During the translation of a global identifier (GUID) to a Windows security ID (SID), no administratively-defined GUID prefix was found. A substitute prefix was used, which will not compromise system security. However, this may provide a more restrictive access than intended.</summary>
        ERROR_GUID_SUBSTITUTION_MADE = 0x2A8,
        /// <summary>[681] The create operation stopped after reaching a symbolic link.</summary>
        ERROR_STOPPED_ON_SYMLINK = 0x2A9,
        /// <summary>[682] A long jump has been executed.</summary>
        ERROR_LONGJUMP = 0x2AA,
        /// <summary>[683] The Plug and Play query operation was not successful.</summary>
        ERROR_PLUGPLAY_QUERY_VETOED = 0x2AB,
        /// <summary>[684] A frame consolidation has been executed.</summary>
        ERROR_UNWIND_CONSOLIDATE = 0x2AC,
        /// <summary>[685] {Registry Hive Recovered} Registry hive (file): %hs was corrupted and it has been recovered. Some data might have been lost.</summary>
        ERROR_REGISTRY_HIVE_RECOVERED = 0x2AD,
        /// <summary>[686] The application is attempting to run executable code from the module %hs. This may be insecure. An alternative, %hs, is available. Should the application use the secure module %hs?</summary>
        ERROR_DLL_MIGHT_BE_INSECURE = 0x2AE,
        /// <summary>[687] The application is loading executable code from the module %hs. This is secure, but may be incompatible with previous releases of the operating system. An alternative, %hs, is available. Should the application use the secure module %hs?</summary>
        ERROR_DLL_MIGHT_BE_INCOMPATIBLE = 0x2AF,
        /// <summary>[688] Debugger did not handle the exception.</summary>
        ERROR_DBG_EXCEPTION_NOT_HANDLED = 0x2B0,
        /// <summary>[689] Debugger will reply later.</summary>
        ERROR_DBG_REPLY_LATER = 0x2B1,
        /// <summary>[690] Debugger cannot provide handle.</summary>
        ERROR_DBG_UNABLE_TO_PROVIDE_HANDLE = 0x2B2,
        /// <summary>[691] Debugger terminated thread.</summary>
        ERROR_DBG_TERMINATE_THREAD = 0x2B3,
        /// <summary>[692] Debugger terminated process.</summary>
        ERROR_DBG_TERMINATE_PROCESS = 0x2B4,
        /// <summary>[693] Debugger got control C.</summary>
        ERROR_DBG_CONTROL_C = 0x2B5,
        /// <summary>[694] Debugger printed exception on control C.</summary>
        ERROR_DBG_PRINTEXCEPTION_C = 0x2B6,
        /// <summary>[695] Debugger received RIP exception.</summary>
        ERROR_DBG_RIPEXCEPTION = 0x2B7,
        /// <summary>[696] Debugger received control break.</summary>
        ERROR_DBG_CONTROL_BREAK = 0x2B8,
        /// <summary>[697] Debugger command communication exception.</summary>
        ERROR_DBG_COMMAND_EXCEPTION = 0x2B9,
        /// <summary>[698] {Object Exists} An attempt was made to create an object and the object name already existed.</summary>
        ERROR_OBJECT_NAME_EXISTS = 0x2BA,
        /// <summary>[699] {Thread Suspended} A thread termination occurred while the thread was suspended. The thread was resumed, and termination proceeded.</summary>
        ERROR_THREAD_WAS_SUSPENDED = 0x2BB,
        /// <summary>[700] {Image Relocated} An image file could not be mapped at the address specified in the image file. Local fixups must be performed on this image.</summary>
        ERROR_IMAGE_NOT_AT_BASE = 0x2BC,
        /// <summary>[701] This informational level status indicates that a specified registry sub-tree transaction state did not yet exist and had to be created.</summary>
        ERROR_RXACT_STATE_CREATED = 0x2BD,
        /// <summary>[702] {Segment Load} A virtual DOS machine (VDM) is loading, unloading, or moving an MS-DOS or Win16 program segment image. An exception is raised so a debugger can load, unload or track symbols and breakpoints within these 16-bit segments.</summary>
        ERROR_SEGMENT_NOTIFICATION = 0x2BE,
        /// <summary>[703] {Invalid Current Directory} The process cannot switch to the startup current directory %hs. Select OK to set current directory to %hs, or select CANCEL to exit.</summary>
        ERROR_BAD_CURRENT_DIRECTORY = 0x2BF,
        /// <summary>[704] {Redundant Read} To satisfy a read request, the NT fault-tolerant file system successfully read the requested data from a redundant copy. This was done because the file system encountered a failure on a member of the fault-tolerant volume, but was unable to reassign the failing area of the device.</summary>
        ERROR_FT_READ_RECOVERY_FROM_BACKUP = 0x2C0,
        /// <summary>[705] {Redundant Write} To satisfy a write request, the NT fault-tolerant file system successfully wrote a redundant copy of the information. This was done because the file system encountered a failure on a member of the fault-tolerant volume, but was not able to reassign the failing area of the device.</summary>
        ERROR_FT_WRITE_RECOVERY = 0x2C1,
        /// <summary>[706] {Machine Type Mismatch} The image file %hs is valid, but is for a machine type other than the current machine. Select OK to continue, or CANCEL to fail the DLL load.</summary>
        ERROR_IMAGE_MACHINE_TYPE_MISMATCH = 0x2C2,
        /// <summary>[707] {Partial Data Received} The network transport returned partial data to its client. The remaining data will be sent later.</summary>
        ERROR_RECEIVE_PARTIAL = 0x2C3,
        /// <summary>[708] {Expedited Data Received} The network transport returned data to its client that was marked as expedited by the remote system.</summary>
        ERROR_RECEIVE_EXPEDITED = 0x2C4,
        /// <summary>[709] {Partial Expedited Data Received} The network transport returned partial data to its client and this data was marked as expedited by the remote system. The remaining data will be sent later.</summary>
        ERROR_RECEIVE_PARTIAL_EXPEDITED = 0x2C5,
        /// <summary>[710] {TDI Event Done} The TDI indication has completed successfully.</summary>
        ERROR_EVENT_DONE = 0x2C6,
        /// <summary>[711] {TDI Event Pending} The TDI indication has entered the pending state.</summary>
        ERROR_EVENT_PENDING = 0x2C7,
        /// <summary>[712] Checking file system on %wZ.</summary>
        ERROR_CHECKING_FILE_SYSTEM = 0x2C8,
        /// <summary>[713] {Fatal Application Exit} %hs.</summary>
        ERROR_FATAL_APP_EXIT = 0x2C9,
        /// <summary>[714] The specified registry key is referenced by a predefined handle.</summary>
        ERROR_PREDEFINED_HANDLE = 0x2CA,
        /// <summary>[715] {Page Unlocked} The page protection of a locked page was changed to 'No Access' and the page was unlocked from memory and from the process.</summary>
        ERROR_WAS_UNLOCKED = 0x2CB,
        /// <summary>[716] %hs</summary>
        ERROR_SERVICE_NOTIFICATION = 0x2CC,
        /// <summary>[717] {Page Locked} One of the pages to lock was already locked.</summary>
        ERROR_WAS_LOCKED = 0x2CD,
        /// <summary>[718] Application popup: %1 : %2</summary>
        ERROR_LOG_HARD_ERROR = 0x2CE,
        /// <summary>[719] ERROR_ALREADY_WIN32</summary>
        ERROR_ALREADY_WIN32 = 0x2CF,
        /// <summary>[720] {Machine Type Mismatch} The image file %hs is valid, but is for a machine type other than the current machine.</summary>
        ERROR_IMAGE_MACHINE_TYPE_MISMATCH_EXE = 0x2D0,
        /// <summary>[721] A yield execution was performed and no thread was available to run.</summary>
        ERROR_NO_YIELD_PERFORMED = 0x2D1,
        /// <summary>[722] The resumable flag to a timer API was ignored.</summary>
        ERROR_TIMER_RESUME_IGNORED = 0x2D2,
        /// <summary>[723] The arbiter has deferred arbitration of these resources to its parent.</summary>
        ERROR_ARBITRATION_UNHANDLED = 0x2D3,
        /// <summary>[724] The inserted CardBus device cannot be started because of a configuration error on "%hs".</summary>
        ERROR_CARDBUS_NOT_SUPPORTED = 0x2D4,
        /// <summary>[725] The CPUs in this multiprocessor system are not all the same revision level. To use all processors the operating system restricts itself to the features of the least capable processor in the system. Should problems occur with this system, contact the CPU manufacturer to see if this mix of processors is supported.</summary>
        ERROR_MP_PROCESSOR_MISMATCH = 0x2D5,
        /// <summary>[726] The system was put into hibernation.</summary>
        ERROR_HIBERNATED = 0x2D6,
        /// <summary>[727] The system was resumed from hibernation.</summary>
        ERROR_RESUME_HIBERNATION = 0x2D7,
        /// <summary>[728] Windows has detected that the system firmware (BIOS) was updated [previous firmware date = %2, current firmware date %3].</summary>
        ERROR_FIRMWARE_UPDATED = 0x2D8,
        /// <summary>[729] A device driver is leaking locked I/O pages causing system degradation. The system has automatically enabled tracking code in order to try and catch the culprit.</summary>
        ERROR_DRIVERS_LEAKING_LOCKED_PAGES = 0x2D9,
        /// <summary>[730] The system has awoken.</summary>
        ERROR_WAKE_SYSTEM = 0x2DA,
        /// <summary>[731] ERROR_WAIT_1</summary>
        ERROR_WAIT_1 = 0x2DB,
        /// <summary>[732] ERROR_WAIT_2</summary>
        ERROR_WAIT_2 = 0x2DC,
        /// <summary>[733] ERROR_WAIT_3</summary>
        ERROR_WAIT_3 = 0x2DD,
        /// <summary>[734] ERROR_WAIT_63</summary>
        ERROR_WAIT_63 = 0x2DE,
        /// <summary>[735] ERROR_ABANDONED_WAIT_0</summary>
        ERROR_ABANDONED_WAIT_0 = 0x2DF,
        /// <summary>[736] ERROR_ABANDONED_WAIT_63</summary>
        ERROR_ABANDONED_WAIT_63 = 0x2E0,
        /// <summary>[737] ERROR_USER_APC</summary>
        ERROR_USER_APC = 0x2E1,
        /// <summary>[738] ERROR_KERNEL_APC</summary>
        ERROR_KERNEL_APC = 0x2E2,
        /// <summary>[739] ERROR_ALERTED</summary>
        ERROR_ALERTED = 0x2E3,
        /// <summary>[740] The requested operation requires elevation.</summary>
        ERROR_ELEVATION_REQUIRED = 0x2E4,
        /// <summary>[741] A reparse should be performed by the Object Manager since the name of the file resulted in a symbolic link.</summary>
        ERROR_REPARSE = 0x2E5,
        /// <summary>[742] An open/create operation completed while an oplock break is underway.</summary>
        ERROR_OPLOCK_BREAK_IN_PROGRESS = 0x2E6,
        /// <summary>[743] A new volume has been mounted by a file system.</summary>
        ERROR_VOLUME_MOUNTED = 0x2E7,
        /// <summary>[744] This success level status indicates that the transaction state already exists for the registry sub-tree, but that a transaction commit was previously aborted. The commit has now been completed.</summary>
        ERROR_RXACT_COMMITTED = 0x2E8,
        /// <summary>[745] This indicates that a notify change request has been completed due to closing the handle which made the notify change request.</summary>
        ERROR_NOTIFY_CLEANUP = 0x2E9,
        /// <summary>[746] {Connect Failure on Primary Transport} An attempt was made to connect to the remote server %hs on the primary transport, but the connection failed. The computer WAS able to connect on a secondary transport.</summary>
        ERROR_PRIMARY_TRANSPORT_CONNECT_FAILED = 0x2EA,
        /// <summary>[747] Page fault was a transition fault.</summary>
        ERROR_PAGE_FAULT_TRANSITION = 0x2EB,
        /// <summary>[748] Page fault was a demand zero fault.</summary>
        ERROR_PAGE_FAULT_DEMAND_ZERO = 0x2EC,
        /// <summary>[749] Page fault was a demand zero fault.</summary>
        ERROR_PAGE_FAULT_COPY_ON_WRITE = 0x2ED,
        /// <summary>[750] Page fault was a demand zero fault.</summary>
        ERROR_PAGE_FAULT_GUARD_PAGE = 0x2EE,
        /// <summary>[751] Page fault was satisfied by reading from a secondary storage device.</summary>
        ERROR_PAGE_FAULT_PAGING_FILE = 0x2EF,
        /// <summary>[752] Cached page was locked during operation.</summary>
        ERROR_CACHE_PAGE_LOCKED = 0x2F0,
        /// <summary>[753] Crash dump exists in paging file.</summary>
        ERROR_CRASH_DUMP = 0x2F1,
        /// <summary>[754] Specified buffer contains all zeros.</summary>
        ERROR_BUFFER_ALL_ZEROS = 0x2F2,
        /// <summary>[755] A reparse should be performed by the Object Manager since the name of the file resulted in a symbolic link.</summary>
        ERROR_REPARSE_OBJECT = 0x2F3,
        /// <summary>[756] The device has succeeded a query-stop and its resource requirements have changed.</summary>
        ERROR_RESOURCE_REQUIREMENTS_CHANGED = 0x2F4,
        /// <summary>[757] The translator has translated these resources into the global space and no further translations should be performed.</summary>
        ERROR_TRANSLATION_COMPLETE = 0x2F5,
        /// <summary>[758] A process being terminated has no threads to terminate.</summary>
        ERROR_NOTHING_TO_TERMINATE = 0x2F6,
        /// <summary>[759] The specified process is not part of a job.</summary>
        ERROR_PROCESS_NOT_IN_JOB = 0x2F7,
        /// <summary>[760] The specified process is part of a job.</summary>
        ERROR_PROCESS_IN_JOB = 0x2F8,
        /// <summary>[761] {Volume Shadow Copy Service} The system is now ready for hibernation.</summary>
        ERROR_VOLSNAP_HIBERNATE_READY = 0x2F9,
        /// <summary>[762] A file system or file system filter driver has successfully completed an FsFilter operation.</summary>
        ERROR_FSFILTER_OP_COMPLETED_SUCCESSFULLY = 0x2FA,
        /// <summary>[763] The specified interrupt vector was already connected.</summary>
        ERROR_INTERRUPT_VECTOR_ALREADY_CONNECTED = 0x2FB,
        /// <summary>[764] The specified interrupt vector is still connected.</summary>
        ERROR_INTERRUPT_STILL_CONNECTED = 0x2FC,
        /// <summary>[765] An operation is blocked waiting for an oplock.</summary>
        ERROR_WAIT_FOR_OPLOCK = 0x2FD,
        /// <summary>[766] Debugger handled exception.</summary>
        ERROR_DBG_EXCEPTION_HANDLED = 0x2FE,
        /// <summary>[767] Debugger continued.</summary>
        ERROR_DBG_CONTINUE = 0x2FF,
        /// <summary>[768] An exception occurred in a user mode callback and the kernel callback frame should be removed.</summary>
        ERROR_CALLBACK_POP_STACK = 0x300,
        /// <summary>[769] Compression is disabled for this volume.</summary>
        ERROR_COMPRESSION_DISABLED = 0x301,
        /// <summary>[770] The data provider cannot fetch backwards through a result set.</summary>
        ERROR_CANTFETCHBACKWARDS = 0x302,
        /// <summary>[771] The data provider cannot scroll backwards through a result set.</summary>
        ERROR_CANTSCROLLBACKWARDS = 0x303,
        /// <summary>[772] The data provider requires that previously fetched data is released before asking for more data.</summary>
        ERROR_ROWSNOTRELEASED = 0x304,
        /// <summary>[773] The data provider was not able to interpret the flags set for a column binding in an accessor.</summary>
        ERROR_BAD_ACCESSOR_FLAGS = 0x305,
        /// <summary>[774] One or more errors occurred while processing the request.</summary>
        ERROR_ERRORS_ENCOUNTERED = 0x306,
        /// <summary>[775] The implementation is not capable of performing the request.</summary>
        ERROR_NOT_CAPABLE = 0x307,
        /// <summary>[776] The client of a component requested an operation which is not valid given the state of the component instance.</summary>
        ERROR_REQUEST_OUT_OF_SEQUENCE = 0x308,
        /// <summary>[777] A version number could not be parsed.</summary>
        ERROR_VERSION_PARSE_ERROR = 0x309,
        /// <summary>[778] The iterator's start position is invalid.</summary>
        ERROR_BADSTARTPOSITION = 0x30A,
        /// <summary>[779] The hardware has reported an uncorrectable memory error.</summary>
        ERROR_MEMORY_HARDWARE = 0x30B,
        /// <summary>[780] The attempted operation required self healing to be enabled.</summary>
        ERROR_DISK_REPAIR_DISABLED = 0x30C,
        /// <summary>[781] The Desktop heap encountered an error while allocating session memory. There is more information in the system event log.</summary>
        ERROR_INSUFFICIENT_RESOURCE_FOR_SPECIFIED_SHARED_SECTION_SIZE = 0x30D,
        /// <summary>[782] The system power state is transitioning from %2 to %3.</summary>
        ERROR_SYSTEM_POWERSTATE_TRANSITION = 0x30E,
        /// <summary>[783] The system power state is transitioning from %2 to %3 but could enter %4.</summary>
        ERROR_SYSTEM_POWERSTATE_COMPLEX_TRANSITION = 0x30F,
        /// <summary>[784] A thread is getting dispatched with MCA EXCEPTION because of MCA.</summary>
        ERROR_MCA_EXCEPTION = 0x310,
        /// <summary>[785] Access to %1 is monitored by policy rule %2.</summary>
        ERROR_ACCESS_AUDIT_BY_POLICY = 0x311,
        /// <summary>[786] Access to %1 has been restricted by your Administrator by policy rule %2.</summary>
        ERROR_ACCESS_DISABLED_NO_SAFER_UI_BY_POLICY = 0x312,
        /// <summary>[787] A valid hibernation file has been invalidated and should be abandoned.</summary>
        ERROR_ABANDON_HIBERFILE = 0x313,
        /// <summary>[788] {Delayed Write Failed} Windows was unable to save all the data for the file %hs; the data has been lost. This error may be caused by network connectivity issues. Please try to save this file elsewhere.</summary>
        ERROR_LOST_WRITEBEHIND_DATA_NETWORK_DISCONNECTED = 0x314,
        /// <summary>[789] {Delayed Write Failed} Windows was unable to save all the data for the file %hs; the data has been lost. This error was returned by the server on which the file exists. Please try to save this file elsewhere.</summary>
        ERROR_LOST_WRITEBEHIND_DATA_NETWORK_SERVER_ERROR = 0x315,
        /// <summary>[790] {Delayed Write Failed} Windows was unable to save all the data for the file %hs; the data has been lost. This error may be caused if the device has been removed or the media is write-protected.</summary>
        ERROR_LOST_WRITEBEHIND_DATA_LOCAL_DISK_ERROR = 0x316,
        /// <summary>[791] The resources required for this device conflict with the MCFG table.</summary>
        ERROR_BAD_MCFG_TABLE = 0x317,
        /// <summary>[792] The volume repair could not be performed while it is online. Please schedule to take the volume offline so that it can be repaired.</summary>
        ERROR_DISK_REPAIR_REDIRECTED = 0x318,
        /// <summary>[793] The volume repair was not successful.</summary>
        ERROR_DISK_REPAIR_UNSUCCESSFUL = 0x319,
        /// <summary>[794] One of the volume corruption logs is full. Further corruptions that may be detected won't be logged.</summary>
        ERROR_CORRUPT_LOG_OVERFULL = 0x31A,
        /// <summary>[795] One of the volume corruption logs is internally corrupted and needs to be recreated. The volume may contain undetected corruptions and must be scanned.</summary>
        ERROR_CORRUPT_LOG_CORRUPTED = 0x31B,
        /// <summary>[796] One of the volume corruption logs is unavailable for being operated on.</summary>
        ERROR_CORRUPT_LOG_UNAVAILABLE = 0x31C,
        /// <summary>[797] One of the volume corruption logs was deleted while still having corruption records in them. The volume contains detected corruptions and must be scanned.</summary>
        ERROR_CORRUPT_LOG_DELETED_FULL = 0x31D,
        /// <summary>[798] One of the volume corruption logs was cleared by chkdsk and no longer contains real corruptions.</summary>
        ERROR_CORRUPT_LOG_CLEARED = 0x31E,
        /// <summary>[799] Orphaned files exist on the volume but could not be recovered because no more new names could be created in the recovery directory. Files must be moved from the recovery directory.</summary>
        ERROR_ORPHAN_NAME_EXHAUSTED = 0x31F,
        /// <summary>[800] The oplock that was associated with this handle is now associated with a different handle.</summary>
        ERROR_OPLOCK_SWITCHED_TO_NEW_HANDLE = 0x320,
        /// <summary>[801] An oplock of the requested level cannot be granted. An oplock of a lower level may be available.</summary>
        ERROR_CANNOT_GRANT_REQUESTED_OPLOCK = 0x321,
        /// <summary>[802] The operation did not complete successfully because it would cause an oplock to be broken. The caller has requested that existing oplocks not be broken.</summary>
        ERROR_CANNOT_BREAK_OPLOCK = 0x322,
        /// <summary>[803] The handle with which this oplock was associated has been closed. The oplock is now broken.</summary>
        ERROR_OPLOCK_HANDLE_CLOSED = 0x323,
        /// <summary>[804] The specified access control entry (ACE) does not contain a condition.</summary>
        ERROR_NO_ACE_CONDITION = 0x324,
        /// <summary>[805] The specified access control entry (ACE) contains an invalid condition.</summary>
        ERROR_INVALID_ACE_CONDITION = 0x325,
        /// <summary>[806] Access to the specified file handle has been revoked.</summary>
        ERROR_FILE_HANDLE_REVOKED = 0x326,
        /// <summary>[807] An image file was mapped at a different address from the one specified in the image file but fixups will still be automatically performed on the image.</summary>
        ERROR_IMAGE_AT_DIFFERENT_BASE = 0x327,
        /// <summary>[994] Access to the extended attribute was denied.</summary>
        ERROR_EA_ACCESS_DENIED = 0x3E2,
        /// <summary>[995] The I/O operation has been aborted because of either a thread exit or an application request.</summary>
        ERROR_OPERATION_ABORTED = 0x3E3,
        /// <summary>[996] Overlapped I/O event is not in a signaled state.</summary>
        ERROR_IO_INCOMPLETE = 0x3E4,
        /// <summary>[997] Overlapped I/O operation is in progress.</summary>
        ERROR_IO_PENDING = 0x3E5,
        /// <summary>[998] Invalid access to memory location.</summary>
        ERROR_NOACCESS = 0x3E6,
        /// <summary>[999] Error performing inpage operation.</summary>
        ERROR_SWAPERROR = 0x3E7,
        /// <summary>[1001] Recursion too deep; the stack overflowed.</summary>
        ERROR_STACK_OVERFLOW = 0x3E9,
        /// <summary>[1002] The window cannot act on the sent message.</summary>
        ERROR_INVALID_MESSAGE = 0x3EA,
        /// <summary>[1003] Cannot complete this function.</summary>
        ERROR_CAN_NOT_COMPLETE = 0x3EB,
        /// <summary>[1004] Invalid flags.</summary>
        ERROR_INVALID_FLAGS = 0x3EC,
        /// <summary>[1005] The volume does not contain a recognized file system. Please make sure that all required file system drivers are loaded and that the volume is not corrupted.</summary>
        ERROR_UNRECOGNIZED_VOLUME = 0x3ED,
        /// <summary>[1006] The volume for a file has been externally altered so that the opened file is no longer valid.</summary>
        ERROR_FILE_INVALID = 0x3EE,
        /// <summary>[1007] The requested operation cannot be performed in full-screen mode.</summary>
        ERROR_FULLSCREEN_MODE = 0x3EF,
        /// <summary>[1008] An attempt was made to reference a token that does not exist.</summary>
        ERROR_NO_TOKEN = 0x3F0,
        /// <summary>[1009] The configuration registry database is corrupt.</summary>
        ERROR_BADDB = 0x3F1,
        /// <summary>[1010] The configuration registry key is invalid.</summary>
        ERROR_BADKEY = 0x3F2,
        /// <summary>[1011] The configuration registry key could not be opened.</summary>
        ERROR_CANTOPEN = 0x3F3,
        /// <summary>[1012] The configuration registry key could not be read.</summary>
        ERROR_CANTREAD = 0x3F4,
        /// <summary>[1013] The configuration registry key could not be written.</summary>
        ERROR_CANTWRITE = 0x3F5,
        /// <summary>[1014] One of the files in the registry database had to be recovered by use of a log or alternate copy. The recovery was successful.</summary>
        ERROR_REGISTRY_RECOVERED = 0x3F6,
        /// <summary>[1015] The registry is corrupted. The structure of one of the files containing registry data is corrupted, or the system's memory image of the file is corrupted, or the file could not be recovered because the alternate copy or log was absent or corrupted.</summary>
        ERROR_REGISTRY_CORRUPT = 0x3F7,
        /// <summary>[1016] An I/O operation initiated by the registry failed unrecoverably. The registry could not read in, or write out, or flush, one of the files that contain the system's image of the registry.</summary>
        ERROR_REGISTRY_IO_FAILED = 0x3F8,
        /// <summary>[1017] The system has attempted to load or restore a file into the registry, but the specified file is not in a registry file format.</summary>
        ERROR_NOT_REGISTRY_FILE = 0x3F9,
        /// <summary>[1018] Illegal operation attempted on a registry key that has been marked for deletion.</summary>
        ERROR_KEY_DELETED = 0x3FA,
        /// <summary>[1019] System could not allocate the required space in a registry log.</summary>
        ERROR_NO_LOG_SPACE = 0x3FB,
        /// <summary>[1020] Cannot create a symbolic link in a registry key that already has subkeys or values.</summary>
        ERROR_KEY_HAS_CHILDREN = 0x3FC,
        /// <summary>[1021] Cannot create a stable subkey under a volatile parent key.</summary>
        ERROR_CHILD_MUST_BE_VOLATILE = 0x3FD,
        /// <summary>[1022] A notify change request is being completed and the information is not being returned in the caller's buffer. The caller now needs to enumerate the files to find the changes.</summary>
        ERROR_NOTIFY_ENUM_DIR = 0x3FE,
        /// <summary>[1051] A stop control has been sent to a service that other running services are dependent on.</summary>
        ERROR_DEPENDENT_SERVICES_RUNNING = 0x41B,
        /// <summary>[1052] The requested control is not valid for this service.</summary>
        ERROR_INVALID_SERVICE_CONTROL = 0x41C,
        /// <summary>[1053] The service did not respond to the start or control request in a timely fashion.</summary>
        ERROR_SERVICE_REQUEST_TIMEOUT = 0x41D,
        /// <summary>[1054] A thread could not be created for the service.</summary>
        ERROR_SERVICE_NO_THREAD = 0x41E,
        /// <summary>[1055] The service database is locked.</summary>
        ERROR_SERVICE_DATABASE_LOCKED = 0x41F,
        /// <summary>[1056] An instance of the service is already running.</summary>
        ERROR_SERVICE_ALREADY_RUNNING = 0x420,
        /// <summary>[1057] The account name is invalid or does not exist, or the password is invalid for the account name specified.</summary>
        ERROR_INVALID_SERVICE_ACCOUNT = 0x421,
        /// <summary>[1058] The service cannot be started, either because it is disabled or because it has no enabled devices associated with it.</summary>
        ERROR_SERVICE_DISABLED = 0x422,
        /// <summary>[1059] Circular service dependency was specified.</summary>
        ERROR_CIRCULAR_DEPENDENCY = 0x423,
        /// <summary>[1060] The specified service does not exist as an installed service.</summary>
        ERROR_SERVICE_DOES_NOT_EXIST = 0x424,
        /// <summary>[1061] The service cannot accept control messages at this time.</summary>
        ERROR_SERVICE_CANNOT_ACCEPT_CTRL = 0x425,
        /// <summary>[1062] The service has not been started.</summary>
        ERROR_SERVICE_NOT_ACTIVE = 0x426,
        /// <summary>[1063] The service process could not connect to the service controller.</summary>
        ERROR_FAILED_SERVICE_CONTROLLER_CONNECT = 0x427,
        /// <summary>[1064] An exception occurred in the service when handling the control request.</summary>
        ERROR_EXCEPTION_IN_SERVICE = 0x428,
        /// <summary>[1065] The database specified does not exist.</summary>
        ERROR_DATABASE_DOES_NOT_EXIST = 0x429,
        /// <summary>[1066] The service has returned a service-specific error code.</summary>
        ERROR_SERVICE_SPECIFIC_ERROR = 0x42A,
        /// <summary>[1067] The process terminated unexpectedly.</summary>
        ERROR_PROCESS_ABORTED = 0x42B,
        /// <summary>[1068] The dependency service or group failed to start.</summary>
        ERROR_SERVICE_DEPENDENCY_FAIL = 0x42C,
        /// <summary>[1069] The service did not start due to a logon failure.</summary>
        ERROR_SERVICE_LOGON_FAILED = 0x42D,
        /// <summary>[1070] After starting, the service hung in a start-pending state.</summary>
        ERROR_SERVICE_START_HANG = 0x42E,
        /// <summary>[1071] The specified service database lock is invalid.</summary>
        ERROR_INVALID_SERVICE_LOCK = 0x42F,
        /// <summary>[1072] The specified service has been marked for deletion.</summary>
        ERROR_SERVICE_MARKED_FOR_DELETE = 0x430,
        /// <summary>[1073] The specified service already exists.</summary>
        ERROR_SERVICE_EXISTS = 0x431,
        /// <summary>[1074] The system is currently running with the last-known-good configuration.</summary>
        ERROR_ALREADY_RUNNING_LKG = 0x432,
        /// <summary>[1075] The dependency service does not exist or has been marked for deletion.</summary>
        ERROR_SERVICE_DEPENDENCY_DELETED = 0x433,
        /// <summary>[1076] The current boot has already been accepted for use as the last-known-good control set.</summary>
        ERROR_BOOT_ALREADY_ACCEPTED = 0x434,
        /// <summary>[1077] No attempts to start the service have been made since the last boot.</summary>
        ERROR_SERVICE_NEVER_STARTED = 0x435,
        /// <summary>[1078] The name is already in use as either a service name or a service display name.</summary>
        ERROR_DUPLICATE_SERVICE_NAME = 0x436,
        /// <summary>[1079] The account specified for this service is different from the account specified for other services running in the same process.</summary>
        ERROR_DIFFERENT_SERVICE_ACCOUNT = 0x437,
        /// <summary>[1080] Failure actions can only be set for Win32 services, not for drivers.</summary>
        ERROR_CANNOT_DETECT_DRIVER_FAILURE = 0x438,
        /// <summary>[1081] This service runs in the same process as the service control manager. Therefore, the service control manager cannot take action if this service's process terminates unexpectedly.</summary>
        ERROR_CANNOT_DETECT_PROCESS_ABORT = 0x439,
        /// <summary>[1082] No recovery program has been configured for this service.</summary>
        ERROR_NO_RECOVERY_PROGRAM = 0x43A,
        /// <summary>[1083] The executable program that this service is configured to run in does not implement the service.</summary>
        ERROR_SERVICE_NOT_IN_EXE = 0x43B,
        /// <summary>[1084] This service cannot be started in Safe Mode.</summary>
        ERROR_NOT_SAFEBOOT_SERVICE = 0x43C,
        /// <summary>[1100] The physical end of the tape has been reached.</summary>
        ERROR_END_OF_MEDIA = 0x44C,
        /// <summary>[1101] A tape access reached a filemark.</summary>
        ERROR_FILEMARK_DETECTED = 0x44D,
        /// <summary>[1102] The beginning of the tape or a partition was encountered.</summary>
        ERROR_BEGINNING_OF_MEDIA = 0x44E,
        /// <summary>[1103] A tape access reached the end of a set of files.</summary>
        ERROR_SETMARK_DETECTED = 0x44F,
        /// <summary>[1104] No more data is on the tape.</summary>
        ERROR_NO_DATA_DETECTED = 0x450,
        /// <summary>[1105] Tape could not be partitioned.</summary>
        ERROR_PARTITION_FAILURE = 0x451,
        /// <summary>[1106] When accessing a new tape of a multivolume partition, the current block size is incorrect.</summary>
        ERROR_INVALID_BLOCK_LENGTH = 0x452,
        /// <summary>[1107] Tape partition information could not be found when loading a tape.</summary>
        ERROR_DEVICE_NOT_PARTITIONED = 0x453,
        /// <summary>[1108] Unable to lock the media eject mechanism.</summary>
        ERROR_UNABLE_TO_LOCK_MEDIA = 0x454,
        /// <summary>[1109] Unable to unload the media.</summary>
        ERROR_UNABLE_TO_UNLOAD_MEDIA = 0x455,
        /// <summary>[1110] The media in the drive may have changed.</summary>
        ERROR_MEDIA_CHANGED = 0x456,
        /// <summary>[1111] The I/O bus was reset.</summary>
        ERROR_BUS_RESET = 0x457,
        /// <summary>[1112] No media in drive.</summary>
        ERROR_NO_MEDIA_IN_DRIVE = 0x458,
        /// <summary>[1113] No mapping for the Unicode character exists in the target multi-byte code page.</summary>
        ERROR_NO_UNICODE_TRANSLATION = 0x459,
        /// <summary>[1114] A dynamic link library (DLL) initialization routine failed.</summary>
        ERROR_DLL_INIT_FAILED = 0x45A,
        /// <summary>[1115] A system shutdown is in progress.</summary>
        ERROR_SHUTDOWN_IN_PROGRESS = 0x45B,
        /// <summary>[1116] Unable to abort the system shutdown because no shutdown was in progress.</summary>
        ERROR_NO_SHUTDOWN_IN_PROGRESS = 0x45C,
        /// <summary>[1117] The request could not be performed because of an I/O device error.</summary>
        ERROR_IO_DEVICE = 0x45D,
        /// <summary>[1118] No serial device was successfully initialized. The serial driver will unload.</summary>
        ERROR_SERIAL_NO_DEVICE = 0x45E,
        /// <summary>[1119] Unable to open a device that was sharing an interrupt request (IRQ) with other devices. At least one other device that uses that IRQ was already opened.</summary>
        ERROR_IRQ_BUSY = 0x45F,
        /// <summary>[1120] A serial I/O operation was completed by another write to the serial port. The IOCTL_SERIAL_XOFF_COUNTER reached zero.)</summary>
        ERROR_MORE_WRITES = 0x460,
        /// <summary>[1121] A serial I/O operation completed because the timeout period expired. The IOCTL_SERIAL_XOFF_COUNTER did not reach zero.)</summary>
        ERROR_COUNTER_TIMEOUT = 0x461,
        /// <summary>[1122] No ID address mark was found on the floppy disk.</summary>
        ERROR_FLOPPY_ID_MARK_NOT_FOUND = 0x462,
        /// <summary>[1123] Mismatch between the floppy disk sector ID field and the floppy disk controller track address.</summary>
        ERROR_FLOPPY_WRONG_CYLINDER = 0x463,
        /// <summary>[1124] The floppy disk controller reported an error that is not recognized by the floppy disk driver.</summary>
        ERROR_FLOPPY_UNKNOWN_ERROR = 0x464,
        /// <summary>[1125] The floppy disk controller returned inconsistent results in its registers.</summary>
        ERROR_FLOPPY_BAD_REGISTERS = 0x465,
        /// <summary>[1126] While accessing the hard disk, a recalibrate operation failed, even after retries.</summary>
        ERROR_DISK_RECALIBRATE_FAILED = 0x466,
        /// <summary>[1127] While accessing the hard disk, a disk operation failed even after retries.</summary>
        ERROR_DISK_OPERATION_FAILED = 0x467,
        /// <summary>[1128] While accessing the hard disk, a disk controller reset was needed, but even that failed.</summary>
        ERROR_DISK_RESET_FAILED = 0x468,
        /// <summary>[1129] Physical end of tape encountered.</summary>
        ERROR_EOM_OVERFLOW = 0x469,
        /// <summary>[1130] Not enough server storage is available to process this command.</summary>
        ERROR_NOT_ENOUGH_SERVER_MEMORY = 0x46A,
        /// <summary>[1131] A potential deadlock condition has been detected.</summary>
        ERROR_POSSIBLE_DEADLOCK = 0x46B,
        /// <summary>[1132] The base address or the file offset specified does not have the proper alignment.</summary>
        ERROR_MAPPED_ALIGNMENT = 0x46C,
        /// <summary>[1140] An attempt to change the system power state was vetoed by another application or driver.</summary>
        ERROR_SET_POWER_STATE_VETOED = 0x474,
        /// <summary>[1141] The system BIOS failed an attempt to change the system power state.</summary>
        ERROR_SET_POWER_STATE_FAILED = 0x475,
        /// <summary>[1142] An attempt was made to create more links on a file than the file system supports.</summary>
        ERROR_TOO_MANY_LINKS = 0x476,
        /// <summary>[1150] The specified program requires a newer version of Windows.</summary>
        ERROR_OLD_WIN_VERSION = 0x47E,
        /// <summary>[1151] The specified program is not a Windows or MS-DOS program.</summary>
        ERROR_APP_WRONG_OS = 0x47F,
        /// <summary>[1152] Cannot start more than one instance of the specified program.</summary>
        ERROR_SINGLE_INSTANCE_APP = 0x480,
        /// <summary>[1153] The specified program was written for an earlier version of Windows.</summary>
        ERROR_RMODE_APP = 0x481,
        /// <summary>[1154] One of the library files needed to run this application is damaged.</summary>
        ERROR_INVALID_DLL = 0x482,
        /// <summary>[1155] No application is associated with the specified file for this operation.</summary>
        ERROR_NO_ASSOCIATION = 0x483,
        /// <summary>[1156] An error occurred in sending the command to the application.</summary>
        ERROR_DDE_FAIL = 0x484,
        /// <summary>[1157] One of the library files needed to run this application cannot be found.</summary>
        ERROR_DLL_NOT_FOUND = 0x485,
        /// <summary>[1158] The current process has used all of its system allowance of handles for Window Manager objects.</summary>
        ERROR_NO_MORE_USER_HANDLES = 0x486,
        /// <summary>[1159] The message can be used only with synchronous operations.</summary>
        ERROR_MESSAGE_SYNC_ONLY = 0x487,
        /// <summary>[1160] The indicated source element has no media.</summary>
        ERROR_SOURCE_ELEMENT_EMPTY = 0x488,
        /// <summary>[1161] The indicated destination element already contains media.</summary>
        ERROR_DESTINATION_ELEMENT_FULL = 0x489,
        /// <summary>[1162] The indicated element does not exist.</summary>
        ERROR_ILLEGAL_ELEMENT_ADDRESS = 0x48A,
        /// <summary>[1163] The indicated element is part of a magazine that is not present.</summary>
        ERROR_MAGAZINE_NOT_PRESENT = 0x48B,
        /// <summary>[1164] The indicated device requires reinitialization due to hardware errors.</summary>
        ERROR_DEVICE_REINITIALIZATION_NEEDED = 0x48C,
        /// <summary>[1165] The device has indicated that cleaning is required before further operations are attempted.</summary>
        ERROR_DEVICE_REQUIRES_CLEANING = 0x48D,
        /// <summary>[1166] The device has indicated that its door is open.</summary>
        ERROR_DEVICE_DOOR_OPEN = 0x48E,
        /// <summary>[1167] The device is not connected.</summary>
        ERROR_DEVICE_NOT_CONNECTED = 0x48F,
        /// <summary>[1168] Element not found.</summary>
        ERROR_NOT_FOUND = 0x490,
        /// <summary>[1169] There was no match for the specified key in the index.</summary>
        ERROR_NO_MATCH = 0x491,
        /// <summary>[1170] The property set specified does not exist on the object.</summary>
        ERROR_SET_NOT_FOUND = 0x492,
        /// <summary>[1171] The point passed to GetMouseMovePoints is not in the buffer.</summary>
        ERROR_POINT_NOT_FOUND = 0x493,
        /// <summary>[1172] The tracking (workstation) service is not running.</summary>
        ERROR_NO_TRACKING_SERVICE = 0x494,
        /// <summary>[1173] The Volume ID could not be found.</summary>
        ERROR_NO_VOLUME_ID = 0x495,
        /// <summary>[1175] Unable to remove the file to be replaced.</summary>
        ERROR_UNABLE_TO_REMOVE_REPLACED = 0x497,
        /// <summary>[1176] Unable to move the replacement file to the file to be replaced. The file to be replaced has retained its original name.</summary>
        ERROR_UNABLE_TO_MOVE_REPLACEMENT = 0x498,
        /// <summary>[1177] Unable to move the replacement file to the file to be replaced. The file to be replaced has been renamed using the backup name.</summary>
        ERROR_UNABLE_TO_MOVE_REPLACEMENT_2 = 0x499,
        /// <summary>[1178] The volume change journal is being deleted.</summary>
        ERROR_JOURNAL_DELETE_IN_PROGRESS = 0x49A,
        /// <summary>[1179] The volume change journal is not active.</summary>
        ERROR_JOURNAL_NOT_ACTIVE = 0x49B,
        /// <summary>[1180] A file was found, but it may not be the correct file.</summary>
        ERROR_POTENTIAL_FILE_FOUND = 0x49C,
        /// <summary>[1181] The journal entry has been deleted from the journal.</summary>
        ERROR_JOURNAL_ENTRY_DELETED = 0x49D,
        /// <summary>[1190] A system shutdown has already been scheduled.</summary>
        ERROR_SHUTDOWN_IS_SCHEDULED = 0x4A6,
        /// <summary>[1191] The system shutdown cannot be initiated because there are other users logged on to the computer.</summary>
        ERROR_SHUTDOWN_USERS_LOGGED_ON = 0x4A7,
        /// <summary>[1200] The specified device name is invalid.</summary>
        ERROR_BAD_DEVICE = 0x4B0,
        /// <summary>[1201] The device is not currently connected but it is a remembered connection.</summary>
        ERROR_CONNECTION_UNAVAIL = 0x4B1,
        /// <summary>[1202] The local device name has a remembered connection to another network resource.</summary>
        ERROR_DEVICE_ALREADY_REMEMBERED = 0x4B2,
        /// <summary>[1203] The network path was either typed incorrectly, does not exist, or the network provider is not currently available. Please try retyping the path or contact your network administrator.</summary>
        ERROR_NO_NET_OR_BAD_PATH = 0x4B3,
        /// <summary>[1204] The specified network provider name is invalid.</summary>
        ERROR_BAD_PROVIDER = 0x4B4,
        /// <summary>[1205] Unable to open the network connection profile.</summary>
        ERROR_CANNOT_OPEN_PROFILE = 0x4B5,
        /// <summary>[1206] The network connection profile is corrupted.</summary>
        ERROR_BAD_PROFILE = 0x4B6,
        /// <summary>[1207] Cannot enumerate a noncontainer.</summary>
        ERROR_NOT_CONTAINER = 0x4B7,
        /// <summary>[1208] An extended error has occurred.</summary>
        ERROR_EXTENDED_ERROR = 0x4B8,
        /// <summary>[1209] The format of the specified group name is invalid.</summary>
        ERROR_INVALID_GROUPNAME = 0x4B9,
        /// <summary>[1210] The format of the specified computer name is invalid.</summary>
        ERROR_INVALID_COMPUTERNAME = 0x4BA,
        /// <summary>[1211] The format of the specified event name is invalid.</summary>
        ERROR_INVALID_EVENTNAME = 0x4BB,
        /// <summary>[1212] The format of the specified domain name is invalid.</summary>
        ERROR_INVALID_DOMAINNAME = 0x4BC,
        /// <summary>[1213] The format of the specified service name is invalid.</summary>
        ERROR_INVALID_SERVICENAME = 0x4BD,
        /// <summary>[1214] The format of the specified network name is invalid.</summary>
        ERROR_INVALID_NETNAME = 0x4BE,
        /// <summary>[1215] The format of the specified share name is invalid.</summary>
        ERROR_INVALID_SHARENAME = 0x4BF,
        /// <summary>[1216] The format of the specified password is invalid.</summary>
        ERROR_INVALID_PASSWORDNAME = 0x4C0,
        /// <summary>[1217] The format of the specified message name is invalid.</summary>
        ERROR_INVALID_MESSAGENAME = 0x4C1,
        /// <summary>[1218] The format of the specified message destination is invalid.</summary>
        ERROR_INVALID_MESSAGEDEST = 0x4C2,
        /// <summary>[1219] Multiple connections to a server or shared resource by the same user, using more than one user name, are not allowed. Disconnect all previous connections to the server or shared resource and try again.</summary>
        ERROR_SESSION_CREDENTIAL_CONFLICT = 0x4C3,
        /// <summary>[1220] An attempt was made to establish a session to a network server, but there are already too many sessions established to that server.</summary>
        ERROR_REMOTE_SESSION_LIMIT_EXCEEDED = 0x4C4,
        /// <summary>[1221] The workgroup or domain name is already in use by another computer on the network.</summary>
        ERROR_DUP_DOMAINNAME = 0x4C5,
        /// <summary>[1222] The network is not present or not started.</summary>
        ERROR_NO_NETWORK = 0x4C6,
        /// <summary>[1223] The operation was canceled by the user.</summary>
        ERROR_CANCELLED = 0x4C7,
        /// <summary>[1224] The requested operation cannot be performed on a file with a user-mapped section open.</summary>
        ERROR_USER_MAPPED_FILE = 0x4C8,
        /// <summary>[1225] The remote computer refused the network connection.</summary>
        ERROR_CONNECTION_REFUSED = 0x4C9,
        /// <summary>[1226] The network connection was gracefully closed.</summary>
        ERROR_GRACEFUL_DISCONNECT = 0x4CA,
        /// <summary>[1227] The network transport endpoint already has an address associated with it.</summary>
        ERROR_ADDRESS_ALREADY_ASSOCIATED = 0x4CB,
        /// <summary>[1228] An address has not yet been associated with the network endpoint.</summary>
        ERROR_ADDRESS_NOT_ASSOCIATED = 0x4CC,
        /// <summary>[1229] An operation was attempted on a nonexistent network connection.</summary>
        ERROR_CONNECTION_INVALID = 0x4CD,
        /// <summary>[1230] An invalid operation was attempted on an active network connection.</summary>
        ERROR_CONNECTION_ACTIVE = 0x4CE,
        /// <summary>[1231] The network location cannot be reached. For information about network troubleshooting, see Windows Help.</summary>
        ERROR_NETWORK_UNREACHABLE = 0x4CF,
        /// <summary>[1232] The network location cannot be reached. For information about network troubleshooting, see Windows Help.</summary>
        ERROR_HOST_UNREACHABLE = 0x4D0,
        /// <summary>[1233] The network location cannot be reached. For information about network troubleshooting, see Windows Help.</summary>
        ERROR_PROTOCOL_UNREACHABLE = 0x4D1,
        /// <summary>[1234] No service is operating at the destination network endpoint on the remote system.</summary>
        ERROR_PORT_UNREACHABLE = 0x4D2,
        /// <summary>[1235] The request was aborted.</summary>
        ERROR_REQUEST_ABORTED = 0x4D3,
        /// <summary>[1236] The network connection was aborted by the local system.</summary>
        ERROR_CONNECTION_ABORTED = 0x4D4,
        /// <summary>[1237] The operation could not be completed. A retry should be performed.</summary>
        ERROR_RETRY = 0x4D5,
        /// <summary>[1238] A connection to the server could not be made because the limit on the number of concurrent connections for this account has been reached.</summary>
        ERROR_CONNECTION_COUNT_LIMIT = 0x4D6,
        /// <summary>[1239] Attempting to log in during an unauthorized time of day for this account.</summary>
        ERROR_LOGIN_TIME_RESTRICTION = 0x4D7,
        /// <summary>[1240] The account is not authorized to log in from this station.</summary>
        ERROR_LOGIN_WKSTA_RESTRICTION = 0x4D8,
        /// <summary>[1241] The network address could not be used for the operation requested.</summary>
        ERROR_INCORRECT_ADDRESS = 0x4D9,
        /// <summary>[1242] The service is already registered.</summary>
        ERROR_ALREADY_REGISTERED = 0x4DA,
        /// <summary>[1243] The specified service does not exist.</summary>
        ERROR_SERVICE_NOT_FOUND = 0x4DB,
        /// <summary>[1244] The operation being requested was not performed because the user has not been authenticated.</summary>
        ERROR_NOT_AUTHENTICATED = 0x4DC,
        /// <summary>[1245] The operation being requested was not performed because the user has not logged on to the network. The specified service does not exist.</summary>
        ERROR_NOT_LOGGED_ON = 0x4DD,
        /// <summary>[1246] Continue with work in progress.</summary>
        ERROR_CONTINUE = 0x4DE,
        /// <summary>[1247] An attempt was made to perform an initialization operation when initialization has already been completed.</summary>
        ERROR_ALREADY_INITIALIZED = 0x4DF,
        /// <summary>[1248] No more local devices.</summary>
        ERROR_NO_MORE_DEVICES = 0x4E0,
        /// <summary>[1249] The specified site does not exist.</summary>
        ERROR_NO_SUCH_SITE = 0x4E1,
        /// <summary>[1250] A domain controller with the specified name already exists.</summary>
        ERROR_DOMAIN_CONTROLLER_EXISTS = 0x4E2,
        /// <summary>[1251] This operation is supported only when you are connected to the server.</summary>
        ERROR_ONLY_IF_CONNECTED = 0x4E3,
        /// <summary>[1252] The group policy framework should call the extension even if there are no changes.</summary>
        ERROR_OVERRIDE_NOCHANGES = 0x4E4,
        /// <summary>[1253] The specified user does not have a valid profile.</summary>
        ERROR_BAD_USER_PROFILE = 0x4E5,
        /// <summary>[1254] This operation is not supported on a computer running Windows Server 2003 for Small Business Server.</summary>
        ERROR_NOT_SUPPORTED_ON_SBS = 0x4E6,
        /// <summary>[1255] The server machine is shutting down.</summary>
        ERROR_SERVER_SHUTDOWN_IN_PROGRESS = 0x4E7,
        /// <summary>[1256] The remote system is not available. For information about network troubleshooting, see Windows Help.</summary>
        ERROR_HOST_DOWN = 0x4E8,
        /// <summary>[1257] The security identifier provided is not from an account domain.</summary>
        ERROR_NON_ACCOUNT_SID = 0x4E9,
        /// <summary>[1258] The security identifier provided does not have a domain component.</summary>
        ERROR_NON_DOMAIN_SID = 0x4EA,
        /// <summary>[1259] AppHelp dialog canceled thus preventing the application from starting.</summary>
        ERROR_APPHELP_BLOCK = 0x4EB,
        /// <summary>[1260] This program is blocked by group policy. For more information, contact your system administrator.</summary>
        ERROR_ACCESS_DISABLED_BY_POLICY = 0x4EC,
        /// <summary>[1261] A program attempt to use an invalid register value. Normally caused by an uninitialized register. This error is Itanium specific.</summary>
        ERROR_REG_NAT_CONSUMPTION = 0x4ED,
        /// <summary>[1262] The share is currently offline or does not exist.</summary>
        ERROR_CSCSHARE_OFFLINE = 0x4EE,
        /// <summary>[1263] The Kerberos protocol encountered an error while validating the KDC certificate during smartcard logon. There is more information in the system event log.</summary>
        ERROR_PKINIT_FAILURE = 0x4EF,
        /// <summary>[1264] The Kerberos protocol encountered an error while attempting to utilize the smartcard subsystem.</summary>
        ERROR_SMARTCARD_SUBSYSTEM_FAILURE = 0x4F0,
        /// <summary>[1265] The system cannot contact a domain controller to service the authentication request. Please try again later.</summary>
        ERROR_DOWNGRADE_DETECTED = 0x4F1,
        /// <summary>[1271] The machine is locked and cannot be shut down without the force option.</summary>
        ERROR_MACHINE_LOCKED = 0x4F7,
        /// <summary>[1273] An application-defined callback gave invalid data when called.</summary>
        ERROR_CALLBACK_SUPPLIED_INVALID_DATA = 0x4F9,
        /// <summary>[1274] The group policy framework should call the extension in the synchronous foreground policy refresh.</summary>
        ERROR_SYNC_FOREGROUND_REFRESH_REQUIRED = 0x4FA,
        /// <summary>[1275] This driver has been blocked from loading.</summary>
        ERROR_DRIVER_BLOCKED = 0x4FB,
        /// <summary>[1276] A dynamic link library (DLL) referenced a module that was neither a DLL nor the process's executable image.</summary>
        ERROR_INVALID_IMPORT_OF_NON_DLL = 0x4FC,
        /// <summary>[1277] Windows cannot open this program since it has been disabled.</summary>
        ERROR_ACCESS_DISABLED_WEBBLADE = 0x4FD,
        /// <summary>[1278] Windows cannot open this program because the license enforcement system has been tampered with or become corrupted.</summary>
        ERROR_ACCESS_DISABLED_WEBBLADE_TAMPER = 0x4FE,
        /// <summary>[1279] A transaction recover failed.</summary>
        ERROR_RECOVERY_FAILURE = 0x4FF,
        /// <summary>[1280] The current thread has already been converted to a fiber.</summary>
        ERROR_ALREADY_FIBER = 0x500,
        /// <summary>[1281] The current thread has already been converted from a fiber.</summary>
        ERROR_ALREADY_THREAD = 0x501,
        /// <summary>[1282] The system detected an overrun of a stack-based buffer in this application. This overrun could potentially allow a malicious user to gain control of this application.</summary>
        ERROR_STACK_BUFFER_OVERRUN = 0x502,
        /// <summary>[1283] Data present in one of the parameters is more than the function can operate on.</summary>
        ERROR_PARAMETER_QUOTA_EXCEEDED = 0x503,
        /// <summary>[1284] An attempt to do an operation on a debug object failed because the object is in the process of being deleted.</summary>
        ERROR_DEBUGGER_INACTIVE = 0x504,
        /// <summary>[1285] An attempt to delay-load a .dll or get a function address in a delay-loaded .dll failed.</summary>
        ERROR_DELAY_LOAD_FAILED = 0x505,
        /// <summary>[1286] %1 is a 16-bit application. You do not have permissions to execute 16-bit applications. Check your permissions with your system administrator.</summary>
        ERROR_VDM_DISALLOWED = 0x506,
        /// <summary>[1287] Insufficient information exists to identify the cause of failure.</summary>
        ERROR_UNIDENTIFIED_ERROR = 0x507,
        /// <summary>[1288] The parameter passed to a C runtime function is incorrect.</summary>
        ERROR_INVALID_CRUNTIME_PARAMETER = 0x508,
        /// <summary>[1289] The operation occurred beyond the valid data length of the file.</summary>
        ERROR_BEYOND_VDL = 0x509,
        /// <summary>
        /// [1290] The service start failed since one or more services in the same process have an incompatible service SID type setting. A service with restricted service SID type can only coexist in the same process with other services with a restricted SID type. If the service SID type for this service was just configured, the hosting process must be restarted in order to start this service.
        /// <br />On Windows Server 2003 and Windows XP, an unrestricted service cannot coexist in the same process with other services. The service with the unrestricted service SID type must be moved to an owned process in order to start this service.
        /// </summary>
        ERROR_INCOMPATIBLE_SERVICE_SID_TYPE = 0x50A,
        /// <summary>[1291] The process hosting the driver for this device has been terminated.</summary>
        ERROR_DRIVER_PROCESS_TERMINATED = 0x50B,
        /// <summary>[1292] An operation attempted to exceed an implementation-defined limit.</summary>
        ERROR_IMPLEMENTATION_LIMIT = 0x50C,
        /// <summary>[1293] Either the target process, or the target thread's containing process, is a protected process.</summary>
        ERROR_PROCESS_IS_PROTECTED = 0x50D,
        /// <summary>[1294] The service notification client is lagging too far behind the current state of services in the machine.</summary>
        ERROR_SERVICE_NOTIFY_CLIENT_LAGGING = 0x50E,
        /// <summary>[1295] The requested file operation failed because the storage quota was exceeded. To free up disk space, move files to a different location or delete unnecessary files. For more information, contact your system administrator.</summary>
        ERROR_DISK_QUOTA_EXCEEDED = 0x50F,
        /// <summary>[1296] The requested file operation failed because the storage policy blocks that type of file. For more information, contact your system administrator.</summary>
        ERROR_CONTENT_BLOCKED = 0x510,
        /// <summary>[1297] A privilege that the service requires to function properly does not exist in the service account configuration. You may use the Services Microsoft Management Console (MMC) snap-in (services.msc) and the Local Security Settings MMC snap-in (secpol.msc) to view the service configuration and the account configuration.</summary>
        ERROR_INCOMPATIBLE_SERVICE_PRIVILEGE = 0x511,
        /// <summary>[1298] A thread involved in this operation appears to be unresponsive.</summary>
        ERROR_APP_HANG = 0x512,
        /// <summary>[1299] Indicates a particular Security ID may not be assigned as the label of an object.</summary>
        ERROR_INVALID_LABEL = 0x513,
        /// <summary>[1300] Not all privileges or groups referenced are assigned to the caller.</summary>
        ERROR_NOT_ALL_ASSIGNED = 0x514,
        /// <summary>[1301] Some mapping between account names and security IDs was not done.</summary>
        ERROR_SOME_NOT_MAPPED = 0x515,
        /// <summary>[1302] No system quota limits are specifically set for this account.</summary>
        ERROR_NO_QUOTAS_FOR_ACCOUNT = 0x516,
        /// <summary>[1303] No encryption key is available. A well-known encryption key was returned.</summary>
        ERROR_LOCAL_USER_SESSION_KEY = 0x517,
        /// <summary>[1304] The password is too complex to be converted to a LAN Manager password. The LAN Manager password returned is a NULL string.</summary>
        ERROR_NULL_LM_PASSWORD = 0x518,
        /// <summary>[1305] The revision level is unknown.</summary>
        ERROR_UNKNOWN_REVISION = 0x519,
        /// <summary>[1306] Indicates two revision levels are incompatible.</summary>
        ERROR_REVISION_MISMATCH = 0x51A,
        /// <summary>[1307] This security ID may not be assigned as the owner of this object.</summary>
        ERROR_INVALID_OWNER = 0x51B,
        /// <summary>[1308] This security ID may not be assigned as the primary group of an object.</summary>
        ERROR_INVALID_PRIMARY_GROUP = 0x51C,
        /// <summary>[1309] An attempt has been made to operate on an impersonation token by a thread that is not currently impersonating a client.</summary>
        ERROR_NO_IMPERSONATION_TOKEN = 0x51D,
        /// <summary>[1310] The group may not be disabled.</summary>
        ERROR_CANT_DISABLE_MANDATORY = 0x51E,
        /// <summary>[1311] There are currently no logon servers available to service the logon request.</summary>
        ERROR_NO_LOGON_SERVERS = 0x51F,
        /// <summary>[1312] A specified logon session does not exist. It may already have been terminated.</summary>
        ERROR_NO_SUCH_LOGON_SESSION = 0x520,
        /// <summary>[1313] A specified privilege does not exist.</summary>
        ERROR_NO_SUCH_PRIVILEGE = 0x521,
        /// <summary>[1314] A required privilege is not held by the client.</summary>
        ERROR_PRIVILEGE_NOT_HELD = 0x522,
        /// <summary>[1315] The name provided is not a properly formed account name.</summary>
        ERROR_INVALID_ACCOUNT_NAME = 0x523,
        /// <summary>[1316] The specified account already exists.</summary>
        ERROR_USER_EXISTS = 0x524,
        /// <summary>[1317] The specified account does not exist.</summary>
        ERROR_NO_SUCH_USER = 0x525,
        /// <summary>[1318] The specified group already exists.</summary>
        ERROR_GROUP_EXISTS = 0x526,
        /// <summary>[1319] The specified group does not exist.</summary>
        ERROR_NO_SUCH_GROUP = 0x527,
        /// <summary>[1320] Either the specified user account is already a member of the specified group, or the specified group cannot be deleted because it contains a member.</summary>
        ERROR_MEMBER_IN_GROUP = 0x528,
        /// <summary>[1321] The specified user account is not a member of the specified group account.</summary>
        ERROR_MEMBER_NOT_IN_GROUP = 0x529,
        /// <summary>[1322] This operation is disallowed as it could result in an administration account being disabled, deleted or unable to log on.</summary>
        ERROR_LAST_ADMIN = 0x52A,
        /// <summary>[1323] Unable to update the password. The value provided as the current password is incorrect.</summary>
        ERROR_WRONG_PASSWORD = 0x52B,
        /// <summary>[1324] Unable to update the password. The value provided for the new password contains values that are not allowed in passwords.</summary>
        ERROR_ILL_FORMED_PASSWORD = 0x52C,
        /// <summary>[1325] Unable to update the password. The value provided for the new password does not meet the length, complexity, or history requirements of the domain.</summary>
        ERROR_PASSWORD_RESTRICTION = 0x52D,
        /// <summary>[1326] The user name or password is incorrect.</summary>
        ERROR_LOGON_FAILURE = 0x52E,
        /// <summary>[1327] Account restrictions are preventing this user from signing in. For example: blank passwords aren't allowed, sign-in times are limited, or a policy restriction has been enforced.</summary>
        ERROR_ACCOUNT_RESTRICTION = 0x52F,
        /// <summary>[1328] Your account has time restrictions that keep you from signing in right now.</summary>
        ERROR_INVALID_LOGON_HOURS = 0x530,
        /// <summary>[1329] This user isn't allowed to sign in to this computer.</summary>
        ERROR_INVALID_WORKSTATION = 0x531,
        /// <summary>[1330] The password for this account has expired.</summary>
        ERROR_PASSWORD_EXPIRED = 0x532,
        /// <summary>[1331] This user can't sign in because this account is currently disabled.</summary>
        ERROR_ACCOUNT_DISABLED = 0x533,
        /// <summary>[1332] No mapping between account names and security IDs was done.</summary>
        ERROR_NONE_MAPPED = 0x534,
        /// <summary>[1333] Too many local user identifiers (LUIDs) were requested at one time.</summary>
        ERROR_TOO_MANY_LUIDS_REQUESTED = 0x535,
        /// <summary>[1334] No more local user identifiers (LUIDs) are available.</summary>
        ERROR_LUIDS_EXHAUSTED = 0x536,
        /// <summary>[1335] The subauthority part of a security ID is invalid for this particular use.</summary>
        ERROR_INVALID_SUB_AUTHORITY = 0x537,
        /// <summary>[1336] The access control list (ACL) structure is invalid.</summary>
        ERROR_INVALID_ACL = 0x538,
        /// <summary>[1337] The security ID structure is invalid.</summary>
        ERROR_INVALID_SID = 0x539,
        /// <summary>[1338] The security descriptor structure is invalid.</summary>
        ERROR_INVALID_SECURITY_DESCR = 0x53A,
        /// <summary>[1340] The inherited access control list (ACL) or access control entry (ACE) could not be built.</summary>
        ERROR_BAD_INHERITANCE_ACL = 0x53C,
        /// <summary>[1341] The server is currently disabled.</summary>
        ERROR_SERVER_DISABLED = 0x53D,
        /// <summary>[1342] The server is currently enabled.</summary>
        ERROR_SERVER_NOT_DISABLED = 0x53E,
        /// <summary>[1343] The value provided was an invalid value for an identifier authority.</summary>
        ERROR_INVALID_ID_AUTHORITY = 0x53F,
        /// <summary>[1344] No more memory is available for security information updates.</summary>
        ERROR_ALLOTTED_SPACE_EXCEEDED = 0x540,
        /// <summary>[1345] The specified attributes are invalid, or incompatible with the attributes for the group as a whole.</summary>
        ERROR_INVALID_GROUP_ATTRIBUTES = 0x541,
        /// <summary>[1346] Either a required impersonation level was not provided, or the provided impersonation level is invalid.</summary>
        ERROR_BAD_IMPERSONATION_LEVEL = 0x542,
        /// <summary>[1347] Cannot open an anonymous level security token.</summary>
        ERROR_CANT_OPEN_ANONYMOUS = 0x543,
        /// <summary>[1348] The validation information class requested was invalid.</summary>
        ERROR_BAD_VALIDATION_CLASS = 0x544,
        /// <summary>[1349] The type of the token is inappropriate for its attempted use.</summary>
        ERROR_BAD_TOKEN_TYPE = 0x545,
        /// <summary>[1350] Unable to perform a security operation on an object that has no associated security.</summary>
        ERROR_NO_SECURITY_ON_OBJECT = 0x546,
        /// <summary>[1351] Configuration information could not be read from the domain controller, either because the machine is unavailable, or access has been denied.</summary>
        ERROR_CANT_ACCESS_DOMAIN_INFO = 0x547,
        /// <summary>[1352] The security account manager (SAM) or local security authority (LSA) server was in the wrong state to perform the security operation.</summary>
        ERROR_INVALID_SERVER_STATE = 0x548,
        /// <summary>[1353] The domain was in the wrong state to perform the security operation.</summary>
        ERROR_INVALID_DOMAIN_STATE = 0x549,
        /// <summary>[1354] This operation is only allowed for the Primary Domain Controller of the domain.</summary>
        ERROR_INVALID_DOMAIN_ROLE = 0x54A,
        /// <summary>[1355] The specified domain either does not exist or could not be contacted.</summary>
        ERROR_NO_SUCH_DOMAIN = 0x54B,
        /// <summary>[1356] The specified domain already exists.</summary>
        ERROR_DOMAIN_EXISTS = 0x54C,
        /// <summary>[1357] An attempt was made to exceed the limit on the number of domains per server.</summary>
        ERROR_DOMAIN_LIMIT_EXCEEDED = 0x54D,
        /// <summary>[1358] Unable to complete the requested operation because of either a catastrophic media failure or a data structure corruption on the disk.</summary>
        ERROR_INTERNAL_DB_CORRUPTION = 0x54E,
        /// <summary>[1359] An internal error occurred.</summary>
        ERROR_INTERNAL_ERROR = 0x54F,
        /// <summary>[1360] Generic access types were contained in an access mask which should already be mapped to nongeneric types.</summary>
        ERROR_GENERIC_NOT_MAPPED = 0x550,
        /// <summary>[1361] A security descriptor is not in the right format (absolute or self-relative).</summary>
        ERROR_BAD_DESCRIPTOR_FORMAT = 0x551,
        /// <summary>[1362] The requested action is restricted for use by logon processes only. The calling process has not registered as a logon process.</summary>
        ERROR_NOT_LOGON_PROCESS = 0x552,
        /// <summary>[1363] Cannot start a new logon session with an ID that is already in use.</summary>
        ERROR_LOGON_SESSION_EXISTS = 0x553,
        /// <summary>[1364] A specified authentication package is unknown.</summary>
        ERROR_NO_SUCH_PACKAGE = 0x554,
        /// <summary>[1365] The logon session is not in a state that is consistent with the requested operation.</summary>
        ERROR_BAD_LOGON_SESSION_STATE = 0x555,
        /// <summary>[1366] The logon session ID is already in use.</summary>
        ERROR_LOGON_SESSION_COLLISION = 0x556,
        /// <summary>[1367] A logon request contained an invalid logon type value.</summary>
        ERROR_INVALID_LOGON_TYPE = 0x557,
        /// <summary>[1368] Unable to impersonate using a named pipe until data has been read from that pipe.</summary>
        ERROR_CANNOT_IMPERSONATE = 0x558,
        /// <summary>[1369] The transaction state of a registry subtree is incompatible with the requested operation.</summary>
        ERROR_RXACT_INVALID_STATE = 0x559,
        /// <summary>[1370] An internal security database corruption has been encountered.</summary>
        ERROR_RXACT_COMMIT_FAILURE = 0x55A,
        /// <summary>[1371] Cannot perform this operation on built-in accounts.</summary>
        ERROR_SPECIAL_ACCOUNT = 0x55B,
        /// <summary>[1372] Cannot perform this operation on this built-in special group.</summary>
        ERROR_SPECIAL_GROUP = 0x55C,
        /// <summary>[1373] Cannot perform this operation on this built-in special user.</summary>
        ERROR_SPECIAL_USER = 0x55D,
        /// <summary>[1374] The user cannot be removed from a group because the group is currently the user's primary group.</summary>
        ERROR_MEMBERS_PRIMARY_GROUP = 0x55E,
        /// <summary>[1375] The token is already in use as a primary token.</summary>
        ERROR_TOKEN_ALREADY_IN_USE = 0x55F,
        /// <summary>[1376] The specified local group does not exist.</summary>
        ERROR_NO_SUCH_ALIAS = 0x560,
        /// <summary>[1377] The specified account name is not a member of the group.</summary>
        ERROR_MEMBER_NOT_IN_ALIAS = 0x561,
        /// <summary>[1378] The specified account name is already a member of the group.</summary>
        ERROR_MEMBER_IN_ALIAS = 0x562,
        /// <summary>[1379] The specified local group already exists.</summary>
        ERROR_ALIAS_EXISTS = 0x563,
        /// <summary>[1380] Logon failure: the user has not been granted the requested logon type at this computer.</summary>
        ERROR_LOGON_NOT_GRANTED = 0x564,
        /// <summary>[1381] The maximum number of secrets that may be stored in a single system has been exceeded.</summary>
        ERROR_TOO_MANY_SECRETS = 0x565,
        /// <summary>[1382] The length of a secret exceeds the maximum length allowed.</summary>
        ERROR_SECRET_TOO_LONG = 0x566,
        /// <summary>[1383] The local security authority database contains an internal inconsistency.</summary>
        ERROR_INTERNAL_DB_ERROR = 0x567,
        /// <summary>[1384] During a logon attempt, the user's security context accumulated too many security IDs.</summary>
        ERROR_TOO_MANY_CONTEXT_IDS = 0x568,
        /// <summary>[1385] Logon failure: the user has not been granted the requested logon type at this computer.</summary>
        ERROR_LOGON_TYPE_NOT_GRANTED = 0x569,
        /// <summary>[1386] A cross-encrypted password is necessary to change a user password.</summary>
        ERROR_NT_CROSS_ENCRYPTION_REQUIRED = 0x56A,
        /// <summary>[1387] A member could not be added to or removed from the local group because the member does not exist.</summary>
        ERROR_NO_SUCH_MEMBER = 0x56B,
        /// <summary>[1388] A new member could not be added to a local group because the member has the wrong account type.</summary>
        ERROR_INVALID_MEMBER = 0x56C,
        /// <summary>[1389] Too many security IDs have been specified.</summary>
        ERROR_TOO_MANY_SIDS = 0x56D,
        /// <summary>[1390] A cross-encrypted password is necessary to change this user password.</summary>
        ERROR_LM_CROSS_ENCRYPTION_REQUIRED = 0x56E,
        /// <summary>[1391] Indicates an ACL contains no inheritable components.</summary>
        ERROR_NO_INHERITANCE = 0x56F,
        /// <summary>[1392] The file or directory is corrupted and unreadable.</summary>
        ERROR_FILE_CORRUPT = 0x570,
        /// <summary>[1393] The disk structure is corrupted and unreadable.</summary>
        ERROR_DISK_CORRUPT = 0x571,
        /// <summary>[1394] There is no user session key for the specified logon session.</summary>
        ERROR_NO_USER_SESSION_KEY = 0x572,
        /// <summary>[1395] The service being accessed is licensed for a particular number of connections. No more connections can be made to the service at this time because there are already as many connections as the service can accept.</summary>
        ERROR_LICENSE_QUOTA_EXCEEDED = 0x573,
        /// <summary>[1396] The target account name is incorrect.</summary>
        ERROR_WRONG_TARGET_NAME = 0x574,
        /// <summary>[1397] Mutual Authentication failed. The server's password is out of date at the domain controller.</summary>
        ERROR_MUTUAL_AUTH_FAILED = 0x575,
        /// <summary>[1398] There is a time and/or date difference between the client and server.</summary>
        ERROR_TIME_SKEW = 0x576,
        /// <summary>[1399] This operation cannot be performed on the current domain.</summary>
        ERROR_CURRENT_DOMAIN_NOT_ALLOWED = 0x577,
        /// <summary>[1400] Invalid window handle.</summary>
        ERROR_INVALID_WINDOW_HANDLE = 0x578,
        /// <summary>[1401] Invalid menu handle.</summary>
        ERROR_INVALID_MENU_HANDLE = 0x579,
        /// <summary>[1402] Invalid cursor handle.</summary>
        ERROR_INVALID_CURSOR_HANDLE = 0x57A,
        /// <summary>[1403] Invalid accelerator table handle.</summary>
        ERROR_INVALID_ACCEL_HANDLE = 0x57B,
        /// <summary>[1404] Invalid hook handle.</summary>
        ERROR_INVALID_HOOK_HANDLE = 0x57C,
        /// <summary>[1405] Invalid handle to a multiple-window position structure.</summary>
        ERROR_INVALID_DWP_HANDLE = 0x57D,
        /// <summary>[1406] Cannot create a top-level child window.</summary>
        ERROR_TLW_WITH_WSCHILD = 0x57E,
        /// <summary>[1407] Cannot find window class.</summary>
        ERROR_CANNOT_FIND_WND_CLASS = 0x57F,
        /// <summary>[1408] Invalid window; it belongs to other thread.</summary>
        ERROR_WINDOW_OF_OTHER_THREAD = 0x580,
        /// <summary>[1409] Hot key is already registered.</summary>
        ERROR_HOTKEY_ALREADY_REGISTERED = 0x581,
        /// <summary>[1410] Class already exists.</summary>
        ERROR_CLASS_ALREADY_EXISTS = 0x582,
        /// <summary>[1411] Class does not exist.</summary>
        ERROR_CLASS_DOES_NOT_EXIST = 0x583,
        /// <summary>[1412] Class still has open windows.</summary>
        ERROR_CLASS_HAS_WINDOWS = 0x584,
        /// <summary>[1413] Invalid index.</summary>
        ERROR_INVALID_INDEX = 0x585,
        /// <summary>[1414] Invalid icon handle.</summary>
        ERROR_INVALID_ICON_HANDLE = 0x586,
        /// <summary>[1415] Using private DIALOG window words.</summary>
        ERROR_PRIVATE_DIALOG_INDEX = 0x587,
        /// <summary>[1416] The list box identifier was not found.</summary>
        ERROR_LISTBOX_ID_NOT_FOUND = 0x588,
        /// <summary>[1417] No wildcards were found.</summary>
        ERROR_NO_WILDCARD_CHARACTERS = 0x589,
        /// <summary>[1418] Thread does not have a clipboard open.</summary>
        ERROR_CLIPBOARD_NOT_OPEN = 0x58A,
        /// <summary>[1419] Hot key is not registered.</summary>
        ERROR_HOTKEY_NOT_REGISTERED = 0x58B,
        /// <summary>[1420] The window is not a valid dialog window.</summary>
        ERROR_WINDOW_NOT_DIALOG = 0x58C,
        /// <summary>[1421] Control ID not found.</summary>
        ERROR_CONTROL_ID_NOT_FOUND = 0x58D,
        /// <summary>[1422] Invalid message for a combo box because it does not have an edit control.</summary>
        ERROR_INVALID_COMBOBOX_MESSAGE = 0x58E,
        /// <summary>[1423] The window is not a combo box.</summary>
        ERROR_WINDOW_NOT_COMBOBOX = 0x58F,
        /// <summary>[1424] Height must be less than 256.</summary>
        ERROR_INVALID_EDIT_HEIGHT = 0x590,
        /// <summary>[1425] Invalid device context (DC) handle.</summary>
        ERROR_DC_NOT_FOUND = 0x591,
        /// <summary>[1426] Invalid hook procedure type.</summary>
        ERROR_INVALID_HOOK_FILTER = 0x592,
        /// <summary>[1427] Invalid hook procedure.</summary>
        ERROR_INVALID_FILTER_PROC = 0x593,
        /// <summary>[1428] Cannot set nonlocal hook without a module handle.</summary>
        ERROR_HOOK_NEEDS_HMOD = 0x594,
        /// <summary>[1429] This hook procedure can only be set globally.</summary>
        ERROR_GLOBAL_ONLY_HOOK = 0x595,
        /// <summary>[1430] The journal hook procedure is already installed.</summary>
        ERROR_JOURNAL_HOOK_SET = 0x596,
        /// <summary>[1431] The hook procedure is not installed.</summary>
        ERROR_HOOK_NOT_INSTALLED = 0x597,
        /// <summary>[1432] Invalid message for single-selection list box.</summary>
        ERROR_INVALID_LB_MESSAGE = 0x598,
        /// <summary>[1433] LB_SETCOUNT sent to non-lazy list box.</summary>
        ERROR_SETCOUNT_ON_BAD_LB = 0x599,
        /// <summary>[1434] This list box does not support tab stops.</summary>
        ERROR_LB_WITHOUT_TABSTOPS = 0x59A,
        /// <summary>[1435] Cannot destroy object created by another thread.</summary>
        ERROR_DESTROY_OBJECT_OF_OTHER_THREAD = 0x59B,
        /// <summary>[1436] Child windows cannot have menus.</summary>
        ERROR_CHILD_WINDOW_MENU = 0x59C,
        /// <summary>[1437] The window does not have a system menu.</summary>
        ERROR_NO_SYSTEM_MENU = 0x59D,
        /// <summary>[1438] Invalid message box style.</summary>
        ERROR_INVALID_MSGBOX_STYLE = 0x59E,
        /// <summary>[1439] Invalid system-wide (SPI_*) parameter.</summary>
        ERROR_INVALID_SPI_VALUE = 0x59F,
        /// <summary>[1440] Screen already locked.</summary>
        ERROR_SCREEN_ALREADY_LOCKED = 0x5A0,
        /// <summary>[1441] All handles to windows in a multiple-window position structure must have the same parent.</summary>
        ERROR_HWNDS_HAVE_DIFF_PARENT = 0x5A1,
        /// <summary>[1442] The window is not a child window.</summary>
        ERROR_NOT_CHILD_WINDOW = 0x5A2,
        /// <summary>[1443] Invalid GW_* command.</summary>
        ERROR_INVALID_GW_COMMAND = 0x5A3,
        /// <summary>[1444] Invalid thread identifier.</summary>
        ERROR_INVALID_THREAD_ID = 0x5A4,
        /// <summary>[1445] Cannot process a message from a window that is not a multiple document interface (MDI) window.</summary>
        ERROR_NON_MDICHILD_WINDOW = 0x5A5,
        /// <summary>[1446] Popup menu already active.</summary>
        ERROR_POPUP_ALREADY_ACTIVE = 0x5A6,
        /// <summary>[1447] The window does not have scroll bars.</summary>
        ERROR_NO_SCROLLBARS = 0x5A7,
        /// <summary>[1448] Scroll bar range cannot be greater than MAXLONG.</summary>
        ERROR_INVALID_SCROLLBAR_RANGE = 0x5A8,
        /// <summary>[1449] Cannot show or remove the window in the way specified.</summary>
        ERROR_INVALID_SHOWWIN_COMMAND = 0x5A9,
        /// <summary>[1450] Insufficient system resources exist to complete the requested service.</summary>
        ERROR_NO_SYSTEM_RESOURCES = 0x5AA,
        /// <summary>[1451] Insufficient system resources exist to complete the requested service.</summary>
        ERROR_NONPAGED_SYSTEM_RESOURCES = 0x5AB,
        /// <summary>[1452] Insufficient system resources exist to complete the requested service.</summary>
        ERROR_PAGED_SYSTEM_RESOURCES = 0x5AC,
        /// <summary>[1453] Insufficient quota to complete the requested service.</summary>
        ERROR_WORKING_SET_QUOTA = 0x5AD,
        /// <summary>[1454] Insufficient quota to complete the requested service.</summary>
        ERROR_PAGEFILE_QUOTA = 0x5AE,
        /// <summary>[1455] The paging file is too small for this operation to complete.</summary>
        ERROR_COMMITMENT_LIMIT = 0x5AF,
        /// <summary>[1456] A menu item was not found.</summary>
        ERROR_MENU_ITEM_NOT_FOUND = 0x5B0,
        /// <summary>[1457] Invalid keyboard layout handle.</summary>
        ERROR_INVALID_KEYBOARD_HANDLE = 0x5B1,
        /// <summary>[1458] Hook type not allowed.</summary>
        ERROR_HOOK_TYPE_NOT_ALLOWED = 0x5B2,
        /// <summary>[1459] This operation requires an interactive window station.</summary>
        ERROR_REQUIRES_INTERACTIVE_WINDOWSTATION = 0x5B3,
        /// <summary>[1460] This operation returned because the timeout period expired.</summary>
        ERROR_TIMEOUT = 0x5B4,
        /// <summary>[1461] Invalid monitor handle.</summary>
        ERROR_INVALID_MONITOR_HANDLE = 0x5B5,
        /// <summary>[1462] Incorrect size argument.</summary>
        ERROR_INCORRECT_SIZE = 0x5B6,
        /// <summary>[1463] The symbolic link cannot be followed because its type is disabled.</summary>
        ERROR_SYMLINK_CLASS_DISABLED = 0x5B7,
        /// <summary>[1464] This application does not support the current operation on symbolic links.</summary>
        ERROR_SYMLINK_NOT_SUPPORTED = 0x5B8,
        /// <summary>[1465] Windows was unable to parse the requested XML data.</summary>
        ERROR_XML_PARSE_ERROR = 0x5B9,
        /// <summary>[1466] An error was encountered while processing an XML digital signature.</summary>
        ERROR_XMLDSIG_ERROR = 0x5BA,
        /// <summary>[1467] This application must be restarted.</summary>
        ERROR_RESTART_APPLICATION = 0x5BB,
        /// <summary>[1468] The caller made the connection request in the wrong routing compartment.</summary>
        ERROR_WRONG_COMPARTMENT = 0x5BC,
        /// <summary>[1469] There was an AuthIP failure when attempting to connect to the remote host.</summary>
        ERROR_AUTHIP_FAILURE = 0x5BD,
        /// <summary>[1470] Insufficient NVRAM resources exist to complete the requested service. A reboot might be required.</summary>
        ERROR_NO_NVRAM_RESOURCES = 0x5BE,
        /// <summary>[1471] Unable to finish the requested operation because the specified process is not a GUI process.</summary>
        ERROR_NOT_GUI_PROCESS = 0x5BF,
        /// <summary>[1500] The event log file is corrupted.</summary>
        ERROR_EVENTLOG_FILE_CORRUPT = 0x5DC,
        /// <summary>[1501] No event log file could be opened, so the event logging service did not start.</summary>
        ERROR_EVENTLOG_CANT_START = 0x5DD,
        /// <summary>[1502] The event log file is full.</summary>
        ERROR_LOG_FILE_FULL = 0x5DE,
        /// <summary>[1503] The event log file has changed between read operations.</summary>
        ERROR_EVENTLOG_FILE_CHANGED = 0x5DF,
        /// <summary>[1550] The specified task name is invalid.</summary>
        ERROR_INVALID_TASK_NAME = 0x60E,
        /// <summary>[1551] The specified task index is invalid.</summary>
        ERROR_INVALID_TASK_INDEX = 0x60F,
        /// <summary>[1552] The specified thread is already joining a task.</summary>
        ERROR_THREAD_ALREADY_IN_TASK = 0x610,
        /// <summary>[1601] The Windows Installer Service could not be accessed. This can occur if the Windows Installer is not correctly installed. Contact your support personnel for assistance.</summary>
        ERROR_INSTALL_SERVICE_FAILURE = 0x641,
        /// <summary>[1602] User cancelled installation.</summary>
        ERROR_INSTALL_USEREXIT = 0x642,
        /// <summary>[1603] Fatal error during installation.</summary>
        ERROR_INSTALL_FAILURE = 0x643,
        /// <summary>[1604] Installation suspended, incomplete.</summary>
        ERROR_INSTALL_SUSPEND = 0x644,
        /// <summary>[1605] This action is only valid for products that are currently installed.</summary>
        ERROR_UNKNOWN_PRODUCT = 0x645,
        /// <summary>[1606] Feature ID not registered.</summary>
        ERROR_UNKNOWN_FEATURE = 0x646,
        /// <summary>[1607] Component ID not registered.</summary>
        ERROR_UNKNOWN_COMPONENT = 0x647,
        /// <summary>[1608] Unknown property.</summary>
        ERROR_UNKNOWN_PROPERTY = 0x648,
        /// <summary>[1609] Handle is in an invalid state.</summary>
        ERROR_INVALID_HANDLE_STATE = 0x649,
        /// <summary>[1610] The configuration data for this product is corrupt. Contact your support personnel.</summary>
        ERROR_BAD_CONFIGURATION = 0x64A,
        /// <summary>[1611] Component qualifier not present.</summary>
        ERROR_INDEX_ABSENT = 0x64B,
        /// <summary>[1612] The installation source for this product is not available. Verify that the source exists and that you can access it.</summary>
        ERROR_INSTALL_SOURCE_ABSENT = 0x64C,
        /// <summary>[1613] This installation package cannot be installed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.</summary>
        ERROR_INSTALL_PACKAGE_VERSION = 0x64D,
        /// <summary>[1614] Product is uninstalled.</summary>
        ERROR_PRODUCT_UNINSTALLED = 0x64E,
        /// <summary>[1615] SQL query syntax invalid or unsupported.</summary>
        ERROR_BAD_QUERY_SYNTAX = 0x64F,
        /// <summary>[1616] Record field does not exist.</summary>
        ERROR_INVALID_FIELD = 0x650,
        /// <summary>[1617] The device has been removed.</summary>
        ERROR_DEVICE_REMOVED = 0x651,
        /// <summary>[1618] Another installation is already in progress. Complete that installation before proceeding with this install.</summary>
        ERROR_INSTALL_ALREADY_RUNNING = 0x652,
        /// <summary>[1619] This installation package could not be opened. Verify that the package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer package.</summary>
        ERROR_INSTALL_PACKAGE_OPEN_FAILED = 0x653,
        /// <summary>[1620] This installation package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer package.</summary>
        ERROR_INSTALL_PACKAGE_INVALID = 0x654,
        /// <summary>[1621] There was an error starting the Windows Installer service user interface. Contact your support personnel.</summary>
        ERROR_INSTALL_UI_FAILURE = 0x655,
        /// <summary>[1622] Error opening installation log file. Verify that the specified log file location exists and that you can write to it.</summary>
        ERROR_INSTALL_LOG_FAILURE = 0x656,
        /// <summary>[1623] The language of this installation package is not supported by your system.</summary>
        ERROR_INSTALL_LANGUAGE_UNSUPPORTED = 0x657,
        /// <summary>[1624] Error applying transforms. Verify that the specified transform paths are valid.</summary>
        ERROR_INSTALL_TRANSFORM_FAILURE = 0x658,
        /// <summary>[1625] This installation is forbidden by system policy. Contact your system administrator.</summary>
        ERROR_INSTALL_PACKAGE_REJECTED = 0x659,
        /// <summary>[1626] Function could not be executed.</summary>
        ERROR_FUNCTION_NOT_CALLED = 0x65A,
        /// <summary>[1627] Function failed during execution.</summary>
        ERROR_FUNCTION_FAILED = 0x65B,
        /// <summary>[1628] Invalid or unknown table specified.</summary>
        ERROR_INVALID_TABLE = 0x65C,
        /// <summary>[1629] Data supplied is of wrong type.</summary>
        ERROR_DATATYPE_MISMATCH = 0x65D,
        /// <summary>[1630] Data of this type is not supported.</summary>
        ERROR_UNSUPPORTED_TYPE = 0x65E,
        /// <summary>[1631] The Windows Installer service failed to start. Contact your support personnel.</summary>
        ERROR_CREATE_FAILED = 0x65F,
        /// <summary>[1632] The Temp folder is on a drive that is full or is inaccessible. Free up space on the drive or verify that you have write permission on the Temp folder.</summary>
        ERROR_INSTALL_TEMP_UNWRITABLE = 0x660,
        /// <summary>[1633] This installation package is not supported by this processor type. Contact your product vendor.</summary>
        ERROR_INSTALL_PLATFORM_UNSUPPORTED = 0x661,
        /// <summary>[1634] Component not used on this computer.</summary>
        ERROR_INSTALL_NOTUSED = 0x662,
        /// <summary>[1635] This update package could not be opened. Verify that the update package exists and that you can access it, or contact the application vendor to verify that this is a valid Windows Installer update package.</summary>
        ERROR_PATCH_PACKAGE_OPEN_FAILED = 0x663,
        /// <summary>[1636] This update package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer update package.</summary>
        ERROR_PATCH_PACKAGE_INVALID = 0x664,
        /// <summary>[1637] This update package cannot be processed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.</summary>
        ERROR_PATCH_PACKAGE_UNSUPPORTED = 0x665,
        /// <summary>[1638] Another version of this product is already installed. Installation of this version cannot continue. To configure or remove the existing version of this product, use Add/Remove Programs on the Control Panel.</summary>
        ERROR_PRODUCT_VERSION = 0x666,
        /// <summary>[1639] Invalid command line argument. Consult the Windows Installer SDK for detailed command line help.</summary>
        ERROR_INVALID_COMMAND_LINE = 0x667,
        /// <summary>[1640] Only administrators have permission to add, remove, or configure server software during a Terminal services remote session. If you want to install or configure software on the server, contact your network administrator.</summary>
        ERROR_INSTALL_REMOTE_DISALLOWED = 0x668,
        /// <summary>[1641] The requested operation completed successfully. The system will be restarted so the changes can take effect.</summary>
        ERROR_SUCCESS_REBOOT_INITIATED = 0x669,
        /// <summary>[1642] The upgrade cannot be installed by the Windows Installer service because the program to be upgraded may be missing, or the upgrade may update a different version of the program. Verify that the program to be upgraded exists on your computer and that you have the correct upgrade.</summary>
        ERROR_PATCH_TARGET_NOT_FOUND = 0x66A,
        /// <summary>[1643] The update package is not permitted by software restriction policy.</summary>
        ERROR_PATCH_PACKAGE_REJECTED = 0x66B,
        /// <summary>[1644] One or more customizations are not permitted by software restriction policy.</summary>
        ERROR_INSTALL_TRANSFORM_REJECTED = 0x66C,
        /// <summary>[1645] The Windows Installer does not permit installation from a Remote Desktop Connection.</summary>
        ERROR_INSTALL_REMOTE_PROHIBITED = 0x66D,
        /// <summary>[1646] Uninstallation of the update package is not supported.</summary>
        ERROR_PATCH_REMOVAL_UNSUPPORTED = 0x66E,
        /// <summary>[1647] The update is not applied to this product.</summary>
        ERROR_UNKNOWN_PATCH = 0x66F,
        /// <summary>[1648] No valid sequence could be found for the set of updates.</summary>
        ERROR_PATCH_NO_SEQUENCE = 0x670,
        /// <summary>[1649] Update removal was disallowed by policy.</summary>
        ERROR_PATCH_REMOVAL_DISALLOWED = 0x671,
        /// <summary>[1650] The XML update data is invalid.</summary>
        ERROR_INVALID_PATCH_XML = 0x672,
        /// <summary>[1651] Windows Installer does not permit updating of managed advertised products. At least one feature of the product must be installed before applying the update.</summary>
        ERROR_PATCH_MANAGED_ADVERTISED_PRODUCT = 0x673,
        /// <summary>[1652] The Windows Installer service is not accessible in Safe Mode. Please try again when your computer is not in Safe Mode or you can use System Restore to return your machine to a previous good state.</summary>
        ERROR_INSTALL_SERVICE_SAFEBOOT = 0x674,
        /// <summary>[1653] A fail fast exception occurred. Exception handlers will not be invoked and the process will be terminated immediately.</summary>
        ERROR_FAIL_FAST_EXCEPTION = 0x675,
        /// <summary>[1654] The app that you are trying to run is not supported on this version of Windows.</summary>
        ERROR_INSTALL_REJECTED = 0x676,
        /// <summary>[1700] The string binding is invalid.</summary>
        RPC_S_INVALID_STRING_BINDING = 0x6A4,
        /// <summary>[1701] The binding handle is not the correct type.</summary>
        RPC_S_WRONG_KIND_OF_BINDING = 0x6A5,
        /// <summary>[1702] The binding handle is invalid.</summary>
        RPC_S_INVALID_BINDING = 0x6A6,
        /// <summary>[1703] The RPC protocol sequence is not supported.</summary>
        RPC_S_PROTSEQ_NOT_SUPPORTED = 0x6A7,
        /// <summary>[1704] The RPC protocol sequence is invalid.</summary>
        RPC_S_INVALID_RPC_PROTSEQ = 0x6A8,
        /// <summary>[1705] The string universal unique identifier (UUID) is invalid.</summary>
        RPC_S_INVALID_STRING_UUID = 0x6A9,
        /// <summary>[1706] The endpoint format is invalid.</summary>
        RPC_S_INVALID_ENDPOINT_FORMAT = 0x6AA,
        /// <summary>[1707] The network address is invalid.</summary>
        RPC_S_INVALID_NET_ADDR = 0x6AB,
        /// <summary>[1708] No endpoint was found.</summary>
        RPC_S_NO_ENDPOINT_FOUND = 0x6AC,
        /// <summary>[1709] The timeout value is invalid.</summary>
        RPC_S_INVALID_TIMEOUT = 0x6AD,
        /// <summary>[1710] The object universal unique identifier (UUID) was not found.</summary>
        RPC_S_OBJECT_NOT_FOUND = 0x6AE,
        /// <summary>[1711] The object universal unique identifier (UUID) has already been registered.</summary>
        RPC_S_ALREADY_REGISTERED = 0x6AF,
        /// <summary>[1712] The type universal unique identifier (UUID) has already been registered.</summary>
        RPC_S_TYPE_ALREADY_REGISTERED = 0x6B0,
        /// <summary>[1713] The RPC server is already listening.</summary>
        RPC_S_ALREADY_LISTENING = 0x6B1,
        /// <summary>[1714] No protocol sequences have been registered.</summary>
        RPC_S_NO_PROTSEQS_REGISTERED = 0x6B2,
        /// <summary>[1715] The RPC server is not listening.</summary>
        RPC_S_NOT_LISTENING = 0x6B3,
        /// <summary>[1716] The manager type is unknown.</summary>
        RPC_S_UNKNOWN_MGR_TYPE = 0x6B4,
        /// <summary>[1717] The interface is unknown.</summary>
        RPC_S_UNKNOWN_IF = 0x6B5,
        /// <summary>[1718] There are no bindings.</summary>
        RPC_S_NO_BINDINGS = 0x6B6,
        /// <summary>[1719] There are no protocol sequences.</summary>
        RPC_S_NO_PROTSEQS = 0x6B7,
        /// <summary>[1720] The endpoint cannot be created.</summary>
        RPC_S_CANT_CREATE_ENDPOINT = 0x6B8,
        /// <summary>[1721] Not enough resources are available to complete this operation.</summary>
        RPC_S_OUT_OF_RESOURCES = 0x6B9,
        /// <summary>[1722] The RPC server is unavailable.</summary>
        RPC_S_SERVER_UNAVAILABLE = 0x6BA,
        /// <summary>[1723] The RPC server is too busy to complete this operation.</summary>
        RPC_S_SERVER_TOO_BUSY = 0x6BB,
        /// <summary>[1724] The network options are invalid.</summary>
        RPC_S_INVALID_NETWORK_OPTIONS = 0x6BC,
        /// <summary>[1725] There are no remote procedure calls active on this thread.</summary>
        RPC_S_NO_CALL_ACTIVE = 0x6BD,
        /// <summary>[1726] The remote procedure call failed.</summary>
        RPC_S_CALL_FAILED = 0x6BE,
        /// <summary>[1727] The remote procedure call failed and did not execute.</summary>
        RPC_S_CALL_FAILED_DNE = 0x6BF,
        /// <summary>[1728] A remote procedure call (RPC) protocol error occurred.</summary>
        RPC_S_PROTOCOL_ERROR = 0x6C0,
        /// <summary>[1729] Access to the HTTP proxy is denied.</summary>
        RPC_S_PROXY_ACCESS_DENIED = 0x6C1,
        /// <summary>[1730] The transfer syntax is not supported by the RPC server.</summary>
        RPC_S_UNSUPPORTED_TRANS_SYN = 0x6C2,
        /// <summary>[1732] The universal unique identifier (UUID) type is not supported.</summary>
        RPC_S_UNSUPPORTED_TYPE = 0x6C4,
        /// <summary>[1733] The tag is invalid.</summary>
        RPC_S_INVALID_TAG = 0x6C5,
        /// <summary>[1734] The array bounds are invalid.</summary>
        RPC_S_INVALID_BOUND = 0x6C6,
        /// <summary>[1735] The binding does not contain an entry name.</summary>
        RPC_S_NO_ENTRY_NAME = 0x6C7,
        /// <summary>[1736] The name syntax is invalid.</summary>
        RPC_S_INVALID_NAME_SYNTAX = 0x6C8,
        /// <summary>[1737] The name syntax is not supported.</summary>
        RPC_S_UNSUPPORTED_NAME_SYNTAX = 0x6C9,
        /// <summary>[1739] No network address is available to use to construct a universal unique identifier (UUID).</summary>
        RPC_S_UUID_NO_ADDRESS = 0x6CB,
        /// <summary>[1740] The endpoint is a duplicate.</summary>
        RPC_S_DUPLICATE_ENDPOINT = 0x6CC,
        /// <summary>[1741] The authentication type is unknown.</summary>
        RPC_S_UNKNOWN_AUTHN_TYPE = 0x6CD,
        /// <summary>[1742] The maximum number of calls is too small.</summary>
        RPC_S_MAX_CALLS_TOO_SMALL = 0x6CE,
        /// <summary>[1743] The string is too long.</summary>
        RPC_S_STRING_TOO_LONG = 0x6CF,
        /// <summary>[1744] The RPC protocol sequence was not found.</summary>
        RPC_S_PROTSEQ_NOT_FOUND = 0x6D0,
        /// <summary>[1745] The procedure number is out of range.</summary>
        RPC_S_PROCNUM_OUT_OF_RANGE = 0x6D1,
        /// <summary>[1746] The binding does not contain any authentication information.</summary>
        RPC_S_BINDING_HAS_NO_AUTH = 0x6D2,
        /// <summary>[1747] The authentication service is unknown.</summary>
        RPC_S_UNKNOWN_AUTHN_SERVICE = 0x6D3,
        /// <summary>[1748] The authentication level is unknown.</summary>
        RPC_S_UNKNOWN_AUTHN_LEVEL = 0x6D4,
        /// <summary>[1749] The security context is invalid.</summary>
        RPC_S_INVALID_AUTH_IDENTITY = 0x6D5,
        /// <summary>[1750] The authorization service is unknown.</summary>
        RPC_S_UNKNOWN_AUTHZ_SERVICE = 0x6D6,
        /// <summary>[1751] The entry is invalid.</summary>
        EPT_S_INVALID_ENTRY = 0x6D7,
        /// <summary>[1752] The server endpoint cannot perform the operation.</summary>
        EPT_S_CANT_PERFORM_OP = 0x6D8,
        /// <summary>[1753] There are no more endpoints available from the endpoint mapper.</summary>
        EPT_S_NOT_REGISTERED = 0x6D9,
        /// <summary>[1754] No interfaces have been exported.</summary>
        RPC_S_NOTHING_TO_EXPORT = 0x6DA,
        /// <summary>[1755] The entry name is incomplete.</summary>
        RPC_S_INCOMPLETE_NAME = 0x6DB,
        /// <summary>[1756] The version option is invalid.</summary>
        RPC_S_INVALID_VERS_OPTION = 0x6DC,
        /// <summary>[1757] There are no more members.</summary>
        RPC_S_NO_MORE_MEMBERS = 0x6DD,
        /// <summary>[1758] There is nothing to unexport.</summary>
        RPC_S_NOT_ALL_OBJS_UNEXPORTED = 0x6DE,
        /// <summary>[1759] The interface was not found.</summary>
        RPC_S_INTERFACE_NOT_FOUND = 0x6DF,
        /// <summary>[1760] The entry already exists.</summary>
        RPC_S_ENTRY_ALREADY_EXISTS = 0x6E0,
        /// <summary>[1761] The entry is not found.</summary>
        RPC_S_ENTRY_NOT_FOUND = 0x6E1,
        /// <summary>[1762] The name service is unavailable.</summary>
        RPC_S_NAME_SERVICE_UNAVAILABLE = 0x6E2,
        /// <summary>[1763] The network address family is invalid.</summary>
        RPC_S_INVALID_NAF_ID = 0x6E3,
        /// <summary>[1764] The requested operation is not supported.</summary>
        RPC_S_CANNOT_SUPPORT = 0x6E4,
        /// <summary>[1765] No security context is available to allow impersonation.</summary>
        RPC_S_NO_CONTEXT_AVAILABLE = 0x6E5,
        /// <summary>[1766] An internal error occurred in a remote procedure call (RPC).</summary>
        RPC_S_INTERNAL_ERROR = 0x6E6,
        /// <summary>[1767] The RPC server attempted an integer division by zero.</summary>
        RPC_S_ZERO_DIVIDE = 0x6E7,
        /// <summary>[1768] An addressing error occurred in the RPC server.</summary>
        RPC_S_ADDRESS_ERROR = 0x6E8,
        /// <summary>[1769] A floating-point operation at the RPC server caused a division by zero.</summary>
        RPC_S_FP_DIV_ZERO = 0x6E9,
        /// <summary>[1770] A floating-point underflow occurred at the RPC server.</summary>
        RPC_S_FP_UNDERFLOW = 0x6EA,
        /// <summary>[1771] A floating-point overflow occurred at the RPC server.</summary>
        RPC_S_FP_OVERFLOW = 0x6EB,
        /// <summary>[1772] The list of RPC servers available for the binding of auto handles has been exhausted.</summary>
        RPC_X_NO_MORE_ENTRIES = 0x6EC,
        /// <summary>[1773] Unable to open the character translation table file.</summary>
        RPC_X_SS_CHAR_TRANS_OPEN_FAIL = 0x6ED,
        /// <summary>[1774] The file containing the character translation table has fewer than 512 bytes.</summary>
        RPC_X_SS_CHAR_TRANS_SHORT_FILE = 0x6EE,
        /// <summary>[1775] A null context handle was passed from the client to the host during a remote procedure call.</summary>
        RPC_X_SS_IN_NULL_CONTEXT = 0x6EF,
        /// <summary>[1777] The context handle changed during a remote procedure call.</summary>
        RPC_X_SS_CONTEXT_DAMAGED = 0x6F1,
        /// <summary>[1778] The binding handles passed to a remote procedure call do not match.</summary>
        RPC_X_SS_HANDLES_MISMATCH = 0x6F2,
        /// <summary>[1779] The stub is unable to get the remote procedure call handle.</summary>
        RPC_X_SS_CANNOT_GET_CALL_HANDLE = 0x6F3,
        /// <summary>[1780] A null reference pointer was passed to the stub.</summary>
        RPC_X_NULL_REF_POINTER = 0x6F4,
        /// <summary>[1781] The enumeration value is out of range.</summary>
        RPC_X_ENUM_VALUE_OUT_OF_RANGE = 0x6F5,
        /// <summary>[1782] The byte count is too small.</summary>
        RPC_X_BYTE_COUNT_TOO_SMALL = 0x6F6,
        /// <summary>[1783] The stub received bad data.</summary>
        RPC_X_BAD_STUB_DATA = 0x6F7,
        /// <summary>[1784] The supplied user buffer is not valid for the requested operation.</summary>
        ERROR_INVALID_USER_BUFFER = 0x6F8,
        /// <summary>[1785] The disk media is not recognized. It may not be formatted.</summary>
        ERROR_UNRECOGNIZED_MEDIA = 0x6F9,
        /// <summary>[1786] The workstation does not have a trust secret.</summary>
        ERROR_NO_TRUST_LSA_SECRET = 0x6FA,
        /// <summary>[1787] The security database on the server does not have a computer account for this workstation trust relationship.</summary>
        ERROR_NO_TRUST_SAM_ACCOUNT = 0x6FB,
        /// <summary>[1788] The trust relationship between the primary domain and the trusted domain failed.</summary>
        ERROR_TRUSTED_DOMAIN_FAILURE = 0x6FC,
        /// <summary>[1789] The trust relationship between this workstation and the primary domain failed.</summary>
        ERROR_TRUSTED_RELATIONSHIP_FAILURE = 0x6FD,
        /// <summary>[1790] The network logon failed.</summary>
        ERROR_TRUST_FAILURE = 0x6FE,
        /// <summary>[1791] A remote procedure call is already in progress for this thread.</summary>
        RPC_S_CALL_IN_PROGRESS = 0x6FF,
        /// <summary>[1792] An attempt was made to logon, but the network logon service was not started.</summary>
        ERROR_NETLOGON_NOT_STARTED = 0x700,
        /// <summary>[1793] The user's account has expired.</summary>
        ERROR_ACCOUNT_EXPIRED = 0x701,
        /// <summary>[1794] The redirector is in use and cannot be unloaded.</summary>
        ERROR_REDIRECTOR_HAS_OPEN_HANDLES = 0x702,
        /// <summary>[1795] The specified printer driver is already installed.</summary>
        ERROR_PRINTER_DRIVER_ALREADY_INSTALLED = 0x703,
        /// <summary>[1796] The specified port is unknown.</summary>
        ERROR_UNKNOWN_PORT = 0x704,
        /// <summary>[1797] The printer driver is unknown.</summary>
        ERROR_UNKNOWN_PRINTER_DRIVER = 0x705,
        /// <summary>[1798] The print processor is unknown.</summary>
        ERROR_UNKNOWN_PRINTPROCESSOR = 0x706,
        /// <summary>[1799] The specified separator file is invalid.</summary>
        ERROR_INVALID_SEPARATOR_FILE = 0x707,
        /// <summary>[1800] The specified priority is invalid.</summary>
        ERROR_INVALID_PRIORITY = 0x708,
        /// <summary>[1801] The printer name is invalid.</summary>
        ERROR_INVALID_PRINTER_NAME = 0x709,
        /// <summary>[1802] The printer already exists.</summary>
        ERROR_PRINTER_ALREADY_EXISTS = 0x70A,
        /// <summary>[1803] The printer command is invalid.</summary>
        ERROR_INVALID_PRINTER_COMMAND = 0x70B,
        /// <summary>[1804] The specified datatype is invalid.</summary>
        ERROR_INVALID_DATATYPE = 0x70C,
        /// <summary>[1805] The environment specified is invalid.</summary>
        ERROR_INVALID_ENVIRONMENT = 0x70D,
        /// <summary>[1806] There are no more bindings.</summary>
        RPC_S_NO_MORE_BINDINGS = 0x70E,
        /// <summary>[1807] The account used is an interdomain trust account. Use your global user account or local user account to access this server.</summary>
        ERROR_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT = 0x70F,
        /// <summary>[1808] The account used is a computer account. Use your global user account or local user account to access this server.</summary>
        ERROR_NOLOGON_WORKSTATION_TRUST_ACCOUNT = 0x710,
        /// <summary>[1809] The account used is a server trust account. Use your global user account or local user account to access this server.</summary>
        ERROR_NOLOGON_SERVER_TRUST_ACCOUNT = 0x711,
        /// <summary>[1810] The name or security ID (SID) of the domain specified is inconsistent with the trust information for that domain.</summary>
        ERROR_DOMAIN_TRUST_INCONSISTENT = 0x712,
        /// <summary>[1811] The server is in use and cannot be unloaded.</summary>
        ERROR_SERVER_HAS_OPEN_HANDLES = 0x713,
        /// <summary>[1812] The specified image file did not contain a resource section.</summary>
        ERROR_RESOURCE_DATA_NOT_FOUND = 0x714,
        /// <summary>[1813] The specified resource type cannot be found in the image file.</summary>
        ERROR_RESOURCE_TYPE_NOT_FOUND = 0x715,
        /// <summary>[1814] The specified resource name cannot be found in the image file.</summary>
        ERROR_RESOURCE_NAME_NOT_FOUND = 0x716,
        /// <summary>[1815] The specified resource language ID cannot be found in the image file.</summary>
        ERROR_RESOURCE_LANG_NOT_FOUND = 0x717,
        /// <summary>[1816] Not enough quota is available to process this command.</summary>
        ERROR_NOT_ENOUGH_QUOTA = 0x718,
        /// <summary>[1817] No interfaces have been registered.</summary>
        RPC_S_NO_INTERFACES = 0x719,
        /// <summary>[1818] The remote procedure call was cancelled.</summary>
        RPC_S_CALL_CANCELLED = 0x71A,
        /// <summary>[1819] The binding handle does not contain all required information.</summary>
        RPC_S_BINDING_INCOMPLETE = 0x71B,
        /// <summary>[1820] A communications failure occurred during a remote procedure call.</summary>
        RPC_S_COMM_FAILURE = 0x71C,
        /// <summary>[1821] The requested authentication level is not supported.</summary>
        RPC_S_UNSUPPORTED_AUTHN_LEVEL = 0x71D,
        /// <summary>[1822] No principal name registered.</summary>
        RPC_S_NO_PRINC_NAME = 0x71E,
        /// <summary>[1823] The error specified is not a valid Windows RPC error code.</summary>
        RPC_S_NOT_RPC_ERROR = 0x71F,
        /// <summary>[1824] A UUID that is valid only on this computer has been allocated.</summary>
        RPC_S_UUID_LOCAL_ONLY = 0x720,
        /// <summary>[1825] A security package specific error occurred.</summary>
        RPC_S_SEC_PKG_ERROR = 0x721,
        /// <summary>[1826] Thread is not canceled.</summary>
        RPC_S_NOT_CANCELLED = 0x722,
        /// <summary>[1827] Invalid operation on the encoding/decoding handle.</summary>
        RPC_X_INVALID_ES_ACTION = 0x723,
        /// <summary>[1828] Incompatible version of the serializing package.</summary>
        RPC_X_WRONG_ES_VERSION = 0x724,
        /// <summary>[1829] Incompatible version of the RPC stub.</summary>
        RPC_X_WRONG_STUB_VERSION = 0x725,
        /// <summary>[1830] The RPC pipe object is invalid or corrupted.</summary>
        RPC_X_INVALID_PIPE_OBJECT = 0x726,
        /// <summary>[1831] An invalid operation was attempted on an RPC pipe object.</summary>
        RPC_X_WRONG_PIPE_ORDER = 0x727,
        /// <summary>[1832] Unsupported RPC pipe version.</summary>
        RPC_X_WRONG_PIPE_VERSION = 0x728,
        /// <summary>[1833] HTTP proxy server rejected the connection because the cookie authentication failed.</summary>
        RPC_S_COOKIE_AUTH_FAILED = 0x729,
        /// <summary>[1898] The group member was not found.</summary>
        RPC_S_GROUP_MEMBER_NOT_FOUND = 0x76A,
        /// <summary>[1899] The endpoint mapper database entry could not be created.</summary>
        EPT_S_CANT_CREATE = 0x76B,
        /// <summary>[1900] The object universal unique identifier (UUID) is the nil UUID.</summary>
        RPC_S_INVALID_OBJECT = 0x76C,
        /// <summary>[1901] The specified time is invalid.</summary>
        ERROR_INVALID_TIME = 0x76D,
        /// <summary>[1902] The specified form name is invalid.</summary>
        ERROR_INVALID_FORM_NAME = 0x76E,
        /// <summary>[1903] The specified form size is invalid.</summary>
        ERROR_INVALID_FORM_SIZE = 0x76F,
        /// <summary>[1904] The specified printer handle is already being waited on.</summary>
        ERROR_ALREADY_WAITING = 0x770,
        /// <summary>[1905] The specified printer has been deleted.</summary>
        ERROR_PRINTER_DELETED = 0x771,
        /// <summary>[1906] The state of the printer is invalid.</summary>
        ERROR_INVALID_PRINTER_STATE = 0x772,
        /// <summary>[1907] The user's password must be changed before signing in.</summary>
        ERROR_PASSWORD_MUST_CHANGE = 0x773,
        /// <summary>[1908] Could not find the domain controller for this domain.</summary>
        ERROR_DOMAIN_CONTROLLER_NOT_FOUND = 0x774,
        /// <summary>[1909] The referenced account is currently locked out and may not be logged on to.</summary>
        ERROR_ACCOUNT_LOCKED_OUT = 0x775,
        /// <summary>[1910] The object exporter specified was not found.</summary>
        OR_INVALID_OXID = 0x776,
        /// <summary>[1911] The object specified was not found.</summary>
        OR_INVALID_OID = 0x777,
        /// <summary>[1912] The object resolver set specified was not found.</summary>
        OR_INVALID_SET = 0x778,
        /// <summary>[1913] Some data remains to be sent in the request buffer.</summary>
        RPC_S_SEND_INCOMPLETE = 0x779,
        /// <summary>[1914] Invalid asynchronous remote procedure call handle.</summary>
        RPC_S_INVALID_ASYNC_HANDLE = 0x77A,
        /// <summary>[1915] Invalid asynchronous RPC call handle for this operation.</summary>
        RPC_S_INVALID_ASYNC_CALL = 0x77B,
        /// <summary>[1916] The RPC pipe object has already been closed.</summary>
        RPC_X_PIPE_CLOSED = 0x77C,
        /// <summary>[1917] The RPC call completed before all pipes were processed.</summary>
        RPC_X_PIPE_DISCIPLINE_ERROR = 0x77D,
        /// <summary>[1918] No more data is available from the RPC pipe.</summary>
        RPC_X_PIPE_EMPTY = 0x77E,
        /// <summary>[1919] No site name is available for this machine.</summary>
        ERROR_NO_SITENAME = 0x77F,
        /// <summary>[1920] The file cannot be accessed by the system.</summary>
        ERROR_CANT_ACCESS_FILE = 0x780,
        /// <summary>[1921] The name of the file cannot be resolved by the system.</summary>
        ERROR_CANT_RESOLVE_FILENAME = 0x781,
        /// <summary>[1922] The entry is not of the expected type.</summary>
        RPC_S_ENTRY_TYPE_MISMATCH = 0x782,
        /// <summary>[1923] Not all object UUIDs could be exported to the specified entry.</summary>
        RPC_S_NOT_ALL_OBJS_EXPORTED = 0x783,
        /// <summary>[1924] Interface could not be exported to the specified entry.</summary>
        RPC_S_INTERFACE_NOT_EXPORTED = 0x784,
        /// <summary>[1925] The specified profile entry could not be added.</summary>
        RPC_S_PROFILE_NOT_ADDED = 0x785,
        /// <summary>[1926] The specified profile element could not be added.</summary>
        RPC_S_PRF_ELT_NOT_ADDED = 0x786,
        /// <summary>[1927] The specified profile element could not be removed.</summary>
        RPC_S_PRF_ELT_NOT_REMOVED = 0x787,
        /// <summary>[1928] The group element could not be added.</summary>
        RPC_S_GRP_ELT_NOT_ADDED = 0x788,
        /// <summary>[1929] The group element could not be removed.</summary>
        RPC_S_GRP_ELT_NOT_REMOVED = 0x789,
        /// <summary>[1930] The printer driver is not compatible with a policy enabled on your computer that blocks NT 4.0 drivers.</summary>
        ERROR_KM_DRIVER_BLOCKED = 0x78A,
        /// <summary>[1931] The context has expired and can no longer be used.</summary>
        ERROR_CONTEXT_EXPIRED = 0x78B,
        /// <summary>[1932] The current user's delegated trust creation quota has been exceeded.</summary>
        ERROR_PER_USER_TRUST_QUOTA_EXCEEDED = 0x78C,
        /// <summary>[1933] The total delegated trust creation quota has been exceeded.</summary>
        ERROR_ALL_USER_TRUST_QUOTA_EXCEEDED = 0x78D,
        /// <summary>[1934] The current user's delegated trust deletion quota has been exceeded.</summary>
        ERROR_USER_DELETE_TRUST_QUOTA_EXCEEDED = 0x78E,
        /// <summary>[1935] The computer you are signing into is protected by an authentication firewall. The specified account is not allowed to authenticate to the computer.</summary>
        ERROR_AUTHENTICATION_FIREWALL_FAILED = 0x78F,
        /// <summary>[1936] Remote connections to the Print Spooler are blocked by a policy set on your machine.</summary>
        ERROR_REMOTE_PRINT_CONNECTIONS_BLOCKED = 0x790,
        /// <summary>[1937] Authentication failed because NTLM authentication has been disabled.</summary>
        ERROR_NTLM_BLOCKED = 0x791,
        /// <summary>[1938] Logon Failure: EAS policy requires that the user change their password before this operation can be performed.</summary>
        ERROR_PASSWORD_CHANGE_REQUIRED = 0x792,
        /// <summary>[2000] The pixel format is invalid.</summary>
        ERROR_INVALID_PIXEL_FORMAT = 0x7D0,
        /// <summary>[2001] The specified driver is invalid.</summary>
        ERROR_BAD_DRIVER = 0x7D1,
        /// <summary>[2002] The window style or class attribute is invalid for this operation.</summary>
        ERROR_INVALID_WINDOW_STYLE = 0x7D2,
        /// <summary>[2003] The requested metafile operation is not supported.</summary>
        ERROR_METAFILE_NOT_SUPPORTED = 0x7D3,
        /// <summary>[2004] The requested transformation operation is not supported.</summary>
        ERROR_TRANSFORM_NOT_SUPPORTED = 0x7D4,
        /// <summary>[2005] The requested clipping operation is not supported.</summary>
        ERROR_CLIPPING_NOT_SUPPORTED = 0x7D5,
        /// <summary>[2010] The specified color management module is invalid.</summary>
        ERROR_INVALID_CMM = 0x7DA,
        /// <summary>[2011] The specified color profile is invalid.</summary>
        ERROR_INVALID_PROFILE = 0x7DB,
        /// <summary>[2012] The specified tag was not found.</summary>
        ERROR_TAG_NOT_FOUND = 0x7DC,
        /// <summary>[2013] A required tag is not present.</summary>
        ERROR_TAG_NOT_PRESENT = 0x7DD,
        /// <summary>[2014] The specified tag is already present.</summary>
        ERROR_DUPLICATE_TAG = 0x7DE,
        /// <summary>[2015] The specified color profile is not associated with the specified device.</summary>
        ERROR_PROFILE_NOT_ASSOCIATED_WITH_DEVICE = 0x7DF,
        /// <summary>[2016] The specified color profile was not found.</summary>
        ERROR_PROFILE_NOT_FOUND = 0x7E0,
        /// <summary>[2017] The specified color space is invalid.</summary>
        ERROR_INVALID_COLORSPACE = 0x7E1,
        /// <summary>[2018] Image Color Management is not enabled.</summary>
        ERROR_ICM_NOT_ENABLED = 0x7E2,
        /// <summary>[2019] There was an error while deleting the color transform.</summary>
        ERROR_DELETING_ICM_XFORM = 0x7E3,
        /// <summary>[2020] The specified color transform is invalid.</summary>
        ERROR_INVALID_TRANSFORM = 0x7E4,
        /// <summary>[2021] The specified transform does not match the bitmap's color space.</summary>
        ERROR_COLORSPACE_MISMATCH = 0x7E5,
        /// <summary>[2022] The specified named color index is not present in the profile.</summary>
        ERROR_INVALID_COLORINDEX = 0x7E6,
        /// <summary>[2023] The specified profile is intended for a device of a different type than the specified device.</summary>
        ERROR_PROFILE_DOES_NOT_MATCH_DEVICE = 0x7E7,
        /// <summary>[2108] The network connection was made successfully, but the user had to be prompted for a password other than the one originally specified.</summary>
        ERROR_CONNECTED_OTHER_PASSWORD = 0x83C,
        /// <summary>[2109] The network connection was made successfully using default credentials.</summary>
        ERROR_CONNECTED_OTHER_PASSWORD_DEFAULT = 0x83D,
        /// <summary>[2202] The specified username is invalid.</summary>
        ERROR_BAD_USERNAME = 0x89A,
        /// <summary>[2250] This network connection does not exist.</summary>
        ERROR_NOT_CONNECTED = 0x8CA,
        /// <summary>[2401] This network connection has files open or requests pending.</summary>
        ERROR_OPEN_FILES = 0x961,
        /// <summary>[2402] Active connections still exist.</summary>
        ERROR_ACTIVE_CONNECTIONS = 0x962,
        /// <summary>[2404] The device is in use by an active process and cannot be disconnected.</summary>
        ERROR_DEVICE_IN_USE = 0x964,
        /// <summary>[3000] The specified print monitor is unknown.</summary>
        ERROR_UNKNOWN_PRINT_MONITOR = 0xBB8,
        /// <summary>[3001] The specified printer driver is currently in use.</summary>
        ERROR_PRINTER_DRIVER_IN_USE = 0xBB9,
        /// <summary>[3002] The spool file was not found.</summary>
        ERROR_SPOOL_FILE_NOT_FOUND = 0xBBA,
        /// <summary>[3003] A StartDocPrinter call was not issued.</summary>
        ERROR_SPL_NO_STARTDOC = 0xBBB,
        /// <summary>[3004] An AddJob call was not issued.</summary>
        ERROR_SPL_NO_ADDJOB = 0xBBC,
        /// <summary>[3005] The specified print processor has already been installed.</summary>
        ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED = 0xBBD,
        /// <summary>[3006] The specified print monitor has already been installed.</summary>
        ERROR_PRINT_MONITOR_ALREADY_INSTALLED = 0xBBE,
        /// <summary>[3007] The specified print monitor does not have the required functions.</summary>
        ERROR_INVALID_PRINT_MONITOR = 0xBBF,
        /// <summary>[3008] The specified print monitor is currently in use.</summary>
        ERROR_PRINT_MONITOR_IN_USE = 0xBC0,
        /// <summary>[3009] The requested operation is not allowed when there are jobs queued to the printer.</summary>
        ERROR_PRINTER_HAS_JOBS_QUEUED = 0xBC1,
        /// <summary>[3010] The requested operation is successful. Changes will not be effective until the system is rebooted.</summary>
        ERROR_SUCCESS_REBOOT_REQUIRED = 0xBC2,
        /// <summary>[3011] The requested operation is successful. Changes will not be effective until the service is restarted.</summary>
        ERROR_SUCCESS_RESTART_REQUIRED = 0xBC3,
        /// <summary>[3012] No printers were found.</summary>
        ERROR_PRINTER_NOT_FOUND = 0xBC4,
        /// <summary>[3013] The printer driver is known to be unreliable.</summary>
        ERROR_PRINTER_DRIVER_WARNED = 0xBC5,
        /// <summary>[3014] The printer driver is known to harm the system.</summary>
        ERROR_PRINTER_DRIVER_BLOCKED = 0xBC6,
        /// <summary>[3015] The specified printer driver package is currently in use.</summary>
        ERROR_PRINTER_DRIVER_PACKAGE_IN_USE = 0xBC7,
        /// <summary>[3016] Unable to find a core driver package that is required by the printer driver package.</summary>
        ERROR_CORE_DRIVER_PACKAGE_NOT_FOUND = 0xBC8,
        /// <summary>[3017] The requested operation failed. A system reboot is required to roll back changes made.</summary>
        ERROR_FAIL_REBOOT_REQUIRED = 0xBC9,
        /// <summary>[3018] The requested operation failed. A system reboot has been initiated to roll back changes made.</summary>
        ERROR_FAIL_REBOOT_INITIATED = 0xBCA,
        /// <summary>[3019] The specified printer driver was not found on the system and needs to be downloaded.</summary>
        ERROR_PRINTER_DRIVER_DOWNLOAD_NEEDED = 0xBCB,
        /// <summary>[3020] The requested print job has failed to print. A print system update requires the job to be resubmitted.</summary>
        ERROR_PRINT_JOB_RESTART_REQUIRED = 0xBCC,
        /// <summary>[3021] The printer driver does not contain a valid manifest, or contains too many manifests.</summary>
        ERROR_INVALID_PRINTER_DRIVER_MANIFEST = 0xBCD,
        /// <summary>[3022] The specified printer cannot be shared.</summary>
        ERROR_PRINTER_NOT_SHAREABLE = 0xBCE,
        /// <summary>[3050] The operation was paused.</summary>
        ERROR_REQUEST_PAUSED = 0xBEA,
        /// <summary>[3950] Reissue the given operation as a cached IO operation.</summary>
        ERROR_IO_REISSUE_AS_CACHED = 0xF6E,
        /// <summary>[4000] WINS encountered an error while processing the command.</summary>
        ERROR_WINS_INTERNAL = 0xFA0,
        /// <summary>[4001] The local WINS cannot be deleted.</summary>
        ERROR_CAN_NOT_DEL_LOCAL_WINS = 0xFA1,
        /// <summary>[4002] The importation from the file failed.</summary>
        ERROR_STATIC_INIT = 0xFA2,
        /// <summary>[4003] The backup failed. Was a full backup done before?</summary>
        ERROR_INC_BACKUP = 0xFA3,
        /// <summary>[4004] The backup failed. Check the directory to which you are backing the database.</summary>
        ERROR_FULL_BACKUP = 0xFA4,
        /// <summary>[4005] The name does not exist in the WINS database.</summary>
        ERROR_REC_NON_EXISTENT = 0xFA5,
        /// <summary>[4006] Replication with a nonconfigured partner is not allowed.</summary>
        ERROR_RPL_NOT_ALLOWED = 0xFA6,
        /// <summary>[4050] The version of the supplied content information is not supported.</summary>
        PEERDIST_ERROR_CONTENTINFO_VERSION_UNSUPPORTED = 0xFD2,
        /// <summary>[4051] The supplied content information is malformed.</summary>
        PEERDIST_ERROR_CANNOT_PARSE_CONTENTINFO = 0xFD3,
        /// <summary>[4052] The requested data cannot be found in local or peer caches.</summary>
        PEERDIST_ERROR_MISSING_DATA = 0xFD4,
        /// <summary>[4053] No more data is available or required.</summary>
        PEERDIST_ERROR_NO_MORE = 0xFD5,
        /// <summary>[4054] The supplied object has not been initialized.</summary>
        PEERDIST_ERROR_NOT_INITIALIZED = 0xFD6,
        /// <summary>[4055] The supplied object has already been initialized.</summary>
        PEERDIST_ERROR_ALREADY_INITIALIZED = 0xFD7,
        /// <summary>[4056] A shutdown operation is already in progress.</summary>
        PEERDIST_ERROR_SHUTDOWN_IN_PROGRESS = 0xFD8,
        /// <summary>[4057] The supplied object has already been invalidated.</summary>
        PEERDIST_ERROR_INVALIDATED = 0xFD9,
        /// <summary>[4058] An element already exists and was not replaced.</summary>
        PEERDIST_ERROR_ALREADY_EXISTS = 0xFDA,
        /// <summary>[4059] Cannot cancel the requested operation as it has already been completed.</summary>
        PEERDIST_ERROR_OPERATION_NOTFOUND = 0xFDB,
        /// <summary>[4060] Can not perform the requested operation because it has already been carried out.</summary>
        PEERDIST_ERROR_ALREADY_COMPLETED = 0xFDC,
        /// <summary>[4061] An operation accessed data beyond the bounds of valid data.</summary>
        PEERDIST_ERROR_OUT_OF_BOUNDS = 0xFDD,
        /// <summary>[4062] The requested version is not supported.</summary>
        PEERDIST_ERROR_VERSION_UNSUPPORTED = 0xFDE,
        /// <summary>[4063] A configuration value is invalid.</summary>
        PEERDIST_ERROR_INVALID_CONFIGURATION = 0xFDF,
        /// <summary>[4064] The SKU is not licensed.</summary>
        PEERDIST_ERROR_NOT_LICENSED = 0xFE0,
        /// <summary>[4065] PeerDist Service is still initializing and will be available shortly.</summary>
        PEERDIST_ERROR_SERVICE_UNAVAILABLE = 0xFE1,
        /// <summary>[4066] Communication with one or more computers will be temporarily blocked due to recent errors.</summary>
        PEERDIST_ERROR_TRUST_FAILURE = 0xFE2,
        /// <summary>[4100] The DHCP client has obtained an IP address that is already in use on the network. The local interface will be disabled until the DHCP client can obtain a new address.</summary>
        ERROR_DHCP_ADDRESS_CONFLICT = 0x1004,
        /// <summary>[4200] The GUID passed was not recognized as valid by a WMI data provider.</summary>
        ERROR_WMI_GUID_NOT_FOUND = 0x1068,
        /// <summary>[4201] The instance name passed was not recognized as valid by a WMI data provider.</summary>
        ERROR_WMI_INSTANCE_NOT_FOUND = 0x1069,
        /// <summary>[4202] The data item ID passed was not recognized as valid by a WMI data provider.</summary>
        ERROR_WMI_ITEMID_NOT_FOUND = 0x106A,
        /// <summary>[4203] The WMI request could not be completed and should be retried.</summary>
        ERROR_WMI_TRY_AGAIN = 0x106B,
        /// <summary>[4204] The WMI data provider could not be located.</summary>
        ERROR_WMI_DP_NOT_FOUND = 0x106C,
        /// <summary>[4205] The WMI data provider references an instance set that has not been registered.</summary>
        ERROR_WMI_UNRESOLVED_INSTANCE_REF = 0x106D,
        /// <summary>[4206] The WMI data block or event notification has already been enabled.</summary>
        ERROR_WMI_ALREADY_ENABLED = 0x106E,
        /// <summary>[4207] The WMI data block is no longer available.</summary>
        ERROR_WMI_GUID_DISCONNECTED = 0x106F,
        /// <summary>[4208] The WMI data service is not available.</summary>
        ERROR_WMI_SERVER_UNAVAILABLE = 0x1070,
        /// <summary>[4209] The WMI data provider failed to carry out the request.</summary>
        ERROR_WMI_DP_FAILED = 0x1071,
        /// <summary>[4210] The WMI MOF information is not valid.</summary>
        ERROR_WMI_INVALID_MOF = 0x1072,
        /// <summary>[4211] The WMI registration information is not valid.</summary>
        ERROR_WMI_INVALID_REGINFO = 0x1073,
        /// <summary>[4212] The WMI data block or event notification has already been disabled.</summary>
        ERROR_WMI_ALREADY_DISABLED = 0x1074,
        /// <summary>[4213] The WMI data item or data block is read only.</summary>
        ERROR_WMI_READ_ONLY = 0x1075,
        /// <summary>[4214] The WMI data item or data block could not be changed.</summary>
        ERROR_WMI_SET_FAILURE = 0x1076,
        /// <summary>[4250] This operation is only valid in the context of an app container.</summary>
        ERROR_NOT_APPCONTAINER = 0x109A,
        /// <summary>[4251] This application can only run in the context of an app container.</summary>
        ERROR_APPCONTAINER_REQUIRED = 0x109B,
        /// <summary>[4252] This functionality is not supported in the context of an app container.</summary>
        ERROR_NOT_SUPPORTED_IN_APPCONTAINER = 0x109C,
        /// <summary>[4253] The length of the SID supplied is not a valid length for app container SIDs.</summary>
        ERROR_INVALID_PACKAGE_SID_LENGTH = 0x109D,
        /// <summary>[4300] The media identifier does not represent a valid medium.</summary>
        ERROR_INVALID_MEDIA = 0x10CC,
        /// <summary>[4301] The library identifier does not represent a valid library.</summary>
        ERROR_INVALID_LIBRARY = 0x10CD,
        /// <summary>[4302] The media pool identifier does not represent a valid media pool.</summary>
        ERROR_INVALID_MEDIA_POOL = 0x10CE,
        /// <summary>[4303] The drive and medium are not compatible or exist in different libraries.</summary>
        ERROR_DRIVE_MEDIA_MISMATCH = 0x10CF,
        /// <summary>[4304] The medium currently exists in an offline library and must be online to perform this operation.</summary>
        ERROR_MEDIA_OFFLINE = 0x10D0,
        /// <summary>[4305] The operation cannot be performed on an offline library.</summary>
        ERROR_LIBRARY_OFFLINE = 0x10D1,
        /// <summary>[4306] The library, drive, or media pool is empty.</summary>
        ERROR_EMPTY = 0x10D2,
        /// <summary>[4307] The library, drive, or media pool must be empty to perform this operation.</summary>
        ERROR_NOT_EMPTY = 0x10D3,
        /// <summary>[4308] No media is currently available in this media pool or library.</summary>
        ERROR_MEDIA_UNAVAILABLE = 0x10D4,
        /// <summary>[4309] A resource required for this operation is disabled.</summary>
        ERROR_RESOURCE_DISABLED = 0x10D5,
        /// <summary>[4310] The media identifier does not represent a valid cleaner.</summary>
        ERROR_INVALID_CLEANER = 0x10D6,
        /// <summary>[4311] The drive cannot be cleaned or does not support cleaning.</summary>
        ERROR_UNABLE_TO_CLEAN = 0x10D7,
        /// <summary>[4312] The object identifier does not represent a valid object.</summary>
        ERROR_OBJECT_NOT_FOUND = 0x10D8,
        /// <summary>[4313] Unable to read from or write to the database.</summary>
        ERROR_DATABASE_FAILURE = 0x10D9,
        /// <summary>[4314] The database is full.</summary>
        ERROR_DATABASE_FULL = 0x10DA,
        /// <summary>[4315] The medium is not compatible with the device or media pool.</summary>
        ERROR_MEDIA_INCOMPATIBLE = 0x10DB,
        /// <summary>[4316] The resource required for this operation does not exist.</summary>
        ERROR_RESOURCE_NOT_PRESENT = 0x10DC,
        /// <summary>[4317] The operation identifier is not valid.</summary>
        ERROR_INVALID_OPERATION = 0x10DD,
        /// <summary>[4318] The media is not mounted or ready for use.</summary>
        ERROR_MEDIA_NOT_AVAILABLE = 0x10DE,
        /// <summary>[4319] The device is not ready for use.</summary>
        ERROR_DEVICE_NOT_AVAILABLE = 0x10DF,
        /// <summary>[4320] The operator or administrator has refused the request.</summary>
        ERROR_REQUEST_REFUSED = 0x10E0,
        /// <summary>[4321] The drive identifier does not represent a valid drive.</summary>
        ERROR_INVALID_DRIVE_OBJECT = 0x10E1,
        /// <summary>[4322] Library is full. No slot is available for use.</summary>
        ERROR_LIBRARY_FULL = 0x10E2,
        /// <summary>[4323] The transport cannot access the medium.</summary>
        ERROR_MEDIUM_NOT_ACCESSIBLE = 0x10E3,
        /// <summary>[4324] Unable to load the medium into the drive.</summary>
        ERROR_UNABLE_TO_LOAD_MEDIUM = 0x10E4,
        /// <summary>[4325] Unable to retrieve the drive status.</summary>
        ERROR_UNABLE_TO_INVENTORY_DRIVE = 0x10E5,
        /// <summary>[4326] Unable to retrieve the slot status.</summary>
        ERROR_UNABLE_TO_INVENTORY_SLOT = 0x10E6,
        /// <summary>[4327] Unable to retrieve status about the transport.</summary>
        ERROR_UNABLE_TO_INVENTORY_TRANSPORT = 0x10E7,
        /// <summary>[4328] Cannot use the transport because it is already in use.</summary>
        ERROR_TRANSPORT_FULL = 0x10E8,
        /// <summary>[4329] Unable to open or close the inject/eject port.</summary>
        ERROR_CONTROLLING_IEPORT = 0x10E9,
        /// <summary>[4330] Unable to eject the medium because it is in a drive.</summary>
        ERROR_UNABLE_TO_EJECT_MOUNTED_MEDIA = 0x10EA,
        /// <summary>[4331] A cleaner slot is already reserved.</summary>
        ERROR_CLEANER_SLOT_SET = 0x10EB,
        /// <summary>[4332] A cleaner slot is not reserved.</summary>
        ERROR_CLEANER_SLOT_NOT_SET = 0x10EC,
        /// <summary>[4333] The cleaner cartridge has performed the maximum number of drive cleanings.</summary>
        ERROR_CLEANER_CARTRIDGE_SPENT = 0x10ED,
        /// <summary>[4334] Unexpected on-medium identifier.</summary>
        ERROR_UNEXPECTED_OMID = 0x10EE,
        /// <summary>[4335] The last remaining item in this group or resource cannot be deleted.</summary>
        ERROR_CANT_DELETE_LAST_ITEM = 0x10EF,
        /// <summary>[4336] The message provided exceeds the maximum size allowed for this parameter.</summary>
        ERROR_MESSAGE_EXCEEDS_MAX_SIZE = 0x10F0,
        /// <summary>[4337] The volume contains system or paging files.</summary>
        ERROR_VOLUME_CONTAINS_SYS_FILES = 0x10F1,
        /// <summary>[4338] The media type cannot be removed from this library since at least one drive in the library reports it can support this media type.</summary>
        ERROR_INDIGENOUS_TYPE = 0x10F2,
        /// <summary>[4339] This offline media cannot be mounted on this system since no enabled drives are present which can be used.</summary>
        ERROR_NO_SUPPORTING_DRIVES = 0x10F3,
        /// <summary>[4340] A cleaner cartridge is present in the tape library.</summary>
        ERROR_CLEANER_CARTRIDGE_INSTALLED = 0x10F4,
        /// <summary>[4341] Cannot use the inject/eject port because it is not empty.</summary>
        ERROR_IEPORT_FULL = 0x10F5,
        /// <summary>[4350] This file is currently not available for use on this computer.</summary>
        ERROR_FILE_OFFLINE = 0x10FE,
        /// <summary>[4351] The remote storage service is not operational at this time.</summary>
        ERROR_REMOTE_STORAGE_NOT_ACTIVE = 0x10FF,
        /// <summary>[4352] The remote storage service encountered a media error.</summary>
        ERROR_REMOTE_STORAGE_MEDIA_ERROR = 0x1100,
        /// <summary>[4390] The file or directory is not a reparse point.</summary>
        ERROR_NOT_A_REPARSE_POINT = 0x1126,
        /// <summary>[4391] The reparse point attribute cannot be set because it conflicts with an existing attribute.</summary>
        ERROR_REPARSE_ATTRIBUTE_CONFLICT = 0x1127,
        /// <summary>[4392] The data present in the reparse point buffer is invalid.</summary>
        ERROR_INVALID_REPARSE_DATA = 0x1128,
        /// <summary>[4393] The tag present in the reparse point buffer is invalid.</summary>
        ERROR_REPARSE_TAG_INVALID = 0x1129,
        /// <summary>[4394] There is a mismatch between the tag specified in the request and the tag present in the reparse point.</summary>
        ERROR_REPARSE_TAG_MISMATCH = 0x112A,
        /// <summary>[4400] Fast Cache data not found.</summary>
        ERROR_APP_DATA_NOT_FOUND = 0x1130,
        /// <summary>[4401] Fast Cache data expired.</summary>
        ERROR_APP_DATA_EXPIRED = 0x1131,
        /// <summary>[4402] Fast Cache data corrupt.</summary>
        ERROR_APP_DATA_CORRUPT = 0x1132,
        /// <summary>[4403] Fast Cache data has exceeded its max size and cannot be updated.</summary>
        ERROR_APP_DATA_LIMIT_EXCEEDED = 0x1133,
        /// <summary>[4404] Fast Cache has been ReArmed and requires a reboot until it can be updated.</summary>
        ERROR_APP_DATA_REBOOT_REQUIRED = 0x1134,
        /// <summary>[4420] Secure Boot detected that rollback of protected data has been attempted.</summary>
        ERROR_SECUREBOOT_ROLLBACK_DETECTED = 0x1144,
        /// <summary>[4421] The value is protected by Secure Boot policy and cannot be modified or deleted.</summary>
        ERROR_SECUREBOOT_POLICY_VIOLATION = 0x1145,
        /// <summary>[4422] The Secure Boot policy is invalid.</summary>
        ERROR_SECUREBOOT_INVALID_POLICY = 0x1146,
        /// <summary>[4423] A new Secure Boot policy did not contain the current publisher on its update list.</summary>
        ERROR_SECUREBOOT_POLICY_PUBLISHER_NOT_FOUND = 0x1147,
        /// <summary>[4424] The Secure Boot policy is either not signed or is signed by a non-trusted signer.</summary>
        ERROR_SECUREBOOT_POLICY_NOT_SIGNED = 0x1148,
        /// <summary>[4425] Secure Boot is not enabled on this machine.</summary>
        ERROR_SECUREBOOT_NOT_ENABLED = 0x1149,
        /// <summary>[4426] Secure Boot requires that certain files and drivers are not replaced by other files or drivers.</summary>
        ERROR_SECUREBOOT_FILE_REPLACED = 0x114A,
        /// <summary>[4440] The copy offload read operation is not supported by a filter.</summary>
        ERROR_OFFLOAD_READ_FLT_NOT_SUPPORTED = 0x1158,
        /// <summary>[4441] The copy offload write operation is not supported by a filter.</summary>
        ERROR_OFFLOAD_WRITE_FLT_NOT_SUPPORTED = 0x1159,
        /// <summary>[4442] The copy offload read operation is not supported for the file.</summary>
        ERROR_OFFLOAD_READ_FILE_NOT_SUPPORTED = 0x115A,
        /// <summary>[4443] The copy offload write operation is not supported for the file.</summary>
        ERROR_OFFLOAD_WRITE_FILE_NOT_SUPPORTED = 0x115B,
        /// <summary>[4500] Single Instance Storage is not available on this volume.</summary>
        ERROR_VOLUME_NOT_SIS_ENABLED = 0x1194,
        /// <summary>[5001] The operation cannot be completed because other resources are dependent on this resource.</summary>
        ERROR_DEPENDENT_RESOURCE_EXISTS = 0x1389,
        /// <summary>[5002] The cluster resource dependency cannot be found.</summary>
        ERROR_DEPENDENCY_NOT_FOUND = 0x138A,
        /// <summary>[5003] The cluster resource cannot be made dependent on the specified resource because it is already dependent.</summary>
        ERROR_DEPENDENCY_ALREADY_EXISTS = 0x138B,
        /// <summary>[5004] The cluster resource is not online.</summary>
        ERROR_RESOURCE_NOT_ONLINE = 0x138C,
        /// <summary>[5005] A cluster node is not available for this operation.</summary>
        ERROR_HOST_NODE_NOT_AVAILABLE = 0x138D,
        /// <summary>[5006] The cluster resource is not available.</summary>
        ERROR_RESOURCE_NOT_AVAILABLE = 0x138E,
        /// <summary>[5007] The cluster resource could not be found.</summary>
        ERROR_RESOURCE_NOT_FOUND = 0x138F,
        /// <summary>[5008] The cluster is being shut down.</summary>
        ERROR_SHUTDOWN_CLUSTER = 0x1390,
        /// <summary>[5009] A cluster node cannot be evicted from the cluster unless the node is down or it is the last node.</summary>
        ERROR_CANT_EVICT_ACTIVE_NODE = 0x1391,
        /// <summary>[5010] The object already exists.</summary>
        ERROR_OBJECT_ALREADY_EXISTS = 0x1392,
        /// <summary>[5011] The object is already in the list.</summary>
        ERROR_OBJECT_IN_LIST = 0x1393,
        /// <summary>[5012] The cluster group is not available for any new requests.</summary>
        ERROR_GROUP_NOT_AVAILABLE = 0x1394,
        /// <summary>[5013] The cluster group could not be found.</summary>
        ERROR_GROUP_NOT_FOUND = 0x1395,
        /// <summary>[5014] The operation could not be completed because the cluster group is not online.</summary>
        ERROR_GROUP_NOT_ONLINE = 0x1396,
        /// <summary>[5015] The operation failed because either the specified cluster node is not the owner of the resource, or the node is not a possible owner of the resource.</summary>
        ERROR_HOST_NODE_NOT_RESOURCE_OWNER = 0x1397,
        /// <summary>[5016] The operation failed because either the specified cluster node is not the owner of the group, or the node is not a possible owner of the group.</summary>
        ERROR_HOST_NODE_NOT_GROUP_OWNER = 0x1398,
        /// <summary>[5017] The cluster resource could not be created in the specified resource monitor.</summary>
        ERROR_RESMON_CREATE_FAILED = 0x1399,
        /// <summary>[5018] The cluster resource could not be brought online by the resource monitor.</summary>
        ERROR_RESMON_ONLINE_FAILED = 0x139A,
        /// <summary>[5019] The operation could not be completed because the cluster resource is online.</summary>
        ERROR_RESOURCE_ONLINE = 0x139B,
        /// <summary>[5020] The cluster resource could not be deleted or brought offline because it is the quorum resource.</summary>
        ERROR_QUORUM_RESOURCE = 0x139C,
        /// <summary>[5021] The cluster could not make the specified resource a quorum resource because it is not capable of being a quorum resource.</summary>
        ERROR_NOT_QUORUM_CAPABLE = 0x139D,
        /// <summary>[5022] The cluster software is shutting down.</summary>
        ERROR_CLUSTER_SHUTTING_DOWN = 0x139E,
        /// <summary>[5023] The group or resource is not in the correct state to perform the requested operation.</summary>
        ERROR_INVALID_STATE = 0x139F,
        /// <summary>[5024] The properties were stored but not all changes will take effect until the next time the resource is brought online.</summary>
        ERROR_RESOURCE_PROPERTIES_STORED = 0x13A0,
        /// <summary>[5025] The cluster could not make the specified resource a quorum resource because it does not belong to a shared storage class.</summary>
        ERROR_NOT_QUORUM_CLASS = 0x13A1,
        /// <summary>[5026] The cluster resource could not be deleted since it is a core resource.</summary>
        ERROR_CORE_RESOURCE = 0x13A2,
        /// <summary>[5027] The quorum resource failed to come online.</summary>
        ERROR_QUORUM_RESOURCE_ONLINE_FAILED = 0x13A3,
        /// <summary>[5028] The quorum log could not be created or mounted successfully.</summary>
        ERROR_QUORUMLOG_OPEN_FAILED = 0x13A4,
        /// <summary>[5029] The cluster log is corrupt.</summary>
        ERROR_CLUSTERLOG_CORRUPT = 0x13A5,
        /// <summary>[5030] The record could not be written to the cluster log since it exceeds the maximum size.</summary>
        ERROR_CLUSTERLOG_RECORD_EXCEEDS_MAXSIZE = 0x13A6,
        /// <summary>[5031] The cluster log exceeds its maximum size.</summary>
        ERROR_CLUSTERLOG_EXCEEDS_MAXSIZE = 0x13A7,
        /// <summary>[5032] No checkpoint record was found in the cluster log.</summary>
        ERROR_CLUSTERLOG_CHKPOINT_NOT_FOUND = 0x13A8,
        /// <summary>[5033] The minimum required disk space needed for logging is not available.</summary>
        ERROR_CLUSTERLOG_NOT_ENOUGH_SPACE = 0x13A9,
        /// <summary>[5034] The cluster node failed to take control of the quorum resource because the resource is owned by another active node.</summary>
        ERROR_QUORUM_OWNER_ALIVE = 0x13AA,
        /// <summary>[5035] A cluster network is not available for this operation.</summary>
        ERROR_NETWORK_NOT_AVAILABLE = 0x13AB,
        /// <summary>[5036] A cluster node is not available for this operation.</summary>
        ERROR_NODE_NOT_AVAILABLE = 0x13AC,
        /// <summary>[5037] All cluster nodes must be running to perform this operation.</summary>
        ERROR_ALL_NODES_NOT_AVAILABLE = 0x13AD,
        /// <summary>[5038] A cluster resource failed.</summary>
        ERROR_RESOURCE_FAILED = 0x13AE,
        /// <summary>[5039] The cluster node is not valid.</summary>
        ERROR_CLUSTER_INVALID_NODE = 0x13AF,
        /// <summary>[5040] The cluster node already exists.</summary>
        ERROR_CLUSTER_NODE_EXISTS = 0x13B0,
        /// <summary>[5041] A node is in the process of joining the cluster.</summary>
        ERROR_CLUSTER_JOIN_IN_PROGRESS = 0x13B1,
        /// <summary>[5042] The cluster node was not found.</summary>
        ERROR_CLUSTER_NODE_NOT_FOUND = 0x13B2,
        /// <summary>[5043] The cluster local node information was not found.</summary>
        ERROR_CLUSTER_LOCAL_NODE_NOT_FOUND = 0x13B3,
        /// <summary>[5044] The cluster network already exists.</summary>
        ERROR_CLUSTER_NETWORK_EXISTS = 0x13B4,
        /// <summary>[5045] The cluster network was not found.</summary>
        ERROR_CLUSTER_NETWORK_NOT_FOUND = 0x13B5,
        /// <summary>[5046] The cluster network interface already exists.</summary>
        ERROR_CLUSTER_NETINTERFACE_EXISTS = 0x13B6,
        /// <summary>[5047] The cluster network interface was not found.</summary>
        ERROR_CLUSTER_NETINTERFACE_NOT_FOUND = 0x13B7,
        /// <summary>[5048] The cluster request is not valid for this object.</summary>
        ERROR_CLUSTER_INVALID_REQUEST = 0x13B8,
        /// <summary>[5049] The cluster network provider is not valid.</summary>
        ERROR_CLUSTER_INVALID_NETWORK_PROVIDER = 0x13B9,
        /// <summary>[5050] The cluster node is down.</summary>
        ERROR_CLUSTER_NODE_DOWN = 0x13BA,
        /// <summary>[5051] The cluster node is not reachable.</summary>
        ERROR_CLUSTER_NODE_UNREACHABLE = 0x13BB,
        /// <summary>[5052] The cluster node is not a member of the cluster.</summary>
        ERROR_CLUSTER_NODE_NOT_MEMBER = 0x13BC,
        /// <summary>[5053] A cluster join operation is not in progress.</summary>
        ERROR_CLUSTER_JOIN_NOT_IN_PROGRESS = 0x13BD,
        /// <summary>[5054] The cluster network is not valid.</summary>
        ERROR_CLUSTER_INVALID_NETWORK = 0x13BE,
        /// <summary>[5056] The cluster node is up.</summary>
        ERROR_CLUSTER_NODE_UP = 0x13C0,
        /// <summary>[5057] The cluster IP address is already in use.</summary>
        ERROR_CLUSTER_IPADDR_IN_USE = 0x13C1,
        /// <summary>[5058] The cluster node is not paused.</summary>
        ERROR_CLUSTER_NODE_NOT_PAUSED = 0x13C2,
        /// <summary>[5059] No cluster security context is available.</summary>
        ERROR_CLUSTER_NO_SECURITY_CONTEXT = 0x13C3,
        /// <summary>[5060] The cluster network is not configured for internal cluster communication.</summary>
        ERROR_CLUSTER_NETWORK_NOT_INTERNAL = 0x13C4,
        /// <summary>[5061] The cluster node is already up.</summary>
        ERROR_CLUSTER_NODE_ALREADY_UP = 0x13C5,
        /// <summary>[5062] The cluster node is already down.</summary>
        ERROR_CLUSTER_NODE_ALREADY_DOWN = 0x13C6,
        /// <summary>[5063] The cluster network is already online.</summary>
        ERROR_CLUSTER_NETWORK_ALREADY_ONLINE = 0x13C7,
        /// <summary>[5064] The cluster network is already offline.</summary>
        ERROR_CLUSTER_NETWORK_ALREADY_OFFLINE = 0x13C8,
        /// <summary>[5065] The cluster node is already a member of the cluster.</summary>
        ERROR_CLUSTER_NODE_ALREADY_MEMBER = 0x13C9,
        /// <summary>[5066] The cluster network is the only one configured for internal cluster communication between two or more active cluster nodes. The internal communication capability cannot be removed from the network.</summary>
        ERROR_CLUSTER_LAST_INTERNAL_NETWORK = 0x13CA,
        /// <summary>[5067] One or more cluster resources depend on the network to provide service to clients. The client access capability cannot be removed from the network.</summary>
        ERROR_CLUSTER_NETWORK_HAS_DEPENDENTS = 0x13CB,
        /// <summary>[5068] This operation cannot be performed on the cluster resource as it the quorum resource. You may not bring the quorum resource offline or modify its possible owners list.</summary>
        ERROR_INVALID_OPERATION_ON_QUORUM = 0x13CC,
        /// <summary>[5069] The cluster quorum resource is not allowed to have any dependencies.</summary>
        ERROR_DEPENDENCY_NOT_ALLOWED = 0x13CD,
        /// <summary>[5070] The cluster node is paused.</summary>
        ERROR_CLUSTER_NODE_PAUSED = 0x13CE,
        /// <summary>[5071] The cluster resource cannot be brought online. The owner node cannot run this resource.</summary>
        ERROR_NODE_CANT_HOST_RESOURCE = 0x13CF,
        /// <summary>[5072] The cluster node is not ready to perform the requested operation.</summary>
        ERROR_CLUSTER_NODE_NOT_READY = 0x13D0,
        /// <summary>[5073] The cluster node is shutting down.</summary>
        ERROR_CLUSTER_NODE_SHUTTING_DOWN = 0x13D1,
        /// <summary>[5074] The cluster join operation was aborted.</summary>
        ERROR_CLUSTER_JOIN_ABORTED = 0x13D2,
        /// <summary>[5075] The cluster join operation failed due to incompatible software versions between the joining node and its sponsor.</summary>
        ERROR_CLUSTER_INCOMPATIBLE_VERSIONS = 0x13D3,
        /// <summary>[5076] This resource cannot be created because the cluster has reached the limit on the number of resources it can monitor.</summary>
        ERROR_CLUSTER_MAXNUM_OF_RESOURCES_EXCEEDED = 0x13D4,
        /// <summary>[5077] The system configuration changed during the cluster join or form operation. The join or form operation was aborted.</summary>
        ERROR_CLUSTER_SYSTEM_CONFIG_CHANGED = 0x13D5,
        /// <summary>[5078] The specified resource type was not found.</summary>
        ERROR_CLUSTER_RESOURCE_TYPE_NOT_FOUND = 0x13D6,
        /// <summary>[5079] The specified node does not support a resource of this type. This may be due to version inconsistencies or due to the absence of the resource DLL on this node.</summary>
        ERROR_CLUSTER_RESTYPE_NOT_SUPPORTED = 0x13D7,
        /// <summary>[5080] The specified resource name is not supported by this resource DLL. This may be due to a bad (or changed) name supplied to the resource DLL.</summary>
        ERROR_CLUSTER_RESNAME_NOT_FOUND = 0x13D8,
        /// <summary>[5081] No authentication package could be registered with the RPC server.</summary>
        ERROR_CLUSTER_NO_RPC_PACKAGES_REGISTERED = 0x13D9,
        /// <summary>[5082] You cannot bring the group online because the owner of the group is not in the preferred list for the group. To change the owner node for the group, move the group.</summary>
        ERROR_CLUSTER_OWNER_NOT_IN_PREFLIST = 0x13DA,
        /// <summary>[5083] The join operation failed because the cluster database sequence number has changed or is incompatible with the locker node. This may happen during a join operation if the cluster database was changing during the join.</summary>
        ERROR_CLUSTER_DATABASE_SEQMISMATCH = 0x13DB,
        /// <summary>[5084] The resource monitor will not allow the fail operation to be performed while the resource is in its current state. This may happen if the resource is in a pending state.</summary>
        ERROR_RESMON_INVALID_STATE = 0x13DC,
        /// <summary>[5085] A non locker code got a request to reserve the lock for making global updates.</summary>
        ERROR_CLUSTER_GUM_NOT_LOCKER = 0x13DD,
        /// <summary>[5086] The quorum disk could not be located by the cluster service.</summary>
        ERROR_QUORUM_DISK_NOT_FOUND = 0x13DE,
        /// <summary>[5087] The backed up cluster database is possibly corrupt.</summary>
        ERROR_DATABASE_BACKUP_CORRUPT = 0x13DF,
        /// <summary>[5088] A DFS root already exists in this cluster node.</summary>
        ERROR_CLUSTER_NODE_ALREADY_HAS_DFS_ROOT = 0x13E0,
        /// <summary>[5089] An attempt to modify a resource property failed because it conflicts with another existing property.</summary>
        ERROR_RESOURCE_PROPERTY_UNCHANGEABLE = 0x13E1,
        /// <summary>[5890] An operation was attempted that is incompatible with the current membership state of the node.</summary>
        ERROR_CLUSTER_MEMBERSHIP_INVALID_STATE = 0x1702,
        /// <summary>[5891] The quorum resource does not contain the quorum log.</summary>
        ERROR_CLUSTER_QUORUMLOG_NOT_FOUND = 0x1703,
        /// <summary>[5892] The membership engine requested shutdown of the cluster service on this node.</summary>
        ERROR_CLUSTER_MEMBERSHIP_HALT = 0x1704,
        /// <summary>[5893] The join operation failed because the cluster instance ID of the joining node does not match the cluster instance ID of the sponsor node.</summary>
        ERROR_CLUSTER_INSTANCE_ID_MISMATCH = 0x1705,
        /// <summary>[5894] A matching cluster network for the specified IP address could not be found.</summary>
        ERROR_CLUSTER_NETWORK_NOT_FOUND_FOR_IP = 0x1706,
        /// <summary>[5895] The actual data type of the property did not match the expected data type of the property.</summary>
        ERROR_CLUSTER_PROPERTY_DATA_TYPE_MISMATCH = 0x1707,
        /// <summary>[5896] The cluster node was evicted from the cluster successfully, but the node was not cleaned up. To determine what cleanup steps failed and how to recover, see the Failover Clustering application event log using Event Viewer.</summary>
        ERROR_CLUSTER_EVICT_WITHOUT_CLEANUP = 0x1708,
        /// <summary>[5897] Two or more parameter values specified for a resource's properties are in conflict.</summary>
        ERROR_CLUSTER_PARAMETER_MISMATCH = 0x1709,
        /// <summary>[5898] This computer cannot be made a member of a cluster.</summary>
        ERROR_NODE_CANNOT_BE_CLUSTERED = 0x170A,
        /// <summary>[5899] This computer cannot be made a member of a cluster because it does not have the correct version of Windows installed.</summary>
        ERROR_CLUSTER_WRONG_OS_VERSION = 0x170B,
        /// <summary>[5900] A cluster cannot be created with the specified cluster name because that cluster name is already in use. Specify a different name for the cluster.</summary>
        ERROR_CLUSTER_CANT_CREATE_DUP_CLUSTER_NAME = 0x170C,
        /// <summary>[5901] The cluster configuration action has already been committed.</summary>
        ERROR_CLUSCFG_ALREADY_COMMITTED = 0x170D,
        /// <summary>[5902] The cluster configuration action could not be rolled back.</summary>
        ERROR_CLUSCFG_ROLLBACK_FAILED = 0x170E,
        /// <summary>[5903] The drive letter assigned to a system disk on one node conflicted with the drive letter assigned to a disk on another node.</summary>
        ERROR_CLUSCFG_SYSTEM_DISK_DRIVE_LETTER_CONFLICT = 0x170F,
        /// <summary>[5904] One or more nodes in the cluster are running a version of Windows that does not support this operation.</summary>
        ERROR_CLUSTER_OLD_VERSION = 0x1710,
        /// <summary>[5905] The name of the corresponding computer account doesn't match the Network Name for this resource.</summary>
        ERROR_CLUSTER_MISMATCHED_COMPUTER_ACCT_NAME = 0x1711,
        /// <summary>[5906] No network adapters are available.</summary>
        ERROR_CLUSTER_NO_NET_ADAPTERS = 0x1712,
        /// <summary>[5907] The cluster node has been poisoned.</summary>
        ERROR_CLUSTER_POISONED = 0x1713,
        /// <summary>[5908] The group is unable to accept the request since it is moving to another node.</summary>
        ERROR_CLUSTER_GROUP_MOVING = 0x1714,
        /// <summary>[5909] The resource type cannot accept the request since is too busy performing another operation.</summary>
        ERROR_CLUSTER_RESOURCE_TYPE_BUSY = 0x1715,
        /// <summary>[5910] The call to the cluster resource DLL timed out.</summary>
        ERROR_RESOURCE_CALL_TIMED_OUT = 0x1716,
        /// <summary>[5911] The address is not valid for an IPv6 Address resource. A global IPv6 address is required, and it must match a cluster network. Compatibility addresses are not permitted.</summary>
        ERROR_INVALID_CLUSTER_IPV6_ADDRESS = 0x1717,
        /// <summary>[5912] An internal cluster error occurred. A call to an invalid function was attempted.</summary>
        ERROR_CLUSTER_INTERNAL_INVALID_FUNCTION = 0x1718,
        /// <summary>[5913] A parameter value is out of acceptable range.</summary>
        ERROR_CLUSTER_PARAMETER_OUT_OF_BOUNDS = 0x1719,
        /// <summary>[5914] A network error occurred while sending data to another node in the cluster. The number of bytes transmitted was less than required.</summary>
        ERROR_CLUSTER_PARTIAL_SEND = 0x171A,
        /// <summary>[5915] An invalid cluster registry operation was attempted.</summary>
        ERROR_CLUSTER_REGISTRY_INVALID_FUNCTION = 0x171B,
        /// <summary>[5916] An input string of characters is not properly terminated.</summary>
        ERROR_CLUSTER_INVALID_STRING_TERMINATION = 0x171C,
        /// <summary>[5917] An input string of characters is not in a valid format for the data it represents.</summary>
        ERROR_CLUSTER_INVALID_STRING_FORMAT = 0x171D,
        /// <summary>[5918] An internal cluster error occurred. A cluster database transaction was attempted while a transaction was already in progress.</summary>
        ERROR_CLUSTER_DATABASE_TRANSACTION_IN_PROGRESS = 0x171E,
        /// <summary>[5919] An internal cluster error occurred. There was an attempt to commit a cluster database transaction while no transaction was in progress.</summary>
        ERROR_CLUSTER_DATABASE_TRANSACTION_NOT_IN_PROGRESS = 0x171F,
        /// <summary>[5920] An internal cluster error occurred. Data was not properly initialized.</summary>
        ERROR_CLUSTER_NULL_DATA = 0x1720,
        /// <summary>[5921] An error occurred while reading from a stream of data. An unexpected number of bytes was returned.</summary>
        ERROR_CLUSTER_PARTIAL_READ = 0x1721,
        /// <summary>[5922] An error occurred while writing to a stream of data. The required number of bytes could not be written.</summary>
        ERROR_CLUSTER_PARTIAL_WRITE = 0x1722,
        /// <summary>[5923] An error occurred while deserializing a stream of cluster data.</summary>
        ERROR_CLUSTER_CANT_DESERIALIZE_DATA = 0x1723,
        /// <summary>[5924] One or more property values for this resource are in conflict with one or more property values associated with its dependent resource(s).</summary>
        ERROR_DEPENDENT_RESOURCE_PROPERTY_CONFLICT = 0x1724,
        /// <summary>[5925] A quorum of cluster nodes was not present to form a cluster.</summary>
        ERROR_CLUSTER_NO_QUORUM = 0x1725,
        /// <summary>[5926] The cluster network is not valid for an IPv6 Address resource, or it does not match the configured address.</summary>
        ERROR_CLUSTER_INVALID_IPV6_NETWORK = 0x1726,
        /// <summary>[5927] The cluster network is not valid for an IPv6 Tunnel resource. Check the configuration of the IP Address resource on which the IPv6 Tunnel resource depends.</summary>
        ERROR_CLUSTER_INVALID_IPV6_TUNNEL_NETWORK = 0x1727,
        /// <summary>[5928] Quorum resource cannot reside in the Available Storage group.</summary>
        ERROR_QUORUM_NOT_ALLOWED_IN_THIS_GROUP = 0x1728,
        /// <summary>[5929] The dependencies for this resource are nested too deeply.</summary>
        ERROR_DEPENDENCY_TREE_TOO_COMPLEX = 0x1729,
        /// <summary>[5930] The call into the resource DLL raised an unhandled exception.</summary>
        ERROR_EXCEPTION_IN_RESOURCE_CALL = 0x172A,
        /// <summary>[5931] The RHS process failed to initialize.</summary>
        ERROR_CLUSTER_RHS_FAILED_INITIALIZATION = 0x172B,
        /// <summary>[5932] The Failover Clustering feature is not installed on this node.</summary>
        ERROR_CLUSTER_NOT_INSTALLED = 0x172C,
        /// <summary>[5933] The resources must be online on the same node for this operation.</summary>
        ERROR_CLUSTER_RESOURCES_MUST_BE_ONLINE_ON_THE_SAME_NODE = 0x172D,
        /// <summary>[5934] A new node can not be added since this cluster is already at its maximum number of nodes.</summary>
        ERROR_CLUSTER_MAX_NODES_IN_CLUSTER = 0x172E,
        /// <summary>[5935] This cluster can not be created since the specified number of nodes exceeds the maximum allowed limit.</summary>
        ERROR_CLUSTER_TOO_MANY_NODES = 0x172F,
        /// <summary>[5936] An attempt to use the specified cluster name failed because an enabled computer object with the given name already exists in the domain.</summary>
        ERROR_CLUSTER_OBJECT_ALREADY_USED = 0x1730,
        /// <summary>[5937] This cluster cannot be destroyed. It has non-core application groups which must be deleted before the cluster can be destroyed.</summary>
        ERROR_NONCORE_GROUPS_FOUND = 0x1731,
        /// <summary>[5938] File share associated with file share witness resource cannot be hosted by this cluster or any of its nodes.</summary>
        ERROR_FILE_SHARE_RESOURCE_CONFLICT = 0x1732,
        /// <summary>[5939] Eviction of this node is invalid at this time. Due to quorum requirements node eviction will result in cluster shutdown. If it is the last node in the cluster, destroy cluster command should be used.</summary>
        ERROR_CLUSTER_EVICT_INVALID_REQUEST = 0x1733,
        /// <summary>[5940] Only one instance of this resource type is allowed in the cluster.</summary>
        ERROR_CLUSTER_SINGLETON_RESOURCE = 0x1734,
        /// <summary>[5941] Only one instance of this resource type is allowed per resource group.</summary>
        ERROR_CLUSTER_GROUP_SINGLETON_RESOURCE = 0x1735,
        /// <summary>[5942] The resource failed to come online due to the failure of one or more provider resources.</summary>
        ERROR_CLUSTER_RESOURCE_PROVIDER_FAILED = 0x1736,
        /// <summary>[5943] The resource has indicated that it cannot come online on any node.</summary>
        ERROR_CLUSTER_RESOURCE_CONFIGURATION_ERROR = 0x1737,
        /// <summary>[5944] The current operation cannot be performed on this group at this time.</summary>
        ERROR_CLUSTER_GROUP_BUSY = 0x1738,
        /// <summary>[5945] The directory or file is not located on a cluster shared volume.</summary>
        ERROR_CLUSTER_NOT_SHARED_VOLUME = 0x1739,
        /// <summary>[5946] The Security Descriptor does not meet the requirements for a cluster.</summary>
        ERROR_CLUSTER_INVALID_SECURITY_DESCRIPTOR = 0x173A,
        /// <summary>[5947] There is one or more shared volumes resources configured in the cluster. Those resources must be moved to available storage in order for operation to succeed.</summary>
        ERROR_CLUSTER_SHARED_VOLUMES_IN_USE = 0x173B,
        /// <summary>[5948] This group or resource cannot be directly manipulated. Use shared volume APIs to perform desired operation.</summary>
        ERROR_CLUSTER_USE_SHARED_VOLUMES_API = 0x173C,
        /// <summary>[5949] Back up is in progress. Please wait for backup completion before trying this operation again.</summary>
        ERROR_CLUSTER_BACKUP_IN_PROGRESS = 0x173D,
        /// <summary>[5950] The path does not belong to a cluster shared volume.</summary>
        ERROR_NON_CSV_PATH = 0x173E,
        /// <summary>[5951] The cluster shared volume is not locally mounted on this node.</summary>
        ERROR_CSV_VOLUME_NOT_LOCAL = 0x173F,
        /// <summary>[5952] The cluster watchdog is terminating.</summary>
        ERROR_CLUSTER_WATCHDOG_TERMINATING = 0x1740,
        /// <summary>[5953] A resource vetoed a move between two nodes because they are incompatible.</summary>
        ERROR_CLUSTER_RESOURCE_VETOED_MOVE_INCOMPATIBLE_NODES = 0x1741,
        /// <summary>[5954] The request is invalid either because node weight cannot be changed while the cluster is in disk-only quorum mode, or because changing the node weight would violate the minimum cluster quorum requirements.</summary>
        ERROR_CLUSTER_INVALID_NODE_WEIGHT = 0x1742,
        /// <summary>[5955] The resource vetoed the call.</summary>
        ERROR_CLUSTER_RESOURCE_VETOED_CALL = 0x1743,
        /// <summary>[5956] Resource could not start or run because it could not reserve sufficient system resources.</summary>
        ERROR_RESMON_SYSTEM_RESOURCES_LACKING = 0x1744,
        /// <summary>[5957] A resource vetoed a move between two nodes because the destination currently does not have enough resources to complete the operation.</summary>
        ERROR_CLUSTER_RESOURCE_VETOED_MOVE_NOT_ENOUGH_RESOURCES_ON_DESTINATION = 0x1745,
        /// <summary>[5958] A resource vetoed a move between two nodes because the source currently does not have enough resources to complete the operation.</summary>
        ERROR_CLUSTER_RESOURCE_VETOED_MOVE_NOT_ENOUGH_RESOURCES_ON_SOURCE = 0x1746,
        /// <summary>[5959] The requested operation can not be completed because the group is queued for an operation.</summary>
        ERROR_CLUSTER_GROUP_QUEUED = 0x1747,
        /// <summary>[5960] The requested operation can not be completed because a resource has locked status.</summary>
        ERROR_CLUSTER_RESOURCE_LOCKED_STATUS = 0x1748,
        /// <summary>[5961] The resource cannot move to another node because a cluster shared volume vetoed the operation.</summary>
        ERROR_CLUSTER_SHARED_VOLUME_FAILOVER_NOT_ALLOWED = 0x1749,
        /// <summary>[5962] A node drain is already in progress.</summary>
        ERROR_CLUSTER_NODE_DRAIN_IN_PROGRESS = 0x174A,
        /// <summary>[5963] Clustered storage is not connected to the node.</summary>
        ERROR_CLUSTER_DISK_NOT_CONNECTED = 0x174B,
        /// <summary>[5964] The disk is not configured in a way to be used with CSV. CSV disks must have at least one partition that is formatted with NTFS.</summary>
        ERROR_DISK_NOT_CSV_CAPABLE = 0x174C,
        /// <summary>[5965] The resource must be part of the Available Storage group to complete this action.</summary>
        ERROR_RESOURCE_NOT_IN_AVAILABLE_STORAGE = 0x174D,
        /// <summary>[5966] CSVFS failed operation as volume is in redirected mode.</summary>
        ERROR_CLUSTER_SHARED_VOLUME_REDIRECTED = 0x174E,
        /// <summary>[5967] CSVFS failed operation as volume is not in redirected mode.</summary>
        ERROR_CLUSTER_SHARED_VOLUME_NOT_REDIRECTED = 0x174F,
        /// <summary>[5968] Cluster properties cannot be returned at this time.</summary>
        ERROR_CLUSTER_CANNOT_RETURN_PROPERTIES = 0x1750,
        /// <summary>[5969] The clustered disk resource contains software snapshot diff area that are not supported for Cluster Shared Volumes.</summary>
        ERROR_CLUSTER_RESOURCE_CONTAINS_UNSUPPORTED_DIFF_AREA_FOR_SHARED_VOLUMES = 0x1751,
        /// <summary>[5970] The operation cannot be completed because the resource is in maintenance mode.</summary>
        ERROR_CLUSTER_RESOURCE_IS_IN_MAINTENANCE_MODE = 0x1752,
        /// <summary>[5971] The operation cannot be completed because of cluster affinity conflicts.</summary>
        ERROR_CLUSTER_AFFINITY_CONFLICT = 0x1753,
        /// <summary>[5972] The operation cannot be completed because the resource is a replica virtual machine.</summary>
        ERROR_CLUSTER_RESOURCE_IS_REPLICA_VIRTUAL_MACHINE = 0x1754,
        /// <summary>[6000] The specified file could not be encrypted.</summary>
        ERROR_ENCRYPTION_FAILED = 0x1770,
        /// <summary>[6001] The specified file could not be decrypted.</summary>
        ERROR_DECRYPTION_FAILED = 0x1771,
        /// <summary>[6002] The specified file is encrypted and the user does not have the ability to decrypt it.</summary>
        ERROR_FILE_ENCRYPTED = 0x1772,
        /// <summary>[6003] There is no valid encryption recovery policy configured for this system.</summary>
        ERROR_NO_RECOVERY_POLICY = 0x1773,
        /// <summary>[6004] The required encryption driver is not loaded for this system.</summary>
        ERROR_NO_EFS = 0x1774,
        /// <summary>[6005] The file was encrypted with a different encryption driver than is currently loaded.</summary>
        ERROR_WRONG_EFS = 0x1775,
        /// <summary>[6006] There are no EFS keys defined for the user.</summary>
        ERROR_NO_USER_KEYS = 0x1776,
        /// <summary>[6007] The specified file is not encrypted.</summary>
        ERROR_FILE_NOT_ENCRYPTED = 0x1777,
        /// <summary>[6008] The specified file is not in the defined EFS export format.</summary>
        ERROR_NOT_EXPORT_FORMAT = 0x1778,
        /// <summary>[6009] The specified file is read only.</summary>
        ERROR_FILE_READ_ONLY = 0x1779,
        /// <summary>[6010] The directory has been disabled for encryption.</summary>
        ERROR_DIR_EFS_DISALLOWED = 0x177A,
        /// <summary>[6011] The server is not trusted for remote encryption operation.</summary>
        ERROR_EFS_SERVER_NOT_TRUSTED = 0x177B,
        /// <summary>[6012] Recovery policy configured for this system contains invalid recovery certificate.</summary>
        ERROR_BAD_RECOVERY_POLICY = 0x177C,
        /// <summary>[6013] The encryption algorithm used on the source file needs a bigger key buffer than the one on the destination file.</summary>
        ERROR_EFS_ALG_BLOB_TOO_BIG = 0x177D,
        /// <summary>[6014] The disk partition does not support file encryption.</summary>
        ERROR_VOLUME_NOT_SUPPORT_EFS = 0x177E,
        /// <summary>[6015] This machine is disabled for file encryption.</summary>
        ERROR_EFS_DISABLED = 0x177F,
        /// <summary>[6016] A newer system is required to decrypt this encrypted file.</summary>
        ERROR_EFS_VERSION_NOT_SUPPORT = 0x1780,
        /// <summary>[6017] The remote server sent an invalid response for a file being opened with Client Side Encryption.</summary>
        ERROR_CS_ENCRYPTION_INVALID_SERVER_RESPONSE = 0x1781,
        /// <summary>[6018] Client Side Encryption is not supported by the remote server even though it claims to support it.</summary>
        ERROR_CS_ENCRYPTION_UNSUPPORTED_SERVER = 0x1782,
        /// <summary>[6019] File is encrypted and should be opened in Client Side Encryption mode.</summary>
        ERROR_CS_ENCRYPTION_EXISTING_ENCRYPTED_FILE = 0x1783,
        /// <summary>[6020] A new encrypted file is being created and a $EFS needs to be provided.</summary>
        ERROR_CS_ENCRYPTION_NEW_ENCRYPTED_FILE = 0x1784,
        /// <summary>[6021] The SMB client requested a CSE FSCTL on a non-CSE file.</summary>
        ERROR_CS_ENCRYPTION_FILE_NOT_CSE = 0x1785,
        /// <summary>[6022] The requested operation was blocked by policy. For more information, contact your system administrator.</summary>
        ERROR_ENCRYPTION_POLICY_DENIES_OPERATION = 0x1786,
        /// <summary>[6118] The list of servers for this workgroup is not currently available.</summary>
        ERROR_NO_BROWSER_SERVERS_FOUND = 0x17E6,
        /// <summary>[6200] The Task Scheduler service must be configured to run in the System account to function properly. Individual tasks may be configured to run in other accounts.</summary>
        SCHED_E_SERVICE_NOT_LOCALSYSTEM = 0x1838,
        /// <summary>[6600] Log service encountered an invalid log sector.</summary>
        ERROR_LOG_SECTOR_INVALID = 0x19C8,
        /// <summary>[6601] Log service encountered a log sector with invalid block parity.</summary>
        ERROR_LOG_SECTOR_PARITY_INVALID = 0x19C9,
        /// <summary>[6602] Log service encountered a remapped log sector.</summary>
        ERROR_LOG_SECTOR_REMAPPED = 0x19CA,
        /// <summary>[6603] Log service encountered a partial or incomplete log block.</summary>
        ERROR_LOG_BLOCK_INCOMPLETE = 0x19CB,
        /// <summary>[6604] Log service encountered an attempt access data outside the active log range.</summary>
        ERROR_LOG_INVALID_RANGE = 0x19CC,
        /// <summary>[6605] Log service user marshalling buffers are exhausted.</summary>
        ERROR_LOG_BLOCKS_EXHAUSTED = 0x19CD,
        /// <summary>[6606] Log service encountered an attempt read from a marshalling area with an invalid read context.</summary>
        ERROR_LOG_READ_CONTEXT_INVALID = 0x19CE,
        /// <summary>[6607] Log service encountered an invalid log restart area.</summary>
        ERROR_LOG_RESTART_INVALID = 0x19CF,
        /// <summary>[6608] Log service encountered an invalid log block version.</summary>
        ERROR_LOG_BLOCK_VERSION = 0x19D0,
        /// <summary>[6609] Log service encountered an invalid log block.</summary>
        ERROR_LOG_BLOCK_INVALID = 0x19D1,
        /// <summary>[6610] Log service encountered an attempt to read the log with an invalid read mode.</summary>
        ERROR_LOG_READ_MODE_INVALID = 0x19D2,
        /// <summary>[6611] Log service encountered a log stream with no restart area.</summary>
        ERROR_LOG_NO_RESTART = 0x19D3,
        /// <summary>[6612] Log service encountered a corrupted metadata file.</summary>
        ERROR_LOG_METADATA_CORRUPT = 0x19D4,
        /// <summary>[6613] Log service encountered a metadata file that could not be created by the log file system.</summary>
        ERROR_LOG_METADATA_INVALID = 0x19D5,
        /// <summary>[6614] Log service encountered a metadata file with inconsistent data.</summary>
        ERROR_LOG_METADATA_INCONSISTENT = 0x19D6,
        /// <summary>[6615] Log service encountered an attempt to erroneous allocate or dispose reservation space.</summary>
        ERROR_LOG_RESERVATION_INVALID = 0x19D7,
        /// <summary>[6616] Log service cannot delete log file or file system container.</summary>
        ERROR_LOG_CANT_DELETE = 0x19D8,
        /// <summary>[6617] Log service has reached the maximum allowable containers allocated to a log file.</summary>
        ERROR_LOG_CONTAINER_LIMIT_EXCEEDED = 0x19D9,
        /// <summary>[6618] Log service has attempted to read or write backward past the start of the log.</summary>
        ERROR_LOG_START_OF_LOG = 0x19DA,
        /// <summary>[6619] Log policy could not be installed because a policy of the same type is already present.</summary>
        ERROR_LOG_POLICY_ALREADY_INSTALLED = 0x19DB,
        /// <summary>[6620] Log policy in question was not installed at the time of the request.</summary>
        ERROR_LOG_POLICY_NOT_INSTALLED = 0x19DC,
        /// <summary>[6621] The installed set of policies on the log is invalid.</summary>
        ERROR_LOG_POLICY_INVALID = 0x19DD,
        /// <summary>[6622] A policy on the log in question prevented the operation from completing.</summary>
        ERROR_LOG_POLICY_CONFLICT = 0x19DE,
        /// <summary>[6623] Log space cannot be reclaimed because the log is pinned by the archive tail.</summary>
        ERROR_LOG_PINNED_ARCHIVE_TAIL = 0x19DF,
        /// <summary>[6624] Log record is not a record in the log file.</summary>
        ERROR_LOG_RECORD_NONEXISTENT = 0x19E0,
        /// <summary>[6625] Number of reserved log records or the adjustment of the number of reserved log records is invalid.</summary>
        ERROR_LOG_RECORDS_RESERVED_INVALID = 0x19E1,
        /// <summary>[6626] Reserved log space or the adjustment of the log space is invalid.</summary>
        ERROR_LOG_SPACE_RESERVED_INVALID = 0x19E2,
        /// <summary>[6627] An new or existing archive tail or base of the active log is invalid.</summary>
        ERROR_LOG_TAIL_INVALID = 0x19E3,
        /// <summary>[6628] Log space is exhausted.</summary>
        ERROR_LOG_FULL = 0x19E4,
        /// <summary>[6629] The log could not be set to the requested size.</summary>
        ERROR_COULD_NOT_RESIZE_LOG = 0x19E5,
        /// <summary>[6630] Log is multiplexed, no direct writes to the physical log is allowed.</summary>
        ERROR_LOG_MULTIPLEXED = 0x19E6,
        /// <summary>[6631] The operation failed because the log is a dedicated log.</summary>
        ERROR_LOG_DEDICATED = 0x19E7,
        /// <summary>[6632] The operation requires an archive context.</summary>
        ERROR_LOG_ARCHIVE_NOT_IN_PROGRESS = 0x19E8,
        /// <summary>[6633] Log archival is in progress.</summary>
        ERROR_LOG_ARCHIVE_IN_PROGRESS = 0x19E9,
        /// <summary>[6634] The operation requires a non-ephemeral log, but the log is ephemeral.</summary>
        ERROR_LOG_EPHEMERAL = 0x19EA,
        /// <summary>[6635] The log must have at least two containers before it can be read from or written to.</summary>
        ERROR_LOG_NOT_ENOUGH_CONTAINERS = 0x19EB,
        /// <summary>[6636] A log client has already registered on the stream.</summary>
        ERROR_LOG_CLIENT_ALREADY_REGISTERED = 0x19EC,
        /// <summary>[6637] A log client has not been registered on the stream.</summary>
        ERROR_LOG_CLIENT_NOT_REGISTERED = 0x19ED,
        /// <summary>[6638] A request has already been made to handle the log full condition.</summary>
        ERROR_LOG_FULL_HANDLER_IN_PROGRESS = 0x19EE,
        /// <summary>[6639] Log service encountered an error when attempting to read from a log container.</summary>
        ERROR_LOG_CONTAINER_READ_FAILED = 0x19EF,
        /// <summary>[6640] Log service encountered an error when attempting to write to a log container.</summary>
        ERROR_LOG_CONTAINER_WRITE_FAILED = 0x19F0,
        /// <summary>[6641] Log service encountered an error when attempting open a log container.</summary>
        ERROR_LOG_CONTAINER_OPEN_FAILED = 0x19F1,
        /// <summary>[6642] Log service encountered an invalid container state when attempting a requested action.</summary>
        ERROR_LOG_CONTAINER_STATE_INVALID = 0x19F2,
        /// <summary>[6643] Log service is not in the correct state to perform a requested action.</summary>
        ERROR_LOG_STATE_INVALID = 0x19F3,
        /// <summary>[6644] Log space cannot be reclaimed because the log is pinned.</summary>
        ERROR_LOG_PINNED = 0x19F4,
        /// <summary>[6645] Log metadata flush failed.</summary>
        ERROR_LOG_METADATA_FLUSH_FAILED = 0x19F5,
        /// <summary>[6646] Security on the log and its containers is inconsistent.</summary>
        ERROR_LOG_INCONSISTENT_SECURITY = 0x19F6,
        /// <summary>[6647] Records were appended to the log or reservation changes were made, but the log could not be flushed.</summary>
        ERROR_LOG_APPENDED_FLUSH_FAILED = 0x19F7,
        /// <summary>[6648] The log is pinned due to reservation consuming most of the log space. Free some reserved records to make space available.</summary>
        ERROR_LOG_PINNED_RESERVATION = 0x19F8,
        /// <summary>[6700] The transaction handle associated with this operation is not valid.</summary>
        ERROR_INVALID_TRANSACTION = 0x1A2C,
        /// <summary>[6701] The requested operation was made in the context of a transaction that is no longer active.</summary>
        ERROR_TRANSACTION_NOT_ACTIVE = 0x1A2D,
        /// <summary>[6702] The requested operation is not valid on the Transaction object in its current state.</summary>
        ERROR_TRANSACTION_REQUEST_NOT_VALID = 0x1A2E,
        /// <summary>[6703] The caller has called a response API, but the response is not expected because the TM did not issue the corresponding request to the caller.</summary>
        ERROR_TRANSACTION_NOT_REQUESTED = 0x1A2F,
        /// <summary>[6704] It is too late to perform the requested operation, since the Transaction has already been aborted.</summary>
        ERROR_TRANSACTION_ALREADY_ABORTED = 0x1A30,
        /// <summary>[6705] It is too late to perform the requested operation, since the Transaction has already been committed.</summary>
        ERROR_TRANSACTION_ALREADY_COMMITTED = 0x1A31,
        /// <summary>[6706] The Transaction Manager was unable to be successfully initialized. Transacted operations are not supported.</summary>
        ERROR_TM_INITIALIZATION_FAILED = 0x1A32,
        /// <summary>[6707] The specified ResourceManager made no changes or updates to the resource under this transaction.</summary>
        ERROR_RESOURCEMANAGER_READ_ONLY = 0x1A33,
        /// <summary>[6708] The resource manager has attempted to prepare a transaction that it has not successfully joined.</summary>
        ERROR_TRANSACTION_NOT_JOINED = 0x1A34,
        /// <summary>[6709] The Transaction object already has a superior enlistment, and the caller attempted an operation that would have created a new superior. Only a single superior enlistment is allow.</summary>
        ERROR_TRANSACTION_SUPERIOR_EXISTS = 0x1A35,
        /// <summary>[6710] The RM tried to register a protocol that already exists.</summary>
        ERROR_CRM_PROTOCOL_ALREADY_EXISTS = 0x1A36,
        /// <summary>[6711] The attempt to propagate the Transaction failed.</summary>
        ERROR_TRANSACTION_PROPAGATION_FAILED = 0x1A37,
        /// <summary>[6712] The requested propagation protocol was not registered as a CRM.</summary>
        ERROR_CRM_PROTOCOL_NOT_FOUND = 0x1A38,
        /// <summary>[6713] The buffer passed in to PushTransaction or PullTransaction is not in a valid format.</summary>
        ERROR_TRANSACTION_INVALID_MARSHALL_BUFFER = 0x1A39,
        /// <summary>[6714] The current transaction context associated with the thread is not a valid handle to a transaction object.</summary>
        ERROR_CURRENT_TRANSACTION_NOT_VALID = 0x1A3A,
        /// <summary>[6715] The specified Transaction object could not be opened, because it was not found.</summary>
        ERROR_TRANSACTION_NOT_FOUND = 0x1A3B,
        /// <summary>[6716] The specified ResourceManager object could not be opened, because it was not found.</summary>
        ERROR_RESOURCEMANAGER_NOT_FOUND = 0x1A3C,
        /// <summary>[6717] The specified Enlistment object could not be opened, because it was not found.</summary>
        ERROR_ENLISTMENT_NOT_FOUND = 0x1A3D,
        /// <summary>[6718] The specified TransactionManager object could not be opened, because it was not found.</summary>
        ERROR_TRANSACTIONMANAGER_NOT_FOUND = 0x1A3E,
        /// <summary>[6719] The object specified could not be created or opened, because its associated TransactionManager is not online. The TransactionManager must be brought fully Online by calling RecoverTransactionManager to recover to the end of its LogFile before objects in its Transaction or ResourceManager namespaces can be opened. In addition, errors in writing records to its LogFile can cause a TransactionManager to go offline.</summary>
        ERROR_TRANSACTIONMANAGER_NOT_ONLINE = 0x1A3F,
        /// <summary>[6720] The specified TransactionManager was unable to create the objects contained in its logfile in the Ob namespace. Therefore, the TransactionManager was unable to recover.</summary>
        ERROR_TRANSACTIONMANAGER_RECOVERY_NAME_COLLISION = 0x1A40,
        /// <summary>[6721] The call to create a superior Enlistment on this Transaction object could not be completed, because the Transaction object specified for the enlistment is a subordinate branch of the Transaction. Only the root of the Transaction can be enlisted on as a superior.</summary>
        ERROR_TRANSACTION_NOT_ROOT = 0x1A41,
        /// <summary>[6722] Because the associated transaction manager or resource manager has been closed, the handle is no longer valid.</summary>
        ERROR_TRANSACTION_OBJECT_EXPIRED = 0x1A42,
        /// <summary>[6723] The specified operation could not be performed on this Superior enlistment, because the enlistment was not created with the corresponding completion response in the NotificationMask.</summary>
        ERROR_TRANSACTION_RESPONSE_NOT_ENLISTED = 0x1A43,
        /// <summary>[6724] The specified operation could not be performed, because the record that would be logged was too long. This can occur because of two conditions: either there are too many Enlistments on this Transaction, or the combined RecoveryInformation being logged on behalf of those Enlistments is too long.</summary>
        ERROR_TRANSACTION_RECORD_TOO_LONG = 0x1A44,
        /// <summary>[6725] Implicit transaction are not supported.</summary>
        ERROR_IMPLICIT_TRANSACTION_NOT_SUPPORTED = 0x1A45,
        /// <summary>[6726] The kernel transaction manager had to abort or forget the transaction because it blocked forward progress.</summary>
        ERROR_TRANSACTION_INTEGRITY_VIOLATED = 0x1A46,
        /// <summary>[6727] The TransactionManager identity that was supplied did not match the one recorded in the TransactionManager's log file.</summary>
        ERROR_TRANSACTIONMANAGER_IDENTITY_MISMATCH = 0x1A47,
        /// <summary>[6728] This snapshot operation cannot continue because a transactional resource manager cannot be frozen in its current state. Please try again.</summary>
        ERROR_RM_CANNOT_BE_FROZEN_FOR_SNAPSHOT = 0x1A48,
        /// <summary>[6729] The transaction cannot be enlisted on with the specified EnlistmentMask, because the transaction has already completed the PrePrepare phase. In order to ensure correctness, the ResourceManager must switch to a write- through mode and cease caching data within this transaction. Enlisting for only subsequent transaction phases may still succeed.</summary>
        ERROR_TRANSACTION_MUST_WRITETHROUGH = 0x1A49,
        /// <summary>[6730] The transaction does not have a superior enlistment.</summary>
        ERROR_TRANSACTION_NO_SUPERIOR = 0x1A4A,
        /// <summary>[6731] The attempt to commit the Transaction completed, but it is possible that some portion of the transaction tree did not commit successfully due to heuristics. Therefore it is possible that some data modified in the transaction may not have committed, resulting in transactional inconsistency. If possible, check the consistency of the associated data.</summary>
        ERROR_HEURISTIC_DAMAGE_POSSIBLE = 0x1A4B,
        /// <summary>[6800] The function attempted to use a name that is reserved for use by another transaction.</summary>
        ERROR_TRANSACTIONAL_CONFLICT = 0x1A90,
        /// <summary>[6801] Transaction support within the specified resource manager is not started or was shut down due to an error.</summary>
        ERROR_RM_NOT_ACTIVE = 0x1A91,
        /// <summary>[6802] The metadata of the RM has been corrupted. The RM will not function.</summary>
        ERROR_RM_METADATA_CORRUPT = 0x1A92,
        /// <summary>[6803] The specified directory does not contain a resource manager.</summary>
        ERROR_DIRECTORY_NOT_RM = 0x1A93,
        /// <summary>[6805] The remote server or share does not support transacted file operations.</summary>
        ERROR_TRANSACTIONS_UNSUPPORTED_REMOTE = 0x1A95,
        /// <summary>[6806] The requested log size is invalid.</summary>
        ERROR_LOG_RESIZE_INVALID_SIZE = 0x1A96,
        /// <summary>[6807] The object (file, stream, link) corresponding to the handle has been deleted by a Transaction Savepoint Rollback.</summary>
        ERROR_OBJECT_NO_LONGER_EXISTS = 0x1A97,
        /// <summary>[6808] The specified file miniversion was not found for this transacted file open.</summary>
        ERROR_STREAM_MINIVERSION_NOT_FOUND = 0x1A98,
        /// <summary>[6809] The specified file miniversion was found but has been invalidated. Most likely cause is a transaction savepoint rollback.</summary>
        ERROR_STREAM_MINIVERSION_NOT_VALID = 0x1A99,
        /// <summary>[6810] A miniversion may only be opened in the context of the transaction that created it.</summary>
        ERROR_MINIVERSION_INACCESSIBLE_FROM_SPECIFIED_TRANSACTION = 0x1A9A,
        /// <summary>[6811] It is not possible to open a miniversion with modify access.</summary>
        ERROR_CANT_OPEN_MINIVERSION_WITH_MODIFY_INTENT = 0x1A9B,
        /// <summary>[6812] It is not possible to create any more miniversions for this stream.</summary>
        ERROR_CANT_CREATE_MORE_STREAM_MINIVERSIONS = 0x1A9C,
        /// <summary>[6814] The remote server sent mismatching version number or Fid for a file opened with transactions.</summary>
        ERROR_REMOTE_FILE_VERSION_MISMATCH = 0x1A9E,
        /// <summary>[6815] The handle has been invalidated by a transaction. The most likely cause is the presence of memory mapping on a file or an open handle when the transaction ended or rolled back to savepoint.</summary>
        ERROR_HANDLE_NO_LONGER_VALID = 0x1A9F,
        /// <summary>[6816] There is no transaction metadata on the file.</summary>
        ERROR_NO_TXF_METADATA = 0x1AA0,
        /// <summary>[6817] The log data is corrupt.</summary>
        ERROR_LOG_CORRUPTION_DETECTED = 0x1AA1,
        /// <summary>[6818] The file can't be recovered because there is a handle still open on it.</summary>
        ERROR_CANT_RECOVER_WITH_HANDLE_OPEN = 0x1AA2,
        /// <summary>[6819] The transaction outcome is unavailable because the resource manager responsible for it has disconnected.</summary>
        ERROR_RM_DISCONNECTED = 0x1AA3,
        /// <summary>[6820] The request was rejected because the enlistment in question is not a superior enlistment.</summary>
        ERROR_ENLISTMENT_NOT_SUPERIOR = 0x1AA4,
        /// <summary>[6821] The transactional resource manager is already consistent. Recovery is not needed.</summary>
        ERROR_RECOVERY_NOT_NEEDED = 0x1AA5,
        /// <summary>[6822] The transactional resource manager has already been started.</summary>
        ERROR_RM_ALREADY_STARTED = 0x1AA6,
        /// <summary>[6823] The file cannot be opened transactionally, because its identity depends on the outcome of an unresolved transaction.</summary>
        ERROR_FILE_IDENTITY_NOT_PERSISTENT = 0x1AA7,
        /// <summary>[6824] The operation cannot be performed because another transaction is depending on the fact that this property will not change.</summary>
        ERROR_CANT_BREAK_TRANSACTIONAL_DEPENDENCY = 0x1AA8,
        /// <summary>[6825] The operation would involve a single file with two transactional resource managers and is therefore not allowed.</summary>
        ERROR_CANT_CROSS_RM_BOUNDARY = 0x1AA9,
        /// <summary>[6826] The $Txf directory must be empty for this operation to succeed.</summary>
        ERROR_TXF_DIR_NOT_EMPTY = 0x1AAA,
        /// <summary>[6827] The operation would leave a transactional resource manager in an inconsistent state and is therefore not allowed.</summary>
        ERROR_INDOUBT_TRANSACTIONS_EXIST = 0x1AAB,
        /// <summary>[6828] The operation could not be completed because the transaction manager does not have a log.</summary>
        ERROR_TM_VOLATILE = 0x1AAC,
        /// <summary>[6829] A rollback could not be scheduled because a previously scheduled rollback has already executed or been queued for execution.</summary>
        ERROR_ROLLBACK_TIMER_EXPIRED = 0x1AAD,
        /// <summary>[6830] The transactional metadata attribute on the file or directory is corrupt and unreadable.</summary>
        ERROR_TXF_ATTRIBUTE_CORRUPT = 0x1AAE,
        /// <summary>[6831] The encryption operation could not be completed because a transaction is active.</summary>
        ERROR_EFS_NOT_ALLOWED_IN_TRANSACTION = 0x1AAF,
        /// <summary>[6832] This object is not allowed to be opened in a transaction.</summary>
        ERROR_TRANSACTIONAL_OPEN_NOT_ALLOWED = 0x1AB0,
        /// <summary>[6833] An attempt to create space in the transactional resource manager's log failed. The failure status has been recorded in the event log.</summary>
        ERROR_LOG_GROWTH_FAILED = 0x1AB1,
        /// <summary>[6834] Memory mapping (creating a mapped section) a remote file under a transaction is not supported.</summary>
        ERROR_TRANSACTED_MAPPING_UNSUPPORTED_REMOTE = 0x1AB2,
        /// <summary>[6835] Transaction metadata is already present on this file and cannot be superseded.</summary>
        ERROR_TXF_METADATA_ALREADY_PRESENT = 0x1AB3,
        /// <summary>[6836] A transaction scope could not be entered because the scope handler has not been initialized.</summary>
        ERROR_TRANSACTION_SCOPE_CALLBACKS_NOT_SET = 0x1AB4,
        /// <summary>[6837] Promotion was required in order to allow the resource manager to enlist, but the transaction was set to disallow it.</summary>
        ERROR_TRANSACTION_REQUIRED_PROMOTION = 0x1AB5,
        /// <summary>[6838] This file is open for modification in an unresolved transaction and may be opened for execute only by a transacted reader.</summary>
        ERROR_CANNOT_EXECUTE_FILE_IN_TRANSACTION = 0x1AB6,
        /// <summary>[6839] The request to thaw frozen transactions was ignored because transactions had not previously been frozen.</summary>
        ERROR_TRANSACTIONS_NOT_FROZEN = 0x1AB7,
        /// <summary>[6840] Transactions cannot be frozen because a freeze is already in progress.</summary>
        ERROR_TRANSACTION_FREEZE_IN_PROGRESS = 0x1AB8,
        /// <summary>[6841] The target volume is not a snapshot volume. This operation is only valid on a volume mounted as a snapshot.</summary>
        ERROR_NOT_SNAPSHOT_VOLUME = 0x1AB9,
        /// <summary>[6842] The savepoint operation failed because files are open on the transaction. This is not permitted.</summary>
        ERROR_NO_SAVEPOINT_WITH_OPEN_FILES = 0x1ABA,
        /// <summary>[6843] Windows has discovered corruption in a file, and that file has since been repaired. Data loss may have occurred.</summary>
        ERROR_DATA_LOST_REPAIR = 0x1ABB,
        /// <summary>[6844] The sparse operation could not be completed because a transaction is active on the file.</summary>
        ERROR_SPARSE_NOT_ALLOWED_IN_TRANSACTION = 0x1ABC,
        /// <summary>[6845] The call to create a TransactionManager object failed because the Tm Identity stored in the logfile does not match the Tm Identity that was passed in as an argument.</summary>
        ERROR_TM_IDENTITY_MISMATCH = 0x1ABD,
        /// <summary>[6846] I/O was attempted on a section object that has been floated as a result of a transaction ending. There is no valid data.</summary>
        ERROR_FLOATED_SECTION = 0x1ABE,
        /// <summary>[6847] The transactional resource manager cannot currently accept transacted work due to a transient condition such as low resources.</summary>
        ERROR_CANNOT_ACCEPT_TRANSACTED_WORK = 0x1ABF,
        /// <summary>[6848] The transactional resource manager had too many transactions outstanding that could not be aborted. The transactional resource manger has been shut down.</summary>
        ERROR_CANNOT_ABORT_TRANSACTIONS = 0x1AC0,
        /// <summary>[6849] The operation could not be completed due to bad clusters on disk.</summary>
        ERROR_BAD_CLUSTERS = 0x1AC1,
        /// <summary>[6850] The compression operation could not be completed because a transaction is active on the file.</summary>
        ERROR_COMPRESSION_NOT_ALLOWED_IN_TRANSACTION = 0x1AC2,
        /// <summary>[6851] The operation could not be completed because the volume is dirty. Please run chkdsk and try again.</summary>
        ERROR_VOLUME_DIRTY = 0x1AC3,
        /// <summary>[6852] The link tracking operation could not be completed because a transaction is active.</summary>
        ERROR_NO_LINK_TRACKING_IN_TRANSACTION = 0x1AC4,
        /// <summary>[6853] This operation cannot be performed in a transaction.</summary>
        ERROR_OPERATION_NOT_SUPPORTED_IN_TRANSACTION = 0x1AC5,
        /// <summary>[6854] The handle is no longer properly associated with its transaction. It may have been opened in a transactional resource manager that was subsequently forced to restart. Please close the handle and open a new one.</summary>
        ERROR_EXPIRED_HANDLE = 0x1AC6,
        /// <summary>[6855] The specified operation could not be performed because the resource manager is not enlisted in the transaction.</summary>
        ERROR_TRANSACTION_NOT_ENLISTED = 0x1AC7,
        /// <summary>[7001] The specified session name is invalid.</summary>
        ERROR_CTX_WINSTATION_NAME_INVALID = 0x1B59,
        /// <summary>[7002] The specified protocol driver is invalid.</summary>
        ERROR_CTX_INVALID_PD = 0x1B5A,
        /// <summary>[7003] The specified protocol driver was not found in the system path.</summary>
        ERROR_CTX_PD_NOT_FOUND = 0x1B5B,
        /// <summary>[7004] The specified terminal connection driver was not found in the system path.</summary>
        ERROR_CTX_WD_NOT_FOUND = 0x1B5C,
        /// <summary>[7005] A registry key for event logging could not be created for this session.</summary>
        ERROR_CTX_CANNOT_MAKE_EVENTLOG_ENTRY = 0x1B5D,
        /// <summary>[7006] A service with the same name already exists on the system.</summary>
        ERROR_CTX_SERVICE_NAME_COLLISION = 0x1B5E,
        /// <summary>[7007] A close operation is pending on the session.</summary>
        ERROR_CTX_CLOSE_PENDING = 0x1B5F,
        /// <summary>[7008] There are no free output buffers available.</summary>
        ERROR_CTX_NO_OUTBUF = 0x1B60,
        /// <summary>[7009] The MODEM.INF file was not found.</summary>
        ERROR_CTX_MODEM_INF_NOT_FOUND = 0x1B61,
        /// <summary>[7010] The modem name was not found in MODEM.INF.</summary>
        ERROR_CTX_INVALID_MODEMNAME = 0x1B62,
        /// <summary>[7011] The modem did not accept the command sent to it. Verify that the configured modem name matches the attached modem.</summary>
        ERROR_CTX_MODEM_RESPONSE_ERROR = 0x1B63,
        /// <summary>[7012] The modem did not respond to the command sent to it. Verify that the modem is properly cabled and powered on.</summary>
        ERROR_CTX_MODEM_RESPONSE_TIMEOUT = 0x1B64,
        /// <summary>[7013] Carrier detect has failed or carrier has been dropped due to disconnect.</summary>
        ERROR_CTX_MODEM_RESPONSE_NO_CARRIER = 0x1B65,
        /// <summary>[7014] Dial tone not detected within the required time. Verify that the phone cable is properly attached and functional.</summary>
        ERROR_CTX_MODEM_RESPONSE_NO_DIALTONE = 0x1B66,
        /// <summary>[7015] Busy signal detected at remote site on callback.</summary>
        ERROR_CTX_MODEM_RESPONSE_BUSY = 0x1B67,
        /// <summary>[7016] Voice detected at remote site on callback.</summary>
        ERROR_CTX_MODEM_RESPONSE_VOICE = 0x1B68,
        /// <summary>[7017] Transport driver error.</summary>
        ERROR_CTX_TD_ERROR = 0x1B69,
        /// <summary>[7022] The specified session cannot be found.</summary>
        ERROR_CTX_WINSTATION_NOT_FOUND = 0x1B6E,
        /// <summary>[7023] The specified session name is already in use.</summary>
        ERROR_CTX_WINSTATION_ALREADY_EXISTS = 0x1B6F,
        /// <summary>[7024] The task you are trying to do can't be completed because Remote Desktop Services is currently busy. Please try again in a few minutes. Other users should still be able to log on.</summary>
        ERROR_CTX_WINSTATION_BUSY = 0x1B70,
        /// <summary>[7025] An attempt has been made to connect to a session whose video mode is not supported by the current client.</summary>
        ERROR_CTX_BAD_VIDEO_MODE = 0x1B71,
        /// <summary>[7035] The application attempted to enable DOS graphics mode. DOS graphics mode is not supported.</summary>
        ERROR_CTX_GRAPHICS_INVALID = 0x1B7B,
        /// <summary>[7037] Your interactive logon privilege has been disabled. Please contact your administrator.</summary>
        ERROR_CTX_LOGON_DISABLED = 0x1B7D,
        /// <summary>[7038] The requested operation can be performed only on the system console. This is most often the result of a driver or system DLL requiring direct console access.</summary>
        ERROR_CTX_NOT_CONSOLE = 0x1B7E,
        /// <summary>[7040] The client failed to respond to the server connect message.</summary>
        ERROR_CTX_CLIENT_QUERY_TIMEOUT = 0x1B80,
        /// <summary>[7041] Disconnecting the console session is not supported.</summary>
        ERROR_CTX_CONSOLE_DISCONNECT = 0x1B81,
        /// <summary>[7042] Reconnecting a disconnected session to the console is not supported.</summary>
        ERROR_CTX_CONSOLE_CONNECT = 0x1B82,
        /// <summary>[7044] The request to control another session remotely was denied.</summary>
        ERROR_CTX_SHADOW_DENIED = 0x1B84,
        /// <summary>[7045] The requested session access is denied.</summary>
        ERROR_CTX_WINSTATION_ACCESS_DENIED = 0x1B85,
        /// <summary>[7049] The specified terminal connection driver is invalid.</summary>
        ERROR_CTX_INVALID_WD = 0x1B89,
        /// <summary>[7050] The requested session cannot be controlled remotely. This may be because the session is disconnected or does not currently have a user logged on.</summary>
        ERROR_CTX_SHADOW_INVALID = 0x1B8A,
        /// <summary>[7051] The requested session is not configured to allow remote control.</summary>
        ERROR_CTX_SHADOW_DISABLED = 0x1B8B,
        /// <summary>[7052] Your request to connect to this Terminal Server has been rejected. Your Terminal Server client license number is currently being used by another user. Please call your system administrator to obtain a unique license number.</summary>
        ERROR_CTX_CLIENT_LICENSE_IN_USE = 0x1B8C,
        /// <summary>[7053] Your request to connect to this Terminal Server has been rejected. Your Terminal Server client license number has not been entered for this copy of the Terminal Server client. Please contact your system administrator.</summary>
        ERROR_CTX_CLIENT_LICENSE_NOT_SET = 0x1B8D,
        /// <summary>[7054] The number of connections to this computer is limited and all connections are in use right now. Try connecting later or contact your system administrator.</summary>
        ERROR_CTX_LICENSE_NOT_AVAILABLE = 0x1B8E,
        /// <summary>[7055] The client you are using is not licensed to use this system. Your logon request is denied.</summary>
        ERROR_CTX_LICENSE_CLIENT_INVALID = 0x1B8F,
        /// <summary>[7056] The system license has expired. Your logon request is denied.</summary>
        ERROR_CTX_LICENSE_EXPIRED = 0x1B90,
        /// <summary>[7057] Remote control could not be terminated because the specified session is not currently being remotely controlled.</summary>
        ERROR_CTX_SHADOW_NOT_RUNNING = 0x1B91,
        /// <summary>[7058] The remote control of the console was terminated because the display mode was changed. Changing the display mode in a remote control session is not supported.</summary>
        ERROR_CTX_SHADOW_ENDED_BY_MODE_CHANGE = 0x1B92,
        /// <summary>[7059] Activation has already been reset the maximum number of times for this installation. Your activation timer will not be cleared.</summary>
        ERROR_ACTIVATION_COUNT_EXCEEDED = 0x1B93,
        /// <summary>[7060] Remote logins are currently disabled.</summary>
        ERROR_CTX_WINSTATIONS_DISABLED = 0x1B94,
        /// <summary>[7061] You do not have the proper encryption level to access this Session.</summary>
        ERROR_CTX_ENCRYPTION_LEVEL_REQUIRED = 0x1B95,
        /// <summary>[7062] The user %s\\%s is currently logged on to this computer. Only the current user or an administrator can log on to this computer.</summary>
        ERROR_CTX_SESSION_IN_USE = 0x1B96,
        /// <summary>[7063] The user %s\\%s is already logged on to the console of this computer. You do not have permission to log in at this time. To resolve this issue, contact %s\\%s and have them log off.</summary>
        ERROR_CTX_NO_FORCE_LOGOFF = 0x1B97,
        /// <summary>[7064] Unable to log you on because of an account restriction.</summary>
        ERROR_CTX_ACCOUNT_RESTRICTION = 0x1B98,
        /// <summary>[7065] The RDP protocol component %2 detected an error in the protocol stream and has disconnected the client.</summary>
        ERROR_RDP_PROTOCOL_ERROR = 0x1B99,
        /// <summary>[7066] The Client Drive Mapping Service Has Connected on Terminal Connection.</summary>
        ERROR_CTX_CDM_CONNECT = 0x1B9A,
        /// <summary>[7067] The Client Drive Mapping Service Has Disconnected on Terminal Connection.</summary>
        ERROR_CTX_CDM_DISCONNECT = 0x1B9B,
        /// <summary>[7068] The Terminal Server security layer detected an error in the protocol stream and has disconnected the client.</summary>
        ERROR_CTX_SECURITY_LAYER_ERROR = 0x1B9C,
        /// <summary>[7069] The target session is incompatible with the current session.</summary>
        ERROR_TS_INCOMPATIBLE_SESSIONS = 0x1B9D,
        /// <summary>[7070] Windows can't connect to your session because a problem occurred in the Windows video subsystem. Try connecting again later, or contact the server administrator for assistance.</summary>
        ERROR_TS_VIDEO_SUBSYSTEM_ERROR = 0x1B9E,
        /// <summary>[8001] The file replication service API was called incorrectly.</summary>
        FRS_ERR_INVALID_API_SEQUENCE = 0x1F41,
        /// <summary>[8002] The file replication service cannot be started.</summary>
        FRS_ERR_STARTING_SERVICE = 0x1F42,
        /// <summary>[8003] The file replication service cannot be stopped.</summary>
        FRS_ERR_STOPPING_SERVICE = 0x1F43,
        /// <summary>[8004] The file replication service API terminated the request. The event log may have more information.</summary>
        FRS_ERR_INTERNAL_API = 0x1F44,
        /// <summary>[8005] The file replication service terminated the request. The event log may have more information.</summary>
        FRS_ERR_INTERNAL = 0x1F45,
        /// <summary>[8006] The file replication service cannot be contacted. The event log may have more information.</summary>
        FRS_ERR_SERVICE_COMM = 0x1F46,
        /// <summary>[8007] The file replication service cannot satisfy the request because the user has insufficient privileges. The event log may have more information.</summary>
        FRS_ERR_INSUFFICIENT_PRIV = 0x1F47,
        /// <summary>[8008] The file replication service cannot satisfy the request because authenticated RPC is not available. The event log may have more information.</summary>
        FRS_ERR_AUTHENTICATION = 0x1F48,
        /// <summary>[8009] The file replication service cannot satisfy the request because the user has insufficient privileges on the domain controller. The event log may have more information.</summary>
        FRS_ERR_PARENT_INSUFFICIENT_PRIV = 0x1F49,
        /// <summary>[8010] The file replication service cannot satisfy the request because authenticated RPC is not available on the domain controller. The event log may have more information.</summary>
        FRS_ERR_PARENT_AUTHENTICATION = 0x1F4A,
        /// <summary>[8011] The file replication service cannot communicate with the file replication service on the domain controller. The event log may have more information.</summary>
        FRS_ERR_CHILD_TO_PARENT_COMM = 0x1F4B,
        /// <summary>[8012] The file replication service on the domain controller cannot communicate with the file replication service on this computer. The event log may have more information.</summary>
        FRS_ERR_PARENT_TO_CHILD_COMM = 0x1F4C,
        /// <summary>[8013] The file replication service cannot populate the system volume because of an internal error. The event log may have more information.</summary>
        FRS_ERR_SYSVOL_POPULATE = 0x1F4D,
        /// <summary>[8014] The file replication service cannot populate the system volume because of an internal timeout. The event log may have more information.</summary>
        FRS_ERR_SYSVOL_POPULATE_TIMEOUT = 0x1F4E,
        /// <summary>[8015] The file replication service cannot process the request. The system volume is busy with a previous request.</summary>
        FRS_ERR_SYSVOL_IS_BUSY = 0x1F4F,
        /// <summary>[8016] The file replication service cannot stop replicating the system volume because of an internal error. The event log may have more information.</summary>
        FRS_ERR_SYSVOL_DEMOTE = 0x1F50,
        /// <summary>[8017] The file replication service detected an invalid parameter.</summary>
        FRS_ERR_INVALID_SERVICE_PARAMETER = 0x1F51,
        /// <summary>[8200] An error occurred while installing the directory service. For more information, see the event log.</summary>
        ERROR_DS_NOT_INSTALLED = 0x2008,
        /// <summary>[8201] The directory service evaluated group memberships locally.</summary>
        ERROR_DS_MEMBERSHIP_EVALUATED_LOCALLY = 0x2009,
        /// <summary>[8202] The specified directory service attribute or value does not exist.</summary>
        ERROR_DS_NO_ATTRIBUTE_OR_VALUE = 0x200A,
        /// <summary>[8203] The attribute syntax specified to the directory service is invalid.</summary>
        ERROR_DS_INVALID_ATTRIBUTE_SYNTAX = 0x200B,
        /// <summary>[8204] The attribute type specified to the directory service is not defined.</summary>
        ERROR_DS_ATTRIBUTE_TYPE_UNDEFINED = 0x200C,
        /// <summary>[8205] The specified directory service attribute or value already exists.</summary>
        ERROR_DS_ATTRIBUTE_OR_VALUE_EXISTS = 0x200D,
        /// <summary>[8206] The directory service is busy.</summary>
        ERROR_DS_BUSY = 0x200E,
        /// <summary>[8207] The directory service is unavailable.</summary>
        ERROR_DS_UNAVAILABLE = 0x200F,
        /// <summary>[8208] The directory service was unable to allocate a relative identifier.</summary>
        ERROR_DS_NO_RIDS_ALLOCATED = 0x2010,
        /// <summary>[8209] The directory service has exhausted the pool of relative identifiers.</summary>
        ERROR_DS_NO_MORE_RIDS = 0x2011,
        /// <summary>[8210] The requested operation could not be performed because the directory service is not the master for that type of operation.</summary>
        ERROR_DS_INCORRECT_ROLE_OWNER = 0x2012,
        /// <summary>[8211] The directory service was unable to initialize the subsystem that allocates relative identifiers.</summary>
        ERROR_DS_RIDMGR_INIT_ERROR = 0x2013,
        /// <summary>[8212] The requested operation did not satisfy one or more constraints associated with the class of the object.</summary>
        ERROR_DS_OBJ_CLASS_VIOLATION = 0x2014,
        /// <summary>[8213] The directory service can perform the requested operation only on a leaf object.</summary>
        ERROR_DS_CANT_ON_NON_LEAF = 0x2015,
        /// <summary>[8214] The directory service cannot perform the requested operation on the RDN attribute of an object.</summary>
        ERROR_DS_CANT_ON_RDN = 0x2016,
        /// <summary>[8215] The directory service detected an attempt to modify the object class of an object.</summary>
        ERROR_DS_CANT_MOD_OBJ_CLASS = 0x2017,
        /// <summary>[8216] The requested cross-domain move operation could not be performed.</summary>
        ERROR_DS_CROSS_DOM_MOVE_ERROR = 0x2018,
        /// <summary>[8217] Unable to contact the global catalog server.</summary>
        ERROR_DS_GC_NOT_AVAILABLE = 0x2019,
        /// <summary>[8218] The policy object is shared and can only be modified at the root.</summary>
        ERROR_SHARED_POLICY = 0x201A,
        /// <summary>[8219] The policy object does not exist.</summary>
        ERROR_POLICY_OBJECT_NOT_FOUND = 0x201B,
        /// <summary>[8220] The requested policy information is only in the directory service.</summary>
        ERROR_POLICY_ONLY_IN_DS = 0x201C,
        /// <summary>[8221] A domain controller promotion is currently active.</summary>
        ERROR_PROMOTION_ACTIVE = 0x201D,
        /// <summary>[8222] A domain controller promotion is not currently active.</summary>
        ERROR_NO_PROMOTION_ACTIVE = 0x201E,
        /// <summary>[8224] An operations error occurred.</summary>
        ERROR_DS_OPERATIONS_ERROR = 0x2020,
        /// <summary>[8225] A protocol error occurred.</summary>
        ERROR_DS_PROTOCOL_ERROR = 0x2021,
        /// <summary>[8226] The time limit for this request was exceeded.</summary>
        ERROR_DS_TIMELIMIT_EXCEEDED = 0x2022,
        /// <summary>[8227] The size limit for this request was exceeded.</summary>
        ERROR_DS_SIZELIMIT_EXCEEDED = 0x2023,
        /// <summary>[8228] The administrative limit for this request was exceeded.</summary>
        ERROR_DS_ADMIN_LIMIT_EXCEEDED = 0x2024,
        /// <summary>[8229] The compare response was false.</summary>
        ERROR_DS_COMPARE_FALSE = 0x2025,
        /// <summary>[8230] The compare response was true.</summary>
        ERROR_DS_COMPARE_TRUE = 0x2026,
        /// <summary>[8231] The requested authentication method is not supported by the server.</summary>
        ERROR_DS_AUTH_METHOD_NOT_SUPPORTED = 0x2027,
        /// <summary>[8232] A more secure authentication method is required for this server.</summary>
        ERROR_DS_STRONG_AUTH_REQUIRED = 0x2028,
        /// <summary>[8233] Inappropriate authentication.</summary>
        ERROR_DS_INAPPROPRIATE_AUTH = 0x2029,
        /// <summary>[8234] The authentication mechanism is unknown.</summary>
        ERROR_DS_AUTH_UNKNOWN = 0x202A,
        /// <summary>[8235] A referral was returned from the server.</summary>
        ERROR_DS_REFERRAL = 0x202B,
        /// <summary>[8236] The server does not support the requested critical extension.</summary>
        ERROR_DS_UNAVAILABLE_CRIT_EXTENSION = 0x202C,
        /// <summary>[8237] This request requires a secure connection.</summary>
        ERROR_DS_CONFIDENTIALITY_REQUIRED = 0x202D,
        /// <summary>[8238] Inappropriate matching.</summary>
        ERROR_DS_INAPPROPRIATE_MATCHING = 0x202E,
        /// <summary>[8239] A constraint violation occurred.</summary>
        ERROR_DS_CONSTRAINT_VIOLATION = 0x202F,
        /// <summary>[8240] There is no such object on the server.</summary>
        ERROR_DS_NO_SUCH_OBJECT = 0x2030,
        /// <summary>[8241] There is an alias problem.</summary>
        ERROR_DS_ALIAS_PROBLEM = 0x2031,
        /// <summary>[8242] An invalid dn syntax has been specified.</summary>
        ERROR_DS_INVALID_DN_SYNTAX = 0x2032,
        /// <summary>[8243] The object is a leaf object.</summary>
        ERROR_DS_IS_LEAF = 0x2033,
        /// <summary>[8244] There is an alias dereferencing problem.</summary>
        ERROR_DS_ALIAS_DEREF_PROBLEM = 0x2034,
        /// <summary>[8245] The server is unwilling to process the request.</summary>
        ERROR_DS_UNWILLING_TO_PERFORM = 0x2035,
        /// <summary>[8246] A loop has been detected.</summary>
        ERROR_DS_LOOP_DETECT = 0x2036,
        /// <summary>[8247] There is a naming violation.</summary>
        ERROR_DS_NAMING_VIOLATION = 0x2037,
        /// <summary>[8248] The result set is too large.</summary>
        ERROR_DS_OBJECT_RESULTS_TOO_LARGE = 0x2038,
        /// <summary>[8249] The operation affects multiple DSAs.</summary>
        ERROR_DS_AFFECTS_MULTIPLE_DSAS = 0x2039,
        /// <summary>[8250] The server is not operational.</summary>
        ERROR_DS_SERVER_DOWN = 0x203A,
        /// <summary>[8251] A local error has occurred.</summary>
        ERROR_DS_LOCAL_ERROR = 0x203B,
        /// <summary>[8252] An encoding error has occurred.</summary>
        ERROR_DS_ENCODING_ERROR = 0x203C,
        /// <summary>[8253] A decoding error has occurred.</summary>
        ERROR_DS_DECODING_ERROR = 0x203D,
        /// <summary>[8254] The search filter cannot be recognized.</summary>
        ERROR_DS_FILTER_UNKNOWN = 0x203E,
        /// <summary>[8255] One or more parameters are illegal.</summary>
        ERROR_DS_PARAM_ERROR = 0x203F,
        /// <summary>[8256] The specified method is not supported.</summary>
        ERROR_DS_NOT_SUPPORTED = 0x2040,
        /// <summary>[8257] No results were returned.</summary>
        ERROR_DS_NO_RESULTS_RETURNED = 0x2041,
        /// <summary>[8258] The specified control is not supported by the server.</summary>
        ERROR_DS_CONTROL_NOT_FOUND = 0x2042,
        /// <summary>[8259] A referral loop was detected by the client.</summary>
        ERROR_DS_CLIENT_LOOP = 0x2043,
        /// <summary>[8260] The preset referral limit was exceeded.</summary>
        ERROR_DS_REFERRAL_LIMIT_EXCEEDED = 0x2044,
        /// <summary>[8261] The search requires a SORT control.</summary>
        ERROR_DS_SORT_CONTROL_MISSING = 0x2045,
        /// <summary>[8262] The search results exceed the offset range specified.</summary>
        ERROR_DS_OFFSET_RANGE_ERROR = 0x2046,
        /// <summary>[8263] The directory service detected the subsystem that allocates relative identifiers is disabled. This can occur as a protective mechanism when the system determines a significant portion of relative identifiers (RIDs) have been exhausted. Please see https://go.microsoft.com/fwlink/p/?linkid=228610 for recommended diagnostic steps and the procedure to re-enable account creation.</summary>
        ERROR_DS_RIDMGR_DISABLED = 0x2047,
        /// <summary>[8301] The root object must be the head of a naming context. The root object cannot have an instantiated parent.</summary>
        ERROR_DS_ROOT_MUST_BE_NC = 0x206D,
        /// <summary>[8302] The add replica operation cannot be performed. The naming context must be writeable in order to create the replica.</summary>
        ERROR_DS_ADD_REPLICA_INHIBITED = 0x206E,
        /// <summary>[8303] A reference to an attribute that is not defined in the schema occurred.</summary>
        ERROR_DS_ATT_NOT_DEF_IN_SCHEMA = 0x206F,
        /// <summary>[8304] The maximum size of an object has been exceeded.</summary>
        ERROR_DS_MAX_OBJ_SIZE_EXCEEDED = 0x2070,
        /// <summary>[8305] An attempt was made to add an object to the directory with a name that is already in use.</summary>
        ERROR_DS_OBJ_STRING_NAME_EXISTS = 0x2071,
        /// <summary>[8306] An attempt was made to add an object of a class that does not have an RDN defined in the schema.</summary>
        ERROR_DS_NO_RDN_DEFINED_IN_SCHEMA = 0x2072,
        /// <summary>[8307] An attempt was made to add an object using an RDN that is not the RDN defined in the schema.</summary>
        ERROR_DS_RDN_DOESNT_MATCH_SCHEMA = 0x2073,
        /// <summary>[8308] None of the requested attributes were found on the objects.</summary>
        ERROR_DS_NO_REQUESTED_ATTS_FOUND = 0x2074,
        /// <summary>[8309] The user buffer is too small.</summary>
        ERROR_DS_USER_BUFFER_TO_SMALL = 0x2075,
        /// <summary>[8310] The attribute specified in the operation is not present on the object.</summary>
        ERROR_DS_ATT_IS_NOT_ON_OBJ = 0x2076,
        /// <summary>[8311] Illegal modify operation. Some aspect of the modification is not permitted.</summary>
        ERROR_DS_ILLEGAL_MOD_OPERATION = 0x2077,
        /// <summary>[8312] The specified object is too large.</summary>
        ERROR_DS_OBJ_TOO_LARGE = 0x2078,
        /// <summary>[8313] The specified instance type is not valid.</summary>
        ERROR_DS_BAD_INSTANCE_TYPE = 0x2079,
        /// <summary>[8314] The operation must be performed at a master DSA.</summary>
        ERROR_DS_MASTERDSA_REQUIRED = 0x207A,
        /// <summary>[8315] The object class attribute must be specified.</summary>
        ERROR_DS_OBJECT_CLASS_REQUIRED = 0x207B,
        /// <summary>[8316] A required attribute is missing.</summary>
        ERROR_DS_MISSING_REQUIRED_ATT = 0x207C,
        /// <summary>[8317] An attempt was made to modify an object to include an attribute that is not legal for its class.</summary>
        ERROR_DS_ATT_NOT_DEF_FOR_CLASS = 0x207D,
        /// <summary>[8318] The specified attribute is already present on the object.</summary>
        ERROR_DS_ATT_ALREADY_EXISTS = 0x207E,
        /// <summary>[8320] The specified attribute is not present, or has no values.</summary>
        ERROR_DS_CANT_ADD_ATT_VALUES = 0x2080,
        /// <summary>[8321] Multiple values were specified for an attribute that can have only one value.</summary>
        ERROR_DS_SINGLE_VALUE_CONSTRAINT = 0x2081,
        /// <summary>[8322] A value for the attribute was not in the acceptable range of values.</summary>
        ERROR_DS_RANGE_CONSTRAINT = 0x2082,
        /// <summary>[8323] The specified value already exists.</summary>
        ERROR_DS_ATT_VAL_ALREADY_EXISTS = 0x2083,
        /// <summary>[8324] The attribute cannot be removed because it is not present on the object.</summary>
        ERROR_DS_CANT_REM_MISSING_ATT = 0x2084,
        /// <summary>[8325] The attribute value cannot be removed because it is not present on the object.</summary>
        ERROR_DS_CANT_REM_MISSING_ATT_VAL = 0x2085,
        /// <summary>[8326] The specified root object cannot be a subref.</summary>
        ERROR_DS_ROOT_CANT_BE_SUBREF = 0x2086,
        /// <summary>[8327] Chaining is not permitted.</summary>
        ERROR_DS_NO_CHAINING = 0x2087,
        /// <summary>[8328] Chained evaluation is not permitted.</summary>
        ERROR_DS_NO_CHAINED_EVAL = 0x2088,
        /// <summary>[8329] The operation could not be performed because the object's parent is either uninstantiated or deleted.</summary>
        ERROR_DS_NO_PARENT_OBJECT = 0x2089,
        /// <summary>[8330] Having a parent that is an alias is not permitted. Aliases are leaf objects.</summary>
        ERROR_DS_PARENT_IS_AN_ALIAS = 0x208A,
        /// <summary>[8331] The object and parent must be of the same type, either both masters or both replicas.</summary>
        ERROR_DS_CANT_MIX_MASTER_AND_REPS = 0x208B,
        /// <summary>[8332] The operation cannot be performed because child objects exist. This operation can only be performed on a leaf object.</summary>
        ERROR_DS_CHILDREN_EXIST = 0x208C,
        /// <summary>[8333] Directory object not found.</summary>
        ERROR_DS_OBJ_NOT_FOUND = 0x208D,
        /// <summary>[8334] The aliased object is missing.</summary>
        ERROR_DS_ALIASED_OBJ_MISSING = 0x208E,
        /// <summary>[8335] The object name has bad syntax.</summary>
        ERROR_DS_BAD_NAME_SYNTAX = 0x208F,
        /// <summary>[8336] It is not permitted for an alias to refer to another alias.</summary>
        ERROR_DS_ALIAS_POINTS_TO_ALIAS = 0x2090,
        /// <summary>[8337] The alias cannot be dereferenced.</summary>
        ERROR_DS_CANT_DEREF_ALIAS = 0x2091,
        /// <summary>[8338] The operation is out of scope.</summary>
        ERROR_DS_OUT_OF_SCOPE = 0x2092,
        /// <summary>[8339] The operation cannot continue because the object is in the process of being removed.</summary>
        ERROR_DS_OBJECT_BEING_REMOVED = 0x2093,
        /// <summary>[8340] The DSA object cannot be deleted.</summary>
        ERROR_DS_CANT_DELETE_DSA_OBJ = 0x2094,
        /// <summary>[8341] A directory service error has occurred.</summary>
        ERROR_DS_GENERIC_ERROR = 0x2095,
        /// <summary>[8342] The operation can only be performed on an internal master DSA object.</summary>
        ERROR_DS_DSA_MUST_BE_INT_MASTER = 0x2096,
        /// <summary>[8343] The object must be of class DSA.</summary>
        ERROR_DS_CLASS_NOT_DSA = 0x2097,
        /// <summary>[8344] Insufficient access rights to perform the operation.</summary>
        ERROR_DS_INSUFF_ACCESS_RIGHTS = 0x2098,
        /// <summary>[8345] The object cannot be added because the parent is not on the list of possible superiors.</summary>
        ERROR_DS_ILLEGAL_SUPERIOR = 0x2099,
        /// <summary>[8346] Access to the attribute is not permitted because the attribute is owned by the Security Accounts Manager (SAM).</summary>
        ERROR_DS_ATTRIBUTE_OWNED_BY_SAM = 0x209A,
        /// <summary>[8347] The name has too many parts.</summary>
        ERROR_DS_NAME_TOO_MANY_PARTS = 0x209B,
        /// <summary>[8348] The name is too long.</summary>
        ERROR_DS_NAME_TOO_LONG = 0x209C,
        /// <summary>[8349] The name value is too long.</summary>
        ERROR_DS_NAME_VALUE_TOO_LONG = 0x209D,
        /// <summary>[8350] The directory service encountered an error parsing a name.</summary>
        ERROR_DS_NAME_UNPARSEABLE = 0x209E,
        /// <summary>[8351] The directory service cannot get the attribute type for a name.</summary>
        ERROR_DS_NAME_TYPE_UNKNOWN = 0x209F,
        /// <summary>[8352] The name does not identify an object; the name identifies a phantom.</summary>
        ERROR_DS_NOT_AN_OBJECT = 0x20A0,
        /// <summary>[8353] The security descriptor is too short.</summary>
        ERROR_DS_SEC_DESC_TOO_SHORT = 0x20A1,
        /// <summary>[8354] The security descriptor is invalid.</summary>
        ERROR_DS_SEC_DESC_INVALID = 0x20A2,
        /// <summary>[8355] Failed to create name for deleted object.</summary>
        ERROR_DS_NO_DELETED_NAME = 0x20A3,
        /// <summary>[8356] The parent of a new subref must exist.</summary>
        ERROR_DS_SUBREF_MUST_HAVE_PARENT = 0x20A4,
        /// <summary>[8357] The object must be a naming context.</summary>
        ERROR_DS_NCNAME_MUST_BE_NC = 0x20A5,
        /// <summary>[8358] It is not permitted to add an attribute which is owned by the system.</summary>
        ERROR_DS_CANT_ADD_SYSTEM_ONLY = 0x20A6,
        /// <summary>[8359] The class of the object must be structural; you cannot instantiate an abstract class.</summary>
        ERROR_DS_CLASS_MUST_BE_CONCRETE = 0x20A7,
        /// <summary>[8360] The schema object could not be found.</summary>
        ERROR_DS_INVALID_DMD = 0x20A8,
        /// <summary>[8361] A local object with this GUID (dead or alive) already exists.</summary>
        ERROR_DS_OBJ_GUID_EXISTS = 0x20A9,
        /// <summary>[8362] The operation cannot be performed on a back link.</summary>
        ERROR_DS_NOT_ON_BACKLINK = 0x20AA,
        /// <summary>[8363] The cross reference for the specified naming context could not be found.</summary>
        ERROR_DS_NO_CROSSREF_FOR_NC = 0x20AB,
        /// <summary>[8364] The operation could not be performed because the directory service is shutting down.</summary>
        ERROR_DS_SHUTTING_DOWN = 0x20AC,
        /// <summary>[8365] The directory service request is invalid.</summary>
        ERROR_DS_UNKNOWN_OPERATION = 0x20AD,
        /// <summary>[8366] The role owner attribute could not be read.</summary>
        ERROR_DS_INVALID_ROLE_OWNER = 0x20AE,
        /// <summary>[8367] The requested FSMO operation failed. The current FSMO holder could not be contacted.</summary>
        ERROR_DS_COULDNT_CONTACT_FSMO = 0x20AF,
        /// <summary>[8368] Modification of a DN across a naming context is not permitted.</summary>
        ERROR_DS_CROSS_NC_DN_RENAME = 0x20B0,
        /// <summary>[8369] The attribute cannot be modified because it is owned by the system.</summary>
        ERROR_DS_CANT_MOD_SYSTEM_ONLY = 0x20B1,
        /// <summary>[8370] Only the replicator can perform this function.</summary>
        ERROR_DS_REPLICATOR_ONLY = 0x20B2,
        /// <summary>[8371] The specified class is not defined.</summary>
        ERROR_DS_OBJ_CLASS_NOT_DEFINED = 0x20B3,
        /// <summary>[8372] The specified class is not a subclass.</summary>
        ERROR_DS_OBJ_CLASS_NOT_SUBCLASS = 0x20B4,
        /// <summary>[8373] The name reference is invalid.</summary>
        ERROR_DS_NAME_REFERENCE_INVALID = 0x20B5,
        /// <summary>[8374] A cross reference already exists.</summary>
        ERROR_DS_CROSS_REF_EXISTS = 0x20B6,
        /// <summary>[8375] It is not permitted to delete a master cross reference.</summary>
        ERROR_DS_CANT_DEL_MASTER_CROSSREF = 0x20B7,
        /// <summary>[8376] Subtree notifications are only supported on NC heads.</summary>
        ERROR_DS_SUBTREE_NOTIFY_NOT_NC_HEAD = 0x20B8,
        /// <summary>[8377] Notification filter is too complex.</summary>
        ERROR_DS_NOTIFY_FILTER_TOO_COMPLEX = 0x20B9,
        /// <summary>[8378] Schema update failed: duplicate RDN.</summary>
        ERROR_DS_DUP_RDN = 0x20BA,
        /// <summary>[8379] Schema update failed: duplicate OID.</summary>
        ERROR_DS_DUP_OID = 0x20BB,
        /// <summary>[8380] Schema update failed: duplicate MAPI identifier.</summary>
        ERROR_DS_DUP_MAPI_ID = 0x20BC,
        /// <summary>[8381] Schema update failed: duplicate schema-id GUID.</summary>
        ERROR_DS_DUP_SCHEMA_ID_GUID = 0x20BD,
        /// <summary>[8382] Schema update failed: duplicate LDAP display name.</summary>
        ERROR_DS_DUP_LDAP_DISPLAY_NAME = 0x20BE,
        /// <summary>[8383] Schema update failed: range-lower less than range upper.</summary>
        ERROR_DS_SEMANTIC_ATT_TEST = 0x20BF,
        /// <summary>[8384] Schema update failed: syntax mismatch.</summary>
        ERROR_DS_SYNTAX_MISMATCH = 0x20C0,
        /// <summary>[8385] Schema deletion failed: attribute is used in must-contain.</summary>
        ERROR_DS_EXISTS_IN_MUST_HAVE = 0x20C1,
        /// <summary>[8386] Schema deletion failed: attribute is used in may-contain.</summary>
        ERROR_DS_EXISTS_IN_MAY_HAVE = 0x20C2,
        /// <summary>[8387] Schema update failed: attribute in may-contain does not exist.</summary>
        ERROR_DS_NONEXISTENT_MAY_HAVE = 0x20C3,
        /// <summary>[8388] Schema update failed: attribute in must-contain does not exist.</summary>
        ERROR_DS_NONEXISTENT_MUST_HAVE = 0x20C4,
        /// <summary>[8389] Schema update failed: class in aux-class list does not exist or is not an auxiliary class.</summary>
        ERROR_DS_AUX_CLS_TEST_FAIL = 0x20C5,
        /// <summary>[8390] Schema update failed: class in poss-superiors does not exist.</summary>
        ERROR_DS_NONEXISTENT_POSS_SUP = 0x20C6,
        /// <summary>[8391] Schema update failed: class in subclassof list does not exist or does not satisfy hierarchy rules.</summary>
        ERROR_DS_SUB_CLS_TEST_FAIL = 0x20C7,
        /// <summary>[8392] Schema update failed: Rdn-Att-Id has wrong syntax.</summary>
        ERROR_DS_BAD_RDN_ATT_ID_SYNTAX = 0x20C8,
        /// <summary>[8393] Schema deletion failed: class is used as auxiliary class.</summary>
        ERROR_DS_EXISTS_IN_AUX_CLS = 0x20C9,
        /// <summary>[8394] Schema deletion failed: class is used as sub class.</summary>
        ERROR_DS_EXISTS_IN_SUB_CLS = 0x20CA,
        /// <summary>[8395] Schema deletion failed: class is used as poss superior.</summary>
        ERROR_DS_EXISTS_IN_POSS_SUP = 0x20CB,
        /// <summary>[8396] Schema update failed in recalculating validation cache.</summary>
        ERROR_DS_RECALCSCHEMA_FAILED = 0x20CC,
        /// <summary>[8397] The tree deletion is not finished. The request must be made again to continue deleting the tree.</summary>
        ERROR_DS_TREE_DELETE_NOT_FINISHED = 0x20CD,
        /// <summary>[8398] The requested delete operation could not be performed.</summary>
        ERROR_DS_CANT_DELETE = 0x20CE,
        /// <summary>[8399] Cannot read the governs class identifier for the schema record.</summary>
        ERROR_DS_ATT_SCHEMA_REQ_ID = 0x20CF,
        /// <summary>[8400] The attribute schema has bad syntax.</summary>
        ERROR_DS_BAD_ATT_SCHEMA_SYNTAX = 0x20D0,
        /// <summary>[8401] The attribute could not be cached.</summary>
        ERROR_DS_CANT_CACHE_ATT = 0x20D1,
        /// <summary>[8402] The class could not be cached.</summary>
        ERROR_DS_CANT_CACHE_CLASS = 0x20D2,
        /// <summary>[8403] The attribute could not be removed from the cache.</summary>
        ERROR_DS_CANT_REMOVE_ATT_CACHE = 0x20D3,
        /// <summary>[8404] The class could not be removed from the cache.</summary>
        ERROR_DS_CANT_REMOVE_CLASS_CACHE = 0x20D4,
        /// <summary>[8405] The distinguished name attribute could not be read.</summary>
        ERROR_DS_CANT_RETRIEVE_DN = 0x20D5,
        /// <summary>[8406] No superior reference has been configured for the directory service. The directory service is therefore unable to issue referrals to objects outside this forest.</summary>
        ERROR_DS_MISSING_SUPREF = 0x20D6,
        /// <summary>[8407] The instance type attribute could not be retrieved.</summary>
        ERROR_DS_CANT_RETRIEVE_INSTANCE = 0x20D7,
        /// <summary>[8408] An internal error has occurred.</summary>
        ERROR_DS_CODE_INCONSISTENCY = 0x20D8,
        /// <summary>[8409] A database error has occurred.</summary>
        ERROR_DS_DATABASE_ERROR = 0x20D9,
        /// <summary>[8410] The attribute GOVERNSID is missing.</summary>
        ERROR_DS_GOVERNSID_MISSING = 0x20DA,
        /// <summary>[8411] An expected attribute is missing.</summary>
        ERROR_DS_MISSING_EXPECTED_ATT = 0x20DB,
        /// <summary>[8412] The specified naming context is missing a cross reference.</summary>
        ERROR_DS_NCNAME_MISSING_CR_REF = 0x20DC,
        /// <summary>[8413] A security checking error has occurred.</summary>
        ERROR_DS_SECURITY_CHECKING_ERROR = 0x20DD,
        /// <summary>[8414] The schema is not loaded.</summary>
        ERROR_DS_SCHEMA_NOT_LOADED = 0x20DE,
        /// <summary>[8415] Schema allocation failed. Please check if the machine is running low on memory.</summary>
        ERROR_DS_SCHEMA_ALLOC_FAILED = 0x20DF,
        /// <summary>[8416] Failed to obtain the required syntax for the attribute schema.</summary>
        ERROR_DS_ATT_SCHEMA_REQ_SYNTAX = 0x20E0,
        /// <summary>[8417] The global catalog verification failed. The global catalog is not available or does not support the operation. Some part of the directory is currently not available.</summary>
        ERROR_DS_GCVERIFY_ERROR = 0x20E1,
        /// <summary>[8418] The replication operation failed because of a schema mismatch between the servers involved.</summary>
        ERROR_DS_DRA_SCHEMA_MISMATCH = 0x20E2,
        /// <summary>[8419] The DSA object could not be found.</summary>
        ERROR_DS_CANT_FIND_DSA_OBJ = 0x20E3,
        /// <summary>[8420] The naming context could not be found.</summary>
        ERROR_DS_CANT_FIND_EXPECTED_NC = 0x20E4,
        /// <summary>[8421] The naming context could not be found in the cache.</summary>
        ERROR_DS_CANT_FIND_NC_IN_CACHE = 0x20E5,
        /// <summary>[8422] The child object could not be retrieved.</summary>
        ERROR_DS_CANT_RETRIEVE_CHILD = 0x20E6,
        /// <summary>[8423] The modification was not permitted for security reasons.</summary>
        ERROR_DS_SECURITY_ILLEGAL_MODIFY = 0x20E7,
        /// <summary>[8424] The operation cannot replace the hidden record.</summary>
        ERROR_DS_CANT_REPLACE_HIDDEN_REC = 0x20E8,
        /// <summary>[8425] The hierarchy file is invalid.</summary>
        ERROR_DS_BAD_HIERARCHY_FILE = 0x20E9,
        /// <summary>[8426] The attempt to build the hierarchy table failed.</summary>
        ERROR_DS_BUILD_HIERARCHY_TABLE_FAILED = 0x20EA,
        /// <summary>[8427] The directory configuration parameter is missing from the registry.</summary>
        ERROR_DS_CONFIG_PARAM_MISSING = 0x20EB,
        /// <summary>[8428] The attempt to count the address book indices failed.</summary>
        ERROR_DS_COUNTING_AB_INDICES_FAILED = 0x20EC,
        /// <summary>[8429] The allocation of the hierarchy table failed.</summary>
        ERROR_DS_HIERARCHY_TABLE_MALLOC_FAILED = 0x20ED,
        /// <summary>[8430] The directory service encountered an internal failure.</summary>
        ERROR_DS_INTERNAL_FAILURE = 0x20EE,
        /// <summary>[8431] The directory service encountered an unknown failure.</summary>
        ERROR_DS_UNKNOWN_ERROR = 0x20EF,
        /// <summary>[8432] A root object requires a class of 'top'.</summary>
        ERROR_DS_ROOT_REQUIRES_CLASS_TOP = 0x20F0,
        /// <summary>[8433] This directory server is shutting down, and cannot take ownership of new floating single-master operation roles.</summary>
        ERROR_DS_REFUSING_FSMO_ROLES = 0x20F1,
        /// <summary>[8434] The directory service is missing mandatory configuration information, and is unable to determine the ownership of floating single-master operation roles.</summary>
        ERROR_DS_MISSING_FSMO_SETTINGS = 0x20F2,
        /// <summary>[8435] The directory service was unable to transfer ownership of one or more floating single-master operation roles to other servers.</summary>
        ERROR_DS_UNABLE_TO_SURRENDER_ROLES = 0x20F3,
        /// <summary>[8436] The replication operation failed.</summary>
        ERROR_DS_DRA_GENERIC = 0x20F4,
        /// <summary>[8437] An invalid parameter was specified for this replication operation.</summary>
        ERROR_DS_DRA_INVALID_PARAMETER = 0x20F5,
        /// <summary>[8438] The directory service is too busy to complete the replication operation at this time.</summary>
        ERROR_DS_DRA_BUSY = 0x20F6,
        /// <summary>[8439] The distinguished name specified for this replication operation is invalid.</summary>
        ERROR_DS_DRA_BAD_DN = 0x20F7,
        /// <summary>[8440] The naming context specified for this replication operation is invalid.</summary>
        ERROR_DS_DRA_BAD_NC = 0x20F8,
        /// <summary>[8441] The distinguished name specified for this replication operation already exists.</summary>
        ERROR_DS_DRA_DN_EXISTS = 0x20F9,
        /// <summary>[8442] The replication system encountered an internal error.</summary>
        ERROR_DS_DRA_INTERNAL_ERROR = 0x20FA,
        /// <summary>[8443] The replication operation encountered a database inconsistency.</summary>
        ERROR_DS_DRA_INCONSISTENT_DIT = 0x20FB,
        /// <summary>[8444] The server specified for this replication operation could not be contacted.</summary>
        ERROR_DS_DRA_CONNECTION_FAILED = 0x20FC,
        /// <summary>[8445] The replication operation encountered an object with an invalid instance type.</summary>
        ERROR_DS_DRA_BAD_INSTANCE_TYPE = 0x20FD,
        /// <summary>[8446] The replication operation failed to allocate memory.</summary>
        ERROR_DS_DRA_OUT_OF_MEM = 0x20FE,
        /// <summary>[8447] The replication operation encountered an error with the mail system.</summary>
        ERROR_DS_DRA_MAIL_PROBLEM = 0x20FF,
        /// <summary>[8448] The replication reference information for the target server already exists.</summary>
        ERROR_DS_DRA_REF_ALREADY_EXISTS = 0x2100,
        /// <summary>[8449] The replication reference information for the target server does not exist.</summary>
        ERROR_DS_DRA_REF_NOT_FOUND = 0x2101,
        /// <summary>[8450] The naming context cannot be removed because it is replicated to another server.</summary>
        ERROR_DS_DRA_OBJ_IS_REP_SOURCE = 0x2102,
        /// <summary>[8451] The replication operation encountered a database error.</summary>
        ERROR_DS_DRA_DB_ERROR = 0x2103,
        /// <summary>[8452] The naming context is in the process of being removed or is not replicated from the specified server.</summary>
        ERROR_DS_DRA_NO_REPLICA = 0x2104,
        /// <summary>[8453] Replication access was denied.</summary>
        ERROR_DS_DRA_ACCESS_DENIED = 0x2105,
        /// <summary>[8454] The requested operation is not supported by this version of the directory service.</summary>
        ERROR_DS_DRA_NOT_SUPPORTED = 0x2106,
        /// <summary>[8455] The replication remote procedure call was cancelled.</summary>
        ERROR_DS_DRA_RPC_CANCELLED = 0x2107,
        /// <summary>[8456] The source server is currently rejecting replication requests.</summary>
        ERROR_DS_DRA_SOURCE_DISABLED = 0x2108,
        /// <summary>[8457] The destination server is currently rejecting replication requests.</summary>
        ERROR_DS_DRA_SINK_DISABLED = 0x2109,
        /// <summary>[8458] The replication operation failed due to a collision of object names.</summary>
        ERROR_DS_DRA_NAME_COLLISION = 0x210A,
        /// <summary>[8459] The replication source has been reinstalled.</summary>
        ERROR_DS_DRA_SOURCE_REINSTALLED = 0x210B,
        /// <summary>[8460] The replication operation failed because a required parent object is missing.</summary>
        ERROR_DS_DRA_MISSING_PARENT = 0x210C,
        /// <summary>[8461] The replication operation was preempted.</summary>
        ERROR_DS_DRA_PREEMPTED = 0x210D,
        /// <summary>[8462] The replication synchronization attempt was abandoned because of a lack of updates.</summary>
        ERROR_DS_DRA_ABANDON_SYNC = 0x210E,
        /// <summary>[8463] The replication operation was terminated because the system is shutting down.</summary>
        ERROR_DS_DRA_SHUTDOWN = 0x210F,
        /// <summary>[8464] Synchronization attempt failed because the destination DC is currently waiting to synchronize new partial attributes from source. This condition is normal if a recent schema change modified the partial attribute set. The destination partial attribute set is not a subset of source partial attribute set.</summary>
        ERROR_DS_DRA_INCOMPATIBLE_PARTIAL_SET = 0x2110,
        /// <summary>[8465] The replication synchronization attempt failed because a master replica attempted to sync from a partial replica.</summary>
        ERROR_DS_DRA_SOURCE_IS_PARTIAL_REPLICA = 0x2111,
        /// <summary>[8466] The server specified for this replication operation was contacted, but that server was unable to contact an additional server needed to complete the operation.</summary>
        ERROR_DS_DRA_EXTN_CONNECTION_FAILED = 0x2112,
        /// <summary>[8467] The version of the directory service schema of the source forest is not compatible with the version of directory service on this computer.</summary>
        ERROR_DS_INSTALL_SCHEMA_MISMATCH = 0x2113,
        /// <summary>[8468] Schema update failed: An attribute with the same link identifier already exists.</summary>
        ERROR_DS_DUP_LINK_ID = 0x2114,
        /// <summary>[8469] Name translation: Generic processing error.</summary>
        ERROR_DS_NAME_ERROR_RESOLVING = 0x2115,
        /// <summary>[8470] Name translation: Could not find the name or insufficient right to see name.</summary>
        ERROR_DS_NAME_ERROR_NOT_FOUND = 0x2116,
        /// <summary>[8471] Name translation: Input name mapped to more than one output name.</summary>
        ERROR_DS_NAME_ERROR_NOT_UNIQUE = 0x2117,
        /// <summary>[8472] Name translation: Input name found, but not the associated output format.</summary>
        ERROR_DS_NAME_ERROR_NO_MAPPING = 0x2118,
        /// <summary>[8473] Name translation: Unable to resolve completely, only the domain was found.</summary>
        ERROR_DS_NAME_ERROR_DOMAIN_ONLY = 0x2119,
        /// <summary>[8474] Name translation: Unable to perform purely syntactical mapping at the client without going out to the wire.</summary>
        ERROR_DS_NAME_ERROR_NO_SYNTACTICAL_MAPPING = 0x211A,
        /// <summary>[8475] Modification of a constructed attribute is not allowed.</summary>
        ERROR_DS_CONSTRUCTED_ATT_MOD = 0x211B,
        /// <summary>[8476] The OM-Object-Class specified is incorrect for an attribute with the specified syntax.</summary>
        ERROR_DS_WRONG_OM_OBJ_CLASS = 0x211C,
        /// <summary>[8477] The replication request has been posted; waiting for reply.</summary>
        ERROR_DS_DRA_REPL_PENDING = 0x211D,
        /// <summary>[8478] The requested operation requires a directory service, and none was available.</summary>
        ERROR_DS_DS_REQUIRED = 0x211E,
        /// <summary>[8479] The LDAP display name of the class or attribute contains non-ASCII characters.</summary>
        ERROR_DS_INVALID_LDAP_DISPLAY_NAME = 0x211F,
        /// <summary>[8480] The requested search operation is only supported for base searches.</summary>
        ERROR_DS_NON_BASE_SEARCH = 0x2120,
        /// <summary>[8481] The search failed to retrieve attributes from the database.</summary>
        ERROR_DS_CANT_RETRIEVE_ATTS = 0x2121,
        /// <summary>[8482] The schema update operation tried to add a backward link attribute that has no corresponding forward link.</summary>
        ERROR_DS_BACKLINK_WITHOUT_LINK = 0x2122,
        /// <summary>[8483] Source and destination of a cross-domain move do not agree on the object's epoch number. Either source or destination does not have the latest version of the object.</summary>
        ERROR_DS_EPOCH_MISMATCH = 0x2123,
        /// <summary>[8484] Source and destination of a cross-domain move do not agree on the object's current name. Either source or destination does not have the latest version of the object.</summary>
        ERROR_DS_SRC_NAME_MISMATCH = 0x2124,
        /// <summary>[8485] Source and destination for the cross-domain move operation are identical. Caller should use local move operation instead of cross-domain move operation.</summary>
        ERROR_DS_SRC_AND_DST_NC_IDENTICAL = 0x2125,
        /// <summary>[8486] Source and destination for a cross-domain move are not in agreement on the naming contexts in the forest. Either source or destination does not have the latest version of the Partitions container.</summary>
        ERROR_DS_DST_NC_MISMATCH = 0x2126,
        /// <summary>[8487] Destination of a cross-domain move is not authoritative for the destination naming context.</summary>
        ERROR_DS_NOT_AUTHORITIVE_FOR_DST_NC = 0x2127,
        /// <summary>[8488] Source and destination of a cross-domain move do not agree on the identity of the source object. Either source or destination does not have the latest version of the source object.</summary>
        ERROR_DS_SRC_GUID_MISMATCH = 0x2128,
        /// <summary>[8489] Object being moved across-domains is already known to be deleted by the destination server. The source server does not have the latest version of the source object.</summary>
        ERROR_DS_CANT_MOVE_DELETED_OBJECT = 0x2129,
        /// <summary>[8490] Another operation which requires exclusive access to the PDC FSMO is already in progress.</summary>
        ERROR_DS_PDC_OPERATION_IN_PROGRESS = 0x212A,
        /// <summary>[8491] A cross-domain move operation failed such that two versions of the moved object exist - one each in the source and destination domains. The destination object needs to be removed to restore the system to a consistent state.</summary>
        ERROR_DS_CROSS_DOMAIN_CLEANUP_REQD = 0x212B,
        /// <summary>[8492] This object may not be moved across domain boundaries either because cross-domain moves for this class are disallowed, or the object has some special characteristics, e.g.: trust account or restricted RID, which prevent its move.</summary>
        ERROR_DS_ILLEGAL_XDOM_MOVE_OPERATION = 0x212C,
        /// <summary>[8493] Can't move objects with memberships across domain boundaries as once moved, this would violate the membership conditions of the account group. Remove the object from any account group memberships and retry.</summary>
        ERROR_DS_CANT_WITH_ACCT_GROUP_MEMBERSHPS = 0x212D,
        /// <summary>[8494] A naming context head must be the immediate child of another naming context head, not of an interior node.</summary>
        ERROR_DS_NC_MUST_HAVE_NC_PARENT = 0x212E,
        /// <summary>[8495] The directory cannot validate the proposed naming context name because it does not hold a replica of the naming context above the proposed naming context. Please ensure that the domain naming master role is held by a server that is configured as a global catalog server, and that the server is up to date with its replication partners. (Applies only to Windows 2000 Domain Naming masters.)</summary>
        ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE = 0x212F,
        /// <summary>[8496] Destination domain must be in native mode.</summary>
        ERROR_DS_DST_DOMAIN_NOT_NATIVE = 0x2130,
        /// <summary>[8497] The operation cannot be performed because the server does not have an infrastructure container in the domain of interest.</summary>
        ERROR_DS_MISSING_INFRASTRUCTURE_CONTAINER = 0x2131,
        /// <summary>[8498] Cross-domain move of non-empty account groups is not allowed.</summary>
        ERROR_DS_CANT_MOVE_ACCOUNT_GROUP = 0x2132,
        /// <summary>[8499] Cross-domain move of non-empty resource groups is not allowed.</summary>
        ERROR_DS_CANT_MOVE_RESOURCE_GROUP = 0x2133,
        /// <summary>[8500] The search flags for the attribute are invalid. The ANR bit is valid only on attributes of Unicode or Teletex strings.</summary>
        ERROR_DS_INVALID_SEARCH_FLAG = 0x2134,
        /// <summary>[8501] Tree deletions starting at an object which has an NC head as a descendant are not allowed.</summary>
        ERROR_DS_NO_TREE_DELETE_ABOVE_NC = 0x2135,
        /// <summary>[8502] The directory service failed to lock a tree in preparation for a tree deletion because the tree was in use.</summary>
        ERROR_DS_COULDNT_LOCK_TREE_FOR_DELETE = 0x2136,
        /// <summary>[8503] The directory service failed to identify the list of objects to delete while attempting a tree deletion.</summary>
        ERROR_DS_COULDNT_IDENTIFY_OBJECTS_FOR_TREE_DELETE = 0x2137,
        /// <summary>[8504] Security Accounts Manager initialization failed because of the following error: %1. Error Status: 0x%2. Please shutdown this system and reboot into Directory Services Restore Mode, check the event log for more detailed information.</summary>
        ERROR_DS_SAM_INIT_FAILURE = 0x2138,
        /// <summary>[8505] Only an administrator can modify the membership list of an administrative group.</summary>
        ERROR_DS_SENSITIVE_GROUP_VIOLATION = 0x2139,
        /// <summary>[8506] Cannot change the primary group ID of a domain controller account.</summary>
        ERROR_DS_CANT_MOD_PRIMARYGROUPID = 0x213A,
        /// <summary>[8507] An attempt is made to modify the base schema.</summary>
        ERROR_DS_ILLEGAL_BASE_SCHEMA_MOD = 0x213B,
        /// <summary>[8508] Adding a new mandatory attribute to an existing class, deleting a mandatory attribute from an existing class, or adding an optional attribute to the special class Top that is not a backlink attribute (directly or through inheritance, for example, by adding or deleting an auxiliary class) is not allowed.</summary>
        ERROR_DS_NONSAFE_SCHEMA_CHANGE = 0x213C,
        /// <summary>[8509] Schema update is not allowed on this DC because the DC is not the schema FSMO Role Owner.</summary>
        ERROR_DS_SCHEMA_UPDATE_DISALLOWED = 0x213D,
        /// <summary>[8510] An object of this class cannot be created under the schema container. You can only create attribute-schema and class-schema objects under the schema container.</summary>
        ERROR_DS_CANT_CREATE_UNDER_SCHEMA = 0x213E,
        /// <summary>[8511] The replica/child install failed to get the objectVersion attribute on the schema container on the source DC. Either the attribute is missing on the schema container or the credentials supplied do not have permission to read it.</summary>
        ERROR_DS_INSTALL_NO_SRC_SCH_VERSION = 0x213F,
        /// <summary>[8512] The replica/child install failed to read the objectVersion attribute in the SCHEMA section of the file schema.ini in the system32 directory.</summary>
        ERROR_DS_INSTALL_NO_SCH_VERSION_IN_INIFILE = 0x2140,
        /// <summary>[8513] The specified group type is invalid.</summary>
        ERROR_DS_INVALID_GROUP_TYPE = 0x2141,
        /// <summary>[8514] You cannot nest global groups in a mixed domain if the group is security-enabled.</summary>
        ERROR_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN = 0x2142,
        /// <summary>[8515] You cannot nest local groups in a mixed domain if the group is security-enabled.</summary>
        ERROR_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN = 0x2143,
        /// <summary>[8516] A global group cannot have a local group as a member.</summary>
        ERROR_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER = 0x2144,
        /// <summary>[8517] A global group cannot have a universal group as a member.</summary>
        ERROR_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER = 0x2145,
        /// <summary>[8518] A universal group cannot have a local group as a member.</summary>
        ERROR_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER = 0x2146,
        /// <summary>[8519] A global group cannot have a cross-domain member.</summary>
        ERROR_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER = 0x2147,
        /// <summary>[8520] A local group cannot have another cross domain local group as a member.</summary>
        ERROR_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER = 0x2148,
        /// <summary>[8521] A group with primary members cannot change to a security-disabled group.</summary>
        ERROR_DS_HAVE_PRIMARY_MEMBERS = 0x2149,
        /// <summary>[8522] The schema cache load failed to convert the string default SD on a class-schema object.</summary>
        ERROR_DS_STRING_SD_CONVERSION_FAILED = 0x214A,
        /// <summary>[8523] Only DSAs configured to be Global Catalog servers should be allowed to hold the Domain Naming Master FSMO role. (Applies only to Windows 2000 servers.)</summary>
        ERROR_DS_NAMING_MASTER_GC = 0x214B,
        /// <summary>[8524] The DSA operation is unable to proceed because of a DNS lookup failure.</summary>
        ERROR_DS_DNS_LOOKUP_FAILURE = 0x214C,
        /// <summary>[8525] While processing a change to the DNS Host Name for an object, the Service Principal Name values could not be kept in sync.</summary>
        ERROR_DS_COULDNT_UPDATE_SPNS = 0x214D,
        /// <summary>[8526] The Security Descriptor attribute could not be read.</summary>
        ERROR_DS_CANT_RETRIEVE_SD = 0x214E,
        /// <summary>[8527] The object requested was not found, but an object with that key was found.</summary>
        ERROR_DS_KEY_NOT_UNIQUE = 0x214F,
        /// <summary>[8528] The syntax of the linked attribute being added is incorrect. Forward links can only have syntax 2.5.5.1, 2.5.5.7, and 2.5.5.14, and backlinks can only have syntax 2.5.5.1.</summary>
        ERROR_DS_WRONG_LINKED_ATT_SYNTAX = 0x2150,
        /// <summary>[8529] Security Account Manager needs to get the boot password.</summary>
        ERROR_DS_SAM_NEED_BOOTKEY_PASSWORD = 0x2151,
        /// <summary>[8530] Security Account Manager needs to get the boot key from floppy disk.</summary>
        ERROR_DS_SAM_NEED_BOOTKEY_FLOPPY = 0x2152,
        /// <summary>[8531] Directory Service cannot start.</summary>
        ERROR_DS_CANT_START = 0x2153,
        /// <summary>[8532] Directory Services could not start.</summary>
        ERROR_DS_INIT_FAILURE = 0x2154,
        /// <summary>[8533] The connection between client and server requires packet privacy or better.</summary>
        ERROR_DS_NO_PKT_PRIVACY_ON_CONNECTION = 0x2155,
        /// <summary>[8534] The source domain may not be in the same forest as destination.</summary>
        ERROR_DS_SOURCE_DOMAIN_IN_FOREST = 0x2156,
        /// <summary>[8535] The destination domain must be in the forest.</summary>
        ERROR_DS_DESTINATION_DOMAIN_NOT_IN_FOREST = 0x2157,
        /// <summary>[8536] The operation requires that destination domain auditing be enabled.</summary>
        ERROR_DS_DESTINATION_AUDITING_NOT_ENABLED = 0x2158,
        /// <summary>[8537] The operation couldn't locate a DC for the source domain.</summary>
        ERROR_DS_CANT_FIND_DC_FOR_SRC_DOMAIN = 0x2159,
        /// <summary>[8538] The source object must be a group or user.</summary>
        ERROR_DS_SRC_OBJ_NOT_GROUP_OR_USER = 0x215A,
        /// <summary>[8539] The source object's SID already exists in destination forest.</summary>
        ERROR_DS_SRC_SID_EXISTS_IN_FOREST = 0x215B,
        /// <summary>[8540] The source and destination object must be of the same type.</summary>
        ERROR_DS_SRC_AND_DST_OBJECT_CLASS_MISMATCH = 0x215C,
        /// <summary>[8541] Security Accounts Manager initialization failed because of the following error: %1. Error Status: 0x%2. Click OK to shut down the system and reboot into Safe Mode. Check the event log for detailed information.</summary>
        ERROR_SAM_INIT_FAILURE = 0x215D,
        /// <summary>[8542] Schema information could not be included in the replication request.</summary>
        ERROR_DS_DRA_SCHEMA_INFO_SHIP = 0x215E,
        /// <summary>[8543] The replication operation could not be completed due to a schema incompatibility.</summary>
        ERROR_DS_DRA_SCHEMA_CONFLICT = 0x215F,
        /// <summary>[8544] The replication operation could not be completed due to a previous schema incompatibility.</summary>
        ERROR_DS_DRA_EARLIER_SCHEMA_CONFLICT = 0x2160,
        /// <summary>[8545] The replication update could not be applied because either the source or the destination has not yet received information regarding a recent cross-domain move operation.</summary>
        ERROR_DS_DRA_OBJ_NC_MISMATCH = 0x2161,
        /// <summary>[8546] The requested domain could not be deleted because there exist domain controllers that still host this domain.</summary>
        ERROR_DS_NC_STILL_HAS_DSAS = 0x2162,
        /// <summary>[8547] The requested operation can be performed only on a global catalog server.</summary>
        ERROR_DS_GC_REQUIRED = 0x2163,
        /// <summary>[8548] A local group can only be a member of other local groups in the same domain.</summary>
        ERROR_DS_LOCAL_MEMBER_OF_LOCAL_ONLY = 0x2164,
        /// <summary>[8549] Foreign security principals cannot be members of universal groups.</summary>
        ERROR_DS_NO_FPO_IN_UNIVERSAL_GROUPS = 0x2165,
        /// <summary>[8550] The attribute is not allowed to be replicated to the GC because of security reasons.</summary>
        ERROR_DS_CANT_ADD_TO_GC = 0x2166,
        /// <summary>[8551] The checkpoint with the PDC could not be taken because there too many modifications being processed currently.</summary>
        ERROR_DS_NO_CHECKPOINT_WITH_PDC = 0x2167,
        /// <summary>[8552] The operation requires that source domain auditing be enabled.</summary>
        ERROR_DS_SOURCE_AUDITING_NOT_ENABLED = 0x2168,
        /// <summary>[8553] Security principal objects can only be created inside domain naming contexts.</summary>
        ERROR_DS_CANT_CREATE_IN_NONDOMAIN_NC = 0x2169,
        /// <summary>[8554] A Service Principal Name (SPN) could not be constructed because the provided hostname is not in the necessary format.</summary>
        ERROR_DS_INVALID_NAME_FOR_SPN = 0x216A,
        /// <summary>[8555] A Filter was passed that uses constructed attributes.</summary>
        ERROR_DS_FILTER_USES_CONTRUCTED_ATTRS = 0x216B,
        /// <summary>[8556] The unicodePwd attribute value must be enclosed in double quotes.</summary>
        ERROR_DS_UNICODEPWD_NOT_IN_QUOTES = 0x216C,
        /// <summary>[8557] Your computer could not be joined to the domain. You have exceeded the maximum number of computer accounts you are allowed to create in this domain. Contact your system administrator to have this limit reset or increased.</summary>
        ERROR_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED = 0x216D,
        /// <summary>[8558] For security reasons, the operation must be run on the destination DC.</summary>
        ERROR_DS_MUST_BE_RUN_ON_DST_DC = 0x216E,
        /// <summary>[8559] For security reasons, the source DC must be NT4SP4 or greater.</summary>
        ERROR_DS_SRC_DC_MUST_BE_SP4_OR_GREATER = 0x216F,
        /// <summary>[8560] Critical Directory Service System objects cannot be deleted during tree delete operations. The tree delete may have been partially performed.</summary>
        ERROR_DS_CANT_TREE_DELETE_CRITICAL_OBJ = 0x2170,
        /// <summary>[8561] Directory Services could not start because of the following error: %1. Error Status: 0x%2. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further.</summary>
        ERROR_DS_INIT_FAILURE_CONSOLE = 0x2171,
        /// <summary>[8562] Security Accounts Manager initialization failed because of the following error: %1. Error Status: 0x%2. Please click OK to shutdown the system. You can use the recovery console to diagnose the system further.</summary>
        ERROR_DS_SAM_INIT_FAILURE_CONSOLE = 0x2172,
        /// <summary>[8563] The version of the operating system is incompatible with the current AD DS forest functional level or AD LDS Configuration Set functional level. You must upgrade to a new version of the operating system before this server can become an AD DS Domain Controller or add an AD LDS Instance in this AD DS Forest or AD LDS Configuration Set.</summary>
        ERROR_DS_FOREST_VERSION_TOO_HIGH = 0x2173,
        /// <summary>[8564] The version of the operating system installed is incompatible with the current domain functional level. You must upgrade to a new version of the operating system before this server can become a domain controller in this domain.</summary>
        ERROR_DS_DOMAIN_VERSION_TOO_HIGH = 0x2174,
        /// <summary>[8565] The version of the operating system installed on this server no longer supports the current AD DS Forest functional level or AD LDS Configuration Set functional level. You must raise the AD DS Forest functional level or AD LDS Configuration Set functional level before this server can become an AD DS Domain Controller or an AD LDS Instance in this Forest or Configuration Set.</summary>
        ERROR_DS_FOREST_VERSION_TOO_LOW = 0x2175,
        /// <summary>[8566] The version of the operating system installed on this server no longer supports the current domain functional level. You must raise the domain functional level before this server can become a domain controller in this domain.</summary>
        ERROR_DS_DOMAIN_VERSION_TOO_LOW = 0x2176,
        /// <summary>[8567] The version of the operating system installed on this server is incompatible with the functional level of the domain or forest.</summary>
        ERROR_DS_INCOMPATIBLE_VERSION = 0x2177,
        /// <summary>[8568] The functional level of the domain (or forest) cannot be raised to the requested value, because there exist one or more domain controllers in the domain (or forest) that are at a lower incompatible functional level.</summary>
        ERROR_DS_LOW_DSA_VERSION = 0x2178,
        /// <summary>[8569] The forest functional level cannot be raised to the requested value since one or more domains are still in mixed domain mode. All domains in the forest must be in native mode, for you to raise the forest functional level.</summary>
        ERROR_DS_NO_BEHAVIOR_VERSION_IN_MIXEDDOMAIN = 0x2179,
        /// <summary>[8570] The sort order requested is not supported.</summary>
        ERROR_DS_NOT_SUPPORTED_SORT_ORDER = 0x217A,
        /// <summary>[8571] The requested name already exists as a unique identifier.</summary>
        ERROR_DS_NAME_NOT_UNIQUE = 0x217B,
        /// <summary>[8572] The machine account was created pre-NT4. The account needs to be recreated.</summary>
        ERROR_DS_MACHINE_ACCOUNT_CREATED_PRENT4 = 0x217C,
        /// <summary>[8573] The database is out of version store.</summary>
        ERROR_DS_OUT_OF_VERSION_STORE = 0x217D,
        /// <summary>[8574] Unable to continue operation because multiple conflicting controls were used.</summary>
        ERROR_DS_INCOMPATIBLE_CONTROLS_USED = 0x217E,
        /// <summary>[8575] Unable to find a valid security descriptor reference domain for this partition.</summary>
        ERROR_DS_NO_REF_DOMAIN = 0x217F,
        /// <summary>[8576] Schema update failed: The link identifier is reserved.</summary>
        ERROR_DS_RESERVED_LINK_ID = 0x2180,
        /// <summary>[8577] Schema update failed: There are no link identifiers available.</summary>
        ERROR_DS_LINK_ID_NOT_AVAILABLE = 0x2181,
        /// <summary>[8578] An account group cannot have a universal group as a member.</summary>
        ERROR_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER = 0x2182,
        /// <summary>[8579] Rename or move operations on naming context heads or read-only objects are not allowed.</summary>
        ERROR_DS_MODIFYDN_DISALLOWED_BY_INSTANCE_TYPE = 0x2183,
        /// <summary>[8580] Move operations on objects in the schema naming context are not allowed.</summary>
        ERROR_DS_NO_OBJECT_MOVE_IN_SCHEMA_NC = 0x2184,
        /// <summary>[8581] A system flag has been set on the object and does not allow the object to be moved or renamed.</summary>
        ERROR_DS_MODIFYDN_DISALLOWED_BY_FLAG = 0x2185,
        /// <summary>[8582] This object is not allowed to change its grandparent container. Moves are not forbidden on this object, but are restricted to sibling containers.</summary>
        ERROR_DS_MODIFYDN_WRONG_GRANDPARENT = 0x2186,
        /// <summary>[8583] Unable to resolve completely, a referral to another forest is generated.</summary>
        ERROR_DS_NAME_ERROR_TRUST_REFERRAL = 0x2187,
        /// <summary>[8584] The requested action is not supported on standard server.</summary>
        ERROR_NOT_SUPPORTED_ON_STANDARD_SERVER = 0x2188,
        /// <summary>[8585] Could not access a partition of the directory service located on a remote server. Make sure at least one server is running for the partition in question.</summary>
        ERROR_DS_CANT_ACCESS_REMOTE_PART_OF_AD = 0x2189,
        /// <summary>[8586] The directory cannot validate the proposed naming context (or partition) name because it does not hold a replica nor can it contact a replica of the naming context above the proposed naming context. Please ensure that the parent naming context is properly registered in DNS, and at least one replica of this naming context is reachable by the Domain Naming master.</summary>
        ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE_V2 = 0x218A,
        /// <summary>[8587] The thread limit for this request was exceeded.</summary>
        ERROR_DS_THREAD_LIMIT_EXCEEDED = 0x218B,
        /// <summary>[8588] The Global catalog server is not in the closest site.</summary>
        ERROR_DS_NOT_CLOSEST = 0x218C,
        /// <summary>[8589] The DS cannot derive a service principal name (SPN) with which to mutually authenticate the target server because the corresponding server object in the local DS database has no serverReference attribute.</summary>
        ERROR_DS_CANT_DERIVE_SPN_WITHOUT_SERVER_REF = 0x218D,
        /// <summary>[8590] The Directory Service failed to enter single user mode.</summary>
        ERROR_DS_SINGLE_USER_MODE_FAILED = 0x218E,
        /// <summary>[8591] The Directory Service cannot parse the script because of a syntax error.</summary>
        ERROR_DS_NTDSCRIPT_SYNTAX_ERROR = 0x218F,
        /// <summary>[8592] The Directory Service cannot process the script because of an error.</summary>
        ERROR_DS_NTDSCRIPT_PROCESS_ERROR = 0x2190,
        /// <summary>[8593] The directory service cannot perform the requested operation because the servers involved are of different replication epochs (which is usually related to a domain rename that is in progress).</summary>
        ERROR_DS_DIFFERENT_REPL_EPOCHS = 0x2191,
        /// <summary>[8594] The directory service binding must be renegotiated due to a change in the server extensions information.</summary>
        ERROR_DS_DRS_EXTENSIONS_CHANGED = 0x2192,
        /// <summary>[8595] Operation not allowed on a disabled cross ref.</summary>
        ERROR_DS_REPLICA_SET_CHANGE_NOT_ALLOWED_ON_DISABLED_CR = 0x2193,
        /// <summary>[8596] Schema update failed: No values for msDS-IntId are available.</summary>
        ERROR_DS_NO_MSDS_INTID = 0x2194,
        /// <summary>[8597] Schema update failed: Duplicate msDS-INtId. Retry the operation.</summary>
        ERROR_DS_DUP_MSDS_INTID = 0x2195,
        /// <summary>[8598] Schema deletion failed: attribute is used in rDNAttID.</summary>
        ERROR_DS_EXISTS_IN_RDNATTID = 0x2196,
        /// <summary>[8599] The directory service failed to authorize the request.</summary>
        ERROR_DS_AUTHORIZATION_FAILED = 0x2197,
        /// <summary>[8600] The Directory Service cannot process the script because it is invalid.</summary>
        ERROR_DS_INVALID_SCRIPT = 0x2198,
        /// <summary>[8601] The remote create cross reference operation failed on the Domain Naming Master FSMO. The operation's error is in the extended data.</summary>
        ERROR_DS_REMOTE_CROSSREF_OP_FAILED = 0x2199,
        /// <summary>[8602] A cross reference is in use locally with the same name.</summary>
        ERROR_DS_CROSS_REF_BUSY = 0x219A,
        /// <summary>[8603] The DS cannot derive a service principal name (SPN) with which to mutually authenticate the target server because the server's domain has been deleted from the forest.</summary>
        ERROR_DS_CANT_DERIVE_SPN_FOR_DELETED_DOMAIN = 0x219B,
        /// <summary>[8604] Writeable NCs prevent this DC from demoting.</summary>
        ERROR_DS_CANT_DEMOTE_WITH_WRITEABLE_NC = 0x219C,
        /// <summary>[8605] The requested object has a non-unique identifier and cannot be retrieved.</summary>
        ERROR_DS_DUPLICATE_ID_FOUND = 0x219D,
        /// <summary>[8606] Insufficient attributes were given to create an object. This object may not exist because it may have been deleted and already garbage collected.</summary>
        ERROR_DS_INSUFFICIENT_ATTR_TO_CREATE_OBJECT = 0x219E,
        /// <summary>[8607] The group cannot be converted due to attribute restrictions on the requested group type.</summary>
        ERROR_DS_GROUP_CONVERSION_ERROR = 0x219F,
        /// <summary>[8608] Cross-domain move of non-empty basic application groups is not allowed.</summary>
        ERROR_DS_CANT_MOVE_APP_BASIC_GROUP = 0x21A0,
        /// <summary>[8609] Cross-domain move of non-empty query based application groups is not allowed.</summary>
        ERROR_DS_CANT_MOVE_APP_QUERY_GROUP = 0x21A1,
        /// <summary>[8610] The FSMO role ownership could not be verified because its directory partition has not replicated successfully with at least one replication partner.</summary>
        ERROR_DS_ROLE_NOT_VERIFIED = 0x21A2,
        /// <summary>[8611] The target container for a redirection of a well known object container cannot already be a special container.</summary>
        ERROR_DS_WKO_CONTAINER_CANNOT_BE_SPECIAL = 0x21A3,
        /// <summary>[8612] The Directory Service cannot perform the requested operation because a domain rename operation is in progress.</summary>
        ERROR_DS_DOMAIN_RENAME_IN_PROGRESS = 0x21A4,
        /// <summary>[8613] The directory service detected a child partition below the requested partition name. The partition hierarchy must be created in a top down method.</summary>
        ERROR_DS_EXISTING_AD_CHILD_NC = 0x21A5,
        /// <summary>[8614] The directory service cannot replicate with this server because the time since the last replication with this server has exceeded the tombstone lifetime.</summary>
        ERROR_DS_REPL_LIFETIME_EXCEEDED = 0x21A6,
        /// <summary>[8615] The requested operation is not allowed on an object under the system container.</summary>
        ERROR_DS_DISALLOWED_IN_SYSTEM_CONTAINER = 0x21A7,
        /// <summary>[8616] The LDAP servers network send queue has filled up because the client is not processing the results of its requests fast enough. No more requests will be processed until the client catches up. If the client does not catch up then it will be disconnected.</summary>
        ERROR_DS_LDAP_SEND_QUEUE_FULL = 0x21A8,
        /// <summary>[8617] The scheduled replication did not take place because the system was too busy to execute the request within the schedule window. The replication queue is overloaded. Consider reducing the number of partners or decreasing the scheduled replication frequency.</summary>
        ERROR_DS_DRA_OUT_SCHEDULE_WINDOW = 0x21A9,
        /// <summary>[8618] At this time, it cannot be determined if the branch replication policy is available on the hub domain controller. Please retry at a later time to account for replication latencies.</summary>
        ERROR_DS_POLICY_NOT_KNOWN = 0x21AA,
        /// <summary>[8619] The site settings object for the specified site does not exist.</summary>
        ERROR_NO_SITE_SETTINGS_OBJECT = 0x21AB,
        /// <summary>[8620] The local account store does not contain secret material for the specified account.</summary>
        ERROR_NO_SECRETS = 0x21AC,
        /// <summary>[8621] Could not find a writable domain controller in the domain.</summary>
        ERROR_NO_WRITABLE_DC_FOUND = 0x21AD,
        /// <summary>[8622] The server object for the domain controller does not exist.</summary>
        ERROR_DS_NO_SERVER_OBJECT = 0x21AE,
        /// <summary>[8623] The NTDS Settings object for the domain controller does not exist.</summary>
        ERROR_DS_NO_NTDSA_OBJECT = 0x21AF,
        /// <summary>[8624] The requested search operation is not supported for ASQ searches.</summary>
        ERROR_DS_NON_ASQ_SEARCH = 0x21B0,
        /// <summary>[8625] A required audit event could not be generated for the operation.</summary>
        ERROR_DS_AUDIT_FAILURE = 0x21B1,
        /// <summary>[8626] The search flags for the attribute are invalid. The subtree index bit is valid only on single valued attributes.</summary>
        ERROR_DS_INVALID_SEARCH_FLAG_SUBTREE = 0x21B2,
        /// <summary>[8627] The search flags for the attribute are invalid. The tuple index bit is valid only on attributes of Unicode strings.</summary>
        ERROR_DS_INVALID_SEARCH_FLAG_TUPLE = 0x21B3,
        /// <summary>[8628] The address books are nested too deeply. Failed to build the hierarchy table.</summary>
        ERROR_DS_HIERARCHY_TABLE_TOO_DEEP = 0x21B4,
        /// <summary>[8629] The specified up-to-date-ness vector is corrupt.</summary>
        ERROR_DS_DRA_CORRUPT_UTD_VECTOR = 0x21B5,
        /// <summary>[8630] The request to replicate secrets is denied.</summary>
        ERROR_DS_DRA_SECRETS_DENIED = 0x21B6,
        /// <summary>[8631] Schema update failed: The MAPI identifier is reserved.</summary>
        ERROR_DS_RESERVED_MAPI_ID = 0x21B7,
        /// <summary>[8632] Schema update failed: There are no MAPI identifiers available.</summary>
        ERROR_DS_MAPI_ID_NOT_AVAILABLE = 0x21B8,
        /// <summary>[8633] The replication operation failed because the required attributes of the local krbtgt object are missing.</summary>
        ERROR_DS_DRA_MISSING_KRBTGT_SECRET = 0x21B9,
        /// <summary>[8634] The domain name of the trusted domain already exists in the forest.</summary>
        ERROR_DS_DOMAIN_NAME_EXISTS_IN_FOREST = 0x21BA,
        /// <summary>[8635] The flat name of the trusted domain already exists in the forest.</summary>
        ERROR_DS_FLAT_NAME_EXISTS_IN_FOREST = 0x21BB,
        /// <summary>[8636] The User Principal Name (UPN) is invalid.</summary>
        ERROR_INVALID_USER_PRINCIPAL_NAME = 0x21BC,
        /// <summary>[8637] OID mapped groups cannot have members.</summary>
        ERROR_DS_OID_MAPPED_GROUP_CANT_HAVE_MEMBERS = 0x21BD,
        /// <summary>[8638] The specified OID cannot be found.</summary>
        ERROR_DS_OID_NOT_FOUND = 0x21BE,
        /// <summary>[8639] The replication operation failed because the target object referred by a link value is recycled.</summary>
        ERROR_DS_DRA_RECYCLED_TARGET = 0x21BF,
        /// <summary>[8640] The redirect operation failed because the target object is in a NC different from the domain NC of the current domain controller.</summary>
        ERROR_DS_DISALLOWED_NC_REDIRECT = 0x21C0,
        /// <summary>[8641] The functional level of the AD LDS configuration set cannot be lowered to the requested value.</summary>
        ERROR_DS_HIGH_ADLDS_FFL = 0x21C1,
        /// <summary>[8642] The functional level of the domain (or forest) cannot be lowered to the requested value.</summary>
        ERROR_DS_HIGH_DSA_VERSION = 0x21C2,
        /// <summary>[8643] The functional level of the AD LDS configuration set cannot be raised to the requested value, because there exist one or more ADLDS instances that are at a lower incompatible functional level.</summary>
        ERROR_DS_LOW_ADLDS_FFL = 0x21C3,
        /// <summary>[8644] The domain join cannot be completed because the SID of the domain you attempted to join was identical to the SID of this machine. This is a symptom of an improperly cloned operating system install. You should run sysprep on this machine in order to generate a new machine SID. Please see https://go.microsoft.com/fwlink/p/?linkid=168895 for more information.</summary>
        ERROR_DOMAIN_SID_SAME_AS_LOCAL_WORKSTATION = 0x21C4,
        /// <summary>[8645] The undelete operation failed because the Sam Account Name or Additional Sam Account Name of the object being undeleted conflicts with an existing live object.</summary>
        ERROR_DS_UNDELETE_SAM_VALIDATION_FAILED = 0x21C5,
        /// <summary>[8646] The system is not authoritative for the specified account and therefore cannot complete the operation. Please retry the operation using the provider associated with this account. If this is an online provider please use the provider's online site.</summary>
        ERROR_INCORRECT_ACCOUNT_TYPE = 0x21C6,
        /// <summary>[9001] DNS server unable to interpret format.</summary>
        DNS_ERROR_RCODE_FORMAT_ERROR = 0x2329,
        /// <summary>[9002] DNS server failure.</summary>
        DNS_ERROR_RCODE_SERVER_FAILURE = 0x232A,
        /// <summary>[9003] DNS name does not exist.</summary>
        DNS_ERROR_RCODE_NAME_ERROR = 0x232B,
        /// <summary>[9004] DNS request not supported by name server.</summary>
        DNS_ERROR_RCODE_NOT_IMPLEMENTED = 0x232C,
        /// <summary>[9005] DNS operation refused.</summary>
        DNS_ERROR_RCODE_REFUSED = 0x232D,
        /// <summary>[9006] DNS name that ought not exist, does exist.</summary>
        DNS_ERROR_RCODE_YXDOMAIN = 0x232E,
        /// <summary>[9007] DNS RR set that ought not exist, does exist.</summary>
        DNS_ERROR_RCODE_YXRRSET = 0x232F,
        /// <summary>[9008] DNS RR set that ought to exist, does not exist.</summary>
        DNS_ERROR_RCODE_NXRRSET = 0x2330,
        /// <summary>[9009] DNS server not authoritative for zone.</summary>
        DNS_ERROR_RCODE_NOTAUTH = 0x2331,
        /// <summary>[9010] DNS name in update or prereq is not in zone.</summary>
        DNS_ERROR_RCODE_NOTZONE = 0x2332,
        /// <summary>[9016] DNS signature failed to verify.</summary>
        DNS_ERROR_RCODE_BADSIG = 0x2338,
        /// <summary>[9017] DNS bad key.</summary>
        DNS_ERROR_RCODE_BADKEY = 0x2339,
        /// <summary>[9018] DNS signature validity expired.</summary>
        DNS_ERROR_RCODE_BADTIME = 0x233A,
        /// <summary>[9101] Only the DNS server acting as the key master for the zone may perform this operation.</summary>
        DNS_ERROR_KEYMASTER_REQUIRED = 0x238D,
        /// <summary>[9102] This operation is not allowed on a zone that is signed or has signing keys.</summary>
        DNS_ERROR_NOT_ALLOWED_ON_SIGNED_ZONE = 0x238E,
        /// <summary>[9103] NSEC3 is not compatible with the RSA-SHA-1 algorithm. Choose a different algorithm or use NSEC.</summary>
        DNS_ERROR_NSEC3_INCOMPATIBLE_WITH_RSA_SHA1 = 0x238F,
        /// <summary>[9104] The zone does not have enough signing keys. There must be at least one key signing key (KSK) and at least one zone signing key (ZSK).</summary>
        DNS_ERROR_NOT_ENOUGH_SIGNING_KEY_DESCRIPTORS = 0x2390,
        /// <summary>[9105] The specified algorithm is not supported.</summary>
        DNS_ERROR_UNSUPPORTED_ALGORITHM = 0x2391,
        /// <summary>[9106] The specified key size is not supported.</summary>
        DNS_ERROR_INVALID_KEY_SIZE = 0x2392,
        /// <summary>[9107] One or more of the signing keys for a zone are not accessible to the DNS server. Zone signing will not be operational until this error is resolved.</summary>
        DNS_ERROR_SIGNING_KEY_NOT_ACCESSIBLE = 0x2393,
        /// <summary>[9108] The specified key storage provider does not support DPAPI++ data protection. Zone signing will not be operational until this error is resolved.</summary>
        DNS_ERROR_KSP_DOES_NOT_SUPPORT_PROTECTION = 0x2394,
        /// <summary>[9109] An unexpected DPAPI++ error was encountered. Zone signing will not be operational until this error is resolved.</summary>
        DNS_ERROR_UNEXPECTED_DATA_PROTECTION_ERROR = 0x2395,
        /// <summary>[9110] An unexpected crypto error was encountered. Zone signing may not be operational until this error is resolved.</summary>
        DNS_ERROR_UNEXPECTED_CNG_ERROR = 0x2396,
        /// <summary>[9111] The DNS server encountered a signing key with an unknown version. Zone signing will not be operational until this error is resolved.</summary>
        DNS_ERROR_UNKNOWN_SIGNING_PARAMETER_VERSION = 0x2397,
        /// <summary>[9112] The specified key service provider cannot be opened by the DNS server.</summary>
        DNS_ERROR_KSP_NOT_ACCESSIBLE = 0x2398,
        /// <summary>[9113] The DNS server cannot accept any more signing keys with the specified algorithm and KSK flag value for this zone.</summary>
        DNS_ERROR_TOO_MANY_SKDS = 0x2399,
        /// <summary>[9114] The specified rollover period is invalid.</summary>
        DNS_ERROR_INVALID_ROLLOVER_PERIOD = 0x239A,
        /// <summary>[9115] The specified initial rollover offset is invalid.</summary>
        DNS_ERROR_INVALID_INITIAL_ROLLOVER_OFFSET = 0x239B,
        /// <summary>[9116] The specified signing key is already in process of rolling over keys.</summary>
        DNS_ERROR_ROLLOVER_IN_PROGRESS = 0x239C,
        /// <summary>[9117] The specified signing key does not have a standby key to revoke.</summary>
        DNS_ERROR_STANDBY_KEY_NOT_PRESENT = 0x239D,
        /// <summary>[9118] This operation is not allowed on a zone signing key (ZSK).</summary>
        DNS_ERROR_NOT_ALLOWED_ON_ZSK = 0x239E,
        /// <summary>[9119] This operation is not allowed on an active signing key.</summary>
        DNS_ERROR_NOT_ALLOWED_ON_ACTIVE_SKD = 0x239F,
        /// <summary>[9120] The specified signing key is already queued for rollover.</summary>
        DNS_ERROR_ROLLOVER_ALREADY_QUEUED = 0x23A0,
        /// <summary>[9121] This operation is not allowed on an unsigned zone.</summary>
        DNS_ERROR_NOT_ALLOWED_ON_UNSIGNED_ZONE = 0x23A1,
        /// <summary>[9122] This operation could not be completed because the DNS server listed as the current key master for this zone is down or misconfigured. Resolve the problem on the current key master for this zone or use another DNS server to seize the key master role.</summary>
        DNS_ERROR_BAD_KEYMASTER = 0x23A2,
        /// <summary>[9123] The specified signature validity period is invalid.</summary>
        DNS_ERROR_INVALID_SIGNATURE_VALIDITY_PERIOD = 0x23A3,
        /// <summary>[9124] The specified NSEC3 iteration count is higher than allowed by the minimum key length used in the zone.</summary>
        DNS_ERROR_INVALID_NSEC3_ITERATION_COUNT = 0x23A4,
        /// <summary>[9125] This operation could not be completed because the DNS server has been configured with DNSSEC features disabled. Enable DNSSEC on the DNS server.</summary>
        DNS_ERROR_DNSSEC_IS_DISABLED = 0x23A5,
        /// <summary>[9126] This operation could not be completed because the XML stream received is empty or syntactically invalid.</summary>
        DNS_ERROR_INVALID_XML = 0x23A6,
        /// <summary>[9127] This operation completed, but no trust anchors were added because all of the trust anchors received were either invalid, unsupported, expired, or would not become valid in less than 30 days.</summary>
        DNS_ERROR_NO_VALID_TRUST_ANCHORS = 0x23A7,
        /// <summary>[9128] The specified signing key is not waiting for parental DS update.</summary>
        DNS_ERROR_ROLLOVER_NOT_POKEABLE = 0x23A8,
        /// <summary>[9129] Hash collision detected during NSEC3 signing. Specify a different user-provided salt, or use a randomly generated salt, and attempt to sign the zone again.</summary>
        DNS_ERROR_NSEC3_NAME_COLLISION = 0x23A9,
        /// <summary>[9130] NSEC is not compatible with the NSEC3-RSA-SHA-1 algorithm. Choose a different algorithm or use NSEC3.</summary>
        DNS_ERROR_NSEC_INCOMPATIBLE_WITH_NSEC3_RSA_SHA1 = 0x23AA,
        /// <summary>[9501] No records found for given DNS query.</summary>
        DNS_INFO_NO_RECORDS = 0x251D,
        /// <summary>[9502] Bad DNS packet.</summary>
        DNS_ERROR_BAD_PACKET = 0x251E,
        /// <summary>[9503] No DNS packet.</summary>
        DNS_ERROR_NO_PACKET = 0x251F,
        /// <summary>[9504] DNS error, check rcode.</summary>
        DNS_ERROR_RCODE = 0x2520,
        /// <summary>[9505] Unsecured DNS packet.</summary>
        DNS_ERROR_UNSECURE_PACKET = 0x2521,
        /// <summary>[9506] DNS query request is pending.</summary>
        DNS_REQUEST_PENDING = 0x2522,
        /// <summary>[9551] Invalid DNS type.</summary>
        DNS_ERROR_INVALID_TYPE = 0x254F,
        /// <summary>[9552] Invalid IP address.</summary>
        DNS_ERROR_INVALID_IP_ADDRESS = 0x2550,
        /// <summary>[9553] Invalid property.</summary>
        DNS_ERROR_INVALID_PROPERTY = 0x2551,
        /// <summary>[9554] Try DNS operation again later.</summary>
        DNS_ERROR_TRY_AGAIN_LATER = 0x2552,
        /// <summary>[9555] Record for given name and type is not unique.</summary>
        DNS_ERROR_NOT_UNIQUE = 0x2553,
        /// <summary>[9556] DNS name does not comply with RFC specifications.</summary>
        DNS_ERROR_NON_RFC_NAME = 0x2554,
        /// <summary>[9557] DNS name is a fully-qualified DNS name.</summary>
        DNS_STATUS_FQDN = 0x2555,
        /// <summary>[9558] DNS name is dotted (multi-label).</summary>
        DNS_STATUS_DOTTED_NAME = 0x2556,
        /// <summary>[9559] DNS name is a single-part name.</summary>
        DNS_STATUS_SINGLE_PART_NAME = 0x2557,
        /// <summary>[9560] DNS name contains an invalid character.</summary>
        DNS_ERROR_INVALID_NAME_CHAR = 0x2558,
        /// <summary>[9561] DNS name is entirely numeric.</summary>
        DNS_ERROR_NUMERIC_NAME = 0x2559,
        /// <summary>[9562] The operation requested is not permitted on a DNS root server.</summary>
        DNS_ERROR_NOT_ALLOWED_ON_ROOT_SERVER = 0x255A,
        /// <summary>[9563] The record could not be created because this part of the DNS namespace has been delegated to another server.</summary>
        DNS_ERROR_NOT_ALLOWED_UNDER_DELEGATION = 0x255B,
        /// <summary>[9564] The DNS server could not find a set of root hints.</summary>
        DNS_ERROR_CANNOT_FIND_ROOT_HINTS = 0x255C,
        /// <summary>[9565] The DNS server found root hints but they were not consistent across all adapters.</summary>
        DNS_ERROR_INCONSISTENT_ROOT_HINTS = 0x255D,
        /// <summary>[9566] The specified value is too small for this parameter.</summary>
        DNS_ERROR_DWORD_VALUE_TOO_SMALL = 0x255E,
        /// <summary>[9567] The specified value is too large for this parameter.</summary>
        DNS_ERROR_DWORD_VALUE_TOO_LARGE = 0x255F,
        /// <summary>[9568] This operation is not allowed while the DNS server is loading zones in the background. Please try again later.</summary>
        DNS_ERROR_BACKGROUND_LOADING = 0x2560,
        /// <summary>[9569] The operation requested is not permitted on against a DNS server running on a read-only DC.</summary>
        DNS_ERROR_NOT_ALLOWED_ON_RODC = 0x2561,
        /// <summary>[9570] No data is allowed to exist underneath a DNAME record.</summary>
        DNS_ERROR_NOT_ALLOWED_UNDER_DNAME = 0x2562,
        /// <summary>[9571] This operation requires credentials delegation.</summary>
        DNS_ERROR_DELEGATION_REQUIRED = 0x2563,
        /// <summary>[9572] Name resolution policy table has been corrupted. DNS resolution will fail until it is fixed. Contact your network administrator.</summary>
        DNS_ERROR_INVALID_POLICY_TABLE = 0x2564,
        /// <summary>[9601] DNS zone does not exist.</summary>
        DNS_ERROR_ZONE_DOES_NOT_EXIST = 0x2581,
        /// <summary>[9602] DNS zone information not available.</summary>
        DNS_ERROR_NO_ZONE_INFO = 0x2582,
        /// <summary>[9603] Invalid operation for DNS zone.</summary>
        DNS_ERROR_INVALID_ZONE_OPERATION = 0x2583,
        /// <summary>[9604] Invalid DNS zone configuration.</summary>
        DNS_ERROR_ZONE_CONFIGURATION_ERROR = 0x2584,
        /// <summary>[9605] DNS zone has no start of authority (SOA) record.</summary>
        DNS_ERROR_ZONE_HAS_NO_SOA_RECORD = 0x2585,
        /// <summary>[9606] DNS zone has no Name Server (NS) record.</summary>
        DNS_ERROR_ZONE_HAS_NO_NS_RECORDS = 0x2586,
        /// <summary>[9607] DNS zone is locked.</summary>
        DNS_ERROR_ZONE_LOCKED = 0x2587,
        /// <summary>[9608] DNS zone creation failed.</summary>
        DNS_ERROR_ZONE_CREATION_FAILED = 0x2588,
        /// <summary>[9609] DNS zone already exists.</summary>
        DNS_ERROR_ZONE_ALREADY_EXISTS = 0x2589,
        /// <summary>[9610] DNS automatic zone already exists.</summary>
        DNS_ERROR_AUTOZONE_ALREADY_EXISTS = 0x258A,
        /// <summary>[9611] Invalid DNS zone type.</summary>
        DNS_ERROR_INVALID_ZONE_TYPE = 0x258B,
        /// <summary>[9612] Secondary DNS zone requires master IP address.</summary>
        DNS_ERROR_SECONDARY_REQUIRES_MASTER_IP = 0x258C,
        /// <summary>[9613] DNS zone not secondary.</summary>
        DNS_ERROR_ZONE_NOT_SECONDARY = 0x258D,
        /// <summary>[9614] Need secondary IP address.</summary>
        DNS_ERROR_NEED_SECONDARY_ADDRESSES = 0x258E,
        /// <summary>[9615] WINS initialization failed.</summary>
        DNS_ERROR_WINS_INIT_FAILED = 0x258F,
        /// <summary>[9616] Need WINS servers.</summary>
        DNS_ERROR_NEED_WINS_SERVERS = 0x2590,
        /// <summary>[9617] NBTSTAT initialization call failed.</summary>
        DNS_ERROR_NBSTAT_INIT_FAILED = 0x2591,
        /// <summary>[9618] Invalid delete of start of authority (SOA).</summary>
        DNS_ERROR_SOA_DELETE_INVALID = 0x2592,
        /// <summary>[9619] A conditional forwarding zone already exists for that name.</summary>
        DNS_ERROR_FORWARDER_ALREADY_EXISTS = 0x2593,
        /// <summary>[9620] This zone must be configured with one or more master DNS server IP addresses.</summary>
        DNS_ERROR_ZONE_REQUIRES_MASTER_IP = 0x2594,
        /// <summary>[9621] The operation cannot be performed because this zone is shut down.</summary>
        DNS_ERROR_ZONE_IS_SHUTDOWN = 0x2595,
        /// <summary>[9622] This operation cannot be performed because the zone is currently being signed. Please try again later.</summary>
        DNS_ERROR_ZONE_LOCKED_FOR_SIGNING = 0x2596,
        /// <summary>[9651] Primary DNS zone requires datafile.</summary>
        DNS_ERROR_PRIMARY_REQUIRES_DATAFILE = 0x25B3,
        /// <summary>[9652] Invalid datafile name for DNS zone.</summary>
        DNS_ERROR_INVALID_DATAFILE_NAME = 0x25B4,
        /// <summary>[9653] Failed to open datafile for DNS zone.</summary>
        DNS_ERROR_DATAFILE_OPEN_FAILURE = 0x25B5,
        /// <summary>[9654] Failed to write datafile for DNS zone.</summary>
        DNS_ERROR_FILE_WRITEBACK_FAILED = 0x25B6,
        /// <summary>[9655] Failure while reading datafile for DNS zone.</summary>
        DNS_ERROR_DATAFILE_PARSING = 0x25B7,
        /// <summary>[9701] DNS record does not exist.</summary>
        DNS_ERROR_RECORD_DOES_NOT_EXIST = 0x25E5,
        /// <summary>[9702] DNS record format error.</summary>
        DNS_ERROR_RECORD_FORMAT = 0x25E6,
        /// <summary>[9703] Node creation failure in DNS.</summary>
        DNS_ERROR_NODE_CREATION_FAILED = 0x25E7,
        /// <summary>[9704] Unknown DNS record type.</summary>
        DNS_ERROR_UNKNOWN_RECORD_TYPE = 0x25E8,
        /// <summary>[9705] DNS record timed out.</summary>
        DNS_ERROR_RECORD_TIMED_OUT = 0x25E9,
        /// <summary>[9706] Name not in DNS zone.</summary>
        DNS_ERROR_NAME_NOT_IN_ZONE = 0x25EA,
        /// <summary>[9707] CNAME loop detected.</summary>
        DNS_ERROR_CNAME_LOOP = 0x25EB,
        /// <summary>[9708] Node is a CNAME DNS record.</summary>
        DNS_ERROR_NODE_IS_CNAME = 0x25EC,
        /// <summary>[9709] A CNAME record already exists for given name.</summary>
        DNS_ERROR_CNAME_COLLISION = 0x25ED,
        /// <summary>[9710] Record only at DNS zone root.</summary>
        DNS_ERROR_RECORD_ONLY_AT_ZONE_ROOT = 0x25EE,
        /// <summary>[9711] DNS record already exists.</summary>
        DNS_ERROR_RECORD_ALREADY_EXISTS = 0x25EF,
        /// <summary>[9712] Secondary DNS zone data error.</summary>
        DNS_ERROR_SECONDARY_DATA = 0x25F0,
        /// <summary>[9713] Could not create DNS cache data.</summary>
        DNS_ERROR_NO_CREATE_CACHE_DATA = 0x25F1,
        /// <summary>[9714] DNS name does not exist.</summary>
        DNS_ERROR_NAME_DOES_NOT_EXIST = 0x25F2,
        /// <summary>[9715] Could not create pointer (PTR) record.</summary>
        DNS_WARNING_PTR_CREATE_FAILED = 0x25F3,
        /// <summary>[9716] DNS domain was undeleted.</summary>
        DNS_WARNING_DOMAIN_UNDELETED = 0x25F4,
        /// <summary>[9717] The directory service is unavailable.</summary>
        DNS_ERROR_DS_UNAVAILABLE = 0x25F5,
        /// <summary>[9718] DNS zone already exists in the directory service.</summary>
        DNS_ERROR_DS_ZONE_ALREADY_EXISTS = 0x25F6,
        /// <summary>[9719] DNS server not creating or reading the boot file for the directory service integrated DNS zone.</summary>
        DNS_ERROR_NO_BOOTFILE_IF_DS_ZONE = 0x25F7,
        /// <summary>[9720] Node is a DNAME DNS record.</summary>
        DNS_ERROR_NODE_IS_DNAME = 0x25F8,
        /// <summary>[9721] A DNAME record already exists for given name.</summary>
        DNS_ERROR_DNAME_COLLISION = 0x25F9,
        /// <summary>[9722] An alias loop has been detected with either CNAME or DNAME records.</summary>
        DNS_ERROR_ALIAS_LOOP = 0x25FA,
        /// <summary>[9751] DNS AXFR (zone transfer) complete.</summary>
        DNS_INFO_AXFR_COMPLETE = 0x2617,
        /// <summary>[9752] DNS zone transfer failed.</summary>
        DNS_ERROR_AXFR = 0x2618,
        /// <summary>[9753] Added local WINS server.</summary>
        DNS_INFO_ADDED_LOCAL_WINS = 0x2619,
        /// <summary>[9801] Secure update call needs to continue update request.</summary>
        DNS_STATUS_CONTINUE_NEEDED = 0x2649,
        /// <summary>[9851] TCP/IP network protocol not installed.</summary>
        DNS_ERROR_NO_TCPIP = 0x267B,
        /// <summary>[9852] No DNS servers configured for local system.</summary>
        DNS_ERROR_NO_DNS_SERVERS = 0x267C,
        /// <summary>[9901] The specified directory partition does not exist.</summary>
        DNS_ERROR_DP_DOES_NOT_EXIST = 0x26AD,
        /// <summary>[9902] The specified directory partition already exists.</summary>
        DNS_ERROR_DP_ALREADY_EXISTS = 0x26AE,
        /// <summary>[9903] This DNS server is not enlisted in the specified directory partition.</summary>
        DNS_ERROR_DP_NOT_ENLISTED = 0x26AF,
        /// <summary>[9904] This DNS server is already enlisted in the specified directory partition.</summary>
        DNS_ERROR_DP_ALREADY_ENLISTED = 0x26B0,
        /// <summary>[9905] The directory partition is not available at this time. Please wait a few minutes and try again.</summary>
        DNS_ERROR_DP_NOT_AVAILABLE = 0x26B1,
        /// <summary>[9906] The operation failed because the domain naming master FSMO role could not be reached. The domain controller holding the domain naming master FSMO role is down or unable to service the request or is not running Windows Server 2003 or later.</summary>
        DNS_ERROR_DP_FSMO_ERROR = 0x26B2,
        /// <summary>[10004] A blocking operation was interrupted by a call to WSACancelBlockingCall.</summary>
        WSAEINTR = 0x2714,
        /// <summary>[10009] The file handle supplied is not valid.</summary>
        WSAEBADF = 0x2719,
        /// <summary>[10013] An attempt was made to access a socket in a way forbidden by its access permissions.</summary>
        WSAEACCES = 0x271D,
        /// <summary>[10014] The system detected an invalid pointer address in attempting to use a pointer argument in a call.</summary>
        WSAEFAULT = 0x271E,
        /// <summary>[10022] An invalid argument was supplied.</summary>
        WSAEINVAL = 0x2726,
        /// <summary>[10024] Too many open sockets.</summary>
        WSAEMFILE = 0x2728,
        /// <summary>[10035] A non-blocking socket operation could not be completed immediately.</summary>
        WSAEWOULDBLOCK = 0x2733,
        /// <summary>[10036] A blocking operation is currently executing.</summary>
        WSAEINPROGRESS = 0x2734,
        /// <summary>[10037] An operation was attempted on a non-blocking socket that already had an operation in progress.</summary>
        WSAEALREADY = 0x2735,
        /// <summary>[10038] An operation was attempted on something that is not a socket.</summary>
        WSAENOTSOCK = 0x2736,
        /// <summary>[10039] A required address was omitted from an operation on a socket.</summary>
        WSAEDESTADDRREQ = 0x2737,
        /// <summary>[10040] A message sent on a datagram socket was larger than the internal message buffer or some other network limit, or the buffer used to receive a datagram into was smaller than the datagram itself.</summary>
        WSAEMSGSIZE = 0x2738,
        /// <summary>[10041] A protocol was specified in the socket function call that does not support the semantics of the socket type requested.</summary>
        WSAEPROTOTYPE = 0x2739,
        /// <summary>[10042] An unknown, invalid, or unsupported option or level was specified in a getsockopt or setsockopt call.</summary>
        WSAENOPROTOOPT = 0x273A,
        /// <summary>[10043] The requested protocol has not been configured into the system, or no implementation for it exists.</summary>
        WSAEPROTONOSUPPORT = 0x273B,
        /// <summary>[10044] The support for the specified socket type does not exist in this address family.</summary>
        WSAESOCKTNOSUPPORT = 0x273C,
        /// <summary>[10045] The attempted operation is not supported for the type of object referenced.</summary>
        WSAEOPNOTSUPP = 0x273D,
        /// <summary>[10046] The protocol family has not been configured into the system or no implementation for it exists.</summary>
        WSAEPFNOSUPPORT = 0x273E,
        /// <summary>[10047] An address incompatible with the requested protocol was used.</summary>
        WSAEAFNOSUPPORT = 0x273F,
        /// <summary>[10048] Only one usage of each socket address (protocol/network address/port) is normally permitted.</summary>
        WSAEADDRINUSE = 0x2740,
        /// <summary>[10049] The requested address is not valid in its context.</summary>
        WSAEADDRNOTAVAIL = 0x2741,
        /// <summary>[10050] A socket operation encountered a dead network.</summary>
        WSAENETDOWN = 0x2742,
        /// <summary>[10051] A socket operation was attempted to an unreachable network.</summary>
        WSAENETUNREACH = 0x2743,
        /// <summary>[10052] The connection has been broken due to keep-alive activity detecting a failure while the operation was in progress.</summary>
        WSAENETRESET = 0x2744,
        /// <summary>[10053] An established connection was aborted by the software in your host machine.</summary>
        WSAECONNABORTED = 0x2745,
        /// <summary>[10054] An existing connection was forcibly closed by the remote host.</summary>
        WSAECONNRESET = 0x2746,
        /// <summary>[10055] An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full.</summary>
        WSAENOBUFS = 0x2747,
        /// <summary>[10056] A connect request was made on an already connected socket.</summary>
        WSAEISCONN = 0x2748,
        /// <summary>[10057] A request to send or receive data was disallowed because the socket is not connected and (when sending on a datagram socket using a sendto call) no address was supplied.</summary>
        WSAENOTCONN = 0x2749,
        /// <summary>[10058] A request to send or receive data was disallowed because the socket had already been shut down in that direction with a previous shutdown call.</summary>
        WSAESHUTDOWN = 0x274A,
        /// <summary>[10059] Too many references to some kernel object.</summary>
        WSAETOOMANYREFS = 0x274B,
        /// <summary>[10060] A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.</summary>
        WSAETIMEDOUT = 0x274C,
        /// <summary>[10061] No connection could be made because the target machine actively refused it.</summary>
        WSAECONNREFUSED = 0x274D,
        /// <summary>[10062] Cannot translate name.</summary>
        WSAELOOP = 0x274E,
        /// <summary>[10063] Name component or name was too long.</summary>
        WSAENAMETOOLONG = 0x274F,
        /// <summary>[10064] A socket operation failed because the destination host was down.</summary>
        WSAEHOSTDOWN = 0x2750,
        /// <summary>[10065] A socket operation was attempted to an unreachable host.</summary>
        WSAEHOSTUNREACH = 0x2751,
        /// <summary>[10066] Cannot remove a directory that is not empty.</summary>
        WSAENOTEMPTY = 0x2752,
        /// <summary>[10067] A Windows Sockets implementation may have a limit on the number of applications that may use it simultaneously.</summary>
        WSAEPROCLIM = 0x2753,
        /// <summary>[10068] Ran out of quota.</summary>
        WSAEUSERS = 0x2754,
        /// <summary>[10069] Ran out of disk quota.</summary>
        WSAEDQUOT = 0x2755,
        /// <summary>[10070] File handle reference is no longer available.</summary>
        WSAESTALE = 0x2756,
        /// <summary>[10071] Item is not available locally.</summary>
        WSAEREMOTE = 0x2757,
        /// <summary>[10091] WSAStartup cannot function at this time because the underlying system it uses to provide network services is currently unavailable.</summary>
        WSASYSNOTREADY = 0x276B,
        /// <summary>[10092] The Windows Sockets version requested is not supported.</summary>
        WSAVERNOTSUPPORTED = 0x276C,
        /// <summary>[10093] Either the application has not called WSAStartup, or WSAStartup failed.</summary>
        WSANOTINITIALISED = 0x276D,
        /// <summary>[10101] Returned by WSARecv or WSARecvFrom to indicate the remote party has initiated a graceful shutdown sequence.</summary>
        WSAEDISCON = 0x2775,
        /// <summary>[10102] No more results can be returned by WSALookupServiceNext.</summary>
        WSAENOMORE = 0x2776,
        /// <summary>[10103] A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.</summary>
        WSAECANCELLED = 0x2777,
        /// <summary>[10104] The procedure call table is invalid.</summary>
        WSAEINVALIDPROCTABLE = 0x2778,
        /// <summary>[10105] The requested service provider is invalid.</summary>
        WSAEINVALIDPROVIDER = 0x2779,
        /// <summary>[10106] The requested service provider could not be loaded or initialized.</summary>
        WSAEPROVIDERFAILEDINIT = 0x277A,
        /// <summary>[10107] A system call has failed.</summary>
        WSASYSCALLFAILURE = 0x277B,
        /// <summary>[10108] No such service is known. The service cannot be found in the specified name space.</summary>
        WSASERVICE_NOT_FOUND = 0x277C,
        /// <summary>[10109] The specified class was not found.</summary>
        WSATYPE_NOT_FOUND = 0x277D,
        /// <summary>[10110] No more results can be returned by WSALookupServiceNext.</summary>
        WSA_E_NO_MORE = 0x277E,
        /// <summary>[10111] A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.</summary>
        WSA_E_CANCELLED = 0x277F,
        /// <summary>[10112] A database query failed because it was actively refused.</summary>
        WSAEREFUSED = 0x2780,
        /// <summary>[11001] No such host is known.</summary>
        WSAHOST_NOT_FOUND = 0x2AF9,
        /// <summary>[11002] This is usually a temporary error during hostname resolution and means that the local server did not receive a response from an authoritative server.</summary>
        WSATRY_AGAIN = 0x2AFA,
        /// <summary>[11003] A non-recoverable error occurred during a database lookup.</summary>
        WSANO_RECOVERY = 0x2AFB,
        /// <summary>[11004] The requested name is valid, but no data of the requested type was found.</summary>
        WSANO_DATA = 0x2AFC,
        /// <summary>[11005] At least one reserve has arrived.</summary>
        WSA_QOS_RECEIVERS = 0x2AFD,
        /// <summary>[11006] At least one path has arrived.</summary>
        WSA_QOS_SENDERS = 0x2AFE,
        /// <summary>[11007] There are no senders.</summary>
        WSA_QOS_NO_SENDERS = 0x2AFF,
        /// <summary>[11008] There are no receivers.</summary>
        WSA_QOS_NO_RECEIVERS = 0x2B00,
        /// <summary>[11009] Reserve has been confirmed.</summary>
        WSA_QOS_REQUEST_CONFIRMED = 0x2B01,
        /// <summary>[11010] Error due to lack of resources.</summary>
        WSA_QOS_ADMISSION_FAILURE = 0x2B02,
        /// <summary>[11011] Rejected for administrative reasons - bad credentials.</summary>
        WSA_QOS_POLICY_FAILURE = 0x2B03,
        /// <summary>[11012] Unknown or conflicting style.</summary>
        WSA_QOS_BAD_STYLE = 0x2B04,
        /// <summary>[11013] Problem with some part of the filterspec or providerspecific buffer in general.</summary>
        WSA_QOS_BAD_OBJECT = 0x2B05,
        /// <summary>[11014] Problem with some part of the flowspec.</summary>
        WSA_QOS_TRAFFIC_CTRL_ERROR = 0x2B06,
        /// <summary>[11015] General QOS error.</summary>
        WSA_QOS_GENERIC_ERROR = 0x2B07,
        /// <summary>[11016] An invalid or unrecognized service type was found in the flowspec.</summary>
        WSA_QOS_ESERVICETYPE = 0x2B08,
        /// <summary>[11017] An invalid or inconsistent flowspec was found in the QOS structure.</summary>
        WSA_QOS_EFLOWSPEC = 0x2B09,
        /// <summary>[11018] Invalid QOS provider-specific buffer.</summary>
        WSA_QOS_EPROVSPECBUF = 0x2B0A,
        /// <summary>[11019] An invalid QOS filter style was used.</summary>
        WSA_QOS_EFILTERSTYLE = 0x2B0B,
        /// <summary>[11020] An invalid QOS filter type was used.</summary>
        WSA_QOS_EFILTERTYPE = 0x2B0C,
        /// <summary>[11021] An incorrect number of QOS FILTERSPECs were specified in the FLOWDESCRIPTOR.</summary>
        WSA_QOS_EFILTERCOUNT = 0x2B0D,
        /// <summary>[11022] An object with an invalid ObjectLength field was specified in the QOS provider-specific buffer.</summary>
        WSA_QOS_EOBJLENGTH = 0x2B0E,
        /// <summary>[11023] An incorrect number of flow descriptors was specified in the QOS structure.</summary>
        WSA_QOS_EFLOWCOUNT = 0x2B0F,
        /// <summary>[11024] An unrecognized object was found in the QOS provider-specific buffer.</summary>
        WSA_QOS_EUNKOWNPSOBJ = 0x2B10,
        /// <summary>[11025] An invalid policy object was found in the QOS provider-specific buffer.</summary>
        WSA_QOS_EPOLICYOBJ = 0x2B11,
        /// <summary>[11026] An invalid QOS flow descriptor was found in the flow descriptor list.</summary>
        WSA_QOS_EFLOWDESC = 0x2B12,
        /// <summary>[11027] An invalid or inconsistent flowspec was found in the QOS provider specific buffer.</summary>
        WSA_QOS_EPSFLOWSPEC = 0x2B13,
        /// <summary>[11028] An invalid FILTERSPEC was found in the QOS provider-specific buffer.</summary>
        WSA_QOS_EPSFILTERSPEC = 0x2B14,
        /// <summary>[11029] An invalid shape discard mode object was found in the QOS provider specific buffer.</summary>
        WSA_QOS_ESDMODEOBJ = 0x2B15,
        /// <summary>[11030] An invalid shaping rate object was found in the QOS provider-specific buffer.</summary>
        WSA_QOS_ESHAPERATEOBJ = 0x2B16,
        /// <summary>[11031] A reserved policy element was found in the QOS provider-specific buffer.</summary>
        WSA_QOS_RESERVED_PETYPE = 0x2B17,
        /// <summary>[11032] No such host is known securely.</summary>
        WSA_SECURE_HOST_NOT_FOUND = 0x2B18,
        /// <summary>[11033] Name based IPSEC policy could not be added.</summary>
        WSA_IPSEC_NAME_POLICY_ERROR = 0x2B19,
        /// <summary>[13000] The specified quick mode policy already exists.</summary>
        ERROR_IPSEC_QM_POLICY_EXISTS = 0x32C8,
        /// <summary>[13001] The specified quick mode policy was not found.</summary>
        ERROR_IPSEC_QM_POLICY_NOT_FOUND = 0x32C9,
        /// <summary>[13002] The specified quick mode policy is being used.</summary>
        ERROR_IPSEC_QM_POLICY_IN_USE = 0x32CA,
        /// <summary>[13003] The specified main mode policy already exists.</summary>
        ERROR_IPSEC_MM_POLICY_EXISTS = 0x32CB,
        /// <summary>[13004] The specified main mode policy was not found.</summary>
        ERROR_IPSEC_MM_POLICY_NOT_FOUND = 0x32CC,
        /// <summary>[13005] The specified main mode policy is being used.</summary>
        ERROR_IPSEC_MM_POLICY_IN_USE = 0x32CD,
        /// <summary>[13006] The specified main mode filter already exists.</summary>
        ERROR_IPSEC_MM_FILTER_EXISTS = 0x32CE,
        /// <summary>[13007] The specified main mode filter was not found.</summary>
        ERROR_IPSEC_MM_FILTER_NOT_FOUND = 0x32CF,
        /// <summary>[13008] The specified transport mode filter already exists.</summary>
        ERROR_IPSEC_TRANSPORT_FILTER_EXISTS = 0x32D0,
        /// <summary>[13009] The specified transport mode filter does not exist.</summary>
        ERROR_IPSEC_TRANSPORT_FILTER_NOT_FOUND = 0x32D1,
        /// <summary>[13010] The specified main mode authentication list exists.</summary>
        ERROR_IPSEC_MM_AUTH_EXISTS = 0x32D2,
        /// <summary>[13011] The specified main mode authentication list was not found.</summary>
        ERROR_IPSEC_MM_AUTH_NOT_FOUND = 0x32D3,
        /// <summary>[13012] The specified main mode authentication list is being used.</summary>
        ERROR_IPSEC_MM_AUTH_IN_USE = 0x32D4,
        /// <summary>[13013] The specified default main mode policy was not found.</summary>
        ERROR_IPSEC_DEFAULT_MM_POLICY_NOT_FOUND = 0x32D5,
        /// <summary>[13014] The specified default main mode authentication list was not found.</summary>
        ERROR_IPSEC_DEFAULT_MM_AUTH_NOT_FOUND = 0x32D6,
        /// <summary>[13015] The specified default quick mode policy was not found.</summary>
        ERROR_IPSEC_DEFAULT_QM_POLICY_NOT_FOUND = 0x32D7,
        /// <summary>[13016] The specified tunnel mode filter exists.</summary>
        ERROR_IPSEC_TUNNEL_FILTER_EXISTS = 0x32D8,
        /// <summary>[13017] The specified tunnel mode filter was not found.</summary>
        ERROR_IPSEC_TUNNEL_FILTER_NOT_FOUND = 0x32D9,
        /// <summary>[13018] The Main Mode filter is pending deletion.</summary>
        ERROR_IPSEC_MM_FILTER_PENDING_DELETION = 0x32DA,
        /// <summary>[13019] The transport filter is pending deletion.</summary>
        ERROR_IPSEC_TRANSPORT_FILTER_PENDING_DELETION = 0x32DB,
        /// <summary>[13020] The tunnel filter is pending deletion.</summary>
        ERROR_IPSEC_TUNNEL_FILTER_PENDING_DELETION = 0x32DC,
        /// <summary>[13021] The Main Mode policy is pending deletion.</summary>
        ERROR_IPSEC_MM_POLICY_PENDING_DELETION = 0x32DD,
        /// <summary>[13022] The Main Mode authentication bundle is pending deletion.</summary>
        ERROR_IPSEC_MM_AUTH_PENDING_DELETION = 0x32DE,
        /// <summary>[13023] The Quick Mode policy is pending deletion.</summary>
        ERROR_IPSEC_QM_POLICY_PENDING_DELETION = 0x32DF,
        /// <summary>[13024] The Main Mode policy was successfully added, but some of the requested offers are not supported.</summary>
        WARNING_IPSEC_MM_POLICY_PRUNED = 0x32E0,
        /// <summary>[13025] The Quick Mode policy was successfully added, but some of the requested offers are not supported.</summary>
        WARNING_IPSEC_QM_POLICY_PRUNED = 0x32E1,
        /// <summary>[13800] ERROR_IPSEC_IKE_NEG_STATUS_BEGIN</summary>
        ERROR_IPSEC_IKE_NEG_STATUS_BEGIN = 0x35E8,
        /// <summary>[13801] IKE authentication credentials are unacceptable.</summary>
        ERROR_IPSEC_IKE_AUTH_FAIL = 0x35E9,
        /// <summary>[13802] IKE security attributes are unacceptable.</summary>
        ERROR_IPSEC_IKE_ATTRIB_FAIL = 0x35EA,
        /// <summary>[13803] IKE Negotiation in progress.</summary>
        ERROR_IPSEC_IKE_NEGOTIATION_PENDING = 0x35EB,
        /// <summary>[13804] General processing error.</summary>
        ERROR_IPSEC_IKE_GENERAL_PROCESSING_ERROR = 0x35EC,
        /// <summary>[13805] Negotiation timed out.</summary>
        ERROR_IPSEC_IKE_TIMED_OUT = 0x35ED,
        /// <summary>[13806] IKE failed to find valid machine certificate. Contact your Network Security Administrator about installing a valid certificate in the appropriate Certificate Store.</summary>
        ERROR_IPSEC_IKE_NO_CERT = 0x35EE,
        /// <summary>[13807] IKE SA deleted by peer before establishment completed.</summary>
        ERROR_IPSEC_IKE_SA_DELETED = 0x35EF,
        /// <summary>[13808] IKE SA deleted before establishment completed.</summary>
        ERROR_IPSEC_IKE_SA_REAPED = 0x35F0,
        /// <summary>[13809] Negotiation request sat in Queue too long.</summary>
        ERROR_IPSEC_IKE_MM_ACQUIRE_DROP = 0x35F1,
        /// <summary>[13810] Negotiation request sat in Queue too long.</summary>
        ERROR_IPSEC_IKE_QM_ACQUIRE_DROP = 0x35F2,
        /// <summary>[13811] Negotiation request sat in Queue too long.</summary>
        ERROR_IPSEC_IKE_QUEUE_DROP_MM = 0x35F3,
        /// <summary>[13812] Negotiation request sat in Queue too long.</summary>
        ERROR_IPSEC_IKE_QUEUE_DROP_NO_MM = 0x35F4,
        /// <summary>[13813] No response from peer.</summary>
        ERROR_IPSEC_IKE_DROP_NO_RESPONSE = 0x35F5,
        /// <summary>[13814] Negotiation took too long.</summary>
        ERROR_IPSEC_IKE_MM_DELAY_DROP = 0x35F6,
        /// <summary>[13815] Negotiation took too long.</summary>
        ERROR_IPSEC_IKE_QM_DELAY_DROP = 0x35F7,
        /// <summary>[13816] Unknown error occurred.</summary>
        ERROR_IPSEC_IKE_ERROR = 0x35F8,
        /// <summary>[13817] Certificate Revocation Check failed.</summary>
        ERROR_IPSEC_IKE_CRL_FAILED = 0x35F9,
        /// <summary>[13818] Invalid certificate key usage.</summary>
        ERROR_IPSEC_IKE_INVALID_KEY_USAGE = 0x35FA,
        /// <summary>[13819] Invalid certificate type.</summary>
        ERROR_IPSEC_IKE_INVALID_CERT_TYPE = 0x35FB,
        /// <summary>[13820] IKE negotiation failed because the machine certificate used does not have a private key. IPsec certificates require a private key. Contact your Network Security administrator about replacing with a certificate that has a private key.</summary>
        ERROR_IPSEC_IKE_NO_PRIVATE_KEY = 0x35FC,
        /// <summary>[13821] Simultaneous rekeys were detected.</summary>
        ERROR_IPSEC_IKE_SIMULTANEOUS_REKEY = 0x35FD,
        /// <summary>[13822] Failure in Diffie-Hellman computation.</summary>
        ERROR_IPSEC_IKE_DH_FAIL = 0x35FE,
        /// <summary>[13823] Don't know how to process critical payload.</summary>
        ERROR_IPSEC_IKE_CRITICAL_PAYLOAD_NOT_RECOGNIZED = 0x35FF,
        /// <summary>[13824] Invalid header.</summary>
        ERROR_IPSEC_IKE_INVALID_HEADER = 0x3600,
        /// <summary>[13825] No policy configured.</summary>
        ERROR_IPSEC_IKE_NO_POLICY = 0x3601,
        /// <summary>[13826] Failed to verify signature.</summary>
        ERROR_IPSEC_IKE_INVALID_SIGNATURE = 0x3602,
        /// <summary>[13827] Failed to authenticate using Kerberos.</summary>
        ERROR_IPSEC_IKE_KERBEROS_ERROR = 0x3603,
        /// <summary>[13828] Peer's certificate did not have a public key.</summary>
        ERROR_IPSEC_IKE_NO_PUBLIC_KEY = 0x3604,
        /// <summary>[13829] Error processing error payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR = 0x3605,
        /// <summary>[13830] Error processing SA payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_SA = 0x3606,
        /// <summary>[13831] Error processing Proposal payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_PROP = 0x3607,
        /// <summary>[13832] Error processing Transform payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_TRANS = 0x3608,
        /// <summary>[13833] Error processing KE payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_KE = 0x3609,
        /// <summary>[13834] Error processing ID payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_ID = 0x360A,
        /// <summary>[13835] Error processing Cert payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_CERT = 0x360B,
        /// <summary>[13836] Error processing Certificate Request payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_CERT_REQ = 0x360C,
        /// <summary>[13837] Error processing Hash payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_HASH = 0x360D,
        /// <summary>[13838] Error processing Signature payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_SIG = 0x360E,
        /// <summary>[13839] Error processing Nonce payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_NONCE = 0x360F,
        /// <summary>[13840] Error processing Notify payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_NOTIFY = 0x3610,
        /// <summary>[13841] Error processing Delete Payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_DELETE = 0x3611,
        /// <summary>[13842] Error processing VendorId payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_VENDOR = 0x3612,
        /// <summary>[13843] Invalid payload received.</summary>
        ERROR_IPSEC_IKE_INVALID_PAYLOAD = 0x3613,
        /// <summary>[13844] Soft SA loaded.</summary>
        ERROR_IPSEC_IKE_LOAD_SOFT_SA = 0x3614,
        /// <summary>[13845] Soft SA torn down.</summary>
        ERROR_IPSEC_IKE_SOFT_SA_TORN_DOWN = 0x3615,
        /// <summary>[13846] Invalid cookie received.</summary>
        ERROR_IPSEC_IKE_INVALID_COOKIE = 0x3616,
        /// <summary>[13847] Peer failed to send valid machine certificate.</summary>
        ERROR_IPSEC_IKE_NO_PEER_CERT = 0x3617,
        /// <summary>[13848] Certification Revocation check of peer's certificate failed.</summary>
        ERROR_IPSEC_IKE_PEER_CRL_FAILED = 0x3618,
        /// <summary>[13849] New policy invalidated SAs formed with old policy.</summary>
        ERROR_IPSEC_IKE_POLICY_CHANGE = 0x3619,
        /// <summary>[13850] There is no available Main Mode IKE policy.</summary>
        ERROR_IPSEC_IKE_NO_MM_POLICY = 0x361A,
        /// <summary>[13851] Failed to enabled TCB privilege.</summary>
        ERROR_IPSEC_IKE_NOTCBPRIV = 0x361B,
        /// <summary>[13852] Failed to load SECURITY.DLL.</summary>
        ERROR_IPSEC_IKE_SECLOADFAIL = 0x361C,
        /// <summary>[13853] Failed to obtain security function table dispatch address from SSPI.</summary>
        ERROR_IPSEC_IKE_FAILSSPINIT = 0x361D,
        /// <summary>[13854] Failed to query Kerberos package to obtain max token size.</summary>
        ERROR_IPSEC_IKE_FAILQUERYSSP = 0x361E,
        /// <summary>[13855] Failed to obtain Kerberos server credentials for ISAKMP/ERROR_IPSEC_IKE service. Kerberos authentication will not function. The most likely reason for this is lack of domain membership. This is normal if your computer is a member of a workgroup.</summary>
        ERROR_IPSEC_IKE_SRVACQFAIL = 0x361F,
        /// <summary>[13856] Failed to determine SSPI principal name for ISAKMP/ERROR_IPSEC_IKE service (QueryCredentialsAttributes).</summary>
        ERROR_IPSEC_IKE_SRVQUERYCRED = 0x3620,
        /// <summary>[13857] Failed to obtain new SPI for the inbound SA from IPsec driver. The most common cause for this is that the driver does not have the correct filter. Check your policy to verify the filters.</summary>
        ERROR_IPSEC_IKE_GETSPIFAIL = 0x3621,
        /// <summary>[13858] Given filter is invalid.</summary>
        ERROR_IPSEC_IKE_INVALID_FILTER = 0x3622,
        /// <summary>[13859] Memory allocation failed.</summary>
        ERROR_IPSEC_IKE_OUT_OF_MEMORY = 0x3623,
        /// <summary>[13860] Failed to add Security Association to IPsec Driver. The most common cause for this is if the IKE negotiation took too long to complete. If the problem persists, reduce the load on the faulting machine.</summary>
        ERROR_IPSEC_IKE_ADD_UPDATE_KEY_FAILED = 0x3624,
        /// <summary>[13861] Invalid policy.</summary>
        ERROR_IPSEC_IKE_INVALID_POLICY = 0x3625,
        /// <summary>[13862] Invalid DOI.</summary>
        ERROR_IPSEC_IKE_UNKNOWN_DOI = 0x3626,
        /// <summary>[13863] Invalid situation.</summary>
        ERROR_IPSEC_IKE_INVALID_SITUATION = 0x3627,
        /// <summary>[13864] Diffie-Hellman failure.</summary>
        ERROR_IPSEC_IKE_DH_FAILURE = 0x3628,
        /// <summary>[13865] Invalid Diffie-Hellman group.</summary>
        ERROR_IPSEC_IKE_INVALID_GROUP = 0x3629,
        /// <summary>[13866] Error encrypting payload.</summary>
        ERROR_IPSEC_IKE_ENCRYPT = 0x362A,
        /// <summary>[13867] Error decrypting payload.</summary>
        ERROR_IPSEC_IKE_DECRYPT = 0x362B,
        /// <summary>[13868] Policy match error.</summary>
        ERROR_IPSEC_IKE_POLICY_MATCH = 0x362C,
        /// <summary>[13869] Unsupported ID.</summary>
        ERROR_IPSEC_IKE_UNSUPPORTED_ID = 0x362D,
        /// <summary>[13870] Hash verification failed.</summary>
        ERROR_IPSEC_IKE_INVALID_HASH = 0x362E,
        /// <summary>[13871] Invalid hash algorithm.</summary>
        ERROR_IPSEC_IKE_INVALID_HASH_ALG = 0x362F,
        /// <summary>[13872] Invalid hash size.</summary>
        ERROR_IPSEC_IKE_INVALID_HASH_SIZE = 0x3630,
        /// <summary>[13873] Invalid encryption algorithm.</summary>
        ERROR_IPSEC_IKE_INVALID_ENCRYPT_ALG = 0x3631,
        /// <summary>[13874] Invalid authentication algorithm.</summary>
        ERROR_IPSEC_IKE_INVALID_AUTH_ALG = 0x3632,
        /// <summary>[13875] Invalid certificate signature.</summary>
        ERROR_IPSEC_IKE_INVALID_SIG = 0x3633,
        /// <summary>[13876] Load failed.</summary>
        ERROR_IPSEC_IKE_LOAD_FAILED = 0x3634,
        /// <summary>[13877] Deleted via RPC call.</summary>
        ERROR_IPSEC_IKE_RPC_DELETE = 0x3635,
        /// <summary>[13878] Temporary state created to perform reinitialization. This is not a real failure.</summary>
        ERROR_IPSEC_IKE_BENIGN_REINIT = 0x3636,
        /// <summary>[13879] The lifetime value received in the Responder Lifetime Notify is below the Windows 2000 configured minimum value. Please fix the policy on the peer machine.</summary>
        ERROR_IPSEC_IKE_INVALID_RESPONDER_LIFETIME_NOTIFY = 0x3637,
        /// <summary>[13880] The recipient cannot handle version of IKE specified in the header.</summary>
        ERROR_IPSEC_IKE_INVALID_MAJOR_VERSION = 0x3638,
        /// <summary>[13881] Key length in certificate is too small for configured security requirements.</summary>
        ERROR_IPSEC_IKE_INVALID_CERT_KEYLEN = 0x3639,
        /// <summary>[13882] Max number of established MM SAs to peer exceeded.</summary>
        ERROR_IPSEC_IKE_MM_LIMIT = 0x363A,
        /// <summary>[13883] IKE received a policy that disables negotiation.</summary>
        ERROR_IPSEC_IKE_NEGOTIATION_DISABLED = 0x363B,
        /// <summary>[13884] Reached maximum quick mode limit for the main mode. New main mode will be started.</summary>
        ERROR_IPSEC_IKE_QM_LIMIT = 0x363C,
        /// <summary>[13885] Main mode SA lifetime expired or peer sent a main mode delete.</summary>
        ERROR_IPSEC_IKE_MM_EXPIRED = 0x363D,
        /// <summary>[13886] Main mode SA assumed to be invalid because peer stopped responding.</summary>
        ERROR_IPSEC_IKE_PEER_MM_ASSUMED_INVALID = 0x363E,
        /// <summary>[13887] Certificate doesn't chain to a trusted root in IPsec policy.</summary>
        ERROR_IPSEC_IKE_CERT_CHAIN_POLICY_MISMATCH = 0x363F,
        /// <summary>[13888] Received unexpected message ID.</summary>
        ERROR_IPSEC_IKE_UNEXPECTED_MESSAGE_ID = 0x3640,
        /// <summary>[13889] Received invalid authentication offers.</summary>
        ERROR_IPSEC_IKE_INVALID_AUTH_PAYLOAD = 0x3641,
        /// <summary>[13890] Sent DoS cookie notify to initiator.</summary>
        ERROR_IPSEC_IKE_DOS_COOKIE_SENT = 0x3642,
        /// <summary>[13891] IKE service is shutting down.</summary>
        ERROR_IPSEC_IKE_SHUTTING_DOWN = 0x3643,
        /// <summary>[13892] Could not verify binding between CGA address and certificate.</summary>
        ERROR_IPSEC_IKE_CGA_AUTH_FAILED = 0x3644,
        /// <summary>[13893] Error processing NatOA payload.</summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_NATOA = 0x3645,
        /// <summary>[13894] Parameters of the main mode are invalid for this quick mode.</summary>
        ERROR_IPSEC_IKE_INVALID_MM_FOR_QM = 0x3646,
        /// <summary>[13895] Quick mode SA was expired by IPsec driver.</summary>
        ERROR_IPSEC_IKE_QM_EXPIRED = 0x3647,
        /// <summary>[13896] Too many dynamically added IKEEXT filters were detected.</summary>
        ERROR_IPSEC_IKE_TOO_MANY_FILTERS = 0x3648,
        /// <summary>[13897] ERROR_IPSEC_IKE_NEG_STATUS_END</summary>
        ERROR_IPSEC_IKE_NEG_STATUS_END = 0x3649,
        /// <summary>[13898] NAP reauth succeeded and must delete the dummy NAP IKEv2 tunnel.</summary>
        ERROR_IPSEC_IKE_KILL_DUMMY_NAP_TUNNEL = 0x364A,
        /// <summary>[13899] Error in assigning inner IP address to initiator in tunnel mode.</summary>
        ERROR_IPSEC_IKE_INNER_IP_ASSIGNMENT_FAILURE = 0x364B,
        /// <summary>[13900] Require configuration payload missing.</summary>
        ERROR_IPSEC_IKE_REQUIRE_CP_PAYLOAD_MISSING = 0x364C,
        /// <summary>[13901] A negotiation running as the security principle who issued the connection is in progress.</summary>
        ERROR_IPSEC_KEY_MODULE_IMPERSONATION_NEGOTIATION_PENDING = 0x364D,
        /// <summary>[13902] SA was deleted due to IKEv1/AuthIP co-existence suppress check.</summary>
        ERROR_IPSEC_IKE_COEXISTENCE_SUPPRESS = 0x364E,
        /// <summary>[13903] Incoming SA request was dropped due to peer IP address rate limiting.</summary>
        ERROR_IPSEC_IKE_RATELIMIT_DROP = 0x364F,
        /// <summary>[13904] Peer does not support MOBIKE.</summary>
        ERROR_IPSEC_IKE_PEER_DOESNT_SUPPORT_MOBIKE = 0x3650,
        /// <summary>[13905] SA establishment is not authorized.</summary>
        ERROR_IPSEC_IKE_AUTHORIZATION_FAILURE = 0x3651,
        /// <summary>[13906] SA establishment is not authorized because there is not a sufficiently strong PKINIT-based credential.</summary>
        ERROR_IPSEC_IKE_STRONG_CRED_AUTHORIZATION_FAILURE = 0x3652,
        /// <summary>[13907] SA establishment is not authorized. You may need to enter updated or different credentials such as a smartcard.</summary>
        ERROR_IPSEC_IKE_AUTHORIZATION_FAILURE_WITH_OPTIONAL_RETRY = 0x3653,
        /// <summary>[13908] SA establishment is not authorized because there is not a sufficiently strong PKINIT-based credential. This might be related to certificate-to-account mapping failure for the SA.</summary>
        ERROR_IPSEC_IKE_STRONG_CRED_AUTHORIZATION_AND_CERTMAP_FAILURE = 0x3654,
        /// <summary>[13909] ERROR_IPSEC_IKE_NEG_STATUS_EXTENDED_END</summary>
        ERROR_IPSEC_IKE_NEG_STATUS_EXTENDED_END = 0x3655,
        /// <summary>[13910] The SPI in the packet does not match a valid IPsec SA.</summary>
        ERROR_IPSEC_BAD_SPI = 0x3656,
        /// <summary>[13911] Packet was received on an IPsec SA whose lifetime has expired.</summary>
        ERROR_IPSEC_SA_LIFETIME_EXPIRED = 0x3657,
        /// <summary>[13912] Packet was received on an IPsec SA that does not match the packet characteristics.</summary>
        ERROR_IPSEC_WRONG_SA = 0x3658,
        /// <summary>[13913] Packet sequence number replay check failed.</summary>
        ERROR_IPSEC_REPLAY_CHECK_FAILED = 0x3659,
        /// <summary>[13914] IPsec header and/or trailer in the packet is invalid.</summary>
        ERROR_IPSEC_INVALID_PACKET = 0x365A,
        /// <summary>[13915] IPsec integrity check failed.</summary>
        ERROR_IPSEC_INTEGRITY_CHECK_FAILED = 0x365B,
        /// <summary>[13916] IPsec dropped a clear text packet.</summary>
        ERROR_IPSEC_CLEAR_TEXT_DROP = 0x365C,
        /// <summary>[13917] IPsec dropped an incoming ESP packet in authenticated firewall mode. This drop is benign.</summary>
        ERROR_IPSEC_AUTH_FIREWALL_DROP = 0x365D,
        /// <summary>[13918] IPsec dropped a packet due to DoS throttling.</summary>
        ERROR_IPSEC_THROTTLE_DROP = 0x365E,
        /// <summary>[13925] IPsec DoS Protection matched an explicit block rule.</summary>
        ERROR_IPSEC_DOSP_BLOCK = 0x3665,
        /// <summary>[13926] IPsec DoS Protection received an IPsec specific multicast packet which is not allowed.</summary>
        ERROR_IPSEC_DOSP_RECEIVED_MULTICAST = 0x3666,
        /// <summary>[13927] IPsec DoS Protection received an incorrectly formatted packet.</summary>
        ERROR_IPSEC_DOSP_INVALID_PACKET = 0x3667,
        /// <summary>[13928] IPsec DoS Protection failed to look up state.</summary>
        ERROR_IPSEC_DOSP_STATE_LOOKUP_FAILED = 0x3668,
        /// <summary>[13929] IPsec DoS Protection failed to create state because the maximum number of entries allowed by policy has been reached.</summary>
        ERROR_IPSEC_DOSP_MAX_ENTRIES = 0x3669,
        /// <summary>[13930] IPsec DoS Protection received an IPsec negotiation packet for a keying module which is not allowed by policy.</summary>
        ERROR_IPSEC_DOSP_KEYMOD_NOT_ALLOWED = 0x366A,
        /// <summary>[13931] IPsec DoS Protection has not been enabled.</summary>
        ERROR_IPSEC_DOSP_NOT_INSTALLED = 0x366B,
        /// <summary>[13932] IPsec DoS Protection failed to create a per internal IP rate limit queue because the maximum number of queues allowed by policy has been reached.</summary>
        ERROR_IPSEC_DOSP_MAX_PER_IP_RATELIMIT_QUEUES = 0x366C,
        /// <summary>[14000] The requested section was not present in the activation context.</summary>
        ERROR_SXS_SECTION_NOT_FOUND = 0x36B0,
        /// <summary>[14001] The application has failed to start because its side-by-side configuration is incorrect. Please see the application event log or use the command-line sxstrace.exe tool for more detail.</summary>
        ERROR_SXS_CANT_GEN_ACTCTX = 0x36B1,
        /// <summary>[14002] The application binding data format is invalid.</summary>
        ERROR_SXS_INVALID_ACTCTXDATA_FORMAT = 0x36B2,
        /// <summary>[14003] The referenced assembly is not installed on your system.</summary>
        ERROR_SXS_ASSEMBLY_NOT_FOUND = 0x36B3,
        /// <summary>[14004] The manifest file does not begin with the required tag and format information.</summary>
        ERROR_SXS_MANIFEST_FORMAT_ERROR = 0x36B4,
        /// <summary>[14005] The manifest file contains one or more syntax errors.</summary>
        ERROR_SXS_MANIFEST_PARSE_ERROR = 0x36B5,
        /// <summary>[14006] The application attempted to activate a disabled activation context.</summary>
        ERROR_SXS_ACTIVATION_CONTEXT_DISABLED = 0x36B6,
        /// <summary>[14007] The requested lookup key was not found in any active activation context.</summary>
        ERROR_SXS_KEY_NOT_FOUND = 0x36B7,
        /// <summary>[14008] A component version required by the application conflicts with another component version already active.</summary>
        ERROR_SXS_VERSION_CONFLICT = 0x36B8,
        /// <summary>[14009] The type requested activation context section does not match the query API used.</summary>
        ERROR_SXS_WRONG_SECTION_TYPE = 0x36B9,
        /// <summary>[14010] Lack of system resources has required isolated activation to be disabled for the current thread of execution.</summary>
        ERROR_SXS_THREAD_QUERIES_DISABLED = 0x36BA,
        /// <summary>[14011] An attempt to set the process default activation context failed because the process default activation context was already set.</summary>
        ERROR_SXS_PROCESS_DEFAULT_ALREADY_SET = 0x36BB,
        /// <summary>[14012] The encoding group identifier specified is not recognized.</summary>
        ERROR_SXS_UNKNOWN_ENCODING_GROUP = 0x36BC,
        /// <summary>[14013] The encoding requested is not recognized.</summary>
        ERROR_SXS_UNKNOWN_ENCODING = 0x36BD,
        /// <summary>[14014] The manifest contains a reference to an invalid URI.</summary>
        ERROR_SXS_INVALID_XML_NAMESPACE_URI = 0x36BE,
        /// <summary>[14015] The application manifest contains a reference to a dependent assembly which is not installed.</summary>
        ERROR_SXS_ROOT_MANIFEST_DEPENDENCY_NOT_INSTALLED = 0x36BF,
        /// <summary>[14016] The manifest for an assembly used by the application has a reference to a dependent assembly which is not installed.</summary>
        ERROR_SXS_LEAF_MANIFEST_DEPENDENCY_NOT_INSTALLED = 0x36C0,
        /// <summary>[14017] The manifest contains an attribute for the assembly identity which is not valid.</summary>
        ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE = 0x36C1,
        /// <summary>[14018] The manifest is missing the required default namespace specification on the assembly element.</summary>
        ERROR_SXS_MANIFEST_MISSING_REQUIRED_DEFAULT_NAMESPACE = 0x36C2,
        /// <summary>[14019] The manifest has a default namespace specified on the assembly element but its value is not "urn:schemas-microsoft-com:asm.v1".</summary>
        ERROR_SXS_MANIFEST_INVALID_REQUIRED_DEFAULT_NAMESPACE = 0x36C3,
        /// <summary>[14020] The private manifest probed has crossed a path with an unsupported reparse point.</summary>
        ERROR_SXS_PRIVATE_MANIFEST_CROSS_PATH_WITH_REPARSE_POINT = 0x36C4,
        /// <summary>[14021] Two or more components referenced directly or indirectly by the application manifest have files by the same name.</summary>
        ERROR_SXS_DUPLICATE_DLL_NAME = 0x36C5,
        /// <summary>[14022] Two or more components referenced directly or indirectly by the application manifest have window classes with the same name.</summary>
        ERROR_SXS_DUPLICATE_WINDOWCLASS_NAME = 0x36C6,
        /// <summary>[14023] Two or more components referenced directly or indirectly by the application manifest have the same COM server CLSIDs.</summary>
        ERROR_SXS_DUPLICATE_CLSID = 0x36C7,
        /// <summary>[14024] Two or more components referenced directly or indirectly by the application manifest have proxies for the same COM interface IIDs.</summary>
        ERROR_SXS_DUPLICATE_IID = 0x36C8,
        /// <summary>[14025] Two or more components referenced directly or indirectly by the application manifest have the same COM type library TLBIDs.</summary>
        ERROR_SXS_DUPLICATE_TLBID = 0x36C9,
        /// <summary>[14026] Two or more components referenced directly or indirectly by the application manifest have the same COM ProgIDs.</summary>
        ERROR_SXS_DUPLICATE_PROGID = 0x36CA,
        /// <summary>[14027] Two or more components referenced directly or indirectly by the application manifest are different versions of the same component which is not permitted.</summary>
        ERROR_SXS_DUPLICATE_ASSEMBLY_NAME = 0x36CB,
        /// <summary>[14028] A component's file does not match the verification information present in the component manifest.</summary>
        ERROR_SXS_FILE_HASH_MISMATCH = 0x36CC,
        /// <summary>[14029] The policy manifest contains one or more syntax errors.</summary>
        ERROR_SXS_POLICY_PARSE_ERROR = 0x36CD,
        /// <summary>[14030] Manifest Parse Error : A string literal was expected, but no opening quote character was found.</summary>
        ERROR_SXS_XML_E_MISSINGQUOTE = 0x36CE,
        /// <summary>[14031] Manifest Parse Error : Incorrect syntax was used in a comment.</summary>
        ERROR_SXS_XML_E_COMMENTSYNTAX = 0x36CF,
        /// <summary>[14032] Manifest Parse Error : A name was started with an invalid character.</summary>
        ERROR_SXS_XML_E_BADSTARTNAMECHAR = 0x36D0,
        /// <summary>[14033] Manifest Parse Error : A name contained an invalid character.</summary>
        ERROR_SXS_XML_E_BADNAMECHAR = 0x36D1,
        /// <summary>[14034] Manifest Parse Error : A string literal contained an invalid character.</summary>
        ERROR_SXS_XML_E_BADCHARINSTRING = 0x36D2,
        /// <summary>[14035] Manifest Parse Error : Invalid syntax for an xml declaration.</summary>
        ERROR_SXS_XML_E_XMLDECLSYNTAX = 0x36D3,
        /// <summary>[14036] Manifest Parse Error : An Invalid character was found in text content.</summary>
        ERROR_SXS_XML_E_BADCHARDATA = 0x36D4,
        /// <summary>[14037] Manifest Parse Error : Required white space was missing.</summary>
        ERROR_SXS_XML_E_MISSINGWHITESPACE = 0x36D5,
        /// <summary>[14038] Manifest Parse Error : The character '>' was expected.</summary>
        ERROR_SXS_XML_E_EXPECTINGTAGEND = 0x36D6,
        /// <summary>[14039] Manifest Parse Error : A semi colon character was expected.</summary>
        ERROR_SXS_XML_E_MISSINGSEMICOLON = 0x36D7,
        /// <summary>[14040] Manifest Parse Error : Unbalanced parentheses.</summary>
        ERROR_SXS_XML_E_UNBALANCEDPAREN = 0x36D8,
        /// <summary>[14041] Manifest Parse Error : Internal error.</summary>
        ERROR_SXS_XML_E_INTERNALERROR = 0x36D9,
        /// <summary>[14042] Manifest Parse Error : Whitespace is not allowed at this location.</summary>
        ERROR_SXS_XML_E_UNEXPECTED_WHITESPACE = 0x36DA,
        /// <summary>[14043] Manifest Parse Error : End of file reached in invalid state for current encoding.</summary>
        ERROR_SXS_XML_E_INCOMPLETE_ENCODING = 0x36DB,
        /// <summary>[14044] Manifest Parse Error : Missing parenthesis.</summary>
        ERROR_SXS_XML_E_MISSING_PAREN = 0x36DC,
        /// <summary>[14045] Manifest Parse Error : A single or double closing quote character (\' or \") is missing.</summary>
        ERROR_SXS_XML_E_EXPECTINGCLOSEQUOTE = 0x36DD,
        /// <summary>[14046] Manifest Parse Error : Multiple colons are not allowed in a name.</summary>
        ERROR_SXS_XML_E_MULTIPLE_COLONS = 0x36DE,
        /// <summary>[14047] Manifest Parse Error : Invalid character for decimal digit.</summary>
        ERROR_SXS_XML_E_INVALID_DECIMAL = 0x36DF,
        /// <summary>[14048] Manifest Parse Error : Invalid character for hexadecimal digit.</summary>
        ERROR_SXS_XML_E_INVALID_HEXIDECIMAL = 0x36E0,
        /// <summary>[14049] Manifest Parse Error : Invalid unicode character value for this platform.</summary>
        ERROR_SXS_XML_E_INVALID_UNICODE = 0x36E1,
        /// <summary>[14050] Manifest Parse Error : Expecting whitespace or '?'.</summary>
        ERROR_SXS_XML_E_WHITESPACEORQUESTIONMARK = 0x36E2,
        /// <summary>[14051] Manifest Parse Error : End tag was not expected at this location.</summary>
        ERROR_SXS_XML_E_UNEXPECTEDENDTAG = 0x36E3,
        /// <summary>[14052] Manifest Parse Error : The following tags were not closed: %1.</summary>
        ERROR_SXS_XML_E_UNCLOSEDTAG = 0x36E4,
        /// <summary>[14053] Manifest Parse Error : Duplicate attribute.</summary>
        ERROR_SXS_XML_E_DUPLICATEATTRIBUTE = 0x36E5,
        /// <summary>[14054] Manifest Parse Error : Only one top level element is allowed in an XML document.</summary>
        ERROR_SXS_XML_E_MULTIPLEROOTS = 0x36E6,
        /// <summary>[14055] Manifest Parse Error : Invalid at the top level of the document.</summary>
        ERROR_SXS_XML_E_INVALIDATROOTLEVEL = 0x36E7,
        /// <summary>[14056] Manifest Parse Error : Invalid xml declaration.</summary>
        ERROR_SXS_XML_E_BADXMLDECL = 0x36E8,
        /// <summary>[14057] Manifest Parse Error : XML document must have a top level element.</summary>
        ERROR_SXS_XML_E_MISSINGROOT = 0x36E9,
        /// <summary>[14058] Manifest Parse Error : Unexpected end of file.</summary>
        ERROR_SXS_XML_E_UNEXPECTEDEOF = 0x36EA,
        /// <summary>[14059] Manifest Parse Error : Parameter entities cannot be used inside markup declarations in an internal subset.</summary>
        ERROR_SXS_XML_E_BADPEREFINSUBSET = 0x36EB,
        /// <summary>[14060] Manifest Parse Error : Element was not closed.</summary>
        ERROR_SXS_XML_E_UNCLOSEDSTARTTAG = 0x36EC,
        /// <summary>[14061] Manifest Parse Error : End element was missing the character '>'.</summary>
        ERROR_SXS_XML_E_UNCLOSEDENDTAG = 0x36ED,
        /// <summary>[14062] Manifest Parse Error : A string literal was not closed.</summary>
        ERROR_SXS_XML_E_UNCLOSEDSTRING = 0x36EE,
        /// <summary>[14063] Manifest Parse Error : A comment was not closed.</summary>
        ERROR_SXS_XML_E_UNCLOSEDCOMMENT = 0x36EF,
        /// <summary>[14064] Manifest Parse Error : A declaration was not closed.</summary>
        ERROR_SXS_XML_E_UNCLOSEDDECL = 0x36F0,
        /// <summary>[14065] Manifest Parse Error : A CDATA section was not closed.</summary>
        ERROR_SXS_XML_E_UNCLOSEDCDATA = 0x36F1,
        /// <summary>[14066] Manifest Parse Error : The namespace prefix is not allowed to start with the reserved string "xml".</summary>
        ERROR_SXS_XML_E_RESERVEDNAMESPACE = 0x36F2,
        /// <summary>[14067] Manifest Parse Error : System does not support the specified encoding.</summary>
        ERROR_SXS_XML_E_INVALIDENCODING = 0x36F3,
        /// <summary>[14068] Manifest Parse Error : Switch from current encoding to specified encoding not supported.</summary>
        ERROR_SXS_XML_E_INVALIDSWITCH = 0x36F4,
        /// <summary>[14069] Manifest Parse Error : The name 'xml' is reserved and must be lower case.</summary>
        ERROR_SXS_XML_E_BADXMLCASE = 0x36F5,
        /// <summary>[14070] Manifest Parse Error : The standalone attribute must have the value 'yes' or 'no'.</summary>
        ERROR_SXS_XML_E_INVALID_STANDALONE = 0x36F6,
        /// <summary>[14071] Manifest Parse Error : The standalone attribute cannot be used in external entities.</summary>
        ERROR_SXS_XML_E_UNEXPECTED_STANDALONE = 0x36F7,
        /// <summary>[14072] Manifest Parse Error : Invalid version number.</summary>
        ERROR_SXS_XML_E_INVALID_VERSION = 0x36F8,
        /// <summary>[14073] Manifest Parse Error : Missing equals sign between attribute and attribute value.</summary>
        ERROR_SXS_XML_E_MISSINGEQUALS = 0x36F9,
        /// <summary>[14074] Assembly Protection Error : Unable to recover the specified assembly.</summary>
        ERROR_SXS_PROTECTION_RECOVERY_FAILED = 0x36FA,
        /// <summary>[14075] Assembly Protection Error : The public key for an assembly was too short to be allowed.</summary>
        ERROR_SXS_PROTECTION_PUBLIC_KEY_TOO_SHORT = 0x36FB,
        /// <summary>[14076] Assembly Protection Error : The catalog for an assembly is not valid, or does not match the assembly's manifest.</summary>
        ERROR_SXS_PROTECTION_CATALOG_NOT_VALID = 0x36FC,
        /// <summary>[14077] An HRESULT could not be translated to a corresponding Win32 error code.</summary>
        ERROR_SXS_UNTRANSLATABLE_HRESULT = 0x36FD,
        /// <summary>[14078] Assembly Protection Error : The catalog for an assembly is missing.</summary>
        ERROR_SXS_PROTECTION_CATALOG_FILE_MISSING = 0x36FE,
        /// <summary>[14079] The supplied assembly identity is missing one or more attributes which must be present in this context.</summary>
        ERROR_SXS_MISSING_ASSEMBLY_IDENTITY_ATTRIBUTE = 0x36FF,
        /// <summary>[14080] The supplied assembly identity has one or more attribute names that contain characters not permitted in XML names.</summary>
        ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE_NAME = 0x3700,
        /// <summary>[14081] The referenced assembly could not be found.</summary>
        ERROR_SXS_ASSEMBLY_MISSING = 0x3701,
        /// <summary>[14082] The activation context activation stack for the running thread of execution is corrupt.</summary>
        ERROR_SXS_CORRUPT_ACTIVATION_STACK = 0x3702,
        /// <summary>[14083] The application isolation metadata for this process or thread has become corrupt.</summary>
        ERROR_SXS_CORRUPTION = 0x3703,
        /// <summary>[14084] The activation context being deactivated is not the most recently activated one.</summary>
        ERROR_SXS_EARLY_DEACTIVATION = 0x3704,
        /// <summary>[14085] The activation context being deactivated is not active for the current thread of execution.</summary>
        ERROR_SXS_INVALID_DEACTIVATION = 0x3705,
        /// <summary>[14086] The activation context being deactivated has already been deactivated.</summary>
        ERROR_SXS_MULTIPLE_DEACTIVATION = 0x3706,
        /// <summary>[14087] A component used by the isolation facility has requested to terminate the process.</summary>
        ERROR_SXS_PROCESS_TERMINATION_REQUESTED = 0x3707,
        /// <summary>[14088] A kernel mode component is releasing a reference on an activation context.</summary>
        ERROR_SXS_RELEASE_ACTIVATION_CONTEXT = 0x3708,
        /// <summary>[14089] The activation context of system default assembly could not be generated.</summary>
        ERROR_SXS_SYSTEM_DEFAULT_ACTIVATION_CONTEXT_EMPTY = 0x3709,
        /// <summary>[14090] The value of an attribute in an identity is not within the legal range.</summary>
        ERROR_SXS_INVALID_IDENTITY_ATTRIBUTE_VALUE = 0x370A,
        /// <summary>[14091] The name of an attribute in an identity is not within the legal range.</summary>
        ERROR_SXS_INVALID_IDENTITY_ATTRIBUTE_NAME = 0x370B,
        /// <summary>[14092] An identity contains two definitions for the same attribute.</summary>
        ERROR_SXS_IDENTITY_DUPLICATE_ATTRIBUTE = 0x370C,
        /// <summary>[14093] The identity string is malformed. This may be due to a trailing comma, more than two unnamed attributes, missing attribute name or missing attribute value.</summary>
        ERROR_SXS_IDENTITY_PARSE_ERROR = 0x370D,
        /// <summary>[14094] A string containing localized substitutable content was malformed. Either a dollar sign ($) was followed by something other than a left parenthesis or another dollar sign or an substitution's right parenthesis was not found.</summary>
        ERROR_MALFORMED_SUBSTITUTION_STRING = 0x370E,
        /// <summary>[14095] The public key token does not correspond to the public key specified.</summary>
        ERROR_SXS_INCORRECT_PUBLIC_KEY_TOKEN = 0x370F,
        /// <summary>[14096] A substitution string had no mapping.</summary>
        ERROR_UNMAPPED_SUBSTITUTION_STRING = 0x3710,
        /// <summary>[14097] The component must be locked before making the request.</summary>
        ERROR_SXS_ASSEMBLY_NOT_LOCKED = 0x3711,
        /// <summary>[14098] The component store has been corrupted.</summary>
        ERROR_SXS_COMPONENT_STORE_CORRUPT = 0x3712,
        /// <summary>[14099] An advanced installer failed during setup or servicing.</summary>
        ERROR_ADVANCED_INSTALLER_FAILED = 0x3713,
        /// <summary>[14100] The character encoding in the XML declaration did not match the encoding used in the document.</summary>
        ERROR_XML_ENCODING_MISMATCH = 0x3714,
        /// <summary>[14101] The identities of the manifests are identical but their contents are different.</summary>
        ERROR_SXS_MANIFEST_IDENTITY_SAME_BUT_CONTENTS_DIFFERENT = 0x3715,
        /// <summary>[14102] The component identities are different.</summary>
        ERROR_SXS_IDENTITIES_DIFFERENT = 0x3716,
        /// <summary>[14103] The assembly is not a deployment.</summary>
        ERROR_SXS_ASSEMBLY_IS_NOT_A_DEPLOYMENT = 0x3717,
        /// <summary>[14104] The file is not a part of the assembly.</summary>
        ERROR_SXS_FILE_NOT_PART_OF_ASSEMBLY = 0x3718,
        /// <summary>[14105] The size of the manifest exceeds the maximum allowed.</summary>
        ERROR_SXS_MANIFEST_TOO_BIG = 0x3719,
        /// <summary>[14106] The setting is not registered.</summary>
        ERROR_SXS_SETTING_NOT_REGISTERED = 0x371A,
        /// <summary>[14107] One or more required members of the transaction are not present.</summary>
        ERROR_SXS_TRANSACTION_CLOSURE_INCOMPLETE = 0x371B,
        /// <summary>[14108] The SMI primitive installer failed during setup or servicing.</summary>
        ERROR_SMI_PRIMITIVE_INSTALLER_FAILED = 0x371C,
        /// <summary>[14109] A generic command executable returned a result that indicates failure.</summary>
        ERROR_GENERIC_COMMAND_FAILED = 0x371D,
        /// <summary>[14110] A component is missing file verification information in its manifest.</summary>
        ERROR_SXS_FILE_HASH_MISSING = 0x371E,
        /// <summary>[15000] The specified channel path is invalid.</summary>
        ERROR_EVT_INVALID_CHANNEL_PATH = 0x3A98,
        /// <summary>[15001] The specified query is invalid.</summary>
        ERROR_EVT_INVALID_QUERY = 0x3A99,
        /// <summary>[15002] The publisher metadata cannot be found in the resource.</summary>
        ERROR_EVT_PUBLISHER_METADATA_NOT_FOUND = 0x3A9A,
        /// <summary>[15003] The template for an event definition cannot be found in the resource (error = %1).</summary>
        ERROR_EVT_EVENT_TEMPLATE_NOT_FOUND = 0x3A9B,
        /// <summary>[15004] The specified publisher name is invalid.</summary>
        ERROR_EVT_INVALID_PUBLISHER_NAME = 0x3A9C,
        /// <summary>[15005] The event data raised by the publisher is not compatible with the event template definition in the publisher's manifest.</summary>
        ERROR_EVT_INVALID_EVENT_DATA = 0x3A9D,
        /// <summary>[15007] The specified channel could not be found. Check channel configuration.</summary>
        ERROR_EVT_CHANNEL_NOT_FOUND = 0x3A9F,
        /// <summary>[15008] The specified xml text was not well-formed. See Extended Error for more details.</summary>
        ERROR_EVT_MALFORMED_XML_TEXT = 0x3AA0,
        /// <summary>[15009] The caller is trying to subscribe to a direct channel which is not allowed. The events for a direct channel go directly to a logfile and cannot be subscribed to.</summary>
        ERROR_EVT_SUBSCRIPTION_TO_DIRECT_CHANNEL = 0x3AA1,
        /// <summary>[15010] Configuration error.</summary>
        ERROR_EVT_CONFIGURATION_ERROR = 0x3AA2,
        /// <summary>[15011] The query result is stale / invalid. This may be due to the log being cleared or rolling over after the query result was created. Users should handle this code by releasing the query result object and reissuing the query.</summary>
        ERROR_EVT_QUERY_RESULT_STALE = 0x3AA3,
        /// <summary>[15012] Query result is currently at an invalid position.</summary>
        ERROR_EVT_QUERY_RESULT_INVALID_POSITION = 0x3AA4,
        /// <summary>[15013] Registered MSXML doesn't support validation.</summary>
        ERROR_EVT_NON_VALIDATING_MSXML = 0x3AA5,
        /// <summary>[15014] An expression can only be followed by a change of scope operation if it itself evaluates to a node set and is not already part of some other change of scope operation.</summary>
        ERROR_EVT_FILTER_ALREADYSCOPED = 0x3AA6,
        /// <summary>[15015] Can't perform a step operation from a term that does not represent an element set.</summary>
        ERROR_EVT_FILTER_NOTELTSET = 0x3AA7,
        /// <summary>[15016] Left hand side arguments to binary operators must be either attributes, nodes or variables and right hand side arguments must be constants.</summary>
        ERROR_EVT_FILTER_INVARG = 0x3AA8,
        /// <summary>[15017] A step operation must involve either a node test or, in the case of a predicate, an algebraic expression against which to test each node in the node set identified by the preceding node set can be evaluated.</summary>
        ERROR_EVT_FILTER_INVTEST = 0x3AA9,
        /// <summary>[15018] This data type is currently unsupported.</summary>
        ERROR_EVT_FILTER_INVTYPE = 0x3AAA,
        /// <summary>[15019] A syntax error occurred at position %1!d!.</summary>
        ERROR_EVT_FILTER_PARSEERR = 0x3AAB,
        /// <summary>[15020] This operator is unsupported by this implementation of the filter.</summary>
        ERROR_EVT_FILTER_UNSUPPORTEDOP = 0x3AAC,
        /// <summary>[15021] The token encountered was unexpected.</summary>
        ERROR_EVT_FILTER_UNEXPECTEDTOKEN = 0x3AAD,
        /// <summary>[15022] The requested operation cannot be performed over an enabled direct channel. The channel must first be disabled before performing the requested operation.</summary>
        ERROR_EVT_INVALID_OPERATION_OVER_ENABLED_DIRECT_CHANNEL = 0x3AAE,
        /// <summary>[15023] Channel property %1!s! contains invalid value. The value has invalid type, is outside of valid range, can't be updated or is not supported by this type of channel.</summary>
        ERROR_EVT_INVALID_CHANNEL_PROPERTY_VALUE = 0x3AAF,
        /// <summary>[15024] Publisher property %1!s! contains invalid value. The value has invalid type, is outside of valid range, can't be updated or is not supported by this type of publisher.</summary>
        ERROR_EVT_INVALID_PUBLISHER_PROPERTY_VALUE = 0x3AB0,
        /// <summary>[15025] The channel fails to activate.</summary>
        ERROR_EVT_CHANNEL_CANNOT_ACTIVATE = 0x3AB1,
        /// <summary>[15026] The xpath expression exceeded supported complexity. Please simplify it or split it into two or more simple expressions.</summary>
        ERROR_EVT_FILTER_TOO_COMPLEX = 0x3AB2,
        /// <summary>[15027] the message resource is present but the message is not found in the string/message table.</summary>
        ERROR_EVT_MESSAGE_NOT_FOUND = 0x3AB3,
        /// <summary>[15028] The message id for the desired message could not be found.</summary>
        ERROR_EVT_MESSAGE_ID_NOT_FOUND = 0x3AB4,
        /// <summary>[15029] The substitution string for insert index (%1) could not be found.</summary>
        ERROR_EVT_UNRESOLVED_VALUE_INSERT = 0x3AB5,
        /// <summary>[15030] The description string for parameter reference (%1) could not be found.</summary>
        ERROR_EVT_UNRESOLVED_PARAMETER_INSERT = 0x3AB6,
        /// <summary>[15031] The maximum number of replacements has been reached.</summary>
        ERROR_EVT_MAX_INSERTS_REACHED = 0x3AB7,
        /// <summary>[15032] The event definition could not be found for event id (%1).</summary>
        ERROR_EVT_EVENT_DEFINITION_NOT_FOUND = 0x3AB8,
        /// <summary>[15033] The locale specific resource for the desired message is not present.</summary>
        ERROR_EVT_MESSAGE_LOCALE_NOT_FOUND = 0x3AB9,
        /// <summary>[15034] The resource is too old to be compatible.</summary>
        ERROR_EVT_VERSION_TOO_OLD = 0x3ABA,
        /// <summary>[15035] The resource is too new to be compatible.</summary>
        ERROR_EVT_VERSION_TOO_NEW = 0x3ABB,
        /// <summary>[15036] The channel at index %1!d! of the query can't be opened.</summary>
        ERROR_EVT_CANNOT_OPEN_CHANNEL_OF_QUERY = 0x3ABC,
        /// <summary>[15037] The publisher has been disabled and its resource is not available. This usually occurs when the publisher is in the process of being uninstalled or upgraded.</summary>
        ERROR_EVT_PUBLISHER_DISABLED = 0x3ABD,
        /// <summary>[15038] Attempted to create a numeric type that is outside of its valid range.</summary>
        ERROR_EVT_FILTER_OUT_OF_RANGE = 0x3ABE,
        /// <summary>[15080] The subscription fails to activate.</summary>
        ERROR_EC_SUBSCRIPTION_CANNOT_ACTIVATE = 0x3AE8,
        /// <summary>[15081] The log of the subscription is in disabled state, and cannot be used to forward events to. The log must first be enabled before the subscription can be activated.</summary>
        ERROR_EC_LOG_DISABLED = 0x3AE9,
        /// <summary>[15082] When forwarding events from local machine to itself, the query of the subscription can't contain target log of the subscription.</summary>
        ERROR_EC_CIRCULAR_FORWARDING = 0x3AEA,
        /// <summary>[15083] The credential store that is used to save credentials is full.</summary>
        ERROR_EC_CREDSTORE_FULL = 0x3AEB,
        /// <summary>[15084] The credential used by this subscription can't be found in credential store.</summary>
        ERROR_EC_CRED_NOT_FOUND = 0x3AEC,
        /// <summary>[15085] No active channel is found for the query.</summary>
        ERROR_EC_NO_ACTIVE_CHANNEL = 0x3AED,
        /// <summary>[15100] The resource loader failed to find MUI file.</summary>
        ERROR_MUI_FILE_NOT_FOUND = 0x3AFC,
        /// <summary>[15101] The resource loader failed to load MUI file because the file fail to pass validation.</summary>
        ERROR_MUI_INVALID_FILE = 0x3AFD,
        /// <summary>[15102] The RC Manifest is corrupted with garbage data or unsupported version or missing required item.</summary>
        ERROR_MUI_INVALID_RC_CONFIG = 0x3AFE,
        /// <summary>[15103] The RC Manifest has invalid culture name.</summary>
        ERROR_MUI_INVALID_LOCALE_NAME = 0x3AFF,
        /// <summary>[15104] The RC Manifest has invalid ultimatefallback name.</summary>
        ERROR_MUI_INVALID_ULTIMATEFALLBACK_NAME = 0x3B00,
        /// <summary>[15105] The resource loader cache doesn't have loaded MUI entry.</summary>
        ERROR_MUI_FILE_NOT_LOADED = 0x3B01,
        /// <summary>[15106] User stopped resource enumeration.</summary>
        ERROR_RESOURCE_ENUM_USER_STOP = 0x3B02,
        /// <summary>[15107] UI language installation failed.</summary>
        ERROR_MUI_INTLSETTINGS_UILANG_NOT_INSTALLED = 0x3B03,
        /// <summary>[15108] Locale installation failed.</summary>
        ERROR_MUI_INTLSETTINGS_INVALID_LOCALE_NAME = 0x3B04,
        /// <summary>[15110] A resource does not have default or neutral value.</summary>
        ERROR_MRM_RUNTIME_NO_DEFAULT_OR_NEUTRAL_RESOURCE = 0x3B06,
        /// <summary>[15111] Invalid PRI config file.</summary>
        ERROR_MRM_INVALID_PRICONFIG = 0x3B07,
        /// <summary>[15112] Invalid file type.</summary>
        ERROR_MRM_INVALID_FILE_TYPE = 0x3B08,
        /// <summary>[15113] Unknown qualifier.</summary>
        ERROR_MRM_UNKNOWN_QUALIFIER = 0x3B09,
        /// <summary>[15114] Invalid qualifier value.</summary>
        ERROR_MRM_INVALID_QUALIFIER_VALUE = 0x3B0A,
        /// <summary>[15115] No Candidate found.</summary>
        ERROR_MRM_NO_CANDIDATE = 0x3B0B,
        /// <summary>[15116] The ResourceMap or NamedResource has an item that does not have default or neutral resource..</summary>
        ERROR_MRM_NO_MATCH_OR_DEFAULT_CANDIDATE = 0x3B0C,
        /// <summary>[15117] Invalid ResourceCandidate type.</summary>
        ERROR_MRM_RESOURCE_TYPE_MISMATCH = 0x3B0D,
        /// <summary>[15118] Duplicate Resource Map.</summary>
        ERROR_MRM_DUPLICATE_MAP_NAME = 0x3B0E,
        /// <summary>[15119] Duplicate Entry.</summary>
        ERROR_MRM_DUPLICATE_ENTRY = 0x3B0F,
        /// <summary>[15120] Invalid Resource Identifier.</summary>
        ERROR_MRM_INVALID_RESOURCE_IDENTIFIER = 0x3B10,
        /// <summary>[15121] Filepath too long.</summary>
        ERROR_MRM_FILEPATH_TOO_LONG = 0x3B11,
        /// <summary>[15122] Unsupported directory type.</summary>
        ERROR_MRM_UNSUPPORTED_DIRECTORY_TYPE = 0x3B12,
        /// <summary>[15126] Invalid PRI File.</summary>
        ERROR_MRM_INVALID_PRI_FILE = 0x3B16,
        /// <summary>[15127] NamedResource Not Found.</summary>
        ERROR_MRM_NAMED_RESOURCE_NOT_FOUND = 0x3B17,
        /// <summary>[15135] ResourceMap Not Found.</summary>
        ERROR_MRM_MAP_NOT_FOUND = 0x3B1F,
        /// <summary>[15136] Unsupported MRT profile type.</summary>
        ERROR_MRM_UNSUPPORTED_PROFILE_TYPE = 0x3B20,
        /// <summary>[15137] Invalid qualifier operator.</summary>
        ERROR_MRM_INVALID_QUALIFIER_OPERATOR = 0x3B21,
        /// <summary>[15138] Unable to determine qualifier value or qualifier value has not been set.</summary>
        ERROR_MRM_INDETERMINATE_QUALIFIER_VALUE = 0x3B22,
        /// <summary>[15139] Automerge is enabled in the PRI file.</summary>
        ERROR_MRM_AUTOMERGE_ENABLED = 0x3B23,
        /// <summary>[15140] Too many resources defined for package.</summary>
        ERROR_MRM_TOO_MANY_RESOURCES = 0x3B24,
        /// <summary>[15200] The monitor returned a DDC/CI capabilities string that did not comply with the ACCESS.bus 3.0, DDC/CI 1.1 or MCCS 2 Revision 1 specification.</summary>
        ERROR_MCA_INVALID_CAPABILITIES_STRING = 0x3B60,
        /// <summary>[15201] The monitor's VCP Version (0xDF) VCP code returned an invalid version value.</summary>
        ERROR_MCA_INVALID_VCP_VERSION = 0x3B61,
        /// <summary>[15202] The monitor does not comply with the MCCS specification it claims to support.</summary>
        ERROR_MCA_MONITOR_VIOLATES_MCCS_SPECIFICATION = 0x3B62,
        /// <summary>[15203] The MCCS version in a monitor's mccs_ver capability does not match the MCCS version the monitor reports when the VCP Version (0xDF) VCP code is used.</summary>
        ERROR_MCA_MCCS_VERSION_MISMATCH = 0x3B63,
        /// <summary>[15204] The Monitor Configuration API only works with monitors that support the MCCS 1.0 specification, MCCS 2.0 specification or the MCCS 2.0 Revision 1 specification.</summary>
        ERROR_MCA_UNSUPPORTED_MCCS_VERSION = 0x3B64,
        /// <summary>[15205] An internal Monitor Configuration API error occurred.</summary>
        ERROR_MCA_INTERNAL_ERROR = 0x3B65,
        /// <summary>[15206] The monitor returned an invalid monitor technology type. CRT, Plasma and LCD (TFT) are examples of monitor technology types. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.</summary>
        ERROR_MCA_INVALID_TECHNOLOGY_TYPE_RETURNED = 0x3B66,
        /// <summary>[15207] The caller of SetMonitorColorTemperature specified a color temperature that the current monitor did not support. This error implies that the monitor violated the MCCS 2.0 or MCCS 2.0 Revision 1 specification.</summary>
        ERROR_MCA_UNSUPPORTED_COLOR_TEMPERATURE = 0x3B67,
        /// <summary>[15250] The requested system device cannot be identified due to multiple indistinguishable devices potentially matching the identification criteria.</summary>
        ERROR_AMBIGUOUS_SYSTEM_DEVICE = 0x3B92,
        /// <summary>[15299] The requested system device cannot be found.</summary>
        ERROR_SYSTEM_DEVICE_NOT_FOUND = 0x3BC3,
        /// <summary>[15300] Hash generation for the specified hash version and hash type is not enabled on the server.</summary>
        ERROR_HASH_NOT_SUPPORTED = 0x3BC4,
        /// <summary>[15301] The hash requested from the server is not available or no longer valid.</summary>
        ERROR_HASH_NOT_PRESENT = 0x3BC5,
        /// <summary>[15321] The secondary interrupt controller instance that manages the specified interrupt is not registered.</summary>
        ERROR_SECONDARY_IC_PROVIDER_NOT_REGISTERED = 0x3BD9,
        /// <summary>[15322] The information supplied by the GPIO client driver is invalid.</summary>
        ERROR_GPIO_CLIENT_INFORMATION_INVALID = 0x3BDA,
        /// <summary>[15323] The version specified by the GPIO client driver is not supported.</summary>
        ERROR_GPIO_VERSION_NOT_SUPPORTED = 0x3BDB,
        /// <summary>[15324] The registration packet supplied by the GPIO client driver is not valid.</summary>
        ERROR_GPIO_INVALID_REGISTRATION_PACKET = 0x3BDC,
        /// <summary>[15325] The requested operation is not supported for the specified handle.</summary>
        ERROR_GPIO_OPERATION_DENIED = 0x3BDD,
        /// <summary>[15326] The requested connect mode conflicts with an existing mode on one or more of the specified pins.</summary>
        ERROR_GPIO_INCOMPATIBLE_CONNECT_MODE = 0x3BDE,
        /// <summary>[15327] The interrupt requested to be unmasked is not masked.</summary>
        ERROR_GPIO_INTERRUPT_ALREADY_UNMASKED = 0x3BDF,
        /// <summary>[15400] The requested run level switch cannot be completed successfully.</summary>
        ERROR_CANNOT_SWITCH_RUNLEVEL = 0x3C28,
        /// <summary>[15401] The service has an invalid run level setting. The run level for a service must not be higher than the run level of its dependent services.</summary>
        ERROR_INVALID_RUNLEVEL_SETTING = 0x3C29,
        /// <summary>[15402] The requested run level switch cannot be completed successfully since one or more services will not stop or restart within the specified timeout.</summary>
        ERROR_RUNLEVEL_SWITCH_TIMEOUT = 0x3C2A,
        /// <summary>[15403] A run level switch agent did not respond within the specified timeout.</summary>
        ERROR_RUNLEVEL_SWITCH_AGENT_TIMEOUT = 0x3C2B,
        /// <summary>[15404] A run level switch is currently in progress.</summary>
        ERROR_RUNLEVEL_SWITCH_IN_PROGRESS = 0x3C2C,
        /// <summary>[15405] One or more services failed to start during the service startup phase of a run level switch.</summary>
        ERROR_SERVICES_FAILED_AUTOSTART = 0x3C2D,
        /// <summary>[15501] The task stop request cannot be completed immediately since task needs more time to shutdown.</summary>
        ERROR_COM_TASK_STOP_PENDING = 0x3C8D,
        /// <summary>[15600] Package could not be opened.</summary>
        ERROR_INSTALL_OPEN_PACKAGE_FAILED = 0x3CF0,
        /// <summary>[15601] Package was not found.</summary>
        ERROR_INSTALL_PACKAGE_NOT_FOUND = 0x3CF1,
        /// <summary>[15602] Package data is invalid.</summary>
        ERROR_INSTALL_INVALID_PACKAGE = 0x3CF2,
        /// <summary>[15603] Package failed updates, dependency or conflict validation.</summary>
        ERROR_INSTALL_RESOLVE_DEPENDENCY_FAILED = 0x3CF3,
        /// <summary>[15604] There is not enough disk space on your computer. Please free up some space and try again.</summary>
        ERROR_INSTALL_OUT_OF_DISK_SPACE = 0x3CF4,
        /// <summary>[15605] There was a problem downloading your product.</summary>
        ERROR_INSTALL_NETWORK_FAILURE = 0x3CF5,
        /// <summary>[15606] Package could not be registered.</summary>
        ERROR_INSTALL_REGISTRATION_FAILURE = 0x3CF6,
        /// <summary>[15607] Package could not be unregistered.</summary>
        ERROR_INSTALL_DEREGISTRATION_FAILURE = 0x3CF7,
        /// <summary>[15608] User cancelled the install request.</summary>
        ERROR_INSTALL_CANCEL = 0x3CF8,
        /// <summary>[15609] Install failed. Please contact your software vendor.</summary>
        ERROR_INSTALL_FAILED = 0x3CF9,
        /// <summary>[15610] Removal failed. Please contact your software vendor.</summary>
        ERROR_REMOVE_FAILED = 0x3CFA,
        /// <summary>[15611] The provided package is already installed, and reinstallation of the package was blocked. Check the AppXDeployment-Server event log for details.</summary>
        ERROR_PACKAGE_ALREADY_EXISTS = 0x3CFB,
        /// <summary>[15612] The application cannot be started. Try reinstalling the application to fix the problem.</summary>
        ERROR_NEEDS_REMEDIATION = 0x3CFC,
        /// <summary>[15613] A Prerequisite for an install could not be satisfied.</summary>
        ERROR_INSTALL_PREREQUISITE_FAILED = 0x3CFD,
        /// <summary>[15614] The package repository is corrupted.</summary>
        ERROR_PACKAGE_REPOSITORY_CORRUPTED = 0x3CFE,
        /// <summary>[15615] To install this application you need either a Windows developer license or a sideloading-enabled system.</summary>
        ERROR_INSTALL_POLICY_FAILURE = 0x3CFF,
        /// <summary>[15616] The application cannot be started because it is currently updating.</summary>
        ERROR_PACKAGE_UPDATING = 0x3D00,
        /// <summary>[15617] The package deployment operation is blocked by policy. Please contact your system administrator.</summary>
        ERROR_DEPLOYMENT_BLOCKED_BY_POLICY = 0x3D01,
        /// <summary>[15618] The package could not be installed because resources it modifies are currently in use.</summary>
        ERROR_PACKAGES_IN_USE = 0x3D02,
        /// <summary>[15619] The package could not be recovered because necessary data for recovery have been corrupted.</summary>
        ERROR_RECOVERY_FILE_CORRUPT = 0x3D03,
        /// <summary>[15620] The signature is invalid. To register in developer mode, AppxSignature.p7x and AppxBlockMap.xml must be valid or should not be present.</summary>
        ERROR_INVALID_STAGED_SIGNATURE = 0x3D04,
        /// <summary>[15621] An error occurred while deleting the package's previously existing application data.</summary>
        ERROR_DELETING_EXISTING_APPLICATIONDATA_STORE_FAILED = 0x3D05,
        /// <summary>[15622] The package could not be installed because a higher version of this package is already installed.</summary>
        ERROR_INSTALL_PACKAGE_DOWNGRADE = 0x3D06,
        /// <summary>[15623] An error in a system binary was detected. Try refreshing the PC to fix the problem.</summary>
        ERROR_SYSTEM_NEEDS_REMEDIATION = 0x3D07,
        /// <summary>[15624] A corrupted CLR NGEN binary was detected on the system.</summary>
        ERROR_APPX_INTEGRITY_FAILURE_CLR_NGEN = 0x3D08,
        /// <summary>[15625] The operation could not be resumed because necessary data for recovery have been corrupted.</summary>
        ERROR_RESILIENCY_FILE_CORRUPT = 0x3D09,
        /// <summary>[15626] The package could not be installed because the Windows Firewall service is not running. Enable the Windows Firewall service and try again.</summary>
        ERROR_INSTALL_FIREWALL_SERVICE_NOT_RUNNING = 0x3D0A,
        /// <summary>[15700] The process has no package identity.</summary>
        APPMODEL_ERROR_NO_PACKAGE = 0x3D54,
        /// <summary>[15701] The package runtime information is corrupted.</summary>
        APPMODEL_ERROR_PACKAGE_RUNTIME_CORRUPT = 0x3D55,
        /// <summary>[15702] The package identity is corrupted.</summary>
        APPMODEL_ERROR_PACKAGE_IDENTITY_CORRUPT = 0x3D56,
        /// <summary>[15703] The process has no application identity.</summary>
        APPMODEL_ERROR_NO_APPLICATION = 0x3D57,
        /// <summary>[15800] Loading the state store failed.</summary>
        ERROR_STATE_LOAD_STORE_FAILED = 0x3DB8,
        /// <summary>[15801] Retrieving the state version for the application failed.</summary>
        ERROR_STATE_GET_VERSION_FAILED = 0x3DB9,
        /// <summary>[15802] Setting the state version for the application failed.</summary>
        ERROR_STATE_SET_VERSION_FAILED = 0x3DBA,
        /// <summary>[15803] Resetting the structured state of the application failed.</summary>
        ERROR_STATE_STRUCTURED_RESET_FAILED = 0x3DBB,
        /// <summary>[15804] State Manager failed to open the container.</summary>
        ERROR_STATE_OPEN_CONTAINER_FAILED = 0x3DBC,
        /// <summary>[15805] State Manager failed to create the container.</summary>
        ERROR_STATE_CREATE_CONTAINER_FAILED = 0x3DBD,
        /// <summary>[15806] State Manager failed to delete the container.</summary>
        ERROR_STATE_DELETE_CONTAINER_FAILED = 0x3DBE,
        /// <summary>[15807] State Manager failed to read the setting.</summary>
        ERROR_STATE_READ_SETTING_FAILED = 0x3DBF,
        /// <summary>[15808] State Manager failed to write the setting.</summary>
        ERROR_STATE_WRITE_SETTING_FAILED = 0x3DC0,
        /// <summary>[15809] State Manager failed to delete the setting.</summary>
        ERROR_STATE_DELETE_SETTING_FAILED = 0x3DC1,
        /// <summary>[15810] State Manager failed to query the setting.</summary>
        ERROR_STATE_QUERY_SETTING_FAILED = 0x3DC2,
        /// <summary>[15811] State Manager failed to read the composite setting.</summary>
        ERROR_STATE_READ_COMPOSITE_SETTING_FAILED = 0x3DC3,
        /// <summary>[15812] State Manager failed to write the composite setting.</summary>
        ERROR_STATE_WRITE_COMPOSITE_SETTING_FAILED = 0x3DC4,
        /// <summary>[15813] State Manager failed to enumerate the containers.</summary>
        ERROR_STATE_ENUMERATE_CONTAINER_FAILED = 0x3DC5,
        /// <summary>[15814] State Manager failed to enumerate the settings.</summary>
        ERROR_STATE_ENUMERATE_SETTINGS_FAILED = 0x3DC6,
        /// <summary>[15815] The size of the state manager composite setting value has exceeded the limit.</summary>
        ERROR_STATE_COMPOSITE_SETTING_VALUE_SIZE_LIMIT_EXCEEDED = 0x3DC7,
        /// <summary>[15816] The size of the state manager setting value has exceeded the limit.</summary>
        ERROR_STATE_SETTING_VALUE_SIZE_LIMIT_EXCEEDED = 0x3DC8,
        /// <summary>[15817] The length of the state manager setting name has exceeded the limit.</summary>
        ERROR_STATE_SETTING_NAME_SIZE_LIMIT_EXCEEDED = 0x3DC9,
        /// <summary>[15818] The length of the state manager container name has exceeded the limit.</summary>
        ERROR_STATE_CONTAINER_NAME_SIZE_LIMIT_EXCEEDED = 0x3DCA,
        /// <summary>[15841] This API cannot be used in the context of the caller's application type.</summary>
        ERROR_API_UNAVAILABLE = 0x3DE1,
    }
}
