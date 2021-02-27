using UnityEditor;
using UnityEngine;


public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;

    private string selectedPropertyPath;
    protected SerializedProperty selectedProperty;
    protected void DrawProps(SerializedProperty property, bool drawChildren)
    {
        var lastPropPath = string.Empty;
        foreach (SerializedProperty prop in property)
        {
            if (prop.isExpanded && drawChildren)
            {
                EditorGUILayout.BeginHorizontal();
                prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, prop.displayName);
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel++;
                DrawProps(prop, drawChildren);
                EditorGUI.indentLevel--;
                
            }
            else 
            {
                if (!string.IsNullOrEmpty(lastPropPath) && prop.propertyPath.Contains(lastPropPath))
                    continue;
                lastPropPath = prop.propertyPath;
                
                EditorGUILayout.PropertyField(prop, drawChildren);
                
            }
        }
    }

    protected void DrawSidebar(SerializedProperty property)
    {
        foreach (SerializedProperty prop in property)
        {
            var path = prop.propertyPath.Split('.');
            if (path.Length > 2) continue;
            if (GUILayout.Button(property.displayName))
            {
                selectedPropertyPath = prop.propertyPath;
            }
        }
        if (!string.IsNullOrEmpty(selectedPropertyPath))
        {
            selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
        }
    }

    protected void DrawField(string propName, bool relative)
    {
        if (relative && currentProperty != null)
        {
            EditorGUILayout.PropertyField(currentProperty.FindPropertyRelative(propName), true);
        } else if (serializedObject != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(propName), true);
        }
    }
    
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
    
}
