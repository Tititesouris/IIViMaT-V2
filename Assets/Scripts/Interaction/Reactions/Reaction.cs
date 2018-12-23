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

        [Tooltip("The amount of time in seconds to wait before reacting when triggered.")]
        public float delay;

        [Tooltip("The amount of time in seconds to wait before reacting again.")]
        public float cooldown;

        protected static Random Rnd;

        private float _lastReaction;

        private IEnumerator _reactAfterDelayCoroutine;

        private void Start()
        {
            if (Rnd == null)
                Rnd = new Random();
        }

        protected abstract bool React(Actor actor, RaycastHit? hit);

        public bool ReactToAction(Actor actor, RaycastHit? hit)
        {
            if (Time.time < _lastReaction + cooldown)
                return false;

            _lastReaction = Time.time;
            if (delay > 0)
            {
                if (_reactAfterDelayCoroutine != null)
                    return false;
                _reactAfterDelayCoroutine = ReactAfterDelay(actor, hit, delay);
                StartCoroutine(_reactAfterDelayCoroutine);
                return true;
            }

            return React(actor, hit);
        }

        private IEnumerator ReactAfterDelay(Actor actor, RaycastHit? hit, float time)
        {
            yield return new WaitForSeconds(time);
            React(actor, hit);
            _reactAfterDelayCoroutine = null;
        }
    }
}