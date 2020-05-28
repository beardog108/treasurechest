using chestcrypto;

namespace chestcrypto.identity
{

    public class PrivateIdentity{
        /*
        PrivateIdentity is a wrapper around a DoublePrivateKey providing associated metadata such as alias and note
        */

        private DoublePrivateKey key;
        private string name;
        private string comment; // human's note

        public PrivateIdentity(DoublePrivateKey doublePrivateKey, string alias){
            key = doublePrivateKey;
            name = alias;
            comment = "";
        }

        public PrivateIdentity(DoublePrivateKey doublePrivateKey, string alias, string note){
            key = doublePrivateKey;
            name = alias;
            comment = note;
        }

        public DoublePrivateKey getPrivateKey(){return key;}
        public string getName(){return name;}
        public string getNote(){return comment;}


    }

}