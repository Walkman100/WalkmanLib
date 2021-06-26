using System;

namespace Tests {
    public class RedirectConsole : IDisposable {
        private readonly System.IO.TextWriter oldConsoleOut;
        private readonly System.IO.TextWriter oldConsoleErr;
        private bool disposed = false;

        public RedirectConsole(System.IO.StringWriter redirectTo) {
            oldConsoleOut = Console.Out;
            oldConsoleErr = Console.Error;

            Console.SetOut(redirectTo);
            Console.SetError(redirectTo);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    Console.SetOut(oldConsoleOut);
                    Console.SetError(oldConsoleErr);
                }
                disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
