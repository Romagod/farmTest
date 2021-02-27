using System;
using Components.ScriptableObjects.Item;
using Data.SerializeObjects;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Components.Ui
{
    public class CellModalComponent : MonoBehaviour
    {
        public Action OnCloseBtnClick;
        public Action<string> OnBtnClick;
        
        public Text TextHeader;
        public Text TextBody;
        public Text resourceValue;
        public Text speedValue;
        public Text buttonText;
        public Text foodCount;
        public Text feedText;
        
        [HideInInspector]
        public int countOfFood;
        public string foodName;

        private void Start()
        {
            gameObject.SetActive(false);
        }
        

        public void OpenModal(string textHeader, string textBody)
        
        {
            TextHeader.text = textHeader;
            if (TextBody != null)
                TextBody.text = textBody;
            gameObject.SetActive(true);
        }

        public void OnCloseBtn()
        {
            gameObject.SetActive(false);
            OnCloseBtnClick();
        }
        
        public void OnSomeBtnClick(string notificationName)
        {
            OnBtnClick(notificationName);
        }
        
        public void OpenCell(string textHeader, string textResource, string textSpeed)
        
        {
            TextHeader.text = textHeader;
            if (resourceValue != null)
                resourceValue.text = textResource;
            if (speedValue != null)
                speedValue.text = textSpeed;
            gameObject.SetActive(true);
        }
        
        public void OpenCell(string textHeader, ItemData data, int countFood)
        
        {
            TextHeader.text = textHeader;
            if (resourceValue != null)
                resourceValue.text = data.Name;
            if (speedValue != null)
                speedValue.text = $"{data.Speed}";
            if (foodCount != null)
                foodCount.text = $"{countFood}";
            foodName = data.FoodItem == null ? null : data.FoodItem.Name;
            feedText.text = $"Покормить {foodName}";
            gameObject.SetActive(true);
        }

        public void CheckHand(object hand = null)
        
        {
            if (hand == null)
            {
                buttonText.text = "Переместить";
                return;
            }

            var data = (ItemData) hand;

            buttonText.text = $"Разместить {data.Name} на клетке";
        }

        public void Close()
        {
            OnCloseBtn();
        }
    }
}