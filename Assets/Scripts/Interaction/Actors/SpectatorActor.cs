using System.Collections.Generic;
using System.Linq;
using Interaction.Actions;
using Interaction.Actions.Spectator;
using UnityEditor;
using UnityEngine;

namespace Interaction.Actors
{
    public class SpectatorActor : Actor
    {
        public enum GazeThroughObjectOptions
        {
            Never,
            UntilTriggered,
            Always
        }

        [Tooltip("The actor will not trigger actions on objects that are further away.")]
        public float interactionReach = 100f;

        // TODO: Bug - Gaze is not stopped by the object the spectator is inside of because the collider only detects hits from the outside. This is a problem for 360 spheres.
        [Tooltip("Specify if the gaze goes through object and how:" +
                 "Never: Stops at the first object" +
                 "UntilTriggered: Stops at the first object triggered" +
                 "Always: Goes through every object and triggers it if possible")]
        public GazeThroughObjectOptions gazeThroughObjects;

        private static int _interactableLayerMask;

        private List<Action> _triggeredActions;

        private List<RaycastHit> _lastHits = new List<RaycastHit>();

        protected new void Awake()
        {
            base.Awake();
            if (GetComponent<Camera>() == null)
            {
                EditorUtility.DisplayDialog("Error", "Camera Actor can only be placed on a camera:\n" +
                                                     GetType().Name + " placed on " + name, "Ok");
                EditorApplication.isPlaying = false;
            }

            _interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");
        }

        private new void Start()
        {
            base.Start();
        }

        protected override List<Action> Act()
        {
            _triggeredActions = new List<Action>();
            var interactables = GameObject.FindGameObjectsWithTag("Interactable")
                .Where(obj => (obj.transform.position - transform.position).magnitude <= interactionReach).ToArray();
            ProximityTriggers(interactables);
            GazeTriggers();
            Gaze360Triggers();
            return _triggeredActions;
        }

        private bool ProximityTriggers(ICollection<GameObject> interactables)
        {
            foreach (var interactable in interactables)
            {
                var actions = interactable.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var proximityAction = action as ProximityAction;
                    var enterAction = action as EnterAction;
                    var exitAction = action as ExitAction;
                    if (proximityAction != null)
                    {
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);

                        proximityAction.Trigger(this);
                    }
                    else if (enterAction != null)
                    {
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);

                        enterAction.Trigger(this);
                    }
                    else if (exitAction != null)
                    {
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);

                        exitAction.Trigger(this);
                    }
                }
            }

            return interactables.Count > 0;
        }


        private bool GazeTriggers()
        {
            var hits = new List<RaycastHit>(GazeLookAtTriggers());
            var notHits = _lastHits.Where(lastHit =>
                !hits.Select(hit => hit.transform.gameObject).Contains(lastHit.transform.gameObject)
            );
            foreach (var notHit in notHits)
            {
                var hitObject = notHit.transform.gameObject;
                var actions = hitObject.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var lookAwayAction = action as LookAwayAction;
                    if (lookAwayAction != null)
                    {
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);

                        lookAwayAction.Trigger(this, notHit);
                    }
                }
            }

            _lastHits = hits;
            return true;
        }

        private IEnumerable<RaycastHit> GazeLookAtTriggers()
        {
            var hits = gazeThroughObjects == GazeThroughObjectOptions.Never
                ? Physics.RaycastAll(transform.position, transform.forward, interactionReach)
                : Physics.RaycastAll(transform.position, transform.forward, interactionReach, _interactableLayerMask);
            hits = hits.OrderBy(hit => hit.distance).ToArray();

            foreach (var hit in hits)
            {
                var hitObject = hit.transform.gameObject;
                var actions = hitObject.GetComponents<Action>();
                var anyGazeAction = false;
                foreach (var action in actions)
                {
                    var gazeAction = action as GazeAction;
                    var lookAtAction = action as LookAtAction;
                    if (gazeAction != null)
                    {
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);

                        gazeAction.Trigger(this, hit);
                        anyGazeAction = true;
                    }
                    else if (lookAtAction != null)
                    {
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);

                        if (!_lastHits.Select(lastHit => lastHit.transform.gameObject)
                            .Contains(hit.transform.gameObject))
                        {
                            lookAtAction.Trigger(this, hit);
                            anyGazeAction = true;
                        }
                    }
                }

                if (anyGazeAction)
                {
                    if (gazeThroughObjects == GazeThroughObjectOptions.UntilTriggered)
                        break;
                }

                if (gazeThroughObjects == GazeThroughObjectOptions.Never)
                    break;
            }

            return hits;
        }

        private bool Gaze360Triggers()
        {
            // TODO: Gaze 360
            // Rays don't hit the object they come from inside of it. Solution:
            // Make ray come from outside towards actor. Take the last hit object, it will be the 360 sphere:
            //var hits = Physics.RaycastAll(transform.position + transform.forward * interactionReach, -transform.forward, interactionReach, _interactableLayerMask);

            return false;
        }
    }
}