using chestcrypto;

namespace chestcrypto.identity
{

    public class PublicIdentity{
        /*
        PublicIdentity is a wrapper around a DoublePublicKey providing associated metadata such as alias and note
        */

        private DoublePublicKey key;
        private string name;
        private string comment; // human's note

        public PublicIdentity(DoublePublicKey doublePublicKey, string alias){
            key = doublePublicKey;
            name = alias;
            comment = "";
        }

        public PublicIdentity(DoublePublicKey doublePublicKey, string alias, string note){
            key = doublePublicKey;
            name = alias;
            comment = note;
        }

        public DoublePublicKey getPublicKey(){return key;}
        public string getName(){return name;}
        public string getNote(){return comment;}


    }

}