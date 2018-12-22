using Interaction.Actions;
using UnityEngine;

namespace Interaction
{
    public class Actor : MonoBehaviour
    {
        private static int _interactableLayerMask;

        [Tooltip("If enabled, the actor will trigger gaze actions in 360 spheres.")]
        public bool Gaze360Action = true;

        [Header("Gaze")] [Tooltip("If enabled, the actor will trigger gaze actions.")]
        public bool GazeAction = true;

        [Tooltip("If enabled, the gaze will go through objects. If disabled, it will stop when hitting an object.")]
        public bool GoThroughObjects = true;

        [Tooltip("The actor will not trigger gaze actions on object that are further away.")]
        public float MaxGazeRange = 50f;

        [Tooltip("The actor will not trigger proximity actions on object that are further away.")]
        public float MaxProximityRange = 10f;

        [Tooltip("If set to 1, the gaze will only trigger reactions on the first object with reactions in its path." +
                 "If set to another number, the gaze will trigger reactions on up to that number of object with reactions in its path.")]
        [Range(1, 10)]
        public int NbObjectsToTrigger = 1;


        [Header("Proximity")] [Tooltip("If enabled, the actor will trigger proximity actions.")]
        public bool ProximityAction = true;

        private void Awake()
        {
            _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");
        }

        private void Update()
        {
            if (GazeAction)
                Gaze();
            if (Gaze360Action)
                Gaze360();
            if (ProximityAction)
                Proximity();
        }

        private bool Proximity()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, MaxProximityRange, _interactableLayerMask);
            foreach (var hitCollider in hitColliders)
            {
                var hitObject = hitCollider.gameObject;
                var actions = hitObject.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var proximityAction = action as ProximityAction;
                    if (proximityAction != null)
                    {
                        proximityAction.Trigger(this);
                    }
                }
            }

            return hitColliders.Length > 0;
        }

        private bool Gaze()
        {
            var hits = new RaycastHit[NbObjectsToTrigger];
            if (Physics.RaycastNonAlloc(transform.position, transform.forward, hits, MaxGazeRange,
                    _interactableLayerMask) > 0)
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

                    if (!GoThroughObjects || NbObjectsToTrigger <= nbTriggered)
                        return true;
                }

                return true;
            }

            return false;
        }

        private bool Gaze360()
        {
            // Rays don't hit the object they come from inside of. Solution:
            // Make ray come from outside towards actor. Take the last hit object, it will be the 360 sphere.
            var hits = Physics.RaycastAll(transform.position + transform.forward * MaxGazeRange, -transform.forward,
                MaxGazeRange, _interactableLayerMask);
            if (hits.Length > 0)
            {
                var hit = hits[hits.Length - 1];
                var hitObject = hit.transform.gameObject;
                var actions = hitObject.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var gazeAction = action as GazeAction;
                    if (gazeAction != null)
                    {
                        gazeAction.Trigger(this, hit);
                    }
                }

                return true;
            }

            return false;
        }
    }
}