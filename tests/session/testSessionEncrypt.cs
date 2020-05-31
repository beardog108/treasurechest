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
        public void TestDecrypt(){
            var us = PublicKeyBox.GenerateKeyPair();
            var them = PublicKeyBox.GenerateKeyPair();
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello friend");
            Session session = new Session(us.PrivateKey, them.PublicKey, true, 5);
            var ourNew = PublicKeyBox.GenerateKeyPair();
            session.addPrivate(ourNew.PrivateKey, getFutureTime(1000));
            byte[] encrypted = Curve25519.encrypt(them.PrivateKey, ourNew.PublicKey, message);
            Assert.AreEqual(
                SessionCrypto.decrypt(session, encrypted),
                message
            );
        }

        [Test]
        public void TestDecryptOlderKey(){
            var us = PublicKeyBox.GenerateKeyPair();
            var them = PublicKeyBox.GenerateKeyPair();
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello friend");
            Session session = new Session(us.PrivateKey, them.PublicKey, true, 5);
            var ourNew = PublicKeyBox.GenerateKeyPair();
            var ourNew2 = PublicKeyBox.GenerateKeyPair();
            session.addPrivate(ourNew.PrivateKey, getFutureTime(1000));
            byte[] encrypted = Curve25519.encrypt(them.PrivateKey, ourNew.PublicKey, message);
            session.addPrivate(ourNew2.PrivateKey, getFutureTime(1005));
            Assert.AreEqual(
                SessionCrypto.decrypt(session, encrypted),
                message
            );
        }

        [Test]
        public void TestEncrypt(){
            // Test ephemeral encrypt
            var us = PublicKeyBox.GenerateKeyPair();
            var them = PublicKeyBox.GenerateKeyPair();
            var ephemeral = PublicKeyBox.GenerateKeyPair();
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello friend");
            Session session = new Session(us.PrivateKey, them.PublicKey, true, 5);
            session.addPublic(ephemeral.PublicKey, getFutureTime(1000));
            byte[] encrypted = SessionCrypto.encrypt(session, message);
            Assert.AreEqual(
                Curve25519.decrypt(ephemeral.PrivateKey, us.PublicKey, encrypted),
                message
            );
        }

    }
}