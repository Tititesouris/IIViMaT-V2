using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ListEditor
{
    public static void Show(SerializedProperty list, string elementName, string noElementError,
        string emptyElementWarning)
    {
        if (!list.isArray)
        {
            EditorGUILayout.HelpBox(list.name + " is not a list", MessageType.Error);
            return;
        }

        if (list.arraySize == 0)
            EditorGUILayout.HelpBox(noElementError, MessageType.Error);
        else if (AnyEmptyElement(list))
            EditorGUILayout.HelpBox(emptyElementWarning, MessageType.Warning);

        EditorGUILayout.PropertyField(list, new GUIContent(list.displayName + " (" + list.arraySize + ")"));
        if (list.isExpanded)
        {
            EditorGUI.indentLevel++;
            for (var i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i),
                    new GUIContent(elementName + " " + (i + 1)));
                if (GUILayout.Button(new GUIContent("X"), GUILayout.Width(18f)))
                {
                    list.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button(new GUIContent("+"), GUILayout.Width(EditorGUIUtility.currentViewWidth / 2f)))
                list.InsertArrayElementAtIndex(list.arraySize);

            EditorGUI.indentLevel--;
        }
    }

    private static bool AnyEmptyElement(SerializedProperty list)
    {
        for (var i = 0; i < list.arraySize; i++)
            if (list.GetArrayElementAtIndex(i).stringValue.Length == 0)
                return true;
        return false;
    }
}