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

        [Tooltip("Select which view mode is used by the camera.\n" +
                 "Free View: The camera can move freely" +
                 "Fixed View: The camera is stuck inside a 360 sphere" +
                 "Follow View: The camera can move freely but a 360 sphere follows so that the camera is always inside it")]
        public CameraViewMode.ViewMode viewMode;

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
            var feet = GameObject.FindWithTag("Player");
            var startTime = Time.time;
            var startPos = feet.transform.position;
            var destination = transform.position - (startPos + GameObject.FindWithTag("MainCamera").transform.position);
            
            do
            {
                feet.transform.position =
                    Vector3.Lerp(startPos, destination, (Time.time - startTime) / Duration);
                yield return null;
            } while (Time.time - startTime <= Duration);

            feet.transform.position = destination;
            _teleportCoroutine = null;
            GameObject.FindWithTag("Player").GetComponent<CameraViewMode>().SetViewMode(viewMode, gameObject);
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