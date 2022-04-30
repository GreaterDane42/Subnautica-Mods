using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;

namespace Blueprints_Improved_UI
{
    [QModCore]
    public class Main
    {
        [QModPatch]
        public static void Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var modName = ($"GreaterDane42_{assembly.GetName().Name}");

            Logger.Log(Logger.Level.Info, $"Patching {modName}");
            Harmony.CreateAndPatchAll(assembly, modName);

            Logger.Log(Logger.Level.Info, "Patched successfully!");
        }
    }
}
