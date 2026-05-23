using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace ToggleRingGlow
{
    public sealed class ModConfig
    {
        public KeybindList ToggleKey { get; set; } = KeybindList.ForSingle(SButton.N);
    }
}
