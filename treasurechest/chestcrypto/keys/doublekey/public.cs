namespace chestcrypto{

    public class DoublePublicKey{

        private byte[] signingPublicKey = new byte[32];
        private byte[] encryptPublicKey = new byte[32];

        private string[] signingPublicKeyString;
        private string[] encryptPublicKeyString;

        public byte[] getRawDouble(){
            return ByteCombiner.Combine(signingPublicKey, encryptPublicKey);
        }
        public byte[] getSigningPublicKey(){
            return signingPublicKey;
        }
        public byte[] getEncryptPublicKey(){
            return encryptPublicKey;
        }


        public DoublePublicKey(byte[] sign, byte[] encrypt){
            // Construct double key from two separate byte arrays
            if (sign.Length != 32 || encrypt.Length != 32){
                throw new exceptions.InvalidDoubleKeyException("Invalid length, both keys should be 32 bytes");
            }
            signingPublicKey = sign;
            encryptPublicKey = encrypt;
        }

        public DoublePublicKey(byte[] joinedKey){
            // Construct double key from one bytearray
            if (joinedKey.Length != 64){
                throw new exceptions.InvalidDoubleKeyException("Invalid length, both keys should be 32 bytes");
            }
            for (int i = 0; i < joinedKey.Length; i++){
                if (i < 32){
                    this.signingPublicKey[i] = joinedKey[i];
                    continue;
                }
                this.encryptPublicKey[i - 32] = joinedKey[i];
            }
        }

    }

}