using System.Collections;
using Interaction.Actors;
using UnityEngine;
using Random = System.Random;

namespace Interaction.Reactions
{
    public abstract class Reaction : MonoBehaviour
    {
        // TODO: Random cooldown
        // TODO: Random delay

        // TODO: Option to interpolate over cooldown time
        // TODO: Option to loop for x seconds
        // TODO: Option to loop indefinitely
        // TODO: Option to loop until triggered again

        [Tooltip("Give the reaction a unique name to identify it.")]
        public string reactionName;

        [Tooltip("The amount of time in seconds to be continuously triggered for in order to react.")]
        public float triggerTime = 1;

        [Tooltip("The amount of time in seconds to wait before reacting when triggered.")]
        public float delay;

        public bool triggerOnlyOnce = true;

        public float cooldown;

        protected static Random Rnd;

        private bool _startedTrigger;

        private bool _triggered;

        private float _lastTrigger;

        private IEnumerator _triggerAfterTimeCoroutine;

        private IEnumerator _reactAfterDelayCoroutine;

        private void Start()
        {
            if (Rnd == null)
                Rnd = new Random();
        }

        protected abstract bool React(Actor actor, RaycastHit? hit);

        public bool Trigger(Actor actor, RaycastHit? hit)
        {
            if (!isActiveAndEnabled)
                return false;

            if (triggerOnlyOnce && (_startedTrigger || _triggered))
                return false;

            _startedTrigger = true;

            if (Time.time < _lastTrigger + cooldown)
                return false;

            _lastTrigger = Time.time;

            if (triggerTime > 0)
            {
                _triggerAfterTimeCoroutine = TriggerAfterTime(actor, hit, triggerTime);
                StartCoroutine(_triggerAfterTimeCoroutine);
                return false;
            }

            if (delay > 0)
            {
                _reactAfterDelayCoroutine = ReactAfterDelay(actor, hit, delay);
                StartCoroutine(_reactAfterDelayCoroutine);
                return true;
            }

            return React(actor, hit);
        }

        public void StopTrigger()
        {
            if (_triggerAfterTimeCoroutine != null)
            {
                StopCoroutine(_triggerAfterTimeCoroutine);
                _startedTrigger = false;
                _lastTrigger = 0;
            }
        }

        private IEnumerator TriggerAfterTime(Actor actor, RaycastHit? hit, float time)
        {
            yield return new WaitForSeconds(time);
            _triggered = true;
            if (delay > 0)
            {
                _reactAfterDelayCoroutine = ReactAfterDelay(actor, hit, delay);
                StartCoroutine(_reactAfterDelayCoroutine);
            }

            React(actor, hit);
        }

        private IEnumerator ReactAfterDelay(Actor actor, RaycastHit? hit, float time)
        {
            yield return new WaitForSeconds(time);
            React(actor, hit);
        }
    }
}