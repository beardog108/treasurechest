using NUnit.Framework;
using chestcrypto.symmetric;
using System;
using System.Text;
using System.Linq;
using Sodium;

namespace SymmetricTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDecrypt()
        {
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello world");
            byte[] message2 = UTF8Encoding.UTF8.GetBytes("Hello worl2");
            int nonceSize = 24;
            byte[] nonce = SecretBox.GenerateNonce();
            byte[] key = SecretBox.GenerateKey();
            byte[] encrypted = SecretBox.Create(message, nonce, key);
            byte[] both = new byte[nonceSize + encrypted.Length];
            Buffer.BlockCopy(nonce, 0, both, 0, nonce.Length);
            Buffer.BlockCopy(encrypted, 0, both, nonce.Length, encrypted.Length);
            Assert.IsTrue(both.Length >= message.Length + 24);
            Assert.IsTrue(Enumerable.SequenceEqual(Symmetric.decrypt(both, key), message));
            Assert.IsFalse(Enumerable.SequenceEqual(Symmetric.decrypt(both, key), message2));

        }

        [Test]
        public void TestEncryptBytesNoKey()
        {
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello world");
            byte[] message2 = UTF8Encoding.UTF8.GetBytes("Hello worl2");
            (byte[] encrypted, byte[] key) = Symmetric.encrypt(message);
            int nonceSize = 24;
            byte[] justCiphertext = new byte[encrypted.Length - nonceSize];

            byte[] nonce = new byte[nonceSize];
            int counter = 0;
            for (int i = 0; i < encrypted.Length; i++){
                if (i < nonceSize){
                    nonce[i] = encrypted[i];
                    continue;
                    }
                justCiphertext[counter] = encrypted[i];
                counter += 1;
            }
            Assert.IsTrue(Enumerable.SequenceEqual(SecretBox.Open(justCiphertext, nonce, key), message));
            Assert.IsFalse(Enumerable.SequenceEqual(SecretBox.Open(justCiphertext, nonce, key), message2));
        }

        [Test]
        public void TestEncryptBytes()
        {
            byte[] message = UTF8Encoding.UTF8.GetBytes("Hello world");
            byte[] message2 = UTF8Encoding.UTF8.GetBytes("Hello worl2");
            byte[] key = SecretBox.GenerateKey();
            byte[] encrypted = Symmetric.encrypt(message, key);
            int nonceSize = 24;
            byte[] justCiphertext = new byte[encrypted.Length - nonceSize];

            byte[] nonce = new byte[nonceSize];
            int counter = 0;
            for (int i = 0; i < encrypted.Length; i++){
                if (i < nonceSize){
                    nonce[i] = encrypted[i];
                    continue;
                    }
                justCiphertext[counter] = encrypted[i];
                counter += 1;

            }
            Assert.IsTrue(Enumerable.SequenceEqual(SecretBox.Open(justCiphertext, nonce, key), message));
            Assert.IsFalse(Enumerable.SequenceEqual(SecretBox.Open(justCiphertext, nonce, key), message2));

        }

    }
}