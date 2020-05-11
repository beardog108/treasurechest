namespace chestcrypto{

    public class DoublePrivateKey{

        private byte[] signingprivateKey;
        public byte[] encryptprivateKey;


        public DoublePrivateKey(byte[] sign, byte[] encrypt){
            signingprivateKey = sign;
            encryptprivateKey = encrypt;
        }

    }

}