using System;
using UnityEngine;

namespace Components.ScriptableObjects.Item
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Items/Add New Item Data", order = 1)]
    public class ItemData : ScriptableObject
    {
        [Tooltip("Main Sprite")]
        [SerializeField] private Sprite mainSprite;
        public Sprite MainSprite
        {
            get => mainSprite;
            protected set => mainSprite = value;
        }
        
        
        [Tooltip("Name")]
        [SerializeField] private string itemName;
        public string Name
        {
            get => itemName;
            protected set => itemName = value;
        }
        
        
        [Tooltip("Food Item")]
        [SerializeField] private ItemData foodItem;
        public ItemData FoodItem
        {
            get => foodItem;
            protected set => foodItem = value;
        }
        
        
        [Tooltip("Generated Item")]
        [SerializeField] private ItemData generatedItem;
        public ItemData GeneratedItem
        {
            get => generatedItem;
            protected set => generatedItem = value;
        }
    
        [Tooltip("Speed")]
        [Range(1.00f, 100.00f)]
        [SerializeField] private float resourceSpeed;
        public float Speed
        {
            get => resourceSpeed;
            protected set => resourceSpeed = value;
        }
    
        [Tooltip("Eating Speed")]
        [Range(1.00f, 100.00f)]
        [SerializeField] private float eatingSpeed;
        public float EatingSpeed
        {
            get => eatingSpeed;
            protected set => eatingSpeed = value;
        }
    
        [Tooltip("Buy price")]
        [Range(1, 1000)]
        [SerializeField] private int buyPrice;
        public int BuyPrice
        {
            get => buyPrice;
            protected set => buyPrice = value;
        }
    
        [Tooltip("Sell price")]
        [Range(1, 1000)]
        [SerializeField] private int sellPrice;
        public int SellPrice
        {
            get => sellPrice;
            protected set => buyPrice = value;
        }
    }
}