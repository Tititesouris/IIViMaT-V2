using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Interaction.Reactions
{
    public abstract class Reaction : MonoBehaviour
    {
        protected static Random rnd;

        // TODO: Random cooldown

        // TODO: Option to interpolate over cooldown time
        // TODO: Option to loop for x seconds
        // TODO: Option to loop indefinitely
        // TODO: Option to loop until triggered again


        private float _lastReaction;

        private IEnumerator _reactAfterDelayCoroutine;

        // TODO: Random delay

        [Tooltip("The amount of time in seconds to wait before reacting again.")]
        public float Cooldown;

        [Tooltip("The amount of time in seconds to wait before reacting when triggered.")]
        public float Delay;

        [Tooltip("Give the reaction a unique name to identify it.")]
        public string ReactionName;

        protected abstract bool React(Actor actor, RaycastHit? hit);

        private void Start()
        {
            if (rnd == null)
                rnd = new Random();
        }

        public bool ReactToAction(Actor actor, RaycastHit? hit)
        {
            if (Time.time < _lastReaction + Cooldown)
                return false;

            _lastReaction = Time.time;
            if (Delay > 0)
            {
                if (_reactAfterDelayCoroutine != null)
                    return false;
                _reactAfterDelayCoroutine = ReactAfterDelay(actor, hit, Delay);
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