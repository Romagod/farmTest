using System;
using Components.ScriptableObjects.Config;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Data.SerializeObjects.Config
{
    [Serializable]
    public class SerializeConfigData
    {
        [SerializeField] public SerializeConfigFieldData Field;
        [SerializeField] public SerializeConfigPlayerData Player;
        [SerializeField] public SerializeConfigGameData Game;
    }
}