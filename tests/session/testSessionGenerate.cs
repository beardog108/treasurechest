using NUnit.Framework;
using System;
using chestcrypto.session;
using chestcrypto.exceptions;
using System.Threading;
using Sodium;

namespace sessionTestGenerate
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public long getFutureTime(int seconds){return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long) seconds;}

        [Test]
        public void TestGenerateTime(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true, 5);
            session.setMinimumKeyExpireSeconds(2);
            session.setMessageDelay(1);
            try{
                session.getLatestPrivateKey();
            }
            catch(NoSessionKeyAvailable){
                goto next;
            }
            Assert.Fail();
            next:
                session.generatePrivate(2);
            try{
                session.getLatestPrivateKey();
            }
            catch(NoSessionKeyAvailable){
                Assert.Fail();
            }
            Thread.Sleep(1000);
            try{
                session.getLatestPrivateKey();
            }
            catch(System.ArgumentOutOfRangeException){
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void TestGenerate(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true, 5);
            try{
                session.getLatestPrivateKey();
            }
            catch(NoSessionKeyAvailable){
                goto next;
            }
            Assert.Fail();
            next:
                session.generatePrivate();
            try{
                session.getLatestPrivateKey();
            }
            catch(NoSessionKeyAvailable){
                Assert.Fail();
            }
        }

    }
}