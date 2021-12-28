using UnityEngine;

namespace BetterDrops.Features
{
    public static class DropExtensions
    {
        public static Vector3 GetRandomDropSpawnPoint(this Team team)
        {
            switch (team)
            {
                case Team.CHI:
                    return new Vector3(Random.Range(46.5f, -20), 1000 + Random.Range(25f, 73f), Random.Range(-51, -64.5f));
                case Team.MTF:
                    return new Vector3(Random.Range(151.5f, 192), 1000 + Random.Range(25f, 73f), Random.Range(-70, -47.5f));
                default:
                    return Vector3.zero;
            }
        }
    }
}