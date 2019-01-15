using UnityEngine;

namespace Interaction.Reactions.Transform
{
    public abstract class TransformReaction : Reaction
    {
        public enum RelativeToOptions
        {
            World,
            Self,
            Object,
            Actor,
            Head
        }
        public Vector3 transformValues;

        public RelativeToOptions relativeTo = RelativeToOptions.World;

        public GameObject referenceObject;
        
        protected new void Awake()
        {
            base.Awake();
        }

        protected new void Start()
        {
            base.Start();
        }
    }
}