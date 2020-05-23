using NUnit.Framework;
using chestcrypto;
using System;

namespace simplepackTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPackUnpackString()
        {
            string message = "hello world";
            string packed;
            packed = chestcrypto.simplepack.SimplePack.pack(message);
            Assert.AreEqual(message, chestcrypto.simplepack.SimplePack.unpack(packed));
        }
        [Test]
        public void TestPackUnpackBytes()
        {
            byte[] message = System.Text.Encoding.UTF8.GetBytes("hello world");
            string packed;
            packed = chestcrypto.simplepack.SimplePack.pack(message);
            Assert.AreEqual(message, chestcrypto.simplepack.SimplePack.unpack(packed));
        }
        [Test]
        public void TestPackUnpackInvalid(){
            byte[] message = System.Text.Encoding.UTF8.GetBytes("hello world");
            string packed;
            packed = chestcrypto.simplepack.SimplePack.pack(message).Remove(1);
            bool success = false;
            try{
                chestcrypto.simplepack.SimplePack.unpack(packed);
            }
            catch(chestcrypto.exceptions.InvalidSimplePackMessage){
                success = true;
            }
            if (! success){
                Assert.Fail();
            }
        }


    }
}