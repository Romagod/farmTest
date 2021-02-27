using Components.ScriptableObjects.Item;
using Components.ScriptableObjects.Level;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Ui.Modal
{
    public class ModalGoalView : Element
    {
        public RawImage itemImage;
        public Text itemName;
        public Text itemCount; 

        [HideInInspector] public GoalData _itemData;
        
        public void RenderInfo(GoalData itemData)
        {
            _itemData = itemData;
            itemName.text = itemData.IsMoney ? "Монеты" : itemData.Item.Name;
            itemCount.text = $"{app.model.player.CountOfItem(itemData.IsMoney ? "Монеты" : itemData.Item.Name)} из {itemData.Count}";
            itemImage.texture = !itemData.IsMoney ? itemData.Item.MainSprite.texture : app.model.player.moneySprite.texture;
        }

        public void RemoveElement()
        {
            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o);
        }
        
        public void Action(string action) 
        {
            app.Notify(action,this);
        }

        private void Start()
        {
            // GenerateId();
        }
    }
}