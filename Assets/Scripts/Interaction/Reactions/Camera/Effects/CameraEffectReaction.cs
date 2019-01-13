using System.Collections;
using Interaction.Actors;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.Camera.Effects
{
    public abstract class CameraEffectReaction : CameraReaction
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