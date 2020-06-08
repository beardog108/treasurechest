using System.IO;
using System.Collections.Generic;
using System;
using keyring;
using chestcrypto.identity;

namespace chestcrypto.profile{

    public class RestoreKeyring{

        private string profileDir;

        public RestoreKeyring(string profileDirectory){
            profileDir = profileDirectory;

            if (! Directory.Exists(profileDir)){
                Directory.CreateDirectory(profileDir); // Does not error if it exists already
            }

        }

        private PrivateIdentity getPrivateIdentityFromLine(string data){
            var parts = data.Split(',');
            int counter = 0;

            DoublePrivateKey key = null;
            string alias = "";
            string note = "";

            foreach (string part in parts){
                switch(counter){
                    case 0:
                        key = new DoublePrivateKey(SimpleBase.Base85.Z85.Decode(part).ToArray());
                    break;
                    case 1:
                        alias = part;
                    break;
                    case 2:
                        note = part;
                    break;
                    default:
                        throw new InvalidDataException();
                }
                counter += 1;
            }
            return new PrivateIdentity(key, alias, note);
        }
        private PublicIdentity getPublicIdentityFromLine(string data){
            var parts = data.Split(',');
            int counter = 0;

            DoublePublicKey key = null;
            string alias = "";
            string note = "";

            foreach (string part in parts){
                switch(counter){
                    case 0:
                        key = new DoublePublicKey(SimpleBase.Base85.Z85.Decode(part).ToArray());
                    break;
                    case 1:
                        alias = part;
                    break;
                    case 2:
                        note = part;
                    break;
                    default:
                        throw new InvalidDataException();
                }
                counter += 1;
            }
            return new PublicIdentity(key, alias, note);
        }
        public KeyRing getKeyring(){
            KeyRing keyRing = new KeyRing();
            string[] lines;
            bool first;
            try{
                lines = System.IO.File.ReadAllLines(profileDir + "/private.keyring.csv");
                first = true;
                foreach (string line in lines){
                    if (first){
                        first = false;
                        continue;
                    }
                    if (line.Length <= 1){
                        continue;
                    }
                    keyRing.addPrivateIdentity(getPrivateIdentityFromLine(line));
                }
            }
            catch(System.IO.FileNotFoundException){}
            try{
                lines = System.IO.File.ReadAllLines(profileDir + "/public.keyring.csv");
                first = true;
                foreach (string line in lines){
                    if (first){
                        first = false;
                        continue;
                    }
                    if (line.Length <= 1){
                        continue;
                    }
                    keyRing.addPublicIdentity(getPublicIdentityFromLine(line));
                }
            }
            catch(System.IO.FileNotFoundException){}

            return keyRing;
        }

    }

}