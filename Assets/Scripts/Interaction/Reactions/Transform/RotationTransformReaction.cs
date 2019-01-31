using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Transform
{
    public class RotationTransformReaction : TransformReaction
    {
        // TODO: Option to keep same orientation or not
        public float angle;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            switch (relativeTo)
            {
                case RelativeToOptions.World:
                    transform.RotateAround(Vector3.zero, transformValues, angle);
                    break;
                case RelativeToOptions.Self:
                    transform.RotateAround(transform.position, transformValues, angle);
                    break;
                case RelativeToOptions.Object:
                    transform.RotateAround(referenceObject.transform.position, transformValues, angle);
                    break;
                case RelativeToOptions.Actor:
                    transform.RotateAround(actor.transform.position, transformValues, angle);
                    break;
                case RelativeToOptions.Head:
                    transform.RotateAround(Camera.current.transform.position, transformValues, angle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}