using Sodium;

namespace chestcrypto {
    public class Curve25519{

        public static int NONCE_BYTE_AMOUNT = 24;
        public static byte[] encrypt(byte[] privkey, byte[] pubkey, byte[] message){
            byte[] nonce = Sodium.PublicKeyBox.GenerateNonce();
            return ByteCombiner.Combine
                    (nonce,
                    Sodium.PublicKeyBox.Create(
                        message,
                        nonce,
                        privkey,
                        pubkey
                    ));
        }
    }
}