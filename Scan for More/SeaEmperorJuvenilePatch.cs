using HarmonyLib;
using UnityEngine;
using QModServices = QModManager.API.QModServices;

namespace Scan_for_More
{
    [HarmonyPatch(typeof(SeaEmperorJuvenile))]
    class SeaEmperorJuvenilePatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(SeaEmperorJuvenile.Start))]
        public static void Start_Postfix(SeaEmperorJuvenile __instance)
        {
            QModServices.Main.AddCriticalMessage("Tracking SeaEmperorJuvenile");

            var resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();

            resourceTracker.prefabIdentifier = __instance.GetComponent<PrefabIdentifier>();
            resourceTracker.techType = TechType.SeaEmperorJuvenile;
            resourceTracker.overrideTechType = TechType.SeaEmperor;
            resourceTracker.rb = __instance.gameObject.GetComponent<Rigidbody>();
        }

        /*
        [HarmonyPostfix]
        [HarmonyPatch(nameof(SeaEmperorJuvenile.OnKill))]
        public static void OnKill_Postfix(SeaEmperorJuvenile __instance)
        {
            // You monster, how could you?!?!
            var resourceTracker = __instance.gameObject.GetComponent<ResourceTracker>();
            if (resourceTracker != null)
            {
                resourceTracker.Unregister();
            }
        }
        */
    }
}
