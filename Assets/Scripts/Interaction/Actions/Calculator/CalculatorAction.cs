using Interaction.Actors;
using UnityEditor;

namespace Interaction.Actions.Calculator
{
    public class CalculatorAction : Action
    {
        
        protected new void Awake()
        {
            base.Awake();
            if (GetComponent<CalculatorActor>() == null)
            {
                EditorUtility.DisplayDialog("Error", "Calculator Action is missing Calculator Actor:\n" +
                                                     "Make sure that a CalculatorActor is present on the same object as CalculatorAction.",
                    "Ok");
                EditorApplication.isPlaying = false;
            }
        }
        
    }
}