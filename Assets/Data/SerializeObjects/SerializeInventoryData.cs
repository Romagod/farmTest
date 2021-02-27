using System;
using System.Collections.Generic;
using Components.ScriptableObjects.Item;
using UnityEngine;

namespace Data.SerializeObjects
{
    [Serializable]
    public class SerializeInventoryData
    {
        [SerializeField]
        public List<SerializeInventoryItemData> Items = new List<SerializeInventoryItemData>();

        public void LoadInventory(List<InventoryItemData> items)
        {
            foreach (var item in items)
            {
                var itemData = new SerializeInventoryItemData();
                itemData.LoadInventoryItem(item);
                Items.Add(itemData);
            }
        }
        public void LoadItems(List<SerializeInventoryItemData> items)
        {
            foreach (var item in items)
            {
                var itemData = new SerializeInventoryItemData();
                itemData.LoadItem(item);
                Items.Add(itemData);
            }
        }
        
        [Serializable]
        public class SerializeInventoryItemData
        {
            public int Count;
            public SerializeItemData itemData;

            public void LoadItem(SerializeInventoryItemData item)
            {
                Count = item.Count;
                itemData = new SerializeItemData();
                itemData.Name = item.itemData.Name;
                itemData.FoodName = item.itemData.FoodName;
                itemData.GeneratedName = item.itemData.GeneratedName;
                itemData.EatingSpeed = item.itemData.EatingSpeed;
                itemData.Speed = item.itemData.Speed;
                itemData.BuyPrice = item.itemData.BuyPrice;
                itemData.SellPrice = item.itemData.SellPrice;
            }
            public void LoadInventoryItem(InventoryItemData item)
            {
                Count = item.Count;
                itemData = new SerializeItemData();
                itemData.Name = item.ItemData.Name;
                itemData.FoodName = item.ItemData.FoodItem == null ? "" : item.ItemData.FoodItem.Name;
                itemData.GeneratedName = item.ItemData.FoodItem == null ? "" : item.ItemData.FoodItem.Name;
                itemData.Speed = item.ItemData.Speed;
                itemData.EatingSpeed = item.ItemData.EatingSpeed;
                itemData.BuyPrice = item.ItemData.BuyPrice;
                itemData.SellPrice = item.ItemData.SellPrice;
            }
            
        }
    }
}