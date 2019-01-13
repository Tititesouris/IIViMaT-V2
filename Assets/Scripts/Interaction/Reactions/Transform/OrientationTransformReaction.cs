﻿using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Transform
{
    public class OrientationTransformReaction : TransformReaction
    {

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            switch (relativeTo)
            {
                case RelativeToOptions.World:
                    transform.rotation = Quaternion.Euler(transformValues);
                    break;
                case RelativeToOptions.Self:
                    transform.rotation = transform.rotation * Quaternion.Euler(transformValues);
                    break;
                case RelativeToOptions.Object:
                    transform.rotation = referenceObject.transform.rotation * Quaternion.Euler(transformValues);
                    break;
                case RelativeToOptions.Actor:
                    transform.rotation = actor.transform.rotation * Quaternion.Euler(transformValues);
                    break;
                case RelativeToOptions.Camera:
                    transform.rotation = UnityEngine.Camera.current.transform.rotation * Quaternion.Euler(transformValues);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}