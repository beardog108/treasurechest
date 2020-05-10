using Sodium;

namespace chestcrypto {
    public class Curve25519{

        public static int NONCE_BYTE_AMOUNT = 24;
        public static byte[] encrypt(byte[] privkey, byte[] pubkey, byte[] message){
            // Take a byte message and priv/pubkey for authenticated encryption and return encrypted data with prepended nonce
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

        public static byte[] decrypt(byte[] privkey, byte[] pubkey, byte[] message){
            byte[] nonce = new byte[NONCE_BYTE_AMOUNT];
            byte[] encrypted = new byte[message.Length - NONCE_BYTE_AMOUNT];
            int counter = 0;
            for (int i = 0; i < message.Length; i++){
                if (i < NONCE_BYTE_AMOUNT){
                    nonce[i] = message[i];
                    continue;
                }
                encrypted[counter] = message[i];
                counter += 1;
            }

            return Sodium.PublicKeyBox.Open(encrypted, nonce, privkey, pubkey);
        }
    }
}