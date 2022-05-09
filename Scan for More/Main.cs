using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using SMLHelper.V2.Json;
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;
using QModServices = QModManager.API.QModServices;

namespace Scan_for_More
{
    [QModCore]
    public class Main
    {
        internal static ConfigFields Config { get; } = new ConfigFields();

        [QModPatch]
        public static void Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var modName = ($"GreaterDane42_{assembly.GetName().Name}");

            if (File.Exists(Config.JsonFilePath))
            {
                Logger.Log(Logger.Level.Info, $"Loading {modName} configuration");
                Config.Load();
            }
            else
            {
                Config.InitDefaults();
            }

            Logger.Log(Logger.Level.Info, $"Patching {modName}");
            Harmony.CreateAndPatchAll(assembly, modName);

            Logger.Log(Logger.Level.Info, "Patched successfully!");

            Config.Save();
            Logger.Log(Logger.Level.Info, $"Saved {modName} configuration");
        }

        [Conditional("DEBUG")]
        public static void DebugMessage(string message)
        {
            QModServices.Main.AddCriticalMessage(message);
        }
    }

    public class ConfigFields : ConfigFile
    {
        public bool trackAllFragments = true;
        public bool trackEverythingWithHealth = false;
        public List<TechType> track = new List<TechType>();
        public List<TechType> exclude = new List<TechType>();
        //public Dictionary<TechType, List<TechType>> groups = new Dictionary<TechType, List<TechType>>();

        public void InitDefaults()
        {
            track.Add(TechType.PurpleBrainCoral);
            track.Add(TechType.Bladderfish);
            track.Add(TechType.Peeper);
            track.Add(TechType.SeaCrown);
        }
    }
}
