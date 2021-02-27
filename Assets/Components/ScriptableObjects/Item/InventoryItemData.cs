using System;
using UnityEngine;

namespace Components.ScriptableObjects.Item
{
    [Serializable]
    [CreateAssetMenu(fileName = "InventoryItemData", menuName = "Inventory/Items/Add New Item For Inventory", order = 1)]
    public class InventoryItemData : ScriptableObject
    {
        [Tooltip("Count")]
        [SerializeField] private int count;
        public int Count
        {
            get => count;
            set => count = value;
        }
        
        [Tooltip("Item")]
        [SerializeField] private ItemData itemData;
        public ItemData ItemData
        {
            get => itemData;
            protected set => itemData = value;
        }
    }
}