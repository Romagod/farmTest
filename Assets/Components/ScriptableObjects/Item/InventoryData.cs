using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.ScriptableObjects.Item
{
    [Serializable]
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/Items/Add New Inventory", order = 1)]
    public class InventoryData : ScriptableObject
    {
        [Tooltip("Items")]
        [SerializeField]
        private List<InventoryItemData> items;
        public List<InventoryItemData> Items
        {
            get => items;
            protected set => items = value;
        }
    }
}