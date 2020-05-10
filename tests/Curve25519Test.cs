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
        public void TestCurve25519Decrypt()
        {
            var alice = PublicKeyBox.GenerateKeyPair();
            var bob = PublicKeyBox.GenerateKeyPair();
            string message = "Hello World";
            byte[] message_bytes = UTF8Encoding.UTF8.GetBytes(message);
            byte[] nonce = Sodium.PublicKeyBox.GenerateNonce();

            byte[] encrypted =
                    Sodium.PublicKeyBox.Create(
                        message,
                        nonce,
                        alice.PrivateKey,
                        bob.PublicKey
                    );
            byte[] both = new byte[nonce.Length + encrypted.Length];
            Buffer.BlockCopy(nonce, 0, both, 0, nonce.Length);
            Buffer.BlockCopy(encrypted, 0, both, nonce.Length, encrypted.Length);
            byte[] decrypted = chestcrypto.Curve25519.decrypt(bob.PrivateKey, alice.PublicKey, both);
            string decrypted_string = Encoding.UTF8.GetString(decrypted, 0, decrypted.Length);
            if (! decrypted_string.Equals(message)){
                Assert.Fail();
            }
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

            for (int i = 24; i < encrypted_with_nonce.Length; i++){
                encrypted_without_nonce[counter] = encrypted_with_nonce[i];
                counter += 1;
            }

            for (int i = 0; i < chestcrypto.Curve25519.NONCE_BYTE_AMOUNT; i++){
                used_nonce[i] = encrypted_with_nonce[i];
            }
            for (int i = 0; i < chestcrypto.Curve25519.NONCE_BYTE_AMOUNT; i++){
                if (used_nonce[i] != encrypted_with_nonce[i]){
                    Assert.Fail();
                }
            }
            byte[] decrypted = PublicKeyBox.Open(encrypted_without_nonce, used_nonce, bob.PrivateKey, alice.PublicKey);
            if (!Encoding.UTF8.GetString(decrypted, 0, decrypted.Length).Equals(message)){
                Assert.Fail();
            }
        }
    }
}