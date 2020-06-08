using NUnit.Framework;
using chestcrypto.identity;
using chestcrypto;
using chestcrypto.profile;
using keyring;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Sodium;
using SimpleBase;

namespace testProfileLoad
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLoadMany(){
            string header = "base85Key,name,note";
            var temp = Path.GetTempPath();
            List<PublicIdentity> pub = new List<PublicIdentity>();
            KeyRing ring = new KeyRing();
            DoublePublicKey key;
            PublicIdentity iden;
            var publicProfile = temp + "/public.keyring.csv";
            var privateProfile = temp + "/private.keyring.csv";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(publicProfile, false))
            {
                file.Write(header + "\r\n");
            }

            for (int i = 0; i < 10; i++){
                key = new DoublePublicKey(PublicKeyAuth.GenerateKeyPair().PublicKey, PublicKeyBox.GenerateKeyPair().PublicKey);
                iden = new PublicIdentity(key, "test");
                ring.addPublicIdentity(iden);
                pub.Add(iden);
                using (StreamWriter w = File.AppendText(publicProfile))
                {
                    w.WriteLine(iden.getEncodedKey() + "," + iden.name + "," + iden.getNote() + "\r\n");
                }
            }
            KeyRing restored = new RestoreKeyring(temp).getKeyring();

            Assert.IsTrue(restored.publicIdentities.Count > 0);
            bool ran = false;
            foreach(PublicIdentity id in restored.publicIdentities){
                ran = false;
                foreach(PublicIdentity existing in pub){
                    if (Enumerable.SequenceEqual(id.getPublicKey().getRawDouble(), existing.getPublicKey().getRawDouble())){
                        ran = true;
                        break;
                    }
                }
                Assert.IsTrue(ran);
            }
            File.Delete(publicProfile);
        }

        [Test]
        public void TestLoadSingleEntries(){
            var temp = Path.GetTempPath();
            var publicProfile = temp + "/public.keyring.csv";
            var privateProfile = temp + "/private.keyring.csv";
            DoublePrivateKey key = new DoublePrivateKey(PublicKeyAuth.GenerateKeyPair().PrivateKey, PublicKeyBox.GenerateKeyPair().PrivateKey);
            PrivateIdentity iden = new PrivateIdentity(key, "bob");
            DoublePublicKey key2 = new DoublePublicKey(PublicKeyAuth.GenerateKeyPair().PublicKey, PublicKeyBox.GenerateKeyPair().PublicKey);
            PublicIdentity iden2 = new PublicIdentity(key2, "alice");
            KeyRing ring = new KeyRing();
            ring.addPrivateIdentity(iden);
            ring.addPublicIdentity(iden2);

            string header = "base85Key,name,note";

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(privateProfile, false))
            {
                file.Write(header + "\r\n" + iden.getEncodedKey() + "," + iden.name + "," + iden.getNote() + "\r\n");
            }

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(publicProfile, false))
            {
                file.Write(header + "\r\n" + iden2.getEncodedKey() + "," + iden2.name + "," + iden2.getNote() + "\r\n");
            }


            KeyRing restored = new RestoreKeyring(temp).getKeyring();
            Assert.IsTrue(restored.privateIdentities.Count > 0);
            Assert.IsTrue(restored.publicIdentities.Count > 0);
            foreach(PrivateIdentity id in restored.privateIdentities){
                Assert.IsTrue(Enumerable.SequenceEqual(id.getPrivateKey().getRawDouble(), iden.getPrivateKey().getRawDouble()));
            }
            foreach(PublicIdentity id in restored.publicIdentities){
                Assert.IsTrue(Enumerable.SequenceEqual(id.getPublicKey().getRawDouble(), iden2.getPublicKey().getRawDouble()));
            }

            File.Delete(privateProfile);
            File.Delete(publicProfile);

        }


    }
}