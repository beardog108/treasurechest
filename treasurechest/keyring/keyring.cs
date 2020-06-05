using chestcrypto.identity;
using chestcrypto.exceptions;
using System.Collections.Generic;
using System.Linq;

namespace keyring{

    public class KeyRing
    {
        public List<PublicIdentity> publicIdentities { get; set; }
        public List<PrivateIdentity> privateIdentities { get; set; }

        public KeyRing(){
            publicIdentities = new List<PublicIdentity>();
            privateIdentities = new List<PrivateIdentity>();
        }

        public void addPublicIdentity(PublicIdentity newIden){
            foreach(PublicIdentity iden in publicIdentities){
                if (Enumerable.ReferenceEquals(iden.getPublicKey(), iden)){
                    throw new DuplicateIdentityException();
                }
            }
            publicIdentities.Add(newIden);
        }
        public void addPrivateIdentity(PrivateIdentity newIden){
            foreach(PrivateIdentity iden in privateIdentities){
                if (Enumerable.ReferenceEquals(iden.getPrivateKey(), iden)){
                    throw new DuplicateIdentityException();
                }
            }
            privateIdentities.Add(newIden);
        }


    }

}