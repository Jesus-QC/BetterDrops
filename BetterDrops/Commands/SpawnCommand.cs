using System;
using CommandSystem;
using BetterDrops.Features.Data;
using BetterDrops.Features;
using Exiled.Permissions.Extensions;
using UnityEngine;

namespace BetterDrops.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnCommand : ICommand
    {
        public string Command { get; } = "SpawnDrop";
        public string[] Aliases { get; } = {};
        public string Description { get; } = "Spawns drops";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            /*
             * - spawndrop X Y Z
             * - spawndrop mtf
             * - spawndrop chaos
             */

            if (!sender.CheckPermission("bd.spawndrop"))
            {
                response = "You don't have permission to execute this command. Required permission: bd.spawndrop";
                return false;
            }
            
            if (arguments.Count == 1)
            {
                if (arguments.At(0).ToLower() == "mtf")
                    new Drop(Team.MTF.GetRandomDropSpawnPoint()).Spawn();
                else if (arguments.At(0).ToLower() == "chaos")
                    new Drop(Team.CHI.GetRandomDropSpawnPoint()).Spawn();
            }
            
            else if(arguments.Count > 2)
            {
                if (!float.TryParse(arguments.At(0), out var x) || !float.TryParse(arguments.At(1), out var y) || !float.TryParse(arguments.At(2), out var z))
                {
                    response = "<color=red>There was an issue parsing the spawn position.</color>";
                    return true;
                }
                
                new Drop(new Vector3(x, y, z)).Spawn();
                response = $"Done! ({x} {y} {z})";
                return true;
            }

            response = "Usage:\n- spawndrop X Y Z\n- spawndrop mtf/chaos";
            return true;
        }
    }
}