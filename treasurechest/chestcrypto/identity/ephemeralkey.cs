namespace chestcrypto{

    namespace identity{
        internal class EphemeralKey{

            private int epochTime;
            private int secondsToExpire;
            private byte[] key = new byte[32];
            private bool isPrivate;

            private Identity identity;
            public EphemeralKey(Identity identity, byte[] key, int secondsToExpire){
                
            }


        }
    }

}