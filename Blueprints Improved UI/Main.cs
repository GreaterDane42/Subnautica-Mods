using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Blueprints_Improved_UI
{
    [BepInPlugin(GUID, pluginName, version)]
    public class Main : BaseUnityPlugin
    {
        private const string GUID = "me.greaterdane.subnautica.mod.blueprintsimprovedui";
        private const string pluginName = "Blueprints Improved UI";
        private const string version = "1.0.2";

        private static readonly Harmony harmony = new Harmony(GUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + version + " " + "loaded.");
            logger = Logger;
        }
    }
}
