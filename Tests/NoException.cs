using System;

namespace Tests {
    public class NoException : Exception {
        public NoException() : base("No Exception was thrown") { }
    }
}
