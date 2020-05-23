using System;
using treasurechest.STDIOWrapper;
using treasurechest;

namespace treasurechestCLI
{

    internal class EncryptMenu{

        public static void enterMenu(translations.Strings strings) {
            int choice = 0;
            int counter = 1;
            string[] encryptMenuOptions =
            {
                strings.ENCRYPT_MENU_ENCRYPT_MESSAGE,
                strings.ENCRYPT_MENU_ENCRYPT_FILE,
                strings.RETURN_TO_PREVIOUS_MENU,
                strings.EXIT
            };

            while (true){
                foreach (string option in encryptMenuOptions){
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
                    EncryptMessageInterface.EncryptMessage();
                }
                else if (choice == encryptMenuOptions.Length - 1){
                    return;
                }
                else if (choice == encryptMenuOptions.Length){
                    System.Environment.Exit(0);
                }
                counter = 1;
            }

        }

    }


}