using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions
{
    public class ActivationReaction : Reaction
    {
        [Tooltip("List of the objects that will be triggered.")]
        public GameObject[] targets;

        [Tooltip("If enable, the targets will be activated. If disabled, the targets will be deactivated.")]
        public bool activateTargets;


        protected override bool React(Actor actor, RaycastHit? hit)
        {
            foreach (var target in targets)
            {
                target.SetActive(activateTargets);
            }

            return true;
        }
    }
}