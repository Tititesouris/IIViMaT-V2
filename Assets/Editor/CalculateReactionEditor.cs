using System.Collections.Generic;
using Interaction.Actors;
using Interaction.Reactions.Calculator;
using Interaction.Reactions.Spectator;
using UnityEditor;

[CustomEditor(typeof(CalculateReaction), true)]
public class CalculateReactionEditor : ReactionEditor
{
    private SerializedProperty _calculator;

    protected override void LoadGui()
    {
        base.LoadGui();
        _calculator = serializedObject.FindProperty("calculator");
    }


    protected override void DrawGui()
    {
        base.DrawGui();
        var reaction = (CalculateReaction) target;
        if (reaction.calculator == null)
            reaction.calculator = reaction.GetComponent<Calculator>();
        EditorGUILayout.PropertyField(_calculator);
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"calculator"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}