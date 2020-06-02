using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading;
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
            Assert.IsTrue(
                Enumerable.SequenceEqual(
                SessionCrypto.decrypt(session, encrypted),
                message
                )
            );
        }


        [Test]
        public void TestDecryptExpired(){
            var us = PublicKeyBox.GenerateKeyPair();
            var them = PublicKeyBox.GenerateKeyPair();
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello friend");
            Session session = new Session(us.PrivateKey, them.PublicKey, true, 5);
            session.setMinimumKeyExpireSeconds(1);
            session.setMessageDelay((long) 1);
            var ourNew = PublicKeyBox.GenerateKeyPair();
            var ourNew2 = PublicKeyBox.GenerateKeyPair();
            session.addPrivate(ourNew.PrivateKey, getFutureTime(1));
            byte[] encrypted = Curve25519.encrypt(them.PrivateKey, ourNew.PublicKey, message);
            session.addPrivate(ourNew2.PrivateKey, getFutureTime(1));
            session.cleanPrivate();
            try{
                Assert.IsFalse(
                    Enumerable.SequenceEqual(
                    SessionCrypto.decrypt(session, encrypted),
                    message
                )
                );
            }
            catch(System.Security.Cryptography.CryptographicException){
                return;
            }
            Assert.Fail();
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
            Assert.IsTrue(
                Enumerable.SequenceEqual(
                SessionCrypto.decrypt(session, encrypted),
                message)
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
            Assert.IsTrue(
                Enumerable.SequenceEqual(
                Curve25519.decrypt(ephemeral.PrivateKey, us.PublicKey, encrypted),
                message)
            );
        }

    }
}