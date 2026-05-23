using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;

namespace ToggleRingGlow
{
    public partial class ModEntry
    {
        public static bool Rings_Update_Prefix(Ring __instance, string? ___lightSourceId, GameTime time, GameLocation environment, Farmer who)
        {
            bool ifGlow = _instance.glowRings;
            if (ifGlow)
            {
                if (!environment.hasLightSource(___lightSourceId))
                {
                    __instance.onEquip(who);
                    return false;
                }
                return true;
            }
            else
            {
                if (environment.hasLightSource(___lightSourceId))
                {
                    __instance.onUnequip(who);
                }
                return false;
            }
        }
    }
}
