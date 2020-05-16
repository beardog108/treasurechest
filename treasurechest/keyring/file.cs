using chestcrypto;

namespace keyring{

    internal class KeyRingFile
    {
        private string storageFile = null;

        internal KeyRingFile(string filePath){
            storageFile = filePath;
        }

        internal void write(byte[] data){
            
        }
    }

}