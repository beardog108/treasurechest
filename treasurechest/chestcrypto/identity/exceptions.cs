using System;
namespace chestcrypto{

    namespace exceptions{
        public class NoIdentityException : Exception
        {
            public NoIdentityException()
            {
            }

            public NoIdentityException(string message)
                : base(message)
            {
            }

            public NoIdentityException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }

}