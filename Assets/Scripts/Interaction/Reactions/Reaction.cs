using UnityEngine;

namespace Interaction.Reactions
{
    public abstract class Reaction : MonoBehaviour
    {
        [Tooltip("Give the reaction a unique name to identify it.")]
        public string ReactionName;

        [Tooltip("The amount of time in seconds to wait before reacting again.")]
        public float Cooldown;

        private float _lastReaction;

        protected abstract bool React();

        public bool ReactToAction()
        {
            if (Time.time < _lastReaction + Cooldown)
                return false;
            
            _lastReaction = Time.time;
            return React();
        }
    }
}