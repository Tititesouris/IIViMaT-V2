using System.Collections.Generic;
using System.Linq;
using Interaction.Reactions.Appearance;
using Interaction.Reactions.Transform;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorReaction), true)]
public class ColorReactionEditor : ReactionEditor
{
    private SerializedProperty _randomColor;

    private SerializedProperty _color;

    protected override void LoadGui()
    {
        base.LoadGui();
        _randomColor = serializedObject.FindProperty("randomColor");
        _color = serializedObject.FindProperty("color");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        var reaction = (ColorReaction) target;
        EditorGUILayout.PropertyField(_randomColor);
        if (!reaction.randomColor)
            EditorGUILayout.PropertyField(_color);
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"randomColor", "color"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}