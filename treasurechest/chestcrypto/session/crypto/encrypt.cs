using Sodium;

using chestcrypto.session;
using chestcrypto;

namespace chestcrypto.session.crypto{

    internal class SessionEncrypt{

        public static byte[] Encrypt(Session activeSession, byte[] message){
            byte[] publicKey = activeSession.getLatestPublicKey();
            byte[] privateKey = activeSession.getLatestPrivateKey();
            return Curve25519.encrypt(privateKey, publicKey, message);
        }

    }

}