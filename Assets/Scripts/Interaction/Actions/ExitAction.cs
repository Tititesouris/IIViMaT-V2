using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
{
    public class ExitAction : Action
    {
        [Tooltip("The reactions will be triggered when the actor exits the zone within this radius of the object.")]
        public float triggerDistance = 1f;

        private bool _actorInRange;


        public bool Trigger(Actor actor)
        {
            if (!isActiveAndEnabled)
                return false;

            if ((actor.transform.position - transform.position).magnitude >= triggerDistance)
            {
                if (_actorInRange)
                {
                    _actorInRange = false;
                    foreach (var reaction in reactions) reaction.Trigger(actor, null);

                    return true;
                }
            }
            else
            {
                _actorInRange = true;
            }

            return false;
        }
    }
}