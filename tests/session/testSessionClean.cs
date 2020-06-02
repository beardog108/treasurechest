using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using chestcrypto.session;
using chestcrypto.exceptions;
using Sodium;

namespace sessionPrivateTestsCleaning
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public long getFutureTime(int seconds){return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long) seconds;}

        [Test]
        public void TestSessionCleanPrivate(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            Session session = new Session(privateK, publicK, true, 5);
            session.setMinimumKeyExpireSeconds(1);
            session.setMessageDelay((long) 1);
            session.addPrivate(newK, getFutureTime(2));
            session.addPrivate(PublicKeyBox.GenerateKeyPair().PrivateKey, getFutureTime(1));
            Thread.Sleep(3);
            session.cleanPrivate();
            Assert.IsTrue(session.getAllPrivateKeys().Length == 0);
        }


        [Test]
        public void TestSessionCleanPublic(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true, 5);
            session.setMinimumKeyExpireSeconds(1);
            session.setMessageDelay((long) 1);
            session.addPublic(newK, getFutureTime(2));
            bool atLeastOneLoop = false;
            while(true){
                try{
                    if (Enumerable.SequenceEqual(session.getLatestPublicKey(), newK)){
                        Thread.Sleep(25); // ms
                        atLeastOneLoop = true; // key should not be deleted instantly
                        continue;
                    }
                }
                catch(System.ArgumentOutOfRangeException){
                    break;
                }
                session.cleanPublic();
            }
            Assert.IsTrue(atLeastOneLoop);
        }


    }
}