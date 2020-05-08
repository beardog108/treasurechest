using Sodium;
using System.Security.Cryptography;
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
        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }
        public static byte[] generate(){
            byte[] ed25519 = Ed25519KeyGenerator.generator();
            byte[] curve25519 = Curve25519KeyGenerator.generator();
            byte[] key = Combine(ed25519, curve25519);
            return key;

        }
    }
}

