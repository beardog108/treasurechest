using Sodium;
using System;


internal class Ed25519KeyGenerator{
    public static byte[] generator(){
        byte[] key = {};
        key = PublicKeyAuth.GenerateKeyPair().PrivateKey;
        return key;
    }
}

internal class Curve25519KeyGenerator{
    public static byte[] generator(){
        byte[] key = {};
        key = PublicKeyBox.GenerateKeyPair().PrivateKey;
        return key;
    }
}

namespace chestcrypto{
    public class PrivateKeyGenerator{
        public static byte[] generate()
        {
            byte[] ed25519 = Ed25519KeyGenerator.generator();
            byte[] curve25519 = Curve25519KeyGenerator.generator();
            byte[] key = chestcrypto.ByteCombiner.Combine(ed25519, curve25519);
            Array.Clear(ed25519, 0, ed25519.Length);
            Array.Clear(curve25519, 0, curve25519.Length);
            return key;

        }
    }
}

