using System.IO;

using keyring;

namespace chestcrypto.profile{

    public class RestoreKeyring{

        private string profileDir;

        public RestoreKeyring(string profileDirectory){
            profileDir = profileDirectory;

            if (! Directory.Exists(profileDir)){
                Directory.CreateDirectory(profileDir); // Does not error if it exists already
            }
        }

        private void getKeyring(){
            KeyRing keyRing = new KeyRing();
        }

    }

}