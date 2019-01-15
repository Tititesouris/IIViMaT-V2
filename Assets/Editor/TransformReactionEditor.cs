using System.Collections.Generic;
using System.Linq;
using Interaction.Reactions.Transform;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformReaction), true)]
public class TransformReactionEditor : ReactionEditor
{
    protected override void DrawGui()
    {
        base.DrawGui();
        var reaction = (TransformReaction) target;
        var transformName = reaction.GetType() == typeof(PositionTransformReaction) ? "position" :
            reaction.GetType() == typeof(OrientationTransformReaction) ? "orientation" :
            reaction.GetType() == typeof(ScaleTransformReaction) ? "scale" :
            reaction.GetType() == typeof(SizeTransformReaction) ? "size" :
            reaction.GetType() == typeof(RotationTransformReaction) ? "rotation" :
            "UNKNOWN";

        var transformValuesLabel = new GUIContent(
            transformName.First().ToString().ToUpper() + transformName.Substring(1),
            "The object whose " + transformName + " the new " + transformName + " is calculated from.");
        reaction.transformValues = EditorGUILayout.Vector3Field(transformValuesLabel, reaction.transformValues);
        EditorGUILayout.HelpBox("X: Horizontal axis\n" +
                                "Y: Vertical axis\n" +
                                "Z: Depth axis", MessageType.Info);

        var relativeToLabel = new GUIContent("Relative to",
            "Select which object " + transformName + " the new " + transformName + " is relative to:\n" +
            "World: Absolute values\n" +
            "Self: Relative to the current " + transformName + "\n" +
            "Object: Relative to an object's " + transformName + "\n" +
            "Actor: Relative to the " + transformName + " of the actor that triggered the action\n" +
            "Camera: Relative to the " + transformName + " of the camera"
        );
        reaction.relativeTo =
            (TransformReaction.RelativeToOptions) EditorGUILayout.EnumPopup(relativeToLabel, reaction.relativeTo);

        if (reaction.relativeTo != TransformReaction.RelativeToOptions.World)
        {
            EditorGUI.indentLevel++;
            
            if (reaction.relativeTo == TransformReaction.RelativeToOptions.Object)
            {
                var relativeObjectLabel = new GUIContent("Reference object",
                    "The object whose " + transformName + " the new " + transformName + " is calculated from.");
                reaction.referenceObject = (GameObject) EditorGUILayout.ObjectField(relativeObjectLabel,
                    reaction.referenceObject, typeof(GameObject), true);
            }

            if (reaction.GetType() == typeof(PositionTransformReaction))
            {
                var positionReaction = (PositionTransformReaction) reaction;
                var relativeHeadingLabel = new GUIContent("Relative heading",
                    "If enabled, the new position will be relative to the orientation as well.");
                positionReaction.relativeHeading =
                    EditorGUILayout.Toggle(relativeHeadingLabel, positionReaction.relativeHeading);
            }

            EditorGUI.indentLevel--;
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"transformValues", "relativeTo", "referenceObject", "relativeHeading"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}