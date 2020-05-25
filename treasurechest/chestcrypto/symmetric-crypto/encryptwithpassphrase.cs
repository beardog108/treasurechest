using Sodium;
using System;
using chestcrypto.kdf;
namespace treasurechest{

    namespace symmetric{

        public class EncryptWithPassphrase{
            /* Class name is somewhat misleading as we actually derive a key from a string pass and use the key for secret key crypto*/
            public static byte[] encrypt(byte[] data, string passphrase, bool extraSensitive = false){
                byte[] key = DeterministicSymmetricKey.generate(passphrase, extraSensitive);
                return SecretBox.Create(data, SecretBox.GenerateNonce(), key);
            }

        }

    }

}