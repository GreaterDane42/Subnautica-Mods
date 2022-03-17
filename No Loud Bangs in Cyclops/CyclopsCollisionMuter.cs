using System.Diagnostics;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;
using QModServices = QModManager.API.QModServices;

namespace No_Loud_Bangs_in_Cyclops
{
    [HarmonyPatch]
    class CyclopsCollisionMuter
    {
        [HarmonyPatch(typeof(SubRoot), "OnCollisionEnter")]
        [HarmonyPrefix]
        public static bool Prefix(Collision col)
        {
            bool playSound;
            

            if (col.gameObject.name == terrainObjectName)
            {
                playSound = true;
                DebugLog($"{col.gameObject.name} ({col.gameObject.tag}) collided with cyclops. Continue to OnCollisionEnter.");
            }
            else
            {
                // If an object doesn't have a LiveMixin, then it's likely a large solid object
                // such as a grand reef anchor pod, in which case we want to play an impact sound.
                // (Ideally we could filter these static decor items by mass, as we do for objects
                // with a LiveMixin, but some of their masses are illogically lower than small fish.)
                //
                LiveMixin liveMixin = col.gameObject.GetComponent<LiveMixin>();
                if (liveMixin == null)
                {
                    liveMixin = Utils.FindAncestorWithComponent<LiveMixin>(col.gameObject);
                }

                if (liveMixin == null)
                {
                    playSound = true;
                    DebugLog($"{col.gameObject.name} ({col.gameObject.tag}) collided with cyclops. No LiveMixin. Continue to OnCollisionEnter.");
                }
                else
                { 
                    // Only play the cyclops' metal hull impact sound if the object has a mass greater
                    // than the heaviest small fish.
                    //
                    Rigidbody rigidbody = Utils.FindAncestorWithComponent<Rigidbody>(col.gameObject);
                    if (rigidbody == null)
                    {
                        playSound = true;
                        DebugLog($"{col.gameObject.name} ({col.gameObject.tag}) collided with cyclops. No rigid body! Continue to OnCollisionEnter.");
                    }
                    else
                    {
                        playSound = (rigidbody.mass > spadefishMassVanilla);
                        DebugLog($"{col.gameObject.name} ({col.gameObject.tag}) collided with cyclops. Mass {rigidbody.mass} (min {spadefishMassVanilla}). Cancelling sound: {!playSound}");
                    }
                }
            }

            return playSound;
        }


        [Conditional("DEBUG")]
        private static void DebugLog(string message)
        {
            QModServices.Main.AddCriticalMessage(message);
            Logger.Log(Logger.Level.Info, message);
        }


        private const string terrainObjectName = "override collider";
        private const int spadefishMassVanilla = 35; // heaviest small fish
    }
}
