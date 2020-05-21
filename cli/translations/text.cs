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
            public string INVALID_OPTION;
            public string MAIN_MENU_SELECT_INTEGER;


            public Strings(){

                switch (GetLanguage.language){
                    case "es":
                        WELCOME = treasurechest.Version.NAME + " - Protege tu valiosa información";
                        HELP_TEXT = "Ejecuta help para más opciones";
                        EXIT = "Salida";
                        MAIN_MENU_ENCRYPT = "Encriptar";
                        MAIN_MENU_DECRYPT = "Desencriptar";
                        INVALID_OPTION = "Opción inválida";
                        MAIN_MENU_SELECT_INTEGER = "Ingrese un número entero desde el menú";

                    break;
                    case "en":
                    default:
                        WELCOME = treasurechest.Version.NAME + " - Protect your treasured information";
                        HELP_TEXT = "Run with help for more options";
                        EXIT = "Exit";
                        MAIN_MENU_ENCRYPT = "Encrypt";
                        MAIN_MENU_DECRYPT = "Decrypt";
                        INVALID_OPTION = "Invalid option";
                        MAIN_MENU_SELECT_INTEGER = "Enter an integer from the menu";
                    break;
                }

            }

        }

    }
}