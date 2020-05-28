using chestcrypto;
using chestcrypto.exceptions;
using chestcrypto.identity;
using System.Collections.Generic;

namespace keyring{

    public class KeyRing
    {
        private List<PublicIdentity> publicIdentities;
        private List<PrivateIdentity> privateIdentities;

        public KeyRing(){
            //publicIdentities = new List<PublicIdentity>();
            privateIdentities = new List<PrivateIdentity>();
        }
    }

}