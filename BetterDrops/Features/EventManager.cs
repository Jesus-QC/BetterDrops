using MEC;
using Respawning;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using BetterDrops.Configs;
using BetterDrops.Features.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BetterDrops.Features
{
    public class EventManager
    {
        private readonly PluginConfig _config;
        public EventManager(BetterDrops plugin) => _config = plugin.Config;

        private readonly HashSet<CoroutineHandle> _coroutines = new HashSet<CoroutineHandle>();
        
        public void OnRestartingRound()
        {
            foreach (CoroutineHandle coroutine in _coroutines)
                Timing.KillCoroutines(coroutine);
            
            _coroutines.Clear();
        }

        public void OnStartingRound()
        {
            if (_config.RandomDrops.WaveSettings.IsEnabled && _coroutines.Count == 0)
                _coroutines.Add(Timing.RunCoroutine(RandomDropCoroutine(_config.RandomDrops)));
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            Team team = ev.NextKnownTeam == SpawnableTeamType.NineTailedFox ? Team.MTF : Team.CHI;
            
            if(team == Team.MTF && !_config.MtfDropWave.IsEnabled || team == Team.CHI && !_config.ChaosDropWave.IsEnabled)
                return;
            
            DropConfig cfg = team == Team.MTF ? _config.MtfDropWave : _config.ChaosDropWave;
            team.SpawnDrops(cfg, cfg.NumberOfDrops);
        }

        private static IEnumerator<float> RandomDropCoroutine(RandomDropConfigs configs)
        {
            yield return Timing.WaitForSeconds(configs.FirstRandomDropOffset);

            for (;;)
            {
                Team team = Random.Range(0, 2) == 1 ? Team.MTF : Team.CHI;
            
                team.SpawnDrops(configs.WaveSettings, configs.WaveSettings.NumberOfDrops);
                yield return Timing.WaitForSeconds(Random.Range(configs.MinRandomDropsInterval, configs.MaxRandomDropsInterval));
            }
        }
    }
}