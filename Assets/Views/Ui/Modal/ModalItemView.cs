using Components.ScriptableObjects.Item;
using DefaultNamespace;
using Models.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Ui.Modal
{
    public class ModalItemView : Element
    {
        public Text itemName;
        public Text itemPrice;
        public Text itemSpeed;

        [HideInInspector] public ItemData _itemData;
        public int id;
        
        public void RenderInfo(ItemData itemData)
        {
            _itemData = itemData;
            itemName.text = _itemData.Name;
            itemPrice.text = $"{_itemData.SellPrice} монет";
            itemSpeed.text = $"{_itemData.Speed} сек.";
            gameObject.SetActive(true);
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

        public void GenerateId()
        {
            id = Random.Range(1, 100 * 100 * 100);
        }
    }
}