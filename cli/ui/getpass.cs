using System.IO;
using System;

namespace getpass{

    public class GetPass{
        private static StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
        public static string getPass(string message){
            Console.Write(message + ": ");
            Console.SetOut(StreamWriter.Null);
            Console.SetError(StreamWriter.Null);
            message = Console.ReadLine();
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.WriteLine();
            return message;
        }

    }

}