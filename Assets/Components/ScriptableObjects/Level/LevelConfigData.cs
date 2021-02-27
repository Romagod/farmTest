using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.ScriptableObjects.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Level Config/Create New Config", order = 2)]
    public class LevelConfigData : ScriptableObject
    {
        [Tooltip("Scene Name")]
        [SerializeField]
        private string sceneName;
        public string SceneName
        {
            get => sceneName;
            protected set => sceneName = value;
        }
        
        [Tooltip("Goals")]
        [SerializeField]
        private List<GoalData> goals;
        public List<GoalData> Goals
        {
            get => goals;
            protected set => goals = value;
        }
        
        [Tooltip("Next Scene Name")]
        [SerializeField]
        private string nextSceneName;
        public string NextSceneName
        {
            get => nextSceneName;
            protected set => nextSceneName = value;
        }
        
        [Tooltip("Welcome Text")]
        [SerializeField]
        private string welcomeText;
        public string WelcomeText
        {
            get => welcomeText;
            protected set => welcomeText = value;
        }
    }
}