using System.IO;
using System;
using System.Text;
using Sodium;
using niceware;

using simplepack;
using chestcrypto.symmetric;

using treasurechest.STDIOWrapper;

namespace treasurechestCLI{

    internal class EncryptMessageInterface{
        private static void EncryptWithMnemonic(){
            byte[] key = new byte[32]; // Key has to be 32 bytes in size
            byte[] message; // Plaintext
            string encrypted; // Ciphertext will be encoded with SimplePack.
            try {
                message = UTF8Encoding.UTF8.GetBytes(GetMessage.getTypedMessage());
            }
            catch(System.NullReferenceException){
                return;
            }
            SimplePack packer = new SimplePack("treasure chest-message ", " end treasure chest message.");

            key = SecretBox.GenerateKey();
            encrypted = packer.encode(Symmetric.encrypt(message, key));
            STDIO.O(encrypted);
            foreach (string word in Niceware.ToPassphrase(key)){
                Console.Write(word + " ");
            }
            STDIO.O("");
        }

        public static void EncryptMessage(){
            int choice = 0;
            int counter = 1;

            translations.Strings strings = new translations.Strings();

            string[] encryptMenuOptions =
            {
                strings.ENCRYPT_MENU_USE_PASSPHRASE,
                strings.ENCRYPT_MENU_USE_PUBKEY,
                strings.RETURN_TO_PREVIOUS_MENU
            };
            while(true){
                counter = 1;
                foreach(string option in encryptMenuOptions){
                    STDIO.O(counter.ToString() + ". " + option);
                    counter += 1;
                }
                try{
                    choice = Int32.Parse(System.Console.ReadLine());
                }
                catch (System.OverflowException){
                    // User being silly with input
                    STDIO.O(strings.MAIN_MENU_SELECT_INTEGER);
                    counter = 1;
                }
                catch(System.FormatException){
                    // Too lazy to check strings, force them to use int from menu which is faster anyway
                    STDIO.O(strings.MAIN_MENU_SELECT_INTEGER);
                    counter = 1;
                }
                catch(System.ArgumentNullException){
                    // Can happen when stream closes (e.g. ctrl-d)
                    // since menu is intended to be directly human interfaced, user probably wants to exit
                    choice = encryptMenuOptions.Length;
                }
                if (choice == 1){
                    EncryptWithMnemonic();
                }
                else if (choice == 2){

                }
                else if (choice == 3){
                    break;
                }

            }

        }

    }

}