using treasurechest;

namespace treasurechestCLI {
    namespace translations {

        internal class Spanish {

            internal static void load(Strings stringInst){
                stringInst.WELCOME = treasurechest.Version.NAME + " - Protege tu valiosa información";
                stringInst.HELP_TEXT = "Ejecuta help para más opciones";
                stringInst.EXIT = "Salir de la aplicación";
                stringInst.MAIN_MENU_ENCRYPT = "Encriptar";
                stringInst.MAIN_MENU_DECRYPT = "Desencriptar";
                stringInst.INVALID_OPTION = "Opción inválida";
                stringInst.MAIN_MENU_SELECT_INTEGER = "Ingrese un número entero desde el menú";
                stringInst.RETURN_TO_PREVIOUS_MENU = "Menú anterior";
                stringInst.ENCRYPT_MENU_ENCRYPT_MESSAGE = "Cifrar texto";
                stringInst.ENCRYPT_MENU_ENCRYPT_FILE = "Cifrar archivo";
                stringInst.ENCRYPT_MENU_USE_PASSPHRASE = "Usar frase de contraseña";
                stringInst.ENCRYPT_MENU_USE_PUBKEY = "Usar clave pública";
                stringInst.ENTER_MESSAGE_UNTIL_DONE = "Ingrese su mensaje y termine con -q en una nueva línea.";
                stringInst.PASSPHRASE = "Frase de contraseña";
            }

        }

    }
}