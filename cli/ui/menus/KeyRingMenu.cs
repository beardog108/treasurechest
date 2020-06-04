using System;
using treasurechestCLI;
using treasurechest.STDIOWrapper;

namespace treasurechestCLI
{


    internal class KeyRingMenu{

        public KeyRingMenu(){
            int choice = 1;
            translations.Strings strings = new translations.Strings();
            string[] options = {strings.ADD_IDENTITY,
                                strings.CREATE_IDENTITY,
                                strings.EXPORT_IDENTITY,
                                strings.RETURN_TO_PREVIOUS_MENU
            };
            while (true){
                for (int i = 0; i < options.Length; i++){
                    STDIO.O((i + 1).ToString() + ". " + options[i]);
                }
                try{
                    choice = Int32.Parse(System.Console.ReadLine());
                    if (choice >= options.Length) throw new System.OverflowException();
                }
                catch (System.OverflowException){
                    // User being silly with input
                    STDIO.O(strings.MAIN_MENU_SELECT_INTEGER);
                }
                catch(System.FormatException){
                    // Too lazy to check strings, force them to use int from menu which is faster anyway
                    STDIO.O(strings.MAIN_MENU_SELECT_INTEGER);
                }
                catch(System.ArgumentNullException){
                    // Can happen when stream closes (e.g. ctrl-d)
                    // since menu is intended to be directly human interfaced, user probably wants to exit
                    choice = options.Length;
                }
                switch(choice){
                    case 1:
                    
                    break;
                    case 2:
                    break;
                    case 3:
                        goto breakLoop;
                }
            }
            breakLoop:
                return;
        }

    }


}