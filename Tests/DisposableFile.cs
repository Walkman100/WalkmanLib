using System;
using System.IO;

namespace Tests {
    public class DisposableFile : IDisposable {
        private string _filePath;
        public string filePath {
            get {
                return _filePath;
            }
        }
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

            _filePath = new FileInfo(path).FullName;
        }

        public override string ToString() {
            return filePath;
        }

        public static implicit operator string(DisposableFile v) {
            return v.filePath;
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing)
                    GeneralFunctions.DeleteFileIfExists(filePath);
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
