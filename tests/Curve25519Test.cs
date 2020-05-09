using NUnit.Framework;
using chestcrypto;
using System;
using System.Text;
using Sodium;

namespace Curve25519Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCurve25519Encrypt()
        {
            var alice = PublicKeyBox.GenerateKeyPair();
            var bob = PublicKeyBox.GenerateKeyPair();
            string message = "Hello World";
            byte[] message_bytes = UTF8Encoding.UTF8.GetBytes(message);
            byte[] encrypted_with_nonce = chestcrypto.Curve25519.encrypt(alice.PrivateKey, bob.PublicKey, message_bytes);
            byte[] used_nonce = new byte[24];

            byte[] encrypted_without_nonce = new byte[encrypted_with_nonce.Length - 24];

            int counter = 0;
            Console.WriteLine(encrypted_without_nonce.Length);
            for (int i = 24; i < encrypted_with_nonce.Length; i++){
                //Console.WriteLine(counter);
                encrypted_without_nonce[counter] = encrypted_with_nonce[i];
                counter += 1;
            }

            for (int i = 0; i < chestcrypto.Curve25519.NONCE_BYTE_AMOUNT; i++){
                //Console.WriteLine(i);
                used_nonce[i] = encrypted_with_nonce[i];
            }
            for (int i = 0; i < chestcrypto.Curve25519.NONCE_BYTE_AMOUNT; i++){
                if (used_nonce[i] != encrypted_with_nonce[i]){
                    Assert.Fail();
                }
            }
            byte[] decrypted = PublicKeyBox.Open(encrypted_without_nonce, used_nonce, bob.PrivateKey, alice.PublicKey);
            if (!Encoding.UTF8.GetString(decrypted, 0, decrypted.Length).Equals(message)){
                Console.WriteLine(Encoding.UTF8.GetString(decrypted, 0, decrypted.Length));
                Assert.Fail();
            }
        }
    }
}