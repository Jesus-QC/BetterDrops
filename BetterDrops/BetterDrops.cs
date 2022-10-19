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
        public override Version Version { get; } = new Version(2, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);
        
        private EventManager _eventManager;      
        
        public override void OnEnabled()
        {
            Exiled.API.Features.Server.IsHeavilyModded = true;
            
            _eventManager = new EventManager(this);

            Server.RestartingRound += _eventManager.OnRestartingRound;
            Server.RespawningTeam += _eventManager.OnRespawningTeam;
            Server.RoundStarted += _eventManager.OnStartingRound;

            Physics.IgnoreLayerCollision(16, 6, true);
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Physics.IgnoreLayerCollision(16, 6, false);

            Server.RestartingRound -= _eventManager.OnRestartingRound;
            Server.RespawningTeam -= _eventManager.OnRespawningTeam;
            Server.RoundStarted -= _eventManager.OnStartingRound;
            
            _eventManager = null;

            base.OnDisabled();
        }
    }
}