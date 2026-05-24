using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;

namespace ToggleRingGlow
{
    public partial class ModEntry
    {
        public static bool Rings_Update_Prefix(Ring __instance, string? ___lightSourceId, GameTime time, GameLocation environment, Farmer who)
        {
            var fields = AccessTools.GetFieldNames(typeof(Ring));
            if (_instance.glowRings)
            {
                if (___lightSourceId is null)
                {
                    __instance.onEquip(who);
                }
                return true;
            }
            else
            {
                if (___lightSourceId is not null)
                {
                    __instance.onUnequip(who);
                }
                return false;
            }
        }
    }
}
