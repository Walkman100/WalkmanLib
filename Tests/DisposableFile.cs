using System;
using System.IO;

namespace Tests {
    public class DisposableFile : IDisposable {
        public string FilePath { get; }
        private bool disposed = false;

        public DisposableFile(string path, bool createFile = true, bool checkFileExistence = true) {
            if (createFile) {
                if (Directory.Exists(path) || File.Exists(path)) {
                    throw new IOException("Path already exists!");
                }
                File.Create(path).Dispose();
            } else if (checkFileExistence) {
                if (!File.Exists(path)) {
                    throw new FileNotFoundException("File doesn't exist, and createFile specified as false!", path);
                }
            }

            FilePath = new FileInfo(path).FullName;
        }

        public override string ToString() => FilePath;

        public static implicit operator string(DisposableFile v) => v.FilePath;

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing)
                    GeneralFunctions.DeleteFileIfExists(FilePath);
                disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableFile() {
            Dispose(false);
        }
    }
}
