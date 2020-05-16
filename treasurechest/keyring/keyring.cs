using chestcrypto;
using chestcrypto.identity;
using System.Collections.Generic;

namespace keyring{

    public class KeyRing
    {
        private string storageFile = null;
        private List<Identity> identities = new List<Identity>();

        public KeyRing(string storageFile){

        }
        public KeyRing(){}

        public List<byte[]> getIdentityPublicKeys(){
            List<byte[]> pubKeys;
            identities.ForEach(delegate(Identity identity){
                pubKeys.Add(identity.getDoublePublicKey().getRawDouble());
            });
        }

        public void addPublicKey(DoublePublicKey key){
            // Create an Identity with a public key if it does not exist already


            Identity newIdentity = new Identity(key);


        }

        public void deletePublicKey(DoublePublicKey key){

        }
    }

}