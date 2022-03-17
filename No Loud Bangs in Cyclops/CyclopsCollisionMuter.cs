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
            string logMessage = $"{col.gameObject.name} ({col.gameObject.tag}) collided with cyclops.";

            if (col.gameObject.name == terrainObjectName)
            {
                playSound = true;
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
                    logMessage += " No LiveMixin.";
                }
                else
                { 
                    // Only play the cyclops' metal hull impact sound if the object has a mass larger
                    // than the heaviest small fish.
                    //
                    Rigidbody rigidbody = Utils.FindAncestorWithComponent<Rigidbody>(col.gameObject);
                    if (rigidbody == null)
                    {
                        playSound = true;
                        logMessage += " No rigid body!";
                    }
                    else
                    {
                        playSound = (rigidbody.mass > spadefishMassVanilla);
                        logMessage += $" Mass {rigidbody.mass} (min {spadefishMassVanilla}).";
                    }
                }
            }

            logMessage += playSound ? " Continue." : " Cancelling sound!";
            QModServices.Main.AddCriticalMessage(logMessage);
            Logger.Log(Logger.Level.Info, logMessage);

            return playSound;
        }

        private readonly static string terrainObjectName = "override collider";
        private readonly static int spadefishMassVanilla = 35; // heaviest small fish
    }
}
