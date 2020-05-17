using NUnit.Framework;
using chestcrypto;
using System;
using Sodium;

namespace DoubleKeyPublicTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDoublePublicKeyThrowsOnBadLoad()
        {
            byte[] invalid = {0};
            byte[] invalid2 = new byte[33];
            for (int i = 0; i < invalid2.Length; i++){
                invalid2[i] = 1;
            }
            bool success = false;
            try{
                new chestcrypto.DoublePublicKey(invalid);
                success = true;
            }
            catch (chestcrypto.exceptions.InvalidDoubleKeyException){
                Console.WriteLine("Throws properly for too small array size");
            }
            if (success){
                Assert.Fail();
            }

            try{
                new chestcrypto.DoublePublicKey(invalid2);
                success = true;
            }
            catch (chestcrypto.exceptions.InvalidDoubleKeyException){
                Console.WriteLine("Throws properly for too large array size");
            }
            if (success){
                Assert.Fail();
            }
        }

        [Test]
        public void TestDoublePublicKeyLoad()
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