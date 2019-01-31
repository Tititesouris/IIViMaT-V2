using System.Collections.Generic;
using UnityEditor;

public abstract class IivimatEditor : Editor
{
    
    private void OnEnable()
    {
        LoadGui();
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawGui();

        var excludedProperties = new List<string> {"m_Script"};
        excludedProperties.AddRange(GetIgnoredFields());

        DrawPropertiesExcluding(serializedObject, excludedProperties.ToArray());

        serializedObject.ApplyModifiedProperties();
        EditorApplication.update.Invoke();
    }

    protected abstract void LoadGui();
    
    protected abstract void DrawGui();

    protected abstract IEnumerable<string> GetIgnoredFields();
}