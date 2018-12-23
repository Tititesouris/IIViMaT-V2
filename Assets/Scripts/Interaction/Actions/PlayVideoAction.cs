using Interaction.Actors;

namespace Interaction.Actions
{
    public class PlayVideoAction : Action
    {
        public bool Trigger(Actor actor)
        {
            foreach (var reaction in Reactions) reaction.ReactToAction(actor, null);

            return true;
        }
    }
}