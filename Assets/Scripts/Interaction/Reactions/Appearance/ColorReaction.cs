using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Appearance
{
    public class ColorReaction : Reaction
    {
        [Tooltip("If enabled, the color will be randomized.")]
        public bool randomColor;

        [Tooltip("The color the object will have after reacting.")]
        public Color color;

        private Renderer _renderer;

        protected new void Awake()
        {
            base.Awake();
            _renderer = GetComponent<Renderer>();
        }

        protected new void Start()
        {
            base.Start();
        }

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            _renderer.material.color = randomColor
                ? new Color(Rnd.Next(10) * 25 / 255f, Rnd.Next(10) * 25 / 255f, Rnd.Next(10) * 25 / 255f)
                : color;
            return true;
        }
    }
}