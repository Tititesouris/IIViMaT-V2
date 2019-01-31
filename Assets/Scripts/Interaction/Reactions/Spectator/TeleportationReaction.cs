using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using Interaction.Actors;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.Spectator
{
    public class TeleportationReaction : SpectatorReaction
    {
        [Tooltip("Object to teleport to.")] public GameObject teleportTo;

        private IEnumerator _teleportCoroutine;

        private ColorGrading _colorGrading;

        private const float Duration = 0.1f;

        [Tooltip("Select which view mode is used by the camera.\n" +
                 "Free View: The camera can move freely" +
                 "Fixed View: The camera is stuck inside a 360 sphere" +
                 "Follow View: The camera can move freely but a 360 sphere follows so that the camera is always inside it")]
        public SpectatorViewMode.ViewMode viewMode;

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
            var startPos = SpectatorHead.transform.position;
            var destination = teleportTo.transform.position;

            do
            {
                SpectatorFeet.transform.position =
                    Vector3.Lerp(startPos, destination, (Time.time - startTime) / Duration)
                    - SpectatorHead.transform.localPosition;
                yield return null;
            } while (Time.time - startTime <= Duration);

            SpectatorFeet.transform.position = destination - SpectatorHead.transform.localPosition;
            _teleportCoroutine = null;
            SpectatorFeet.GetComponent<SpectatorViewMode>().SetViewMode(viewMode, gameObject);
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