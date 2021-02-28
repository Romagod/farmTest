using System;
using System.Collections.Generic;
using System.Linq;
using Components.Notifications;
using Components.ScriptableObjects.Item;
using Data.SerializeObjects;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Models
{
    public class PlayerModel : Element
    {
        public Sprite moneySprite;
        public InventoryData inventory;
        public InventoryData shop;
        
        [HideInInspector]
        public SerializeInventoryData serializeInventory = new SerializeInventoryData();
        [HideInInspector]
        public SerializeInventoryData serializeShop = new SerializeInventoryData();
        [HideInInspector]
        public SerializePlayerData serializePlayer = new SerializePlayerData();
        [HideInInspector]
        public int money = 0;
        [HideInInspector]
        public string levelName;
        
        
        [HideInInspector] 
        private object _hand;

        public void SetLevelName(string name)
        {
            levelName = name;
            serializePlayer.levelName = levelName;
            app.Save(serializePlayer, "player");
        }

        public void SetMoney(int count)
        {
            money = count;
            serializePlayer.money = count;
            app.Save(serializePlayer, "player");
            app.Notify(Notification.GameMoney, this);
        }

        public void PutToHand(object data = null)
        {
            _hand = data;
        }

        public object GetHand()
        {
            return _hand;
        }

        public void DeleteSavedData()
        {
            app.Delete("inventory");
            app.Delete("player");
            app.Delete("shop");
            app.Delete("field");
        }

        public void AddResourceToInventory(string name, int count)
        {
            bool added = false;
            foreach (var item in serializeInventory.Items)
            {
                if (item.itemData.Name == name)
                {
                    item.Count += count;
                    added = true;
                }
            }

            if (!added)
            {
                if (!app.model.ResourceExists(name)) return;
                var item = app.model.ResourcesDictionary[name];
                var itemData = new SerializeItemData
                {
                    Name = item.Name,
                    FoodName = item.FoodItem == null ? null : item.FoodItem.Name,
                    GeneratedName = item.GeneratedItem == null ? null : item.GeneratedItem.Name,
                    EatingSpeed = item.EatingSpeed,
                    Speed = item.Speed,
                    BuyPrice = item.BuyPrice,
                    SellPrice = item.SellPrice
                };
                var inventoryItemData = new SerializeInventoryData.SerializeInventoryItemData
                {
                    Count = count, itemData = itemData
                };


                serializeInventory.Items.Add(inventoryItemData);
            }
            var data = new SerializeInventoryData();
            data.LoadItems(serializeInventory.Items);
            app.Save(data, "inventory");
            app.Notify(Notification.GameCheckGoals, this);
        }

        private void Start()
        {
            moneySprite = app.ConfigData.configData.Player.MoneySprite;
            inventory = app.ConfigData.configData.Player.Inventory;
            shop = app.ConfigData.configData.Player.Shop;
            money = app.ConfigData.configData.Player.money;
            levelName = app.ConfigData.configData.Player.levelName;
            LoadInventory();
            LoadPlayer();
            LoadShop();
            app.Notify(Notification.ModalOpen, this);
        }

        private void LoadInventory()
        {
            var inventoryModel = (SerializeInventoryData) app.Load("inventory");
            if (inventoryModel == null)
            {
                inventoryModel = new SerializeInventoryData();
                inventoryModel.LoadInventory(app.ConfigData.configData.Player.Inventory.Items);
                app.Save(inventoryModel, "inventory");
            }
            
            serializeInventory.LoadItems(inventoryModel.Items);
        }
        private void LoadShop()
        {
            var inventoryModel = (SerializeInventoryData) app.Load("shop");
            if (inventoryModel == null)
            {
                inventoryModel = new SerializeInventoryData();
                inventoryModel.LoadInventory(app.ConfigData.configData.Player.Shop.Items);
                app.Save(inventoryModel, "shop");
            }
            
            serializeShop.LoadItems(inventoryModel.Items);
        }
        private void LoadPlayer()
        {
            serializePlayer = (SerializePlayerData) app.Load("player");
            if (serializePlayer == null)
            {
                serializePlayer = new SerializePlayerData();
                serializePlayer.money = money;
                serializePlayer.levelName = levelName;
            }
            SetMoney(serializePlayer.money);
            SetLevelName(serializePlayer.levelName);
        }

        public void DeleteResource(string foodItemName, int i)
        {
            foreach (var item in serializeInventory.Items)
            {
                if (item.itemData.Name == foodItemName)
                {
                    item.Count -= i;
                }
            }

            SerializeInventoryData data = new SerializeInventoryData();
            data.LoadItems(serializeInventory.Items);
            app.Save(data, "inventory");
        }

        public int GetResourceCount(string foodItemName)
        {
            var result = 0;
            var items = serializeInventory.Items.Where(item => item.itemData.Name == foodItemName);
            foreach (var item in items.ToArray())
            {
                result = item.Count;
            }
            return result;
        }

        public string CountOfItem(string itemName)
        {
            if (itemName == "Монеты")
                return money.ToString();
            var item = serializeInventory.Items.FindLast(elem => elem.itemData.Name == itemName);
            if (item == null)
                return "0";
            return item.Count.ToString();
        }

        public int CountOfItemInt(string itemName)
        {
            if (itemName == "Монеты")
                return money;
            var item = serializeInventory.Items.FindLast(elem => elem.itemData.Name == itemName);
            if (item == null)
                return 0;
            return item.Count;
        }
    }
}