using Components.Notifications;
using Components.ScriptableObjects.Config;
using Controllers;
using Data.SerializeObjects.Config;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;

namespace DefaultNamespace
{
    public class Element : MonoBehaviour
    {
        // Gives access to the application and all instances.
        protected Application app => GameObject.FindObjectOfType<Application>();
    }
    public class Application : MonoBehaviour
    {
        [SerializeField]
        public ConfigData ConfigData;
        public void Notify(string pEventPath, Object pTarget, params object[] pData)
        {
            // Controller[] controller_list = GetAllControllers();
            // foreach(Controller c in controller_list)
            // {
            //     c.OnNotification(p_event_path,p_target,p_data);
            // }
            controller.OnNotification(pEventPath,pTarget,pData);
        }
        public void Save(object pData, string pName)
        {
            model.Save(pData,pName);
        }
        public object Load(string pName)
        {
            return model.Load(pName);
        }

        // Reference to the root instances of the MVC.
        public Model model;
        public View view;
        public Controller controller;
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Notify(Notification.SceneLoaded, this, new object[] {scene, mode});
        }
        
        // called first
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Delete(string name)
        {
            model.Delete(name);
        }

        public void RefreshEditorProjectWindow() 
        {
            #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
            #endif
        }
    }
}