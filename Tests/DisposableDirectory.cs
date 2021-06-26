using System;
using System.IO;

namespace Tests {
    public class DisposableDirectory : IDisposable {
        private string _dirPath;
        public string dirPath {
            get {
                return _dirPath;
            }
        }
        private bool disposed = false;

        public DisposableDirectory(string path, bool createDir = true) {
            if (createDir) {
                if (Directory.Exists(path) || File.Exists(path)) {
                    throw new IOException("Path already exists!");
                }
                _dirPath = Directory.CreateDirectory(path).FullName;
            } else {
                _dirPath = new DirectoryInfo(path).FullName;
            }
        }

        public override string ToString() {
            return dirPath;
        }

        public static implicit operator string(DisposableDirectory v) {
            return v.dirPath;
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                try {
                    if (disposing)
                        Directory.Delete(dirPath, true);
                } catch (DirectoryNotFoundException) { }

                disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableDirectory() {
            Dispose(false);
        }
    }
}
