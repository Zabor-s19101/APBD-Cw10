using System;

namespace Cw10.Exceptions {
    public class ExistException : Exception {
        public ExistException(string? message) : base(message) {
        }
    }
}