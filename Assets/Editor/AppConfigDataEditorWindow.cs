using Components.ScriptableObjects.Config;
using Data.SerializeObjects.Config;
using UnityEditor;
using UnityEngine;

public class AppConfigDataEditorWindow : ExtendedEditorWindow
{
    // [MenuItem("Application/Create new Config Data")]
    public static void ShowWindow(ConfigData data)
    {
        var window = GetWindow<AppConfigDataEditorWindow>();
        window.serializedObject = new SerializedObject(data);
        // window.titleContent = new GUIContent("Настройки игры");
        // window.Show();
    }

    private void OnGUI()
    {
        var currentProperty = serializedObject.FindProperty("configData");

        EditorGUILayout.BeginHorizontal();
        
            EditorGUILayout.BeginVertical(
                "box", 
                GUILayout.MaxWidth(150), 
                GUILayout.ExpandHeight(true)
            );
                DrawSidebar(currentProperty);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(
                "box", 
                GUILayout.ExpandHeight(true)
            );
                if (selectedProperty != null)
                {
                    DrawSelectedPropPanel();
                    Apply();
                }
                else
                {
                    EditorGUILayout.LabelField("Требуется выбрать элемент в левой панели, который будем редактировать");
                }
            EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    void DrawSelectedPropPanel()
    {
        currentProperty = selectedProperty;
        if (currentProperty.displayName == "Field")
        {
            EditorGUILayout.BeginVertical("box");
                // DrawProps(currentProperty, true);
                DrawField("CountOfCellsWidth", true);
                DrawField("CountOfCellsHeight", true);
                DrawField("Cell", true);
            EditorGUILayout.EndVertical();
        }

        if (currentProperty.displayName == "Player")
        {
            EditorGUILayout.BeginVertical("box");
                // DrawProps(currentProperty, true);
                DrawField("MoneySprite", true);
                DrawField("Inventory", true);
                DrawField("Shop", true);
            EditorGUILayout.EndVertical();
            
        }

        if (currentProperty.displayName == "Game")
        {
            EditorGUILayout.BeginVertical("box");
            // DrawProps(currentProperty, true);
                DrawField("LevelConfig", true);
                DrawField("Resources", true);
            EditorGUILayout.EndVertical();
            
        }
        
    }
}
