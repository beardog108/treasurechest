using Sodium;

namespace chestcrypto{

    public class DoublePrivateKey{

        private byte[] signingPrivateKey = new byte[64];
        private byte[] encryptPrivateKey = new byte[32];

        private string signingPrivateKeyString;
        private string encryptPrivateKeyString;

        // Test DoubleKeyPrivateTests.Tests.TestDoublePrivateKeyGetters
        public byte[] getCurve25519PrivateKey(){return encryptPrivateKey;}
        // Test DoubleKeyPrivateTests.Tests.TestDoublePrivateKeyGetters
        public byte[] getEd25519PrivateKey(){return signingPrivateKey;}

        public byte[] getCurve25519PublicKey(){
            return PublicKeyBox.GenerateKeyPair(getCurve25519PrivateKey()).PublicKey;
        }
        public byte[] getEd25519PublicKey(){
            return PublicKeyAuth.GenerateKeyPair(getEd25519PrivateKey()).PublicKey;
        }
        // Test DoubleKeyPrivateTests.TestDoubleKeyLoad
        public byte[] getRawDouble(){
            return ByteCombiner.Combine(signingPrivateKey, encryptPrivateKey);
        }

        // Test DoubleKeyPrivateTests.TestDoubleKeyLoad
        public DoublePrivateKey(byte[] sign, byte[] encrypt){
            if (sign.Length != 64){
                throw new exceptions.InvalidDoubleKeyException("Signing private key must be 64 bytes in length.");
            }
            if (encrypt.Length != 32){
                throw new exceptions.InvalidDoubleKeyException("Signing private key must be 32 bytes in length.");
            }
            signingPrivateKey = sign;
            encryptPrivateKey = encrypt;
        }
        // Test DoubleKeyPrivateTests.TestDoubleKeyLoad
        public DoublePrivateKey(byte[] combinedKey){
            if (combinedKey.Length != 96){
                throw new exceptions.InvalidDoubleKeyException("Invalid key length, must be 96 bytes in length");
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