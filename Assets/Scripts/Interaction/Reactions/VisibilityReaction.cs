using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions
{
    public class VisibilityReaction : Reaction
    {
        [Tooltip("The new visibility of the object after reacting.")]
        public bool newVisibility;

        [Tooltip("If enabled, the visibility will become hidden if shown and become shown if hidden.")]
        public bool toggleVisibility;


        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            if (toggleVisibility)
                meshRenderer.enabled = !meshRenderer.enabled;
            else
                meshRenderer.enabled = newVisibility;
            return true;
        }
    }
}