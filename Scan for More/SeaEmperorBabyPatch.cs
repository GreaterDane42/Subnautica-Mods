using HarmonyLib;
using UnityEngine;
using QModServices = QModManager.API.QModServices;

namespace Scan_for_More
{
    [HarmonyPatch(typeof(SeaEmperorBaby))]
    class SeaEmperorBabyPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(SeaEmperorBaby.Start))]
        public static void Start_Postfix(SeaEmperorBaby __instance)
        {
            QModServices.Main.AddCriticalMessage("Tracking SeaEmperorBaby");

            var resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();

            resourceTracker.prefabIdentifier = __instance.GetComponent<PrefabIdentifier>();
            resourceTracker.techType = TechType.SeaEmperorBaby;
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
