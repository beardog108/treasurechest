using Sodium;

using chestcrypto.session;
using chestcrypto;

namespace chestcrypto.session.crypto{

    internal class SessionCrypto{

        public static byte[] encrypt(Session activeSession, byte[] message){
            byte[] publicKey = activeSession.getLatestPublicKey();
            byte[] privateKey = activeSession.getOurMasterPrivate();
            return Curve25519.encrypt(privateKey, publicKey, message);
        }

        public static byte[] decrypt(Session activeSession, byte[] ciphertext){
            byte[] publicKey = activeSession.getTheirMasterPublic();
            byte[] decrypted;
            byte[] privateKey;
            foreach (var privKey in activeSession.getAllPrivateKeys()){
                try{
                    privateKey = privKey.Item2;
                    decrypted = Curve25519.decrypt(privateKey, publicKey, ciphertext);
                    return decrypted;
                }
                catch(System.Security.Cryptography.CryptographicException){}
            }
            throw new System.Security.Cryptography.CryptographicException();
        }

    }

}