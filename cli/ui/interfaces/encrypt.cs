using System.IO;
using System;
using System.Text;
using Sodium;
using niceware;

using chestcrypto.simplepack;
using chestcrypto.symmetric;

using treasurechest.STDIOWrapper;

namespace treasurechestCLI{

    internal class EncryptMessageInterface{
        public static void EncryptMessage(){
            int choice = 0;
            int counter = 1;
            byte[] key = new byte[32];
            byte[] message;
            string encrypted;

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
                    try {
                        message = UTF8Encoding.UTF8.GetBytes(GetMessage.getTypedMessage());
                    }
                    catch(System.NullReferenceException){
                        continue;
                    }

                    key = SecretBox.GenerateKey();
                    encrypted = SimplePack.pack(Symmetric.encrypt(message, key));
                    STDIO.O(encrypted);
                    foreach (string word in Niceware.ToPassphrase(key)){
                        Console.Write(word + " ");
                    }
                    STDIO.O("");

                }
                else if (choice == encryptMenuOptions.Length){
                    break;
                }

            }

        }

    }

}