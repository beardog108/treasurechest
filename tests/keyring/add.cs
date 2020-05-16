using NUnit.Framework;
using System;
using System.IO;
using Sodium;
using System.Collections.Generic;
using keyring;
using chestcrypto;

namespace KeyRingTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestKeyRingStore()
        {
            string tempFile = Path.GetTempFileName();
            KeyRing keyRing = new KeyRing();
            byte[] signingKey = PublicKeyAuth.GenerateKeyPair().PublicKey;
            byte[] encryptionKey = PublicKeyBox.GenerateKeyPair().PublicKey;

            byte[] combinedKey = new byte[signingKey.Length + encryptionKey.Length];
            Buffer.BlockCopy(signingKey, 0, combinedKey, 0, signingKey.Length);
            Buffer.BlockCopy(encryptionKey, 0, combinedKey, signingKey.Length, encryptionKey.Length);
            DoublePublicKey combo = new DoublePublicKey(signingKey, encryptionKey);
            keyRing.addPublicKey(combo);

            List<byte[]> storedKeys = keyRing.getIdentityPublicKeys();
            bool success = false;
            storedKeys.ForEach(delegate(byte[] key)
            {
                if (key.Equals(combinedKey)){
                    success = true;
                }
            });
            if (! success){
                Assert.Fail();
            }


        }

    }
}