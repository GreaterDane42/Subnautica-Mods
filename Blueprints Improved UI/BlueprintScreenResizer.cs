using System.Diagnostics;
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
        [HarmonyPatch]
        class BlueprintEntryPatch
        {
            public static int smallerFontSize = 15; // Vanilla is 20

            [HarmonyPatch(typeof(uGUI_BlueprintEntry), "Awake")]
            [HarmonyPostfix]
            public static void Awake(uGUI_BlueprintEntry __instance)
            {
                __instance.icon.SetSize(66, 66); // Same size as inventory 1x1 item

                __instance.title.fontSize = smallerFontSize;

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


        [HarmonyPatch]
        class ProgressBarPatch
        {
            [HarmonyPatch(typeof(uGUI_BlueprintProgress), "Awake")]
            [HarmonyPostfix]
            public static void Awake(uGUI_BlueprintProgress __instance)
            {
                float barScale = 0.75f;
                int yOffset = -10; // Fixes the overlap between progress bar and blueprint text.
                
                var frame = __instance.bar.transform.parent;
                var frameGraphic = frame.GetComponent<Graphic>();
                frameGraphic.rectTransform.localPosition += new Vector3(2.5f, yOffset, 0); // Also recenter x-axis, since we pushed the elements right by leaving the entrys' left-padding alone.

                var frameRect = frameGraphic.rectTransform.rect;
                frameGraphic.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, frameRect.height * barScale);
                frameGraphic.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, frameRect.width * barScale);
                frameGraphic.rectTransform.ForceUpdateRectTransforms();

                __instance.amount.fontSize = BlueprintEntryPatch.smallerFontSize;
                __instance.amount.rectTransform.localPosition += new Vector3(4, yOffset, 0); // Also recenter x-axis
            }
        }
    }
}

// TODO: Add search bar (in a new class)
//uGUI_SignInput / uGUI_InputGroup
//signInput.Select(true)