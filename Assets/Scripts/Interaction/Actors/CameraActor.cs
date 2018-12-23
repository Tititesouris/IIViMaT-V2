using System.Linq;
using Interaction.Actions;
using UnityEngine;

namespace Interaction.Actors
{
    public class CameraActor : Actor
    {
        [Header("Proximity")] [Tooltip("If enabled, the actor will trigger proximity actions.")]
        public bool triggerProximityActions = true;

        [Tooltip("The actor will not trigger proximity actions on object that are further away.")]
        public float maxProximityRange = 10f;


        [Header("Gaze")] [Tooltip("If enabled, the actor will trigger gaze actions.")]
        public bool triggerGazeActions = true;

        [Tooltip("If enabled, the actor will trigger gaze actions in 360 spheres.")]
        public bool triggerGaze360Actions = true;

        [Tooltip("The actor will not trigger gaze actions on object that are further away.")]
        public float maxGazeRange = 50f;

        [Tooltip("If enabled, the gaze will go through objects. If disabled, it will stop when hitting an object.")]
        public bool goThroughObjects = true;
        
        // TODO: Option: Go through 360 spheres

        [Tooltip("If set to 1, the gaze will only trigger reactions on the first object with reactions in its path." +
                 "If set to another number, the gaze will trigger reactions on up to that number of object with reactions in its path.")]
        [Range(1, 10)]
        public int nbObjectsToTrigger = 1;
        
        private static int _interactableLayerMask;
        
        private void Awake()
        {
            _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");
        }

        private void Update()
        {
            if (triggerProximityActions)
                ProximityTriggers();
            if (triggerGazeActions)
                GazeTriggers();
            if (triggerGaze360Actions)
                Gaze360Triggers();
        }

        private bool ProximityTriggers()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, maxProximityRange, _interactableLayerMask);
            foreach (var hitCollider in hitColliders)
            {
                var hitObject = hitCollider.gameObject;
                var actions = hitObject.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var proximityAction = action as ProximityAction;
                    if (proximityAction != null) proximityAction.Trigger(this);
                }
            }

            return hitColliders.Length > 0;
        }

        private bool GazeTriggers()
        {
            var hits = Physics.RaycastAll(transform.position, transform.forward, maxGazeRange, _interactableLayerMask);
            hits = hits.OrderBy(hit => hit.distance).ToArray();
            if (hits.Length > 0)
            {
                var nbTriggered = 0;
                foreach (var hit in hits)
                {
                    var hitObject = hit.transform.gameObject;
                    var actions = hitObject.GetComponents<Action>();
                    foreach (var action in actions)
                    {
                        var gazeAction = action as GazeAction;
                        if (gazeAction != null)
                        {
                            gazeAction.Trigger(this, hit);
                            nbTriggered++;
                        }
                    }

                    if (!goThroughObjects || nbObjectsToTrigger <= nbTriggered)
                        return true;
                }

                return nbTriggered > 0;
            }

            return false;
        }

        private bool Gaze360Triggers()
        {
            // Rays don't hit the object they come from inside of. Solution:
            // Make ray come from outside towards actor. Take the last hit object, it will be the 360 sphere.
            var hits = Physics.RaycastAll(transform.position + transform.forward * maxGazeRange, -transform.forward,
                maxGazeRange, _interactableLayerMask);
            if (hits.Length > 0)
            {
                var hit = hits.OrderBy(x => x.distance).Last();
                var hitObject = hit.transform.gameObject;
                var actions = hitObject.GetComponents<Action>();
                foreach (var action in actions)
                {
                    /*var gazeAction = action as GazeAction;
                    if (gazeAction != null)
                    {
                        gazeAction.Trigger(this, hit);
                    }*/
                }

                //return nbTriggered > 0;
            }

            return false;
        }
    }
}