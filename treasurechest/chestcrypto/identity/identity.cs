using System.Collections.Generic;

namespace chestcrypto{

    namespace identity {
        internal class Identity {
            private DoublePrivateKey privateKey;
            private DoublePublicKey publicKey;
            private bool hasPrivate = false;

            private List<EphemeralKey> ephemeralKeys = new List<EphemeralKey>();

            public DoublePublicKey getDoublePublicKey(){return publicKey;}
            public DoublePrivateKey getDoublePrivateKey(){return privateKey;}


            public Identity(){}
            public Identity (List<EphemeralKey> ephemeralKeys){

            }
            public Identity(DoublePublicKey publicKey){
                this.publicKey = publicKey;
            }
            public Identity(DoublePrivateKey privateKey){
                this.privateKey = privateKey;
            }
            public Identity(DoublePrivateKey privateKey, List<EphemeralKey> ephemeralKeys){

            }
            public Identity(DoublePublicKey publicKey, List<EphemeralKey> ephemeralKeys){

            }


        }

    }
}