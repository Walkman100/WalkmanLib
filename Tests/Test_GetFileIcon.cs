using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace Tests {
    public static class Tests_GetFileIcon {
        public static bool Test_GetFileIcon1(string rootTestFolder) {
            Icon ico = WalkmanLib.GetFileIcon(Environment.GetEnvironmentVariable("WinDir"));

            using (var savedImage = new DisposableFile(Path.Combine(rootTestFolder, "getFileIcon1.png"), false, false)) {
                ico.ToBitmap().Save(savedImage);
                return GeneralFunctions.TestBoolean("GetFileIcon1", File.Exists(savedImage), true);
            }
        }

        public static bool Test_GetFileIcon2(string rootTestFolder) {
            Icon ico = WalkmanLib.GetFileIcon(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "explorer.exe"));

            using (var savedImage = new DisposableFile(Path.Combine(rootTestFolder, "getFileIcon2.png"), false, false)) {
                ico.ToBitmap().Save(savedImage);

                return GeneralFunctions.TestBoolean("GetFileIcon2", File.Exists(savedImage), true);
            }
        }

        public static bool Test_GetFileIcon3() {
            Exception ex = new NoException();
            try {
                Icon ico = WalkmanLib.GetFileIcon("nonexistantfile.txt");
            } catch (Exception ex2) {
                ex = ex2;
            }

            return GeneralFunctions.TestType("GetFileIcon3", ex.GetType(), typeof(FileNotFoundException));
        }

        public static bool Test_GetFileIcon4() {
            Exception ex = new NoException();
            try {
                Icon ico = WalkmanLib.GetFileIcon("nonexistantfile.txt", false);
            } catch (Exception ex2) {
                ex = ex2;
            }

            return GeneralFunctions.TestType("GetFileIcon4", ex.GetType(), typeof(NoException));
        }

        private static bool ImageIsEqual(Bitmap bmp1, Bitmap bmp2) {
            if (bmp1.PixelFormat != bmp2.PixelFormat)
                return false;
            if (!bmp1.RawFormat.Equals(bmp2.RawFormat))
                return false;
            if (bmp1.Size != bmp2.Size)
                return false;

            for (int x = 0; x < bmp1.Width; x++) {
                for (int y = 0; y < bmp1.Height; y++) {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y)) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool Test_GetFileIcon5() {
            Icon ico1 = WalkmanLib.GetFileIcon(".txt", false);
            Icon ico2 = WalkmanLib.GetFileIcon(".txt", false);

            return GeneralFunctions.TestBoolean("GetFileIcon5", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), true);
        }

        public static bool Test_GetFileIcon6() {
            Icon ico1 = WalkmanLib.GetFileIcon(".txt", false, linkOverlay: false);
            Icon ico2 = WalkmanLib.GetFileIcon(".txt", false, linkOverlay: true);

            return GeneralFunctions.TestBoolean("GetFileIcon6", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), false);
        }

        public static bool Test_GetFileIcon7() {
            string filePath = Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "hh.exe");
            Icon ico1 = WalkmanLib.GetFileIcon(filePath);
            Icon ico2 = WalkmanLib.ExtractIconByIndex(filePath, 0, 16);

            return GeneralFunctions.TestBoolean("GetFileIcon7", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), true);
        }

        public static bool Test_GetFileIcon8() {
            string filePath = Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "hh.exe");
            Icon ico1 = WalkmanLib.GetFileIcon(filePath, smallIcon: false);
            Icon ico2 = WalkmanLib.ExtractIconByIndex(filePath, 0, 32);

            return GeneralFunctions.TestBoolean("GetFileIcon8", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), true);
        }

        private static string Sha1Image(Image img) {
            using (var ms = new MemoryStream()) {
                // ImageFormat.MemoryBmp has no encoder, but it's still a BMP...
                img.Save(ms, img.RawFormat.Equals(ImageFormat.MemoryBmp) ? ImageFormat.Bmp : img.RawFormat);

                ms.Position = 0;
                byte[] hash = new SHA1Managed().ComputeHash(ms);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        public static bool Test_GetFileIcon9() {
            // the icon for %WinDir%\hh.exe hasn't changed since XP, should be pretty safe
            using (Icon ico = WalkmanLib.GetFileIcon(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "hh.exe")))
            using (Bitmap bmp = ico.ToBitmap()) {
                string hash = Sha1Image(bmp);

                return GeneralFunctions.TestString("GetFileIcon9", hash, "f805d57e484d7f00298fe32b072b8c57928b3edb");
            }
        }
    }
}
