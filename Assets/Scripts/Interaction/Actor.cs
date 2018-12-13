using Interaction.Actions;
using UnityEngine;

namespace Interaction
{
    public class Actor : MonoBehaviour
    {

        [Header("Gaze")] [Tooltip("If enabled, the actor will trigger gaze actions.")]
        public bool GazeAction = true;

        [Tooltip("The actor will not trigger gaze actions on object that are further away.")]
        public float MaxGazeRange = 50f;


        [Header("Proximity")] [Tooltip("If enabled, the actor will trigger proximity actions.")]
        public bool ProximityAction = true;

        [Tooltip("The actor will not trigger proximity actions on object that are further away.")]
        public float MaxProximityRange = 10f;


        private static int _interactableLayerMask;

        private void Awake()
        {
            _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");
        }

        private void Update()
        {
            if (GazeAction)
                Gaze();
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
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, MaxGazeRange, _interactableLayerMask))
            {
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