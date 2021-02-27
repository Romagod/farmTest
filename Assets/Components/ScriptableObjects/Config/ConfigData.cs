using Data.SerializeObjects.Config;
using UnityEngine;

namespace Components.ScriptableObjects.Config
{
    [CreateAssetMenu(fileName = "ConfigData", menuName = "Application/Config/Create New Config Data", order = 0)]
    public class ConfigData : ScriptableObject
    {
        public SerializeConfigData configData;
    }
}