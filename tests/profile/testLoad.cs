using NUnit.Framework;
using chestcrypto.identity;
using chestcrypto;
using chestcrypto.profile;
using System;
using System.IO;
using System.Linq;

namespace testProfileLoad
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLoad(){
            RestoreKeyring restore = new RestoreKeyring("test.profile");
        }


    }
}