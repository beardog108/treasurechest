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
using CsvHelper;

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
            var temp = Path.GetTempPath();
            var publicProfile = temp + "/public.keyring.csv";
            var privateProfile = temp + "/private.keyring.csv";
            DoublePrivateKey key = new DoublePrivateKey(PublicKeyAuth.GenerateKeyPair().PrivateKey, PublicKeyBox.GenerateKeyPair().PrivateKey);
            PrivateIdentity iden = new PrivateIdentity(key, "bob");
            DoublePrivateKey key2 = new DoublePrivateKey(PublicKeyAuth.GenerateKeyPair().PrivateKey, PublicKeyBox.GenerateKeyPair().PrivateKey);
            PrivateIdentity iden2 = new PrivateIdentity(key2, "alice");
            KeyRing ring = new KeyRing();
            ring.addPrivateIdentity(iden);


            using (var writer = new StreamWriter(privateProfile))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(ring.privateIdentities);
            }
            using (var writer = new StreamWriter(publicProfile))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(ring.publicIdentities);
            }

            KeyRing restored = new RestoreKeyring(temp);

            Directory.Delete(privateProfile);
        }


    }
}