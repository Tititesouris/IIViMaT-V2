using UnityEngine;

namespace Interaction.Reactions
{
    public class ColorReaction : Reaction
    {
        [Tooltip("The color the object will have after reacting.")]
        public Color NewColor;

        [Tooltip(
            "If enabled, the new color will be relative to the current color. If disabled, the new color will be relative to black.")]
        public bool RelativeNewColor;

        [Tooltip(
            "If enabled, the new color will be a random color between [New Color] and [Max Random Color].")]
        public bool RandomNewColor;

        [Tooltip("The new color will be randomized up to this color.")]
        public Color MaxRandomColor;


        private Renderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        protected override bool React()
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