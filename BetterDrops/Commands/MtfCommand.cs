using System;
using BetterDrops.Features.Extensions;
using CommandSystem;

namespace BetterDrops.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class MtfCommand : ICommand
    {
        public static MtfCommand Instance { get; } = new MtfCommand();
        
        public string Command { get; } = "mtf";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Team.MTF.SpawnDrops(BetterDrops.PluginConfig.MtfDropWave, BetterDrops.PluginConfig.MtfDropWave.NumberOfDrops);

            response = "Spawned!";
            return true;
        }
    }
}