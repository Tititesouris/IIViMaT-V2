using System.Collections;
using UnityEngine;

namespace Interaction.Reactions
{
    public abstract class Reaction : MonoBehaviour
    {
        [Tooltip("Give the reaction a unique name to identify it.")]
        public string ReactionName;

        [Tooltip("The amount of time in seconds to wait before reacting when triggered.")]
        public float Delay;

        [Tooltip("The amount of time in seconds to wait before reacting again.")]
        public float Cooldown;

        // TODO: Option to interpolate over cooldown time
        // TODO: Option to loop for x seconds
        // TODO: Option to loop indefinitely
        // TODO: Option to loop until triggered again

        private float _lastReaction;

        private IEnumerator _reactAfterDelayCoroutine;

        protected abstract bool React();

        public bool ReactToAction()
        {
            if (Time.time < _lastReaction + Cooldown)
                return false;

            _lastReaction = Time.time;
            if (Delay > 0)
            {
                if (_reactAfterDelayCoroutine != null)
                    return false;
                _reactAfterDelayCoroutine = ReactAfterDelay(Delay);
                StartCoroutine(_reactAfterDelayCoroutine);
                return true;
            }

            return React();
        }

        private IEnumerator ReactAfterDelay(float time)
        {
            yield return new WaitForSeconds(time);
            React();
            _reactAfterDelayCoroutine = null;
        }
    }
}