using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Meta
{
    public class ActivationReaction : Reaction
    {
        public enum ActivationOptions
        {
            Enabled,
            Disabled
        }

        [Tooltip("List of the objects that will be triggered.")]
        public GameObject[] targets;

        public ActivationOptions activation;


        protected override bool React(Actor actor, RaycastHit? hit)
        {
            foreach (var target in targets)
            {
                target.SetActive(activation == ActivationOptions.Enabled);
            }

            return true;
        }
    }
}