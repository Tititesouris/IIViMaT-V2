using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Appearance
{
    public class VisibilityReaction : Reaction
    {
        public enum VisibilityOptions
        {
            Hidden,
            Visible
        }

        [Tooltip("The visibility of the object after reacting.")]
        public VisibilityOptions visibility = VisibilityOptions.Hidden;

        [Tooltip("If enabled, the visibility will become hidden if shown and become shown if hidden.")]
        public bool toggleVisibility;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            if (toggleVisibility)
                meshRenderer.enabled = !meshRenderer.enabled;
            else
                meshRenderer.enabled = visibility == VisibilityOptions.Visible;
            return true;
        }
    }
}