using System;

namespace chestcrypto {

    namespace exceptions{
        public class DuplicateIdentityException : Exception
        {
            public DuplicateIdentityException()
            {
            }

            public DuplicateIdentityException(string message)
                : base(message)
            {
            }

            public DuplicateIdentityException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

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

}