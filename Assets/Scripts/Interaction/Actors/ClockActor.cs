using System.Collections.Generic;
using Interaction.Actions;
using Interaction.Actions.Clock;
using UnityEngine;

namespace Interaction.Actors
{
    public class ClockActor : Actor
    {
        private List<Action> _triggeredActions;

        protected override List<Action> Act()
        {
            _triggeredActions = new List<Action>();
            var interactables = GameObject.FindGameObjectsWithTag("Interactable");
            TimeTriggers(interactables);
            return _triggeredActions;
        }

        private bool TimeTriggers(ICollection<GameObject> interactables)
        {
            foreach (var interactable in interactables)
            {
                var actions = interactable.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var timeAction = action as ClockTimeAction;
                    if (timeAction != null)
                    {
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);

                        timeAction.Trigger(this, Time.time);
                    }
                }
            }

            return interactables.Count > 0;
        }
    }
}