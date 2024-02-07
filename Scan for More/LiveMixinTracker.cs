using System.Diagnostics;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace Scan_for_Anything
{
    [HarmonyPatch(typeof(LiveMixin))]
    class LiveMixinTracker
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(LiveMixin.Awake))]
        public static void Awake_Postfix(LiveMixin __instance)
        {
            TechType techType = CraftData.GetTechType(__instance.gameObject);

            if (ShouldTrack(__instance, techType))
            {
                var resourceTracker = __instance.gameObject.EnsureComponent<ResourceTracker>();

                if (resourceTracker != null)
                {
                    resourceTracker.techType = techType;
                    resourceTracker.overrideTechType = techType;
                    resourceTracker.prefabIdentifier = __instance.GetComponent<PrefabIdentifier>();
                    resourceTracker.pickupable = __instance.gameObject.GetComponent<Pickupable>();
                    resourceTracker.rb = __instance.gameObject.GetComponent<Rigidbody>();

                    Main.DebugMessage("Tracking " + techType);
                }
            }
        }

        private static bool ShouldTrack(LiveMixin __instance, TechType techType)
        {
            if (techType == TechType.None)
            {
                return false;
            }

            if (Main.Config.trackAllLife)
            {
                bool isPlayerCreated = (__instance.GetComponent<EnergyMixin>() != null
                    || __instance.GetComponent<Constructable>() != null);
                bool isSchool = (__instance.GetComponentInChildren<VFXSchoolFish>() != null);
                bool isEgg = (__instance.GetComponent<CreatureEgg>() != null);

                if (isPlayerCreated || isSchool || isEgg 
                    || techType == TechType.TreeMushroom // mushroom forest stuff not worth tracking
                    || techType == TechType.BallClusters //
                    || techType == TechType.BarnacleSuckers
                    || techType == TechType.BlueBarnacle
                    || techType == TechType.BlueCluster
                    || techType == TechType.BlueCoralTubes)
                {
                    return Main.Config.track.Contains(techType);
                }
                else
                {
                    return !Main.Config.exclude.Contains(techType);
                }
            }
            else
            {
                return Main.Config.track.Contains(techType);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(LiveMixin.Kill))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Hook must match arguments.")]
        public static void Kill_Postfix(LiveMixin __instance, DamageType damageType)
        {
            var resourceTracker = __instance.gameObject.GetComponent<ResourceTracker>();
            if (resourceTracker != null)
            {
                resourceTracker.Unregister();

                Main.DebugMessage("Untracking " + __instance.name);
            }
        }
    }
}
