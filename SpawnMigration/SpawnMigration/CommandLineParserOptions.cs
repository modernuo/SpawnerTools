using CommandLine;

namespace SpawnMigration
{
    public class CommandLineParserOptions
    {
        [Option('m', "mode", Required = true, HelpText = "Program mode like: Migrate (migrate spawner)")]
        public string Mode { get; set; }

        [Option('n', "spawnername", HelpText = "Source spawner type name like: XMLSpawner2.")]
        public string SourceSpawnerType { get; set; }

        [Option('s', "source", HelpText = "Source path to the source file")]
        public string SourcePath { get; set; }

        [Option('t', "target", HelpText = "Target output path")]
        public string TargetPath { get; set; }

        [Option('f', "facet", HelpText = "Facet only")]
        public string Facet { get; set; }
    }
}
