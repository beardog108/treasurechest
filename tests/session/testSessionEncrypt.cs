using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using chestcrypto.session;
using chestcrypto.exceptions;
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
            byte[] publicK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte[] privateK = PublicKeyBox.GenerateKeyPair().PrivateKey;
            byte[] newK = PublicKeyBox.GenerateKeyPair().PublicKey;
            byte message = ""
            Session session = new Session(privateK, publicK, true, 5);
            SessionCrypto sessionCrypto = new SessionCrypto(session);
            session.setMinimumKeyExpireSeconds(1);
            session.setMessageDelay((long) 1);
            session.addPublic(newK, getFutureTime(9));
            sessionCrypto.encrypt()
        }

    }
}