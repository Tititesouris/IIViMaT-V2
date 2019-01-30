using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Spectator.Effects
{
    public abstract class VisualEffectReaction : SpectatorReaction
    {
        [Tooltip("The length of time the visual effect will be playing for.")]
        public float duration = 1f;

        [Tooltip("If enabled, the visual effect will disappear after it is complete." +
                 "If disabled the effect will remain with its last appearance.")]
        public bool clearAfter;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            StartEffect();
            EffectCoroutine = Effect(Time.time, duration, clearAfter);
            StartCoroutine(EffectCoroutine);
            return true;
        }
    }
}