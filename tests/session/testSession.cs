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

        public long getFutureTime(int seconds){
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long) seconds;
        }

        [Test]
        public void TestSessionAddValidPublic(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true);
            session.addPublic(newK, getFutureTime(61));
            Assert.IsTrue(Enumerable.SequenceEqual(newK, session.getLatestPublicKey()));
        }

        [Test]
        public void TestSessionAddPublicInvalidTime(){
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            Session session = new Session(privateK, publicK, true);
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
            Session session = new Session(privateK, publicK, true);
            byte[] invalid = {0, 0, 0};

            try{
                new Session(invalid, publicK, true);
            }
            catch(InvalidKeyLength){
                goto secondAssert;
            }
            Assert.Fail();
            secondAssert:
                try{
                    new Session(privateK, invalid, true);
                }
                catch(InvalidKeyLength){
                    return;
                }
                Assert.Fail();
        }


    }
}