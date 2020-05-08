using System;
using CommandLine;
using treasurechest.STDIOWrapper;

namespace treasurechest
{
    class Program
    {

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions);
        }
        static void RunOptions(Options opts)
        {
            bool command = true;

            if (opts.Version){
                STDIO.O(Version.NAME + " - " + Version.VERSION);
            }
            else{
                command = false;
            }

            if (! command){
                STDIO.O(Version.NAME + " - " + Version.VERSION);
                STDIO.O("Run with help for more options");
            }

        }
    }
    public class Options
    {
        [Option('i', "interactive", Required = false, HelpText = "Interactive STDIO usage")]
        public bool Interactive { get; set; }
        [Option('v', "version", Required = false, HelpText="Show current version")]
        public bool Version {get; set;}

    }
}
