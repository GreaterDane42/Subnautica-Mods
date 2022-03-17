// Tutorial example following https://mroshaw.github.io/Subnautica/yourfirstmod_sn
//
using HarmonyLib;
using Logger = QModManager.Utility.Logger;

namespace Knife_Damage_Example
{
    class KnifeDamage
    {
        [HarmonyPatch(typeof(PlayerTool))]
        [HarmonyPatch("Awake")]
        internal class PatchPlayerToolAwake
        {
            [HarmonyPostfix]
            public static void Postfix(PlayerTool __instance)
            {
                if (__instance.GetType() == typeof(Knife))
                {
                    var knife = __instance as Knife;
                    knife.damage *= 2;
                    Logger.Log(Logger.Level.Debug, $"Knife damage was {knife.damage / 2}, is now {knife.damage}");
                }
            }
        }
    }
}
