using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Transform
{
    public class PositionTransformReaction : TransformReaction
    {
        public bool relativeHeading;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            if (relativeTo == RelativeToOptions.World)
            {
                transform.position = transformValues;
            }
            else
            {
                var reference =
                    relativeTo == RelativeToOptions.Self ? transform :
                    relativeTo == RelativeToOptions.Object ? referenceObject.transform :
                    relativeTo == RelativeToOptions.Actor ? actor.transform :
                    Camera.main.transform;

                var pos = reference.position + transformValues;
                if (relativeHeading)
                    pos = Quaternion.Euler(reference.eulerAngles) * (pos - reference.position) +
                                         reference.position;

                transform.position = pos;
            }
            return true;
        }
    }
}