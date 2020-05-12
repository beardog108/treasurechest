namespace chestcrypto{

    public class DoublePrivateKey{

        private byte[] signingPrivateKey = new byte[64];
        private byte[] encryptPrivateKey = new byte[32];

        private string signingPrivateKeyString;
        private string encryptPrivateKeyString;

        public byte[] getRawDouble(){
            return ByteCombiner.Combine(signingPrivateKey, encryptPrivateKey);
        }

        public DoublePrivateKey(byte[] sign, byte[] encrypt){
            if (sign.Length != 64){
                throw new InvalidDoubleKeyException("Signing private key must be 64 bytes in length.");
            }
            if (encrypt.Length != 32){
                throw new InvalidDoubleKeyException("Signing private key must be 32 bytes in length.");
            }
            signingPrivateKey = sign;
            encryptPrivateKey = encrypt;
        }

        public DoublePrivateKey(byte[] combinedKey){
            if (combinedKey.Length != 96){
                throw new InvalidDoubleKeyException("Invalid key length, must be 96 bytes in length");
            }
            for (int i = 0; i < combinedKey.Length; i++){
                if (i < 64){
                    signingPrivateKey[i] = combinedKey[i];
                    continue;
                }
                encryptPrivateKey[i - 64] = combinedKey[i];
            }
        }

    }

}