using Interaction.Actions;
using UnityEngine;

namespace Interaction
{
	public class TestActor : MonoBehaviour
	{

		public GameObject test;
	
		void Update ()
		{
			Action[] actions = test.GetComponents<Action>();
			foreach (var action in actions)
			{
				if (action is ProximityAction)
				{
					Debug.Log(action.SpecifyReactions);
					((ProximityAction) action).Trigger((transform.position - test.transform.position).magnitude);
				}
			}
		}
	}
}
