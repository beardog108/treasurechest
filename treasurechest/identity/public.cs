namespace chestcrypto.identity
{

    public class PublicIdentity{
        /*
        PublicIdentity is a wrapper around a DoublePublicKey providing associated metadata such as alias and note
        */
        private DoublePublicKey key;
        public string base85Key { get {
            return getEncodedKey();
        }}
        public string name { get; set; }
        public string comment { get; set; } // human's note

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
        public string getEncodedKey(){
             return SimpleBase.Base85.Z85.Encode(key.getRawDouble());
        }

    }

}