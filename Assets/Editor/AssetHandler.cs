using Components.ScriptableObjects.Config;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Editor
{
    public class AssetHandler
    {
        [OnOpenAsset()]
        public static bool OpenEditor(int id, int line)
        {
            ConfigData obj = EditorUtility.InstanceIDToObject(id) as ConfigData;

            if (obj != null)
            {
                AppConfigDataEditorWindow.ShowWindow(obj);
            
                return true;
            }

            return false;
        }
    }
}