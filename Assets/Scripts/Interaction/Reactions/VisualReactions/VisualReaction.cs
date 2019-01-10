using System;
using System.Collections;
using Interaction.Actors;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.VisualReactions
{
    public abstract class VisualReaction : Reaction
    {
        [Tooltip("The length of time the visual effect will be playing for.")]
        public float duration = 1f;

        [Tooltip("If enabled, the visual effect will disappear after it is complete." +
                 "If disabled the effect will remain with its last appearance.")]
        public bool clearAfter;

        protected static PostProcessEffectSettings[] Effects;
        
        private static PostProcessVolume _volume;

        private IEnumerator _effectCoroutine;

        private void Start()
        {
            if (_volume == null)
            {
                Effects = new PostProcessEffectSettings[]
                {
                    ScriptableObject.CreateInstance<ColorGrading>(),
                    ScriptableObject.CreateInstance<Vignette>()
                };
                _volume = PostProcessManager.instance.QuickVolume(LayerMask.NameToLayer("Post Processing"), 100f,
                    Effects);
            }
        }

        private void OnDestroy()
        {
            if (_volume != null)
                RuntimeUtilities.DestroyVolume(_volume, true, true);
        }

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            StartEffect();
            _effectCoroutine = Effect(Time.time);
            StartCoroutine(_effectCoroutine);
            return true;
        }

        protected abstract void StartEffect();

        protected abstract void ApplyEffect(float value);

        protected abstract void ResetEffect();

        private IEnumerator Effect(float startTime)
        {
            while (Time.time < startTime + duration)
            {
                ApplyEffect((Time.time - startTime) / duration);
                yield return null;
            }

            ApplyEffect(1);
            if (clearAfter) ResetEffect();
        }
    }
}