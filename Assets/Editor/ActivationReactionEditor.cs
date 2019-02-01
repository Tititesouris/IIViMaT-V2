using System.Collections.Generic;
using Interaction.Reactions.Meta;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActivationReaction), true)]
public class ActivationReactionEditor : ReactionEditor
{
    private SerializedProperty _targets;

    protected override void LoadGui()
    {
        base.LoadGui();
        _targets = serializedObject.FindProperty("targets");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        ListEditor.Show(_targets, typeof(GameObject), "Target", "No target specified!",
            "Some targets are not specified!");
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"targets"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}