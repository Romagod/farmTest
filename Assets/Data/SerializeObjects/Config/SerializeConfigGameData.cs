using System;
using System.Collections.Generic;
using Components.ScriptableObjects.Item;
using Components.ScriptableObjects.Level;
using UnityEngine;

namespace Data.SerializeObjects.Config
{
    [Serializable]
    public class SerializeConfigGameData
    {
        [SerializeField] public LevelConfigData LevelConfig;
        [SerializeField] public List<ItemData> Resources;
    }
}