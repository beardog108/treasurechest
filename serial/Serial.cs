using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace serial
{
    class Serial
    {
        private static SerialCommands.Commands extractCommand(string inp){
            int[] cmd = new int[4];
            int newInt;
            int counter = 0;
            foreach (char c in inp.ToCharArray()){
                try{
                    newInt = Int32.Parse(c.ToString());
                    cmd[counter] = newInt;
                    counter += 1;
                }
                catch (System.FormatException){
                    break;
                }
                catch (System.IndexOutOfRangeException){
                    return SerialCommands.Commands.UnknownCommand;
                }
            }
            return (SerialCommands.Commands) cmd.Sum();
        }
        public static string[] extractArgs(string inp, int skip){
            try{
                inp = inp.Substring(skip);
            }
            catch(System.ArgumentOutOfRangeException){
                return new string[0];
            }
            return inp.Split(' ');
        }

        static void Main(string[] cliArgs)
        {
            TextWriter errorWriter = Console.Error;
            string input;
            SerialCommands.Commands cmd;
            string cmdAndArgs;
            string[] args;
            int cmdDigitCount;

            while (true){
                cmd = SerialCommands.Commands.UnknownCommand;
                while (cmd == SerialCommands.Commands.UnknownCommand){
                    cmdAndArgs = Console.ReadLine();
                    if (cmdAndArgs == null){
                        cmd = SerialCommands.Commands.Exit;
                        break;
                    }
                    cmd = extractCommand(cmdAndArgs);
                    if (cmd == SerialCommands.Commands.Exit){return;}
                    else if (cmd == SerialCommands.Commands.UnknownCommand){break;}
                    cmdDigitCount = cmd.ToString().Length;
                    args = extractArgs(cmdAndArgs, cmdDigitCount);
                }
                switch(cmd){
                    case SerialCommands.Commands.Exit:
                    return;
                    default:
                    case SerialCommands.Commands.UnknownCommand:
                        errorWriter.Write("Unknown command");
                    break;
                }

            }
        }
    }
}
