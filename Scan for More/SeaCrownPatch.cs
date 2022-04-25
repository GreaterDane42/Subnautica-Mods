using System.Diagnostics;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;
using QModServices = QModManager.API.QModServices;

namespace Scan_for_More
{
    [HarmonyPatch(typeof(LiveMixin))]
    class SeaCrownPatch
    {
        // TODO: Only enable after TechType.HatchingEnzymes is unlocked?

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LiveMixin.Awake))]
        public static void Awake_Postfix(LiveMixin __instance)
        {
            if (__instance.name == "Coral_reef_sea_crown(Clone)")
            {
                var resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();

                if (resourceTracker != null)
                {
                    resourceTracker.prefabIdentifier = __instance.GetComponent<PrefabIdentifier>();
                    resourceTracker.techType = TechType.SeaCrown;
                    resourceTracker.overrideTechType = TechType.SeaCrown;
                    resourceTracker.rb = __instance.gameObject.GetComponent<Rigidbody>();

                    DebugLog("Tracking sea crown");
                }
                else
                {
                    DebugLog("Failed to track sea crown: could not create ResourceTracker!");
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LiveMixin.Kill))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Hook must match arguments.")]
        public static void OnDestroy_Postfix(LiveMixin __instance, DamageType damageType)
        {
            if (__instance.name == "Coral_reef_sea_crown(Clone)")
            {
                var resourceTracker = __instance.gameObject.GetComponent<ResourceTracker>();
                if (resourceTracker != null)
                {
                    resourceTracker.Unregister();

                    DebugLog("Untracking sea crown");
                }
            }
        }

        [Conditional("DEBUG")]
        private static void DebugLog(string message)
        {
            QModServices.Main.AddCriticalMessage(message);
            Logger.Log(Logger.Level.Info, message);
        }
    }
}
