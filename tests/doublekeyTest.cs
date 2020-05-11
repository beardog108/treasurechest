using NUnit.Framework;
using chestcrypto;
using System;
using Sodium;

namespace DoubleKeyTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDoublePublicKey()
        {
            // Test that the combined key loader loads both constructors with the same results
            byte[] signingKey = PublicKeyAuth.GenerateKeyPair().PublicKey;
            byte[] encryptionKey = PublicKeyBox.GenerateKeyPair().PublicKey;

            byte[] combinedKey = new byte[signingKey.Length + encryptionKey.Length];
            Buffer.BlockCopy(signingKey, 0, combinedKey, 0, signingKey.Length);
            Buffer.BlockCopy(encryptionKey, 0, combinedKey, signingKey.Length, encryptionKey.Length);

            DoublePublicKey combinedLoad = new chestcrypto.DoublePublicKey(combinedKey);
            DoublePublicKey parameterLoad = new chestcrypto.DoublePublicKey(signingKey, encryptionKey);
            if (combinedLoad.getRawDouble().Length != parameterLoad.getRawDouble().Length){
                Assert.Fail();
            }
            if (combinedLoad.getRawDouble().Length != 64){
                Assert.Fail();
            }
            for (int i = 0; i < combinedLoad.getRawDouble().Length; i++){
                if (combinedLoad.getRawDouble()[i] != parameterLoad.getRawDouble()[i]){
                    Assert.Fail();
                }
                if (combinedLoad.getRawDouble()[i] != combinedKey[i]){
                    Assert.Fail();
                }
            }
        }
    }
}