using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Appearance
{
    public class VisibilityReaction : Reaction
    {
        public enum VisibilityOptions
        {
            Toggle,
            Hidden,
            Visible
        }

        [Tooltip("The visibility of the object after reacting.\n" +
                 "Toggle: Hidden becomes Visible, Visible becomes Hidden\n" +
                 "Hidden: Hide object\n" +
                 "Visible: Show object")]
        public VisibilityOptions visibility = VisibilityOptions.Hidden;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            switch (visibility)
            {
                case VisibilityOptions.Toggle:
                    meshRenderer.enabled = !meshRenderer.enabled;
                    break;
                case VisibilityOptions.Hidden:
                    meshRenderer.enabled = false;
                    break;
                case VisibilityOptions.Visible:
                    meshRenderer.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }
    }
}