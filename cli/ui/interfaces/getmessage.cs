using treasurechest.STDIOWrapper;
using System;

namespace treasurechestCLI{


    internal class GetMessage{

        internal static string getTypedMessage(){

            translations.Strings strings = new translations.Strings();
            string message = "";
            string line = "";
            STDIO.O(strings.ENTER_MESSAGE_UNTIL_DONE);
            while (true){
                line = Console.ReadLine();
                if (line.Equals("-q")){
                    break;
                }
                message += line;
            }
            return message;
        }

    }


}