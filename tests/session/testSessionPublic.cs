using NUnit.Framework;
using System;
using System.Linq;
using chestcrypto.session;
using chestcrypto.exceptions;
using Sodium;

namespace sessionTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public long getFutureTime(int seconds){return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long) seconds;}

        [Test]
        public void TestSessionGetLatestPublic(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true, 5);
            for (int i = 0; i < 5; i++){
                session.addPublic(PublicKeyBox.GenerateKeyPair().PublicKey, getFutureTime(630));
            }
            session.addPublic(newK, getFutureTime(650));
            Assert.IsTrue(Enumerable.SequenceEqual(newK, session.getLatestPublicKey()));
        }

        [Test]
        public void TestSessionAddValidPublic(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true, 5);
            session.addPublic(newK, getFutureTime(610));
            Assert.IsTrue(Enumerable.SequenceEqual(newK, session.getLatestPublicKey()));
        }

        [Test]
        public void TestSessionNoPublicDupes(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true, 5);
            session.addPublic(newK, getFutureTime(615));
            try{
                session.addPublic(newK, getFutureTime(615));
            }
            catch(DuplicatePublicKey){return;}
            Assert.Fail();
        }

        [Test]
        public void TestSessionAddPublicInvalidKey(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = {3, 5};
            Session session = new Session(privateK, publicK, true, 5);
            try{
                session.addPublic(newK, getFutureTime(61));
            }
            catch(InvalidKeyLength){
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void TestSessionAddPublicInvalidTime(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true, 5);
            try{
                session.addPublic(newK, getFutureTime(-1));
            }
            catch(System.ArgumentOutOfRangeException){
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void TestSessionConstructor()
        {
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            Session session = new Session(privateK, publicK, true, 5);
            byte[] invalid = {0, 0, 0};

            try{
                new Session(invalid, publicK, true, 5);
            }
            catch(InvalidKeyLength){
                goto secondAssert;
            }
            Assert.Fail();
            secondAssert:
                try{
                    new Session(privateK, invalid, true, 5);
                }
                catch(InvalidKeyLength){
                    return;
                }
                Assert.Fail();
        }


    }
}