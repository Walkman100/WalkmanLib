using System;
using System.IO;

namespace Tests {
    static class Tests_StreamCopy {
#if NoOokii

        public static bool Test_StreamCopy1(string rootTestFolder) {
            GeneralFunctions.WriteTestSkipped(new System.Collections.Generic.List<string>(new[] { 
                "StreamCopy1", 
                "StreamCopy2", 
                "StreamCopy3", 
                "StreamCopyThrows1", 
                "StreamCopyThrows2" 
            }), "No Ookii.Dialogs available");
            return true;
        }
        public static bool Test_StreamCopy2(string rootTestFolder) => true;
        public static bool Test_StreamCopy3(string rootTestFolder) => true;
        public static bool Test_StreamCopyThrows1() => true;
        public static bool Test_StreamCopyThrows2(string rootTestFolder) => true;

#else     // disable if NoOokii is defined (default for tests project)

        public static bool Test_StreamCopy1(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "streamCopy1Source"))) {
                int randByteAmount = 1024;
                byte[] randBytes = new byte[randByteAmount];
                //byte[] randBytes = new byte[checked(randByteAmount + 1)];
                new Random().NextBytes(randBytes);

                string fileText = Convert.ToBase64String(randBytes);
                File.WriteAllText(testFileSource, fileText, System.Text.Encoding.ASCII);

                using (var testFileTarget = new DisposableFile(Path.Combine(rootTestFolder, "streamCopy1Target"))) {
                    var source = new FileStream(testFileSource, FileMode.Open);
                    var target = new FileStream(testFileTarget, FileMode.Truncate);

                    System.Threading.Tasks.Task.Run(() => {
                        try {
                            WalkmanLib.StreamCopy(source, target);
                        } catch (Exception) { // StreamCopy Disposes the streams if copy started successfully, if it didn't then we manually close them
                            source.Dispose();
                            target.Dispose();
                            throw;
                        }
                    });
                    System.Threading.Thread.Sleep(200);

                    return GeneralFunctions.TestString("StreamCopy1", File.ReadAllText(testFileTarget), fileText);
                }
            }
        }

        public static bool Test_StreamCopy2(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "streamCopy2Source")))
            using (var testFileTarget = new DisposableFile(Path.Combine(rootTestFolder, "streamCopy2Target"))) {
                byte[] randBytes = new byte[8];
                new Random().NextBytes(randBytes);
                File.WriteAllText(testFileSource, Convert.ToBase64String(randBytes), System.Text.Encoding.ASCII);

                bool returned = false;

                FileStream source = File.OpenRead(testFileSource);
                FileStream target = File.OpenWrite(testFileTarget);

                System.Threading.Tasks.Task.Run(() => {
                    WalkmanLib.StreamCopy(source, target, onComplete: (_, _) => returned = true);
                });
                System.Threading.Thread.Sleep(100);

                return GeneralFunctions.TestBoolean("StreamCopy2", returned, true);
            }
        }

        public static bool Test_StreamCopy3(string rootTestFolder) {
            using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "streamCopy3Source")))
            using (var testFileTarget = new DisposableFile(Path.Combine(rootTestFolder, "streamCopy3Target"))) {
                byte[] randBytes = new byte[8];
                new Random().NextBytes(randBytes);
                File.WriteAllText(testFileTarget, Convert.ToBase64String(randBytes), System.Text.Encoding.ASCII);

                WalkmanLib.StreamCopy(File.OpenRead(testFileSource), File.OpenWrite(testFileTarget));

                return GeneralFunctions.TestString("StreamCopy3", File.ReadAllText(testFileTarget), "");
            }
        }

        public static bool Test_StreamCopyThrows1() {
            Exception ex = new NoException();
            try {
                WalkmanLib.StreamCopy(null, null);
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("StreamCopyThrows1", ex.GetType(), typeof(ArgumentNullException));
        }

        public static bool Test_StreamCopyThrows2(string rootTestFolder) {
            Exception ex = new NoException();
            try {
                using (var testFileSource = new DisposableFile(Path.Combine(rootTestFolder, "streamCopyThrows2Source")))
                using (var testFileTarget = new DisposableFile(Path.Combine(rootTestFolder, "streamCopyThrows2Target")))
                using (FileStream source = File.OpenWrite(testFileSource))
                using (FileStream target = File.OpenRead(testFileTarget)) {
                    WalkmanLib.StreamCopy(source, target);
                }
            } catch (Exception ex2) {
                ex = ex2;
            }
            return GeneralFunctions.TestType("StreamCopyThrows2", ex.GetType(), typeof(InvalidOperationException));
        }

#endif
    }
}
