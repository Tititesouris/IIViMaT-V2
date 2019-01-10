using System.Linq;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.VisualReactions
{
    public class BlackAndWhiteVisualReaction : VisualReaction
    {
        private ColorGrading _colorGrading;

        protected override void StartEffect()
        {
            _colorGrading = Effects.OfType<ColorGrading>().First();
            _colorGrading.enabled.Override(true);
            _colorGrading.saturation.Override(0f);
        }

        protected override void ApplyEffect(float value)
        {
            _colorGrading.saturation.value = -100 * value;
        }

        protected override void ResetEffect()
        {
            _colorGrading.enabled.Override(false);
        }
    }
}