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
            bool makeSound;
            string logMessage = $"{col.gameObject.name} ({col.gameObject.tag}) collided with cyclops.";

            if (col.gameObject.name == terrainObjectName)
            {
                makeSound = true;
            }
            else
            {
                Rigidbody rigidbody = Utils.FindAncestorWithComponent<Rigidbody>(col.gameObject);
                if (rigidbody == null)
                {
                    makeSound = true;
                    logMessage += " Rigid body is NULL!";
                }
                else
                {
                    makeSound = (rigidbody.mass > spadefishMassVanilla);
                    logMessage += $" Mass {rigidbody.mass} (min {spadefishMassVanilla}).";
                }
            }

            logMessage += makeSound ? " Continue." : " Cancelling sound!";
            //QModServices.Main.AddCriticalMessage(logMessage);
            Logger.Log(Logger.Level.Info, logMessage);

            return makeSound;
        }

        private readonly static string terrainObjectName = "override collider";
        private readonly static int spadefishMassVanilla = 35; // heaviest small fish
    }
}
