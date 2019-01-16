using System.Collections.Generic;
using System.Linq;
using Interaction.Actions;
using Interaction.Actors;
using UnityEngine;
using Action = Interaction.Actions.Action;

namespace Interaction.Reactions.Meta
{
    public class PropagateReaction : Reaction
    {
        [Tooltip("List of the objects that will be triggered.")]
        public GameObject[] targets;

        public bool triggerSpecific;

        public int nbPropagations = 1;

        public bool randomPropagation;

        [Tooltip("If enabled, the targets triggered will be removed from the list and thus cannot be triggered again.")]
        public bool removeTriggeredTargets;

        public bool specifyActions;

        public List<string> actionNames = new List<string>();

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var selectedTargets = (randomPropagation ? targets.OrderBy(x => Rnd.Next()).ToArray() : targets)
                .Take(triggerSpecific ? nbPropagations : targets.Length).ToArray();
            foreach (var target in selectedTargets)
            {
                var actions = target.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var propagatedAction = action as PropagatedAction;
                    if (propagatedAction != null)
                        if (!specifyActions || actionNames.Contains(propagatedAction.actionName))
                            propagatedAction.Trigger(actor, hit, gameObject);
                }
            }

            if (removeTriggeredTargets) targets = targets.Where(x => !selectedTargets.Contains(x)).ToArray();

            return true;
        }
    }
}