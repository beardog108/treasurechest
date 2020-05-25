using System.Collections.Generic;
using Sodium;

namespace chestcrypto{

    namespace symmetric{

        public class Symmetric{

            public static byte[] encrypt(byte[] plaintext, byte[] key){
                if (key.Length != 32){
                    throw new exceptions.InvalidKeyLength();
                }
                List<byte> encrypted = new List<byte>();
                byte[] nonce = SecretBox.GenerateNonce();
                encrypted.AddRange(nonce);
                byte[] ciphertext = SecretBox.Create(plaintext, nonce, key);
                encrypted.AddRange(ciphertext);
                return encrypted.ToArray();
            }
            public static (byte[] ciphertext, byte[] key) encrypt(byte[] plaintext){
                byte[] key = SecretBox.GenerateKey();
                return (encrypt(plaintext, key), key);
            }

            public static byte[] decrypt(byte[] ciphertext, byte[] key){
                // Nonce is first 24 bytes of ciphertext, unencrypted (this is safe according to libsodium docs)
                int nonceSize = 24;
                byte[] nonce = new byte[nonceSize];
                List<byte> message = new List<byte>();
                for (int i = 0; i < ciphertext.Length; i++){
                    if (i < nonceSize){
                        nonce[i] = ciphertext[i];
                        continue;
                    }
                    message.Add(ciphertext[i]);
                }
                return SecretBox.Open(message.ToArray(), nonce, key);
            }

        }

    }

}