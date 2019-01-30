using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.Spectator.Effects
{
    public class FadeVisualEffectReaction : VisualEffectReaction
    {
        public enum FadeOptions
        {
            FadeIn,
            FadeOut
        }

        public FadeOptions fadeType;

        private Vignette _vignette;

        protected override void StartEffect()
        {
            _vignette = Effects.OfType<Vignette>().First();
            _vignette.enabled.Override(true);
            switch (fadeType)
            {
                case FadeOptions.FadeIn:
                    _vignette.intensity.Override(0);
                    break;
                case FadeOptions.FadeOut:
                    _vignette.intensity.Override(50);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void ApplyEffect(float value)
        {
            switch (fadeType)
            {
                case FadeOptions.FadeIn:
                    _vignette.intensity.Override(Mathf.Exp(1 + 2 * value) - Mathf.Exp(1));
                    break;
                case FadeOptions.FadeOut:
                    _vignette.intensity.Override(Mathf.Exp(1 + 2 * (1 - value)) - Mathf.Exp(1));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void ResetEffect()
        {
            _vignette.enabled.Override(false);
            _vignette.intensity.Override(0);
        }
    }
}