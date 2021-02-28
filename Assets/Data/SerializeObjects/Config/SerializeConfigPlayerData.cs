using System;
using Components.ScriptableObjects.Item;
using UnityEngine;

namespace Data.SerializeObjects.Config
{
    [Serializable]
    public class SerializeConfigPlayerData
    {
        [SerializeField] public Sprite MoneySprite;
        [SerializeField] public InventoryData Inventory;
        [SerializeField] public InventoryData Shop;
        [SerializeField] public int money;
        [SerializeField] public string levelName;
    }
}