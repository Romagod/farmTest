using System;
using Components.ScriptableObjects.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Ui
{
    public class ModalComponent : MonoBehaviour
    {
        public Action OnCloseBtnClick;
        public Action<string> OnBtnClick;
        
        public Text TextHeader;
        public Text TextBody;

        protected virtual void Start()
        {
            // gameObject.SetActive(false);
        }
        

        public virtual void OpenModal(string textHeader, string textBody)
        
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

        public void Close()
        {
            OnCloseBtn();
        }

        public void OpenWelcomeModal(string textHeader, LevelConfigData gameLevelConfig)
        {
            TextHeader.text = textHeader;
            if (TextBody != null)
                TextBody.text = gameLevelConfig.WelcomeText;
            
            gameObject.SetActive(true);
        }
    }
}