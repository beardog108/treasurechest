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
        public class DuplicatePrivateKey : Exception
        {
            public DuplicatePrivateKey()
            {
            }

            public DuplicatePrivateKey(string message)
                : base(message)
            {
            }

            public DuplicatePrivateKey(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class NoSessionKeyAvailable : Exception
        {
            public NoSessionKeyAvailable()
            {
            }

            public NoSessionKeyAvailable(string message)
                : base(message)
            {
            }

            public NoSessionKeyAvailable(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

    }

}