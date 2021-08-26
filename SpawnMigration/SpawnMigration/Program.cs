using CommandLine;
using SpawnMigration.Framework.XMLSpawner2;
using System;
using System.Collections.Generic;

namespace SpawnMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<CommandLineParserOptions>(args)
              .WithParsed(RunOptions)
              .WithNotParsed(HandleParseError);
        }
        static void RunOptions(CommandLineParserOptions opts)
        {
            //handle options
            switch (Enum.Parse(typeof(Mode), opts.Mode, true))
            {
                case Mode.None:
                    break;
                case Mode.Migrate:
                    Console.WriteLine("geht");
                    break;
                default:
                    break;
            }
            
            
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}
