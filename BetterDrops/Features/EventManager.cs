using MEC;
using Respawning;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using BetterDrops.Configs;
using BetterDrops.Features.Extensions;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using Random = UnityEngine.Random;

namespace BetterDrops.Features
{
    public class EventManager
    {
        private readonly HashSet<CoroutineHandle> _coroutines = new HashSet<CoroutineHandle>();
        
        public void OnRestartingRound()
        {
            foreach (CoroutineHandle coroutine in _coroutines)
                Timing.KillCoroutines(coroutine);
            
            _coroutines.Clear();
        }

        public void OnStartingRound()
        {
            if (BetterDrops.PluginConfig.RandomDrops.WaveSettings.IsEnabled && _coroutines.Count == 0)
                _coroutines.Add(Timing.RunCoroutine(RandomDropCoroutine(BetterDrops.PluginConfig.RandomDrops)));
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            Team team = ev.NextKnownTeam == SpawnableTeamType.NineTailedFox ? Team.FoundationForces : Team.ChaosInsurgency;
            
            if(team == Team.FoundationForces && !BetterDrops.PluginConfig.MtfDropWave.IsEnabled || team == Team.ChaosInsurgency && !BetterDrops.PluginConfig.ChaosDropWave.IsEnabled)
                return;
            
            DropConfig cfg = team == Team.FoundationForces ? BetterDrops.PluginConfig.MtfDropWave : BetterDrops.PluginConfig.ChaosDropWave;
            team.SpawnDrops(cfg, cfg.NumberOfDrops);
        }

        private static IEnumerator<float> RandomDropCoroutine(RandomDropConfigs configs)
        {
            yield return Timing.WaitForSeconds(configs.FirstRandomDropOffset);

            for (;;)
            {
                Team team = Random.Range(0, 2) == 1 ? Team.FoundationForces : Team.ChaosInsurgency;
            
                team.SpawnDrops(configs.WaveSettings, configs.WaveSettings.NumberOfDrops);
                yield return Timing.WaitForSeconds(Random.Range(configs.MinRandomDropsInterval, configs.MaxRandomDropsInterval));
            }
        }
    }
}