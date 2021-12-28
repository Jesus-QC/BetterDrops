using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BetterDrops
{
    public class PluginConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        [Description("The Unity Layer that the drop will use to handle collisions (Recommendation: dont touch this!)")]
        public int DropLayer { get; set; } = 6;
        
        [Description("The possible items inside the drop")]
        public List<ItemType> PossibleItems { get; set; } = new List<ItemType> {ItemType.Coal, ItemType.Coin, ItemType.GunCrossvec, ItemType.GunLogicer, ItemType.GunRevolver, ItemType.GunShotgun, ItemType.GunAK, ItemType.GunCOM15, ItemType.GunE11SR, ItemType.Ammo9x19, ItemType.Ammo12gauge, ItemType.Adrenaline, ItemType.Flashlight, ItemType.Medkit, ItemType.GrenadeFlash, ItemType.GrenadeHE};

        [Description("Should drops spawn on MTFs spawns")]
        public bool MtfDrops { get; set; } = true;
        [Description("Should drops spawn on Chaos spawns")]
        public bool ChaosDrops { get; set; } = true;
        [Description("Should drops spawn randomly")]
        public bool RandomDrops { get; set; } = true;

        [Description("The minimum time that has to happen until the first random drop")] 
        public ushort FirstRandomDropOffset { get; set; } = 120;
        [Description("Minimum time between random drops")]
        public ushort MinRandomDropsInterval { get; set; } = 120;
        [Description("Maximum time between random drops")]
        public ushort MaxRandomDropsInterval { get; set; } = 240;

        [Description("The number of drops spawned in each spawn (TUT = Random)")]
        public Dictionary<Team, ushort> NumberOfDrops { get; set; } = new Dictionary<Team, ushort> {{Team.MTF, 2}, {Team.CHI, 3}, {Team.TUT, 1}};
    }
}