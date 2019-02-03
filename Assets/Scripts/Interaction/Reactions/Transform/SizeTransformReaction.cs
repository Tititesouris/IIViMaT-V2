using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Transform
{
    public class SizeTransformReaction : TransformReaction
    {

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            switch (relativeTo)
            {
                case RelativeToOptions.World:
                    transform.localScale = transformValues;
                    break;
                case RelativeToOptions.Self:
                    transform.localScale += transformValues;
                    break;
                case RelativeToOptions.Object:
                    transform.localScale = referenceObject.transform.localScale + transformValues;
                    break;
                case RelativeToOptions.Actor:
                    transform.localScale = actor.transform.localScale + transformValues;
                    break;
                case RelativeToOptions.Head:
                    transform.localScale = Camera.main.transform.localScale + transformValues;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}