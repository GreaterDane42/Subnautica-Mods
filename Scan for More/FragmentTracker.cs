
using HarmonyLib;
using UnityEngine;

namespace Scan_for_Everything
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
                if (ShouldTrack(__instance))
                {
                    __instance.Register();

                    Main.DebugMessage("Tracking " + __instance.name);
                }
            }
        }

        private static bool ShouldTrack(ResourceTracker __instance)
        {
            if (Main.Config.trackAllFragments && Main.Config.exclude.Count == 0)
            {
                return true;
            }

            TechType techType = GetSpecificTechType(__instance);

            if (Main.Config.trackAllFragments)
            {
                return !Main.Config.exclude.Contains(techType);
            }
            else
            {
                return Main.Config.track.Contains(techType);
            }
        }

        private static TechType GetSpecificTechType(ResourceTracker __instance)
        {
            TechType specificTechType = TechType.Fragment;
            var pickupable = __instance.GetComponent<Pickupable>();
            if (pickupable != null)
            {
                specificTechType = pickupable.GetTechType();
            }
            else
            {
                var techTag = __instance.GetComponent<TechTag>();
                if (techTag != null)
                {
                    specificTechType = techTag.type;
                }
            }
            return specificTechType;
        }
    }
}
