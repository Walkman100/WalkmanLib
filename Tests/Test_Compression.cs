using System;
using System.IO;

namespace Tests {
    static class Tests_Compression {
        public static bool Test_Compression1(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression1.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.SetCompression(testFile, false);

                if (File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression1", "File is compressed", "File is not compressed");
                }

                if (!WalkmanLib.SetCompression(testFile, true)) {
                    return GeneralFunctions.TestString("Compression1", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression1", (int)File.GetAttributes(testFile), (int)FileAttributes.Compressed);
            }
        }

        public static bool Test_Compression2(string rootTestFolder) {
            using (var testFolder = new DisposableDirectory(Path.Combine(rootTestFolder, "compression2"))) {
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal);
                WalkmanLib.SetCompression(testFolder, false);

                if (File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression2", "Folder is compressed", "Folder is not compressed");
                }

                if (!WalkmanLib.SetCompression(testFolder, true)) {
                    return GeneralFunctions.TestString("Compression2", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression2", (int)File.GetAttributes(testFolder), (int)(FileAttributes.Compressed | FileAttributes.Directory));
            }
        }

        public static bool Test_Compression3(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression3.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.SetCompression(testFile, true);

                if (!File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression3", "File is not compressed", "File is compressed");
                }

                if (!WalkmanLib.SetCompression(testFile, false)) {
                    return GeneralFunctions.TestString("Compression3", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression3", (int)File.GetAttributes(testFile), (int)FileAttributes.Normal);
            }
        }

        public static bool Test_Compression4(string rootTestFolder) {
            using (var testFolder = new DisposableDirectory(Path.Combine(rootTestFolder, "compression4"))) {
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal);
                WalkmanLib.SetCompression(testFolder, true);
                
                if (!File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression4", "Folder is not compressed", "Folder is compressed");
                }

                if (!WalkmanLib.SetCompression(testFolder, false)) {
                    return GeneralFunctions.TestString("Compression4", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression4", (int)File.GetAttributes(testFolder), (int)FileAttributes.Directory);
            }
        }

        public static bool Test_Compression5(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression5.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.UncompressFile(testFile);

                if (File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression5", "File is compressed", "File is not compressed");
                }

                if (!WalkmanLib.CompressFile(testFile)) {
                    return GeneralFunctions.TestString("Compression5", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression5", (int)File.GetAttributes(testFile), (int)FileAttributes.Compressed);
            }
        }

        public static bool Test_Compression6(string rootTestFolder) {
            using (var testFolder = new DisposableDirectory(Path.Combine(rootTestFolder, "compression6"))) {
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal);
                WalkmanLib.UncompressFile(testFolder);

                if (File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression6", "Folder is compressed", "Folder is not compressed");
                }

                if (!WalkmanLib.CompressFile(testFolder)) {
                    return GeneralFunctions.TestString("Compression6", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression6", (int)File.GetAttributes(testFolder), (int)(FileAttributes.Compressed | FileAttributes.Directory));
            }
        }

        public static bool Test_Compression7(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression7.txt"))) {
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.CompressFile(testFile);

                if (!File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression7", "File is not compressed", "File is compressed");
                }

                if (!WalkmanLib.UncompressFile(testFile)) {
                    return GeneralFunctions.TestString("Compression7", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression7", (int)File.GetAttributes(testFile), (int)FileAttributes.Normal);
            }
        }

        public static bool Test_Compression8(string rootTestFolder) {
            using (var testFolder = new DisposableDirectory(Path.Combine(rootTestFolder, "compression8"))) {
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal);
                WalkmanLib.CompressFile(testFolder);

                if (!File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression8", "Folder is not compressed", "Folder is compressed");
                }

                if (!WalkmanLib.UncompressFile(testFolder, false)) {
                    return GeneralFunctions.TestString("Compression8", "Return value: False", "Return value: True");
                }

                return GeneralFunctions.TestNumber("Compression8", (int)File.GetAttributes(testFolder), (int)FileAttributes.Directory);
            }
        }

        public static bool Test_Compression9(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression9.txt"))) {
                long fileLength = new FileInfo(testFile).Length;
                if (fileLength != 0) {
                    return GeneralFunctions.TestString("Compression9", "FileSize:" + fileLength, "FileSize:0");
                }

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.CompressFile(testFile);
                if (!File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression9", "File is not compressed", "File is compressed");
                }

                return GeneralFunctions.TestNumber("Compression9", WalkmanLib.GetCompressedSize(testFile), 0d);
            }
        }

        public static bool Test_Compression10(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression10.txt"))) {
                long fileLength = new FileInfo(testFile).Length;
                if (fileLength != 0) {
                    return GeneralFunctions.TestString("Compression10", "FileSize:" + fileLength, "FileSize:0");
                }

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.CompressFile(testFile);
                if (!File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression10", "File is not compressed", "File is compressed");
                }

                File.WriteAllText(testFile, string.Empty, System.Text.Encoding.UTF8);
                fileLength = new FileInfo(testFile).Length;
                if (fileLength != 3) {
                    return GeneralFunctions.TestString("Compression10", "FileSize:" + fileLength, "FileSize:3");
                }

                // currently GetCompressedSize returns the filesize for 0 compressed size...
                return GeneralFunctions.TestNumber("Compression10", WalkmanLib.GetCompressedSize(testFile), 3);
            }
        }

        public static bool Test_Compression11(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression11.txt"))) {
                long fileLength = new FileInfo(testFile).Length;
                if (fileLength != 0) {
                    return GeneralFunctions.TestString("Compression11", "FileSize:" + fileLength, "FileSize:0");
                }

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.UncompressFile(testFile);
                if (File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression11", "File is compressed", "File is not compressed");
                }

                File.WriteAllText(testFile, string.Empty, System.Text.Encoding.UTF8);
                fileLength = new FileInfo(testFile).Length;
                if (fileLength != 3) {
                    return GeneralFunctions.TestString("Compression11", "FileSize:" + fileLength, "FileSize:3");
                }

                return GeneralFunctions.TestNumber("Compression11", WalkmanLib.GetCompressedSize(testFile), 3);
            }
        }

        public static bool Test_Compression12(string rootTestFolder) {
            using (var testFile = new DisposableFile(Path.Combine(rootTestFolder, "compression12.txt"))) {
                long fileLength = new FileInfo(testFile).Length;
                if (fileLength != 0) {
                    return GeneralFunctions.TestString("Compression12", "FileSize:" + fileLength, "FileSize:0");
                }

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal);
                WalkmanLib.UncompressFile(testFile);
                if (File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression12", "File is compressed", "File is not compressed");
                }

                int newFileSize = 1024 * 64 + 1;
                byte[] randBytes = new byte[newFileSize];
                var rand = new Random();
                rand.NextBytes(randBytes);

                char[] randChars = new char[newFileSize];
                randBytes.CopyTo(randChars, 0);
                File.WriteAllText(testFile, new string(randChars), System.Text.Encoding.ASCII);

                fileLength = new FileInfo(testFile).Length;
                if (fileLength != newFileSize) {
                    return GeneralFunctions.TestString("Compression12", "FileSize:" + fileLength, "FileSize:" + newFileSize);
                }

                WalkmanLib.CompressFile(testFile);
                if (!File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed)) {
                    return GeneralFunctions.TestString("Compression12", "File is not compressed", "File is compressed");
                }

                return GeneralFunctions.TestNumber("Compression12", WalkmanLib.GetCompressedSize(testFile), 1024 * 60);
            }
        }

        public static bool Test_Compression13(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                WalkmanLib.SetCompression(Path.Combine(rootTestFolder, "compression13.txt"), true);
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("Compression13", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_Compression14() {
            Exception ex = new NoException();
            try {
                WalkmanLib.SetCompression(Path.Combine(Environment.SystemDirectory, "shell32.dll"), true);
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("Compression14", ex.GetType(), typeof(UnauthorizedAccessException));
        }
    }
}
