using System;
using BetterDrops.Features;
using Exiled.API.Features;
using UnityEngine;

using Server = Exiled.Events.Handlers.Server;

namespace BetterDrops
{
    public class BetterDrops : Plugin<PluginConfig>
    {
        public override string Author { get; } = "Jesus-QC";
        public override string Name { get; } = "BetterDrops";
        public override string Prefix { get; } = "better_drops";
        public override Version Version { get; } = new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(4, 1, 7);

        public static PluginConfig Cfg;
        public static EventManager EventManager;
        
        public override void OnEnabled()
        {
            Cfg = Config;

            EventManager = new EventManager();

            Server.RestartingRound += EventManager.OnRestartingRound;
            Server.RespawningTeam += EventManager.OnRespawningTeam;
            Server.RoundStarted += EventManager.OnStartingRound;

            Physics.IgnoreLayerCollision(Config.DropLayer, 16); // Invisible barriers

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Physics.IgnoreLayerCollision(Config.DropLayer, 16, false);

            Server.RestartingRound -= EventManager.OnRestartingRound;
            Server.RespawningTeam -= EventManager.OnRespawningTeam;
            Server.RoundStarted -= EventManager.OnStartingRound;
            
            EventManager = null;
            
            Cfg = null;
            
            base.OnDisabled();
        }
    }
}