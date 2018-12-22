using UnityEngine;

namespace Interaction.Reactions
{
    public class ColorReaction : Reaction
    {
        private Renderer _renderer;

        [Tooltip("The new color will be randomized up to this color.")]
        public Color MaxRandomColor;

        [Tooltip("The color the object will have after reacting.")]
        public Color NewColor;

        [Tooltip(
            "If enabled, the new color will be a random color between [New Color] and [Max Random Color].")]
        public bool RandomNewColor;

        [Tooltip(
            "If enabled, the new color will be relative to the current color. If disabled, the new color will be relative to black.")]
        public bool RelativeNewColor;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var newColor = _renderer.material.color;
            if (RelativeNewColor)
            {
                newColor += NewColor;
            }
            else
            {
                newColor = NewColor;
            }

            if (RandomNewColor)
            {
                newColor = new Color(
                    Random.Range(NewColor.r, MaxRandomColor.r),
                    Random.Range(NewColor.g, MaxRandomColor.g),
                    Random.Range(NewColor.b, MaxRandomColor.b)
                );
            }

            _renderer.material.color = newColor;
            return true;
        }
    }
}