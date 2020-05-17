using System.Collections.Generic;

namespace chestcrypto{

    namespace identity {
        internal class Identity {
            private DoublePrivateKey privateKey;
            private DoublePublicKey publicKey;

            private List<EphemeralKey> ephemeralKeys = new List<EphemeralKey>();

            public DoublePublicKey getDoublePublicKey(){return publicKey;}


            public Identity(){}
            public Identity (List<EphemeralKey> ephemeralKeys){

            }
            public Identity(DoublePublicKey publicKey){
                this.publicKey = publicKey;
            }
            public Identity(DoublePrivateKey privateKey){

            }
            public Identity(DoublePrivateKey privateKey, List<EphemeralKey> ephemeralKeys){

            }
            public Identity(DoublePublicKey publicKey, List<EphemeralKey> ephemeralKeys){

            }


        }

    }
}