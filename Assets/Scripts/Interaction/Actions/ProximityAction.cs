using UnityEngine;

namespace Interaction.Actions
{
    public class ProximityAction : Action
    {
        [Tooltip("The reactions will be triggered if the actor is in this radius.")]
        public float TriggerDistance = 1f;


        public bool Trigger(Actor actor)
        {
            var distance = (actor.transform.position - transform.position).magnitude;
            if (distance <= TriggerDistance)
            {
                foreach (var reaction in Reactions)
                {
                    reaction.React();
                }

                return true;
            }

            return false;
        }
    }
}