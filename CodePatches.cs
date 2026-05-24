using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Objects;
using System.Collections.Generic;

namespace ToggleRingGlow
{
    public partial class ModEntry
    {
        private static readonly HashSet<string> _lightRingItemIds = new();
        public static bool Rings_Update_Prefix(Ring __instance, string? ___lightSourceId, GameTime time, GameLocation environment, Farmer who)
        {
            if (___lightSourceId != null)
                _lightRingItemIds.Add(__instance.ItemId);
            if (___lightSourceId == null)
            {
                if (_instance.glowRings && _lightRingItemIds.Contains(__instance.ItemId))
                {
                    __instance.onEquip(who);
                    return false;
                }
                return true;
            }
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
