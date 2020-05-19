using chestcrypto;
using chestcrypto.identity;
using chestcrypto.exceptions;
using System.Collections.Generic;

namespace keyring{

    public class KeyRing
    {
        private string storageFile = null;
        private List<Identity> identities = new List<Identity>();

        private bool identityExists(Identity iden){
            bool success = false;
            identities.ForEach(delegate(Identity ident)
            {
                if (ident.getDoublePublicKey().Equals(iden.getDoublePublicKey())){
                    success = true;
                    return;
                }
            });
            return success;
        }

        private int getIdentityListPosition(Identity iden){
            int counter = 0;
            bool success = false;
            identities.ForEach(delegate(Identity ident)
            {
                if (ident.getDoublePublicKey().Equals(iden.getDoublePublicKey())){
                    success = true;
                    return;
                }
                counter += 1;
            });
            if (success){
                return counter;
            }
            throw new NoIdentityException();
        }

        public KeyRing(string storageFile){

        }
        public KeyRing(){}

        public int getIdentityCount(){return identities.Count;}

        public int getIdentityInstance(DoublePublicKey){

        }

        public List<byte[]> getIdentityPublicKeys(){
            List<byte[]> pubKeys = new List<byte[]>();
            identities.ForEach(delegate(Identity identity){
                pubKeys.Add(identity.getDoublePublicKey().getRawDouble());
            });
            return pubKeys;
        }

        public void addPublicKey(DoublePublicKey key){
            // Create an Identity with a public key if it does not exist already


            Identity newIdentity = new Identity(key);
            if (identityExists(newIdentity)){
                throw new DuplicateIdentityException("An identity with that public key already exists");
            }

            identities.Add(newIdentity);

        }

        public void removeIdentityByPubkey(DoublePublicKey key){

        }
    }

}