using NUnit.Framework;
using System;
using System.Linq;
using chestcrypto.session;
using chestcrypto.exceptions;
using Sodium;

namespace sessionPrivateTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public long getFutureTime(int seconds){return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long) seconds;}

        [Test]
        public void TestSessionAddValidPrivate(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            Session session = new Session(privateK, publicK, true, 5);
            session.addPrivate(newK, getFutureTime(670));
            Assert.IsTrue(Enumerable.SequenceEqual(newK, session.getLatestPrivateKey()));
        }

        [Test]
        public void TestSessionAddInvalidPrivateTime(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            Session session = new Session(privateK, publicK, true, 5);
            try{
                session.addPrivate(newK, getFutureTime(1));
            }
            catch(System.ArgumentOutOfRangeException){return;}
            Assert.Fail();
        }
        [Test]
        public void TestSessionAddInvalidPrivate(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = {5, 3, 2, 1};
            Session session = new Session(privateK, publicK, true, 5);
            try{
                session.addPrivate(newK, getFutureTime(7010));
            }
            catch(InvalidKeyLength){return;}
            Assert.Fail();
        }

    }
}