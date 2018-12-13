using UnityEngine;

namespace Interaction.Reactions
{
	public class OrientationReaction : Reaction {

		[Tooltip("The orientation the object will have after reacting.")]
		public Vector3 NewOrientation;

		protected override bool React()
		{
			transform.localRotation = Quaternion.Euler(NewOrientation);
			return true;
		}
	}
}
