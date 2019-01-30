using Interaction.Actors;

namespace Interaction.Actions.Video
{
    public class PlayVideoAction : Action
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