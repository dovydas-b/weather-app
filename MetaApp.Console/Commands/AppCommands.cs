using CommandDotNet;
using MetaApp.Console.Controllers;

namespace MetaApp.Console.Commands
{
    public class AppCommands
    {
        [Subcommand(RenameAs = WeatherCommandController.CommandName)]
        public WeatherCommandController Weather { get; set; }
    }
}
