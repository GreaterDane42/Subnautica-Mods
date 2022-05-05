using System.Diagnostics;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;
using QModServices = QModManager.API.QModServices;

namespace Scan_for_More
{
    [HarmonyPatch]
    class FragmentTracker
    {
        [HarmonyPatch(typeof(ResourceTracker), nameof(ResourceTracker.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(ResourceTracker __instance)
        {
            if (__instance.techType == TechType.Fragment)
            {
                __instance.Register();
            }
        }
    }
}
