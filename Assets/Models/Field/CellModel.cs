using System;
using System.Collections.Generic;
using Components.Field;
using Components.Notifications;
using Components.ScriptableObjects.Field;
using Components.ScriptableObjects.Item;
using Data.SerializeObjects;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Field
{
    public class CellModel : Element
    {
        private CellData _data;
        
        public Vector2 Size
        {
            get => _data.Size;
            private set {}
        }
        
        [NotNull]
        public Sprite TopSprite
        {
            get => _topSprite ? _topSprite : _data.TopSprite;
            private set => _topSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        [NotNull]
        public Sprite RightSprite
        {
            get => _rightSprite ? _rightSprite : _data.RightSprite;
            private set => _rightSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        [NotNull]
        public Sprite LeftSprite
        {
            get => _leftSprite ? _leftSprite : _data.LeftSprite;
            private set => _leftSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        [NotNull]
        public Sprite AllSprite
        {
            get => _allSprite ? _allSprite : _data.AllSprite;
            private set => _allSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        [NotNull]
        public Sprite ActiveTopSprite
        {
            get => _activeTopSprite ? _activeTopSprite : _data.ActiveTopSprite;
            private set => _activeTopSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        [NotNull]
        public Sprite ActiveRightSprite
        {
            get => _activeRightSprite ? _activeRightSprite : _data.ActiveRightSprite;
            private set => _activeRightSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        [NotNull]
        public Sprite ActiveLeftSprite
        {
            get => _activeLeftSprite ? _activeLeftSprite : _data.ActiveLeftSprite;
            private set => _activeLeftSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        [NotNull]
        public Sprite ActiveAllSprite
        {
            get => _activeAllSprite ? _activeAllSprite : _data.ActiveAllSprite;
            private set => _activeAllSprite = value ? value : throw new ArgumentNullException(nameof(value));
        }
        
        // [NotNull]
        // public Sprite MainSprite
        // {
        //     get => _mainSprite ? _mainSprite : _data.MainSprite;
        //     private set => _mainSprite = value ? value : throw new ArgumentNullException(nameof(value));
        // }
        // [NotNull]
        // public Sprite ActiveSprite
        // {
        //     get => _activeSprite ? _activeSprite : _data.ActiveSprite;
        //     private set => _activeSprite = value ? value : throw new ArgumentNullException(nameof(value));
        // }
        public string ResourceName
        {
            get => (_resourceName ?? (_data == null ? null : _data.ResourceName)) ?? "Нет";
            private set => _resourceName = value == "" ? "Нет" : value;
        }
        
        public float ResourceSpeed
        {
            get => float.IsNaN(_resourceSpeed) ? _data.ResourceSpeed : _resourceSpeed;
            private set => _resourceSpeed = value;
        }

        public int foodCount = 0;
        
        private Sprite _topSprite;
        private Sprite _rightSprite;
        private Sprite _leftSprite;
        private Sprite _allSprite;
        private Sprite _activeTopSprite;
        private Sprite _activeRightSprite;
        private Sprite _activeLeftSprite;
        private Sprite _activeAllSprite;
        // private Sprite _mainSprite;
        // private Sprite _activeSprite;
        private string _resourceName;
        private float _resourceSpeed;
        private Dictionary<string, int> _resources = new Dictionary<string, int>();

        public void LoadCellModel(CellData cellData)
        {
            _data = cellData;
        }

        public void SayHello()
        {
        }

        public void SetResource(ItemData itemData)
        {
            ResourceSpeed = itemData.Speed;
            ResourceName = itemData.Name;
            Size = _data.Size;
            TopSprite = _data.TopSprite;
            RightSprite = _data.RightSprite;
            LeftSprite = _data.LeftSprite;
            AllSprite = _data.AllSprite;
            ActiveTopSprite = _data.ActiveTopSprite;
            ActiveRightSprite = _data.ActiveRightSprite;
            ActiveLeftSprite = _data.ActiveLeftSprite;
            ActiveAllSprite = _data.ActiveAllSprite;
            gameObject.GetComponent<Cell>().SetItemSprite(itemData.MainSprite);
            gameObject.GetComponent<Cell>().StartGenerateResource(itemData);
            gameObject.GetComponent<Cell>().countOfFood = foodCount;
            // app.Notify(Notification.ModalCellReload, this, new object[] {itemData.Name});
        }

        public void DeleteResource()
        {
            ResourceSpeed = 0.00f;
            ResourceName = "";
            gameObject.GetComponent<Cell>().SetItemSprite(null);
            gameObject.GetComponent<Cell>().StopGenerateResource();
            // app.Notify(Notification.ModalCellReload, this, new object[] {itemData.Name});
        }

        public void Load(SerializeCellData itemData)
        {
            
            if (app.model.ResourceExists(itemData.ResourceName))
            {
                var resource = app.model.ResourcesDictionary[itemData.ResourceName];
                foodCount = itemData.foodCount;
                SetResource(resource);
                NewResource(itemData.Count);
            }
        }

        public void NewResource(int data)
        {
            if (!app.model.ResourceExists(ResourceName)) return;
            var item = app.model.ResourcesDictionary[ResourceName];
            if (item.GeneratedItem != null)
            {
                AddToResources(item.GeneratedItem.Name, data);
            }

        }

        private void AddToResources(string key, int value)
        {
            if (_resources.ContainsKey(key))
            {
                _resources[key] += value;
            }
            else
            {
                _resources.Add(key,value);
            }
        }

        public Dictionary<string, int> NewResourcesInfo()
        {
            return _resources;
        }
        
        public void PickUpResource()
        {
            foreach(string key in _resources.Keys)
            {
                app.model.player.AddResourceToInventory(key, _resources[key]);
            }

            _resources = new Dictionary<string, int>();
        }
        
        public int ResourcesCount()
        {

            return _resources.Count;
        }
        
    }
    
    
}