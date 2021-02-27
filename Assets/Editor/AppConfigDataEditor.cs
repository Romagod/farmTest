namespace Editor
{
    using Components.ScriptableObjects.Config;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ConfigData))]
    public class AppConfigDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open editor"))
            {
                AppConfigDataEditorWindow.ShowWindow((ConfigData) target);
            }
            base.OnInspectorGUI();
        }
    }
}