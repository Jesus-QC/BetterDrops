using System;
using BetterDrops.Features.Extensions;
using CommandSystem;

namespace BetterDrops.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ChaosCommand : ICommand
    {
        public static ChaosCommand Instance { get; } = new ChaosCommand();
        
        public string Command { get; } = "chaos";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Team.CHI.SpawnDrops(BetterDrops.PluginConfig.ChaosDropWave, BetterDrops.PluginConfig.ChaosDropWave.NumberOfDrops);

            response = "Spawned!";
            return true;
        }
    }
}