using System.Linq;
using Interaction.Actions;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Meta
{
    public class PropagateReaction : Reaction
    {
        [Tooltip("List of the objects that will be triggered.")]
        public GameObject[] targets;

        [Tooltip("If enabled, trigger all targets.")]
        public bool propagateAll = true;

        [Tooltip("The number of targets that will be triggered.")]
        public int nbPropagations = 1;

        [Tooltip("If enabled, [Nb Propagations] random targets will be triggered." +
                 "If disabled, the first [Nb Propagations] targets will be triggered.")]
        public bool randomPropagation;

        [Tooltip("If enabled, the targets triggered will be removed from the list and thus cannot be triggered again.")]
        public bool removeTargetOnPropagation;

        public bool specifyActions;

        public string[] actionNames;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var selectedTargets = (randomPropagation ? targets.OrderBy(x => Rnd.Next()).ToArray() : targets)
                .Take(propagateAll ? targets.Length : nbPropagations).ToArray();
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

            if (removeTargetOnPropagation) targets = targets.Where(x => !selectedTargets.Contains(x)).ToArray();

            return true;
        }
    }
}