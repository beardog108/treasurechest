using System;

namespace chestcrypto {

    public class InvalidDoubleKeyException : Exception
    {
        public InvalidDoubleKeyException()
        {
        }

        public InvalidDoubleKeyException(string message)
            : base(message)
        {
        }

        public InvalidDoubleKeyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}