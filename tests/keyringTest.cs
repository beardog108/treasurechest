using NUnit.Framework;
using System;
using chestcrypto;
using keyring;
using chestcrypto.identity;
using Sodium;

namespace keyringTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestKeyringAddPublic()
        {
            DoublePublicKey key = new DoublePublicKey(PublicKeyAuth.GenerateKeyPair().PublicKey, PublicKeyBox.GenerateKeyPair().PublicKey);
            PublicIdentity iden = new PublicIdentity(key, "bob");
            DoublePublicKey key2 = new DoublePublicKey(PublicKeyAuth.GenerateKeyPair().PublicKey, PublicKeyBox.GenerateKeyPair().PublicKey);
            PublicIdentity iden2 = new PublicIdentity(key2, "alice");
            KeyRing ring = new KeyRing();
            ring.addPublicIdentity(iden);

            Assert.IsTrue(ring.publicIdentities.Contains(iden));
            Assert.IsFalse(ring.publicIdentities.Contains(iden2));
        }

        [Test]
        public void TestKeyringAddPrivate(){
            DoublePrivateKey key = new DoublePrivateKey(PublicKeyAuth.GenerateKeyPair().PrivateKey, PublicKeyBox.GenerateKeyPair().PrivateKey);
            PrivateIdentity iden = new PrivateIdentity(key, "bob");
            DoublePrivateKey key2 = new DoublePrivateKey(PublicKeyAuth.GenerateKeyPair().PrivateKey, PublicKeyBox.GenerateKeyPair().PrivateKey);
            PrivateIdentity iden2 = new PrivateIdentity(key2, "alice");
            KeyRing ring = new KeyRing();
            ring.addPrivateIdentity(iden);

            Assert.IsTrue(ring.privateIdentities.Contains(iden));
            Assert.IsFalse(ring.privateIdentities.Contains(iden2));
        }

    }
}