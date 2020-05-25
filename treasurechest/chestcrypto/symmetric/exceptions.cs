using System;
namespace chestcrypto{

    namespace exceptions{
        public class InvalidKeyLength : Exception
        {
            public InvalidKeyLength()
            {
            }

            public InvalidKeyLength(string message)
                : base(message)
            {
            }

            public InvalidKeyLength(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }

}