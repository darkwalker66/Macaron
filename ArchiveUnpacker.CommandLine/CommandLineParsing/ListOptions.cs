using CommandLine;

namespace ArchiveUnpacker.CommandLine.CommandLineParsing
{
    [Verb("list", HelpText = "List all files inside an archive or game folder")]
    public class ListOptions
    {
        [Value(0, MetaName = "Game directory", HelpText = "The root directory of the game", Required = true)]
        public string Directory { get; set; }
    }
}
