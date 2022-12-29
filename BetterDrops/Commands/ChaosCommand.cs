using System;
using BetterDrops.Features.Extensions;
using CommandSystem;
using Exiled.Permissions.Extensions;
using PlayerRoles;

namespace BetterDrops.Commands
{
    public class ChaosCommand : ICommand
    {
        public static ChaosCommand Instance { get; } = new ChaosCommand();
        
        public string Command { get; } = "chaos";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("bd.spawn"))
            {
                response = "You don't have perms to do that!";
                return false;
            }
            
            Team.ChaosInsurgency.SpawnDrops(BetterDrops.PluginConfig.ChaosDropWave, BetterDrops.PluginConfig.ChaosDropWave.NumberOfDrops);

            response = "Spawned!";
            return true;
        }
    }
}