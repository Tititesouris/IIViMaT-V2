using System.Linq;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.Spectator.Effects
{
    public class BlackAndWhiteVisualEffectReaction : VisualEffectReaction
    {
        private ColorGrading _colorGrading;

        protected override void StartEffect()
        {
            _colorGrading = Effects.OfType<ColorGrading>().First();
            _colorGrading.enabled.Override(true);
            _colorGrading.saturation.Override(0);
        }

        protected override void ApplyEffect(float value)
        {
            _colorGrading.saturation.Override(-100 * value);
        }

        protected override void ResetEffect()
        {
            _colorGrading.enabled.Override(false);
        }
    }
}