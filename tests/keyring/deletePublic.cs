using NUnit.Framework;
using System;
using System.IO;
using Sodium;
using System.Collections.Generic;
using keyring;
using chestcrypto;
using chestcrypto.exceptions;

namespace KeyRingDeletePublicTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDeleteIdentityByPublicKey(){
            string tempFile = Path.GetTempFileName();

            DoublePublicKey getKey(){
                KeyRing keyRing = new KeyRing();
                byte[] signingKey = PublicKeyAuth.GenerateKeyPair().PublicKey;
                byte[] encryptionKey = PublicKeyBox.GenerateKeyPair().PublicKey;

                byte[] combinedKey = new byte[signingKey.Length + encryptionKey.Length];
                Buffer.BlockCopy(signingKey, 0, combinedKey, 0, signingKey.Length);
                Buffer.BlockCopy(encryptionKey, 0, combinedKey, signingKey.Length, encryptionKey.Length);
                DoublePublicKey combo = new DoublePublicKey(signingKey, encryptionKey);
                return combo;
            }
            DoublePublicKey combo = getKey();
            KeyRing keyRing = new KeyRing();
            keyRing.addPublicKey(combo);
            Assert.IsTrue(keyRing.getIdentityCount() == 1);
            keyRing.removeIdentityByPubkey(combo);
            Assert.IsTrue(keyRing.getIdentityCount() == 0);


        }

    }
}