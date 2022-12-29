using System;
using BetterDrops.Features.Extensions;
using CommandSystem;
using Exiled.Permissions.Extensions;
using PlayerRoles;

namespace BetterDrops.Commands
{
    public class MtfCommand : ICommand
    {
        public static MtfCommand Instance { get; } = new MtfCommand();
        
        public string Command { get; } = "mtf";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("bd.spawn"))
            {
                response = "You don't have perms to do that!";
                return false;
            }
            
            Team.FoundationForces.SpawnDrops(BetterDrops.PluginConfig.MtfDropWave, BetterDrops.PluginConfig.MtfDropWave.NumberOfDrops);

            response = "Spawned!";
            return true;
        }
    }
}