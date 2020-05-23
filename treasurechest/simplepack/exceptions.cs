using System;
namespace chestcrypto{

    namespace exceptions{
        public class InvalidSimplePackMessage : Exception
        {
            public InvalidSimplePackMessage()
            {
            }

            public InvalidSimplePackMessage(string message)
                : base(message)
            {
            }

            public InvalidSimplePackMessage(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }

}