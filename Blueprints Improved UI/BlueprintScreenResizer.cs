﻿using System.Diagnostics;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using Logger = QModManager.Utility.Logger;
using QModServices = QModManager.API.QModServices;

namespace Blueprints_Improved_UI
{
    [HarmonyPatch]
    class BlueprintScreenResizer
    {
        [HarmonyPatch(typeof(uGUI_BlueprintEntry), "Awake")]
        [HarmonyPostfix]
        public static void Awake(uGUI_BlueprintEntry __instance)
        {
            __instance.icon.SetSize(66, 66); // Same size as inventory 1x1 item

            __instance.title.fontSize = 15; // Vanilla is 20

            var verticalLayout = __instance.GetComponent<VerticalLayoutGroup>();
            // Leave left-padding the same, otherwise the blueprint entries are justified further to the left than the group titles, which looks weird.
            verticalLayout.padding.right = 0;
            verticalLayout.padding.top = 0;
            verticalLayout.padding.bottom /= 2; // Text will directly touch the titlebar below without padding.
            
            verticalLayout.spacing = 0;
        }

        [HarmonyPatch(typeof(uGUI_BlueprintEntry), "get_minWidth")]
        [HarmonyPostfix]
        public static void get_minWidth(ref float __result)
        {
            __result /= 1.7f; // A happy-medium that doesn't wrap long words.
        }
    }
}
