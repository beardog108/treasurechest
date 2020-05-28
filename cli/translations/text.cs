using System.Globalization;
using treasurechest;
namespace treasurechestCLI {
    namespace translations {

        internal class GetLanguage{
            internal static string language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        }

        public class Strings{
            public string WELCOME;
            public string HELP_TEXT;
            public string EXIT;
            public string MAIN_MENU_ENCRYPT;
            public string MAIN_MENU_DECRYPT;
            public string MAIN_MENU_KEYRING;
            public string INVALID_OPTION;
            public string MAIN_MENU_SELECT_INTEGER;
            public string RETURN_TO_PREVIOUS_MENU;
            public string ENCRYPT_MENU_ENCRYPT_MESSAGE;
            public string ENCRYPT_MENU_ENCRYPT_FILE;
            public string ENCRYPT_MENU_USE_PASSPHRASE;
            public string ENCRYPT_MENU_USE_PUBKEY;
            public string ENTER_MESSAGE_UNTIL_DONE;
            public string PASSPHRASE;


            public Strings(){

                switch (GetLanguage.language){
                    case "es":
                        Spanish.load(this);
                    break;
                    case "en":
                    default:
                        WELCOME = treasurechest.Version.NAME + " - Protect your treasured information";
                        HELP_TEXT = "Run with help for more options";
                        EXIT = "Exit application";
                        MAIN_MENU_ENCRYPT = "Encrypt";
                        MAIN_MENU_DECRYPT = "Decrypt";
                        MAIN_MENU_KEYRING = "Manage contacts";
                        INVALID_OPTION = "Invalid option";
                        MAIN_MENU_SELECT_INTEGER = "Enter an integer from the menu";
                        RETURN_TO_PREVIOUS_MENU = "Previous menu";
                        ENCRYPT_MENU_ENCRYPT_MESSAGE = "Encrypt text";
                        ENCRYPT_MENU_ENCRYPT_FILE = "Encrypt file";
                        ENCRYPT_MENU_USE_PASSPHRASE = "Use mnemonic";
                        ENCRYPT_MENU_USE_PUBKEY = "Use public key";
                        ENTER_MESSAGE_UNTIL_DONE = "Enter your message and finish with -q on a new line.";
                        PASSPHRASE = "Passphrase";
                    break;
                }

            }

        }

    }
}