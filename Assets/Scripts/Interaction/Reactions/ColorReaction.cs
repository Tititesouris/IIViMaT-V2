using UnityEngine;

namespace Interaction.Reactions
{
    public class ColorReaction : Reaction
    {
        [Tooltip("The color the object will have after reacting.")]
        public Color NewColor;


        private Renderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        protected override bool React()
        {
            _renderer.material.color = NewColor;
            return true;
        }
    }
}