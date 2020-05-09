using NUnit.Framework;
using chestcrypto;
using System;

namespace tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestKeyGen()
        {
            byte[] key = chestcrypto.PrivateKeyGenerator.generate();
            if (key.Length != 96){
                Assert.Fail();
            }
            if (key.Equals(chestcrypto.PrivateKeyGenerator.generate())){
                Assert.Fail();
            }
            Assert.Pass();
        }

    }
}