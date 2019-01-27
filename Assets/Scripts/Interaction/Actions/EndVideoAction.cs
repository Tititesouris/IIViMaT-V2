using Interaction.Actors;

namespace Interaction.Actions
{
    public class EndVideoAction : Action
    {
        public bool Trigger(Actor actor)
        {
            if (!isActiveAndEnabled)
                return false;

            foreach (var reaction in GetSpecifiedReactions()) reaction.Trigger(actor, null);

            return true;
        }
    }
}