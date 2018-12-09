using UnityEngine;

namespace Interaction.Reactions
{
    public abstract class Reaction : MonoBehaviour
    {
        [Tooltip("Give the reaction a unique name to identify it.")]
        public string ReactionName;


        public abstract bool React();
    }
}