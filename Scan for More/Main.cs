using System.IO;
using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;
using SMLHelper.V2.Json;
using System.Collections.Generic;

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
    }

    public class ConfigFields : ConfigFile
    {
        public List<TechType> LiveMixins = new List<TechType>();

        public void InitDefaults()
        {
            LiveMixins.Add(TechType.SeaCrown);
        }
    }
}
