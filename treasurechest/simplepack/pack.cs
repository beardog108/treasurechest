using Base58Check;

namespace chestcrypto{

    namespace simplepack{

        public class SimplePack{

            private const string header = "CHEST-MESSAGE ";
            private const string footer = " END-CHEST-MESSAGE.";

            // Test simplepackTest.TestPackUnpackBytes
            public static string pack(byte[] data){
                return header + Base58CheckEncoding.Encode(data) + footer;
            }
            // Test simplepackTest.TestPackUnpackString
            public static string pack(string data){
                return pack(System.Text.Encoding.UTF8.GetBytes(data));
            }
            // Test simplepackTest.TestPackUnpackBytes
            public static byte[] unpack(string checkedBase58String){
                if (! checkedBase58String.Contains(header) | ! checkedBase58String.Contains(footer)){
                    throw new exceptions.InvalidSimplePackMessage("Message does not have valid header and footer");
                }
                string encodedMessage = "";
                for (int i = header.Length; i < checkedBase58String.Length - footer.Length; i++){
                    encodedMessage += checkedBase58String[i];
                }
                return Base58CheckEncoding.Decode(encodedMessage);

            }

        }

    }

}