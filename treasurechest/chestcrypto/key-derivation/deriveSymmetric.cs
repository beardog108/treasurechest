
using System.Text;
using Sodium;

namespace chestcrypto{

    namespace kdf{
        public class DeterministicSymmetricKey{
            // Test
            public static byte[] generate(string passphrase, bool extraSensitive=false){
                var nonce = SecretBox.GenerateNonce();
                int strength = 2;
                if (extraSensitive){
                    strength = 3;
                }
                return PasswordHash.ArgonHashBinary(Encoding.UTF8.GetBytes(passphrase), // Passphrase converted to bytes
                                            PasswordHash.ArgonGenerateSalt(), // Salt
                                            strength, strength,
                                            32);
            }

        }

    }

}
