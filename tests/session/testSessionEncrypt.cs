using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using chestcrypto.session;
using chestcrypto.session.crypto;
using chestcrypto.exceptions;
using chestcrypto;
using Sodium;

namespace sessionTestEncrypt
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public long getFutureTime(int seconds){return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long) seconds;}

        [Test]
        public void TestEncrypt(){
            var pair1 = PublicKeyBox.GenerateKeyPair();
            byte[] publicK = pair1.PublicKey;
            byte[] privateK = pair1.PrivateKey;
            var pair = PublicKeyBox.GenerateKeyPair();
            byte[] privKey = pair.PrivateKey;
            byte[] pubKey = pair.PublicKey;
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello friend");
            Session session = new Session(privateK, publicK, true, 5);
            session.setMinimumKeyExpireSeconds(10);
            session.setMessageDelay((long) 25);
            session.addPublic(pubKey, getFutureTime(100));
            byte[] encrypted = SessionCrypto.encrypt(session, message);
            byte[] decrypted = Curve25519.decrypt(privKey, publicK, encrypted);
            Assert.AreEqual(decrypted, message);
        }

    }
}