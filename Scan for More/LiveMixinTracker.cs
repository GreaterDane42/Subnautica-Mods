using System.Diagnostics;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;
using QModServices = QModManager.API.QModServices;

namespace Scan_for_More
{
    [HarmonyPatch(typeof(LiveMixin))]
    class LiveMixinTracker
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(LiveMixin.Awake))]
        public static void Awake_Postfix(LiveMixin __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);

            if (Main.Config.LiveMixins.Contains(techType))
            {
                var resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();

                if (resourceTracker != null)
                {
                    resourceTracker.prefabIdentifier = __instance.GetComponent<PrefabIdentifier>();
                    resourceTracker.techType = techType;
                    resourceTracker.overrideTechType = techType;
                    resourceTracker.rb = __instance.gameObject.GetComponent<Rigidbody>();
                    resourceTracker.pickupable = __instance.gameObject.GetComponent<Pickupable>();

                    DebugLog("Tracking " + techType);
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LiveMixin.Kill))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Hook must match arguments.")]
        public static void Kill_Postfix(LiveMixin __instance, DamageType damageType)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);

            if (Main.Config.LiveMixins.Contains(techType))
            {
                var resourceTracker = __instance.gameObject.GetComponent<ResourceTracker>();
                if (resourceTracker != null)
                {
                    resourceTracker.Unregister();

                    DebugLog("Untracking " + techType);
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
