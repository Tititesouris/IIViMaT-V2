using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions
{
    public class ColorReaction : Reaction
    {
        private Renderer _renderer;

        [Tooltip("The new color will be randomized up to this color.")]
        public Color maxRandomColor;

        [Tooltip("The color the object will have after reacting.")]
        public Color newColor;

        [Tooltip(
            "If enabled, the new color will be a random color between [New Color] and [Max Random Color].")]
        public bool randomNewColor;

        [Tooltip(
            "If enabled, the new color will be relative to the current color. If disabled, the new color will be relative to black.")]
        public bool relativeNewColor;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var color = _renderer.material.color;
            if (relativeNewColor) // TODO: Bug, there is no negative color so relative always increases
                color += newColor;
            else
                color = newColor;

            if (randomNewColor)
                color = new Color(
                    Random.Range(newColor.r, maxRandomColor.r),
                    Random.Range(newColor.g, maxRandomColor.g),
                    Random.Range(newColor.b, maxRandomColor.b)
                );

            _renderer.material.color = color;
            return true;
        }
    }
}