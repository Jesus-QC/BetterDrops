using System.ComponentModel;
using BetterDrops.Configs;
using Exiled.API.Interfaces;

namespace BetterDrops
{
    public class PluginConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("The configs of the MTF drop waves.")]
        public DropConfig MtfDropWave { get; set; } = new DropConfig();
        [Description("The configs of the Chaos drop waves.")]
        public DropConfig ChaosDropWave { get; set; } = new DropConfig();

        [Description("The configs of random drops")]
        public RandomDropConfigs RandomDrops { get; set; } = new RandomDropConfigs();
    }
}