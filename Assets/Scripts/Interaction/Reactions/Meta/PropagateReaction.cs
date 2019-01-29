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
        public GameObject[] targets = new GameObject[0];

        public bool triggerSpecific;

        public int nbPropagations = 1;

        public bool randomPropagation;

        [Tooltip("If enabled, the targets triggered will be removed from the list and thus cannot be triggered again.")]
        public bool removeTriggeredTargets;

        public bool specifyActions;

        [SerializeField] private List<PropagatedAction> actions = new List<PropagatedAction>();

        public List<PropagatedAction> GetSpecifiedActions()
        {
            if (!specifyActions)
                actions = new List<PropagatedAction>(
                    targets.Select(target => target.GetComponents<PropagatedAction>()).SelectMany(action => action)
                );
            actions.RemoveAll(reaction => reaction == null);
            return actions;
        }

        public void SetSpecifiedActions(List<PropagatedAction> actions)
        {
            this.actions = actions;
        }

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var selectedTargets = (randomPropagation ? targets.OrderBy(x => Rnd.Next()).ToArray() : targets)
                .Take(triggerSpecific ? nbPropagations : targets.Length).ToArray();
            foreach (var target in selectedTargets)
            {
                foreach (var propagatedAction in target.GetComponents<PropagatedAction>())
                {
                    if (!specifyActions || GetSpecifiedActions().Contains(propagatedAction))
                        propagatedAction.Trigger(actor, hit, gameObject);
                }
            }

            if (removeTriggeredTargets) targets = targets.Where(x => !selectedTargets.Contains(x)).ToArray();

            return true;
        }
    }
}