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
            System.Console.Write(chestcrypto.PrivateKeyGenerator.generate().Length);
            if (chestcrypto.PrivateKeyGenerator.generate().Length != 96){
                Assert.Fail();
            }
            Assert.Pass();
        }
    }
}