namespace chestcrypto{

    public class DoublePublicKey{

        private byte[] signingPublicKey = new byte[32];
        private byte[] encryptPublicKey = new byte[32];

        private string[] signingPublicKeyString;
        private string[] encryptPublicKeyString;

        public byte[] getRawDouble(){
            return ByteCombiner.Combine(signingPublicKey, encryptPublicKey);
        }


        public DoublePublicKey(byte[] sign, byte[] encrypt){
            signingPublicKey = sign;
            encryptPublicKey = encrypt;
        }

        public DoublePublicKey(byte[] joinedKey){
            if (joinedKey.Length != 64){
                throw new InvalidDoubleKeyException();
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