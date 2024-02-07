using BepInEx;
using BepInEx.Logging;
using Nautilus.Json;
using HarmonyLib;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Scan_for_Anything
{
    [BepInPlugin(GUID, pluginName, version)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Main : BaseUnityPlugin
    {
        private const string GUID = "me.greaterdane.subnautica.mod.scanforanything";
        private const string pluginName = "Scan for Anything";
        private const string version = "1.0.4";

        internal static new ConfigFields Config { get; } = new ConfigFields();
        private static readonly Harmony harmony = new Harmony(GUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            if (File.Exists(Config.JsonFilePath))
            {
                Logger.LogInfo($"Loading {pluginName} configuration");
                Config.Load();
                Logger.LogInfo($"Loaded {pluginName} configuration!");
            }
            else
            {
                Config.InitDefaults();
                Config.Save();
                Logger.LogInfo($"Created {pluginName} configuration");
            }

            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + version + " " + "loaded.");
            logger = Logger;
        }

        [Conditional("DEBUG")]
        public static void DebugMessage(string message)
        {
            ErrorMessage.AddMessage(message);
        }
    }

    public class ConfigFields : ConfigFile
    {
        public bool trackAllFragments = true;
        public bool trackAllLife = false;
        public List<TechType> track = new List<TechType>();
        public List<TechType> exclude = new List<TechType>();
        //public Dictionary<TechType, List<TechType>> groups = new Dictionary<TechType, List<TechType>>(); // If people want to group different things together into one scanner room entry.

        public void InitDefaults()
        {
            track.Add(TechType.PurpleBrainCoral);
            track.Add(TechType.Bladderfish);
            track.Add(TechType.Peeper);
        }
    }
}
