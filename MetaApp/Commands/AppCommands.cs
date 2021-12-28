using CommandDotNet;
using MetaApp.Controllers;

namespace MetaApp.Commands
{
    public class AppCommands
    {
        [Subcommand(RenameAs = WeatherCommandController.CommandName)]
        public WeatherCommandController Weather { get; set; }
    }
}
