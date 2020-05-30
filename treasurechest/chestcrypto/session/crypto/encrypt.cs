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

    }

}