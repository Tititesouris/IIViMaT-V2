using System.Collections;
using Interaction.Actors;
using UnityEngine;
using Random = System.Random;

namespace Interaction.Reactions
{
    public abstract class Reaction : MonoBehaviour
    {
        public enum RepeatOptions
        {
            No,
            Indefinitely,
            Fixed
        }
        // TODO: Random cooldown
        // TODO: Random delay
        // TODO: Option to interpolate over cooldown time
        // TODO: Option to loop until triggered again

        [Tooltip("Give the reaction a unique name to identify it.")]
        public string reactionName;

        [Tooltip("The amount of time in seconds to be continuously triggered for in order to react.")]
        public float triggerDuration = 1;

        [Tooltip("The amount of time in seconds to wait before reacting when triggered.")]
        public float delay;

        public bool triggerOnlyOnce = true;

        public float cooldown;

        public RepeatOptions repeat = RepeatOptions.No;

        public int nbRepeat = 1;

        protected static Random Rnd;

        private bool _startedTrigger;

        private bool _triggered;

        private float _lastTrigger;

        private bool _repeating;

        private int _nbRepeated;

        private IEnumerator _startTriggerCoroutine;

        private IEnumerator _reactAfterDelayCoroutine;

        protected void Awake()
        {
        }

        protected void Start()
        {
            if (Rnd == null)
                Rnd = new Random();
        }

        protected abstract bool React(Actor actor, RaycastHit? hit);

        public bool Trigger(Actor actor, RaycastHit? hit)
        {
            // Don't trigger if component is disabled
            if (!isActiveAndEnabled)
                return false;

            // Don't trigger if already in the process of triggering
            if (_startedTrigger)
                return false;

            // Don't trigger if the reaction is currently repeating
            if (_repeating)
                return false;

            // Don't trigger if only triggering once and already triggered
            if (triggerOnlyOnce && (_startedTrigger || _triggered))
                return false;

            // Don't trigger if not enough time has passed since last trigger
            if (Time.time < _lastTrigger + cooldown)
                return false;
            _nbRepeated = 0;

            // If triggering takes time
            if (triggerDuration > 0)
            {
                _startTriggerCoroutine = StartTrigger(actor, hit, triggerDuration);
                StartCoroutine(_startTriggerCoroutine);
                return false;
            }

            return Triggered(actor, hit);
        }

        private bool Triggered(Actor actor, RaycastHit? hit)
        {
            _triggered = true;
            _startedTrigger = false;
            _lastTrigger = Time.time;

            if (repeat == RepeatOptions.Indefinitely || repeat == RepeatOptions.Fixed && _nbRepeated < nbRepeat)
            {
                _repeating = true;
                _nbRepeated++;
                StartCoroutine(StartTrigger(actor, hit, cooldown));
            }
            else
            {
                _repeating = false;
            }

            if (delay > 0)
            {
                _reactAfterDelayCoroutine = ReactAfterDelay(actor, hit, delay);
                StartCoroutine(_reactAfterDelayCoroutine);
                return true;
            }

            return React(actor, hit);
        }

        private IEnumerator StartTrigger(Actor actor, RaycastHit? hit, float time)
        {
            _startedTrigger = true;
            yield return new WaitForSeconds(time);
            Triggered(actor, hit);
        }

        public void StopTrigger()
        {
            if (_startTriggerCoroutine != null)
            {
                StopCoroutine(_startTriggerCoroutine);
                _startedTrigger = false;
                _lastTrigger = 0;
            }
        }

        private IEnumerator ReactAfterDelay(Actor actor, RaycastHit? hit, float time)
        {
            yield return new WaitForSeconds(time);
            React(actor, hit);
        }
    }
}