using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Transform
{
    public class ScaleTransformReaction : TransformReaction
    {

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            switch (relativeTo)
            {
                case RelativeToOptions.World:
                    transform.localScale = transformValues;
                    break;
                case RelativeToOptions.Self:
                    transform.localScale = Vector3.Scale(transform.localScale, transformValues);
                    break;
                case RelativeToOptions.Object:
                    transform.localScale = Vector3.Scale(referenceObject.transform.localScale, transformValues);
                    break;
                case RelativeToOptions.Actor:
                    transform.localScale = Vector3.Scale(actor.transform.localScale, transformValues);
                    break;
                case RelativeToOptions.Camera:
                    transform.localScale = Vector3.Scale(UnityEngine.Camera.current.transform.localScale, transformValues);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}