﻿using System;
using CommandSystem;

namespace BetterDrops.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class BetterDropsCommand : ParentCommand
    {
        public BetterDropsCommand() => LoadGeneratedCommands();
        
        public sealed override void LoadGeneratedCommands()
        { 
            RegisterCommand(SpawnCommand.Instance);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Please, specify a valid subcommand! Available ones: spawn";
            return false;
        }

        public override string Command { get; } = "BetterDrops";
        public override string[] Aliases { get; } = Array.Empty<string>();
        public override string Description { get; } = "BetterDrops parent command.";
    }
}