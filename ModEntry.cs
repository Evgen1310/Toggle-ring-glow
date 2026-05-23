using GenericModConfigMenu;
using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.Objects;

namespace ToggleRingGlow
{
    public partial class ModEntry : Mod
    {
        private static ModEntry _instance;
        private ModConfig Config;
        private bool glowRings = true;

        public override void Entry(IModHelper helper)
        {
            _instance = this;
            this.Config = this.Helper.ReadConfig<ModConfig>();

            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;

            var harmony = new Harmony(ModManifest.UniqueID);
            harmony.Patch(
              original: AccessTools.Method(typeof(Ring), nameof(Ring.update)),
              prefix: new HarmonyMethod(typeof(ModEntry), nameof(Rings_Update_Prefix))
            );
        }

        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (this.Config.ToggleKey.JustPressed())
            {
                this.glowRings = !glowRings;
            }
        }

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            configMenu.AddKeybindList(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("settings_name"),
                tooltip: () => this.Helper.Translation.Get("settings_tooltip"),
                getValue: () => this.Config.ToggleKey,
                setValue: value => this.Config.ToggleKey = value
            );
        }
    }
}