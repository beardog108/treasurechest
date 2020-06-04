using NUnit.Framework;
using chestcrypto.identity;
using chestcrypto;
using System;
using System.Linq;
using Sodium;

namespace PublicIndentityTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPublicIdentityGetDoublePublicIdentity(){
            byte[] signingKey = PublicKeyAuth.GenerateKeyPair().PublicKey;
            byte[] encryptionKey = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] combinedKey = new byte[signingKey.Length + encryptionKey.Length];
            Buffer.BlockCopy(signingKey, 0, combinedKey, 0, signingKey.Length);
            Buffer.BlockCopy(encryptionKey, 0, combinedKey, signingKey.Length, encryptionKey.Length);
            DoublePublicKey doubleKey = new DoublePublicKey(combinedKey);
            PublicIdentity iden = new PublicIdentity(doubleKey, "Picard");
            Assert.IsTrue(Enumerable.SequenceEqual(iden.getPublicKey().getRawDouble(), combinedKey));

        }

        [Test]
        public void TestPrivateIdentityConstructor()
        {
            byte[] signingKey = PublicKeyAuth.GenerateKeyPair().PrivateKey;
            byte[] encryptionKey = PublicKeyBox.GenerateKeyPair().PrivateKey;

            byte[] combinedKey = new byte[signingKey.Length + encryptionKey.Length];
            Buffer.BlockCopy(signingKey, 0, combinedKey, 0, signingKey.Length);
            Buffer.BlockCopy(encryptionKey, 0, combinedKey, signingKey.Length, encryptionKey.Length);

            DoublePrivateKey combinedLoad = new chestcrypto.DoublePrivateKey(combinedKey);

            PrivateIdentity iden = new PrivateIdentity(combinedLoad, "Picard");
            Assert.AreEqual(iden.getName(), "Picard");
            Assert.AreEqual(iden.getNote(), "");

            PrivateIdentity iden2 = new PrivateIdentity(combinedLoad, "Picard2", "test");
            Assert.AreEqual(iden2.getName(), "Picard2");
            Assert.AreEqual(iden2.getNote(), "test");
        }



    }
}