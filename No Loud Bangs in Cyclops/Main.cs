using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace No_Loud_Bangs_in_Cyclops
{
    [BepInPlugin(GUID, pluginName, version)]
    public class Main : BaseUnityPlugin
    {
        private const string GUID = "me.greaterdane.subnautica.mod.noloudbangsincyclops";
        private const string pluginName = "No Loud Bangs in Cyclops";
        private const string version = "1.1.0";

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
