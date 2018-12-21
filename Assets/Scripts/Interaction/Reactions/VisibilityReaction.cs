using UnityEngine;

namespace Interaction.Reactions
{
    public class VisibilityReaction : Reaction
    {
        [Tooltip("The new visibility of the object after reacting.")]
        public bool NewVisibility;

        [Tooltip("If enabled, the visibility will become hidden if shown and become shown if hidden.")]
        public bool ToggleVisibility;


        protected override bool React()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            if (ToggleVisibility)
                meshRenderer.enabled = !meshRenderer.enabled;
            else
                meshRenderer.enabled = NewVisibility;
            return true;
        }
    }
}