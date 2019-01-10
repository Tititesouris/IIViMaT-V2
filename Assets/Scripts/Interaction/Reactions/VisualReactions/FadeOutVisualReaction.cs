using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.VisualReactions
{
    public class FadeOutVisualReaction : VisualReaction
    {
        private Vignette _vignette;

        protected override void StartEffect()
        {
            _vignette = Effects.OfType<Vignette>().First();
            _vignette.enabled.Override(true);
            _vignette.intensity.Override(50f);
        }

        protected override void ApplyEffect(float value)
        {
            _vignette.intensity.value = Mathf.Exp(1 + 2 * value) - Mathf.Exp(1);
        }

        protected override void ResetEffect()
        {
            _vignette.enabled.Override(false);
        }
    }
}