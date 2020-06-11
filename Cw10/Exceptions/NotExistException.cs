using System;

namespace Cw10.Exceptions {
    public class NotExistException : Exception {
        public NotExistException(string? message) : base(message) {
        }
    }
}