using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using static VFXParticlesPool;

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
                RectOffset formerPadding = verticalLayout.padding;
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


    [HarmonyPatch]
    class PinPatch
    {
        readonly static Vector3 scale = new Vector3(0.62f, 0.62f, 1f);
        readonly static Vector2 offset = new Vector2(0.5f, -6f);

        [HarmonyPatch(typeof(uGUI_BlueprintsTab), "UpdatePinHover")]
        [HarmonyPostfix]
        public static void UpdatePinHover(uGUI_BlueprintsTab __instance, uGUI_BlueprintEntry entry)
        {
            if (entry == null || entry.pin != null) return;

            __instance.pinHover.localScale = scale;
            __instance.pinHover.offsetMin += offset; // must add, the on-hover pin icon already changes with offset
            __instance.pinHover.offsetMax += offset; //
        }

        [HarmonyPatch(typeof(uGUI_BlueprintsTab), "SetPin")]
        [HarmonyPostfix]
        public static void SetPin(uGUI_BlueprintEntry entry, bool state)
        {
            if (state)
            {
                entry.pin.localScale = scale;
                entry.pin.offsetMin = offset;
                entry.pin.offsetMax = offset;
            }
        }
    }
}

// TODO: Add search bar (in a new class)
//uGUI_SignInput / uGUI_InputGroup
//signInput.Select(true)