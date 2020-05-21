using System;
using CommandLine;
using treasurechest.STDIOWrapper;
using treasurechest;

namespace treasurechestCLI
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
                STDIO.O(treasurechest.Version.NAME + " - " + treasurechest.Version.VERSION);
            }
            else if (opts.Menu){
                new TreasureChestMenu().showMenu();
            }
            else{
                command = false;
            }

            if (! command){
                STDIO.O(treasurechest.Version.NAME + " - " + treasurechest.Version.VERSION);
                STDIO.O(new translations.Strings().HELP_TEXT);
            }

        }
    }
    public class Options
    {
        [Option('i', "interactive", Required = false, HelpText = "Interactive STDIO usage")]
        public bool Interactive { get; set; }
        [Option('v', "version", Required = false, HelpText="Show current version")]
        public bool Version {get; set;}
        [Option('m', "menu", Required = false, HelpText="Interactive CLI menu")]
        public bool Menu {get;set;}

    }
}
