using SimpleBase;

namespace chestcrypto.identity
{
    public class PrivateIdentity{
        /*
        PrivateIdentity is a wrapper around a DoublePrivateKey providing associated metadata such as alias and note
        */
        private DoublePrivateKey key;
        public string base85Key { get {
            return getEncodedKey();
        }}
        public string name { get; set; }
        public string comment { get; set; } // human's note

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
        public string getEncodedKey(){
             return SimpleBase.Base85.Z85.Encode(key.getRawDouble());
        }
        public string getName(){return name;}
        public string getNote(){return comment;}

    }

}