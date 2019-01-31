using System.Collections.Generic;
using Interaction.Reactions.Spectator;
using UnityEditor;

[CustomEditor(typeof(TeleportationReaction), true)]
public class TeleportationReactionEditor : ReactionEditor
{
    private SerializedProperty _teleportTo;

    protected override void LoadGui()
    {
        base.LoadGui();
        _teleportTo = serializedObject.FindProperty("teleportTo");
    }


    protected override void DrawGui()
    {
        base.DrawGui();
        var reaction = (TeleportationReaction) target;
        if (reaction.teleportTo == null)
            reaction.teleportTo = reaction.gameObject;
        EditorGUILayout.PropertyField(_teleportTo);
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"teleportTo"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}