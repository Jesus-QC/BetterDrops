using System;
using System.Collections.Generic;
using BetterDrops.Configs;
using BetterDrops.Features.Components;
using Exiled.API.Features;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BetterDrops.Features.Extensions
{
    public static class DropExtensions
    {
        public static void SpawnDrops(this Team team, DropConfig config, uint numberOfDrops)
        {
            for(int i = 0; i < numberOfDrops; i++)
                team.SpawnDrop(config);
        }

        private static void SpawnDrop(this Team team, DropConfig config)
        {
            Vector3 spawnPosition = team == Team.TUT ? (Random.Range(0, 2) == 1 ? Team.MTF : Team.CHI).GetRandomSpawnPosition() : team.GetRandomSpawnPosition();
            SpawnDrop(spawnPosition, ParseColor(config.Color), GetRandomItem(config.PossibleItems, config.ItemsPerDrop));
        }

        public static void SpawnDrop(Vector3 position, Color color, IEnumerable<ItemType> items)
        {
            GameObject go = new GameObject("DROP")
            { transform = { position = position } };
            go.AddComponent<DropController>().Init(color, items);
        }
        
        private static Vector3 GetRandomSpawnPosition(this Team team)
        {
            switch (team)
            {
                case Team.CHI:
                    return new Vector3(Random.Range(46.5f, -20), 1000 + Random.Range(25f, 73f), Random.Range(-51, -64.5f));
                case Team.MTF:
                    return new Vector3(Random.Range(151.5f, 192), 1000 + Random.Range(25f, 73f), Random.Range(-70, -47.5f));
                default:
                    throw new IndexOutOfRangeException("Index out of range. Only MTF and CHI can get a random spawn position.");
            }
        }

        private static Color ParseColor(string color) => ColorUtility.TryParseHtmlString(color, out Color c) ? c : Random.ColorHSV();

        private static IEnumerable<ItemType> GetRandomItem(IReadOnlyList<ItemType> items, uint amount)
        {
            List<ItemType> ret = new List<ItemType>();

            for (int i = 0; i < amount; i++)
            {
                ret.Add(items[Random.Range(0, items.Count)]);
            }

            return ret;
        }
    }
}