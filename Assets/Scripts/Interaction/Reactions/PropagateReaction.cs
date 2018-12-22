using System.Linq;
using Interaction.Actions;
using UnityEngine;

namespace Interaction.Reactions
{
    public class PropagateReaction : Reaction
    {
        [Tooltip("The number of targets that will be triggered.")]
        public int NbPropagations;

        [Tooltip("If enabled, [NbPropagations] random targets will be triggered." +
                 "If disabled, the first [NbPropagations] targets will be triggered.")]
        public bool RandomPropagation;

        [Tooltip("If enabled, the targets triggered will be removed from the list and thus cannot be triggered again.")]
        public bool RemoveTargetOnPropagation;

        [Tooltip("List of the objects that will be triggered.")]
        public GameObject[] Targets;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var targets = (RandomPropagation ? Targets.OrderBy(x => rnd.Next()).ToArray() : Targets)
                .Take(NbPropagations).ToArray();
            foreach (var target in targets)
            {
                var actions = target.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var propagatedAction = action as PropagatedAction;
                    if (propagatedAction != null)
                    {
                        propagatedAction.Trigger(actor, hit, gameObject);
                    }
                }
            }

            if (RemoveTargetOnPropagation)
            {
                Targets = Targets.Where(x => !targets.Contains(x)).ToArray();
            }

            return true;
        }
    }
}