using System;
namespace chestcrypto{

    namespace exceptions{
        public class DuplicatePublicKey : Exception
        {
            public DuplicatePublicKey()
            {
            }

            public DuplicatePublicKey(string message)
                : base(message)
            {
            }

            public DuplicatePublicKey(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }

}