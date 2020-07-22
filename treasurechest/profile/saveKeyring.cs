using System.IO;
using keyring;
using chestcrypto.identity;

namespace chestcrypto.profile{

    public class KeyRingSave{

        private string profileDir;
        private string privateProfile;
        private string publicProfile;

        public KeyRingSave(string profileDirectory){
            profileDir = profileDirectory;

            if (! Directory.Exists(profileDir)){
                Directory.CreateDirectory(profileDir); // Does not error if it exists already
            }
            privateProfile = profileDir + "/private.keyring.csv";
            publicProfile = profileDir + "/public.keyring.csv";
        }

        public void save(KeyRing ring){
            string header = "base85Key,name,note";


            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(privateProfile, false))
            {
                foreach(PrivateIdentity iden in ring.privateIdentities){
                    file.Write(header + "\r\n" + iden.getEncodedKey() + "," + iden.name + "," + iden.getNote() + "\r\n");
                }
            }
            
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(publicProfile, false))
            {
                foreach(PublicIdentity iden in ring.publicIdentities){
                    file.Write(header + "\r\n" + iden.getEncodedKey() + "," + iden.name + "," + iden.getNote() + "\r\n");
                }
            }

        }

    }

}