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
        public override Version Version { get; } = new Version(2, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);
        
        private EventManager _eventManager;

        public static PluginConfig PluginConfig;
        
        public override void OnEnabled()
        {
            Exiled.API.Features.Server.IsHeavilyModded = true;
            
            _eventManager = new EventManager();

            Server.RestartingRound += _eventManager.OnRestartingRound;
            Server.RespawningTeam += _eventManager.OnRespawningTeam;
            Server.RoundStarted += _eventManager.OnStartingRound;

            Physics.IgnoreLayerCollision(16, 6, true);

            PluginConfig = Config;
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Physics.IgnoreLayerCollision(16, 6, false);

            Server.RestartingRound -= _eventManager.OnRestartingRound;
            Server.RespawningTeam -= _eventManager.OnRespawningTeam;
            Server.RoundStarted -= _eventManager.OnStartingRound;

            PluginConfig = null;
            
            _eventManager = null;

            base.OnDisabled();
        }
    }
}