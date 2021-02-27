using System;

namespace Data.SerializeObjects
{
    [Serializable]
    public class SerializeItemData
    {
        public string Name;
        public string FoodName;
        public string GeneratedName;
        public float Speed;
        public float EatingSpeed;
        public int BuyPrice;
        public int SellPrice;
    }
}