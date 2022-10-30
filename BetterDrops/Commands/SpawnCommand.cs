using System;
using BetterDrops.Features.Extensions;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BetterDrops.Commands
{
    public class SpawnCommand : ICommand
    {
        public static SpawnCommand Instance { get; } = new SpawnCommand();
        
        public string Command { get; } = "spawn";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("bd.spawn"))
            {
                response = "You don't have perms to do that!";
                return false;
            }
            
            DropExtensions.SpawnDrop(Player.Get(sender).Position + Vector3.up * 10f, Random.ColorHSV(), new [] { ItemType.Coin });

            response = "Spawned!";
            return true;
        }
    }
}