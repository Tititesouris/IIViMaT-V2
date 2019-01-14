using System.Collections;
using System.Linq;
using Interaction.Actors;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.Camera
{
    public class TeleportationReaction : CameraReaction
    {
        [Tooltip("Target to teleport to.")] public GameObject target;

        private IEnumerator _teleportCoroutine;

        private ColorGrading _colorGrading;

        private const float Duration = 0.1f;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            if (_teleportCoroutine == null)
            {
                _teleportCoroutine = Teleport();
                StartCoroutine(_teleportCoroutine);
                StartEffect();
                EffectCoroutine = Effect(Time.time, Duration, true);
                StartCoroutine(EffectCoroutine);
                return true;
            }

            return false;
        }

        private IEnumerator Teleport()
        {
            var startTime = Time.time;
            var startPos = transform.position;
            var destination = target.transform.position;
            destination.y -= 1.75f;
            do
            {
                transform.position =
                    Vector3.Lerp(startPos, destination, (Time.time - startTime) / Duration);
                yield return null;
            } while (Time.time - startTime < Duration);

            transform.position = destination;
            _teleportCoroutine = null;
        }

        protected override void StartEffect()
        {
            _colorGrading = Effects.OfType<ColorGrading>().First();
            _colorGrading.enabled.Override(true);
            _colorGrading.postExposure.Override(0);
        }

        protected override void ApplyEffect(float value)
        {
            _colorGrading.postExposure.Override(value * 2);
        }

        protected override void ResetEffect()
        {
            _colorGrading.enabled.Override(false);
            _colorGrading.postExposure.Override(0);
        }
    }
}