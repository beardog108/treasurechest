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

namespace testProfileSave
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSaveMany(){
            var temp = Path.GetTempPath();
            List<PublicIdentity> pub = new List<PublicIdentity>();
            List<PrivateIdentity> priva = new List<PrivateIdentity>();
            KeyRing ring = new KeyRing();
            DoublePublicKey key;
            DoublePrivateKey privKey;
            PublicIdentity iden;
            PrivateIdentity priv;

            var publicProfile = temp + "/public.keyring.csv";
            var privateProfile = temp + "/private.keyring.csv";
            string[] names = {"bob", "kevin", "sam", "joe", "sarah sara"};
            for (int i = 0; i < 5; i++){
                key = new DoublePublicKey(PublicKeyAuth.GenerateKeyPair().PublicKey, PublicKeyBox.GenerateKeyPair().PublicKey);
                iden = new PublicIdentity(key, names[i]);
                pub.Add(iden);
                ring.addPublicIdentity(iden);
            }

            for (int i = 0; i < 5; i++){
                privKey = new DoublePrivateKey(PublicKeyAuth.GenerateKeyPair().PrivateKey, PublicKeyBox.GenerateKeyPair().PrivateKey);
                priv = new PrivateIdentity(privKey, names[i]);
                priva.Add(priv);
                ring.addPrivateIdentity(priv);
            }

            KeyRingSave saver = new KeyRingSave(temp);
            saver.save(ring);

            string data = System.IO.File.ReadAllText(publicProfile);

            foreach(string name in names){
                Assert.IsTrue(data.Contains(name));
            }
            foreach(PublicIdentity id in pub){
                if (! data.Contains(id.getEncodedKey())){
                    Assert.Fail();
                }
                if (! names.Contains(id.getName())){
                    Assert.Fail();
                }
            }
            data = System.IO.File.ReadAllText(privateProfile);
            foreach(PrivateIdentity id in priva){
                if (! data.Contains(id.getEncodedKey())){
                    Assert.Fail();
                }
                if (! names.Contains(id.getName())){
                    Assert.Fail();
                }
            }
            File.Delete(privateProfile);
            File.Delete(publicProfile);
        }
    }
}