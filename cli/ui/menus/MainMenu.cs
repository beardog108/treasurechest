using System;
using treasurechest.STDIOWrapper;
using treasurechest;

namespace treasurechestCLI
{

    internal class TreasureChestMenu{

        private string language;

        public TreasureChestMenu(){
            language = translations.GetLanguage.language;
        }

        internal void showMenu(){
            translations.Strings strings = new translations.Strings();
            string[] mainMenuOptions = {strings.MAIN_MENU_ENCRYPT, strings.MAIN_MENU_DECRYPT, strings.EXIT};
            STDIO.O(strings.WELCOME);
            int counter = 1;
            int choice = 0;
            int mainMenuOptionsSize = mainMenuOptions.Length;

            while (choice != mainMenuOptions.Length){
                foreach (string option in mainMenuOptions){
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
                    choice = mainMenuOptionsSize;
                }
                if (choice == 1){
                    EncryptMenu.enterMenu(strings);
                }
                else if (choice == mainMenuOptionsSize){
                    // Exit is final option
                    break;
                }
                else{
                    STDIO.O(strings.INVALID_OPTION);
                }
                counter = 1;
            }
        }

    }

}