using System.Collections.Generic;
using System.Xml.Linq;
using Components.ScriptableObjects.Item;
using Data.SerializeObjects;
using DefaultNamespace;
using Models.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Ui
{
    public class ModalModel : Element
    {
        public string headerText;
        public string bodyText;
        public string resourceText;
        public string speedText;
        public GameObject itemsContainer;
        
        [HideInInspector] 
        public SerializeItemData value;
        public Dictionary<int, ItemModel> Items;
        [HideInInspector]
        public List<ItemModel> itemModels;
        

        public void CreateItemModels()
        {
            if (itemModels.Count > 0)
            {
                itemModels = new List<ItemModel>();
                ClearContainer();
            }
            var data = app.model.player.serializeInventory.Items;
            foreach (var item in data)
            {
                if (item == null) continue;
                var i = 1;
                
                while (i <= item.Count)
                {
                    var model = Instantiate(gameObject, itemsContainer.transform ).AddComponent<ItemModel>();
                    model.itemInfo = item.itemData;
                    itemModels.Add(model);
                    i++;
                }

            }
        }

        public void RemoveItem(string name)
        {
            foreach (var item in app.model.player.serializeInventory.Items)
            {
                if (item.itemData.Name == name)
                {
                    item.Count--;
                };
            }
            SerializeInventoryData data = new SerializeInventoryData();
            data.LoadItems(app.model.player.serializeInventory.Items);
            app.Save(data, "inventory");
        }
        
        private void ClearContainer()
        {
            foreach (Transform child in itemsContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}