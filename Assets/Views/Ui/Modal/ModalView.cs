using System.Collections.Generic;
using Components.ScriptableObjects.Level;
using Components.Ui;
using DefaultNamespace;
using Models.Items;
using UnityEngine;

namespace Views.Ui.Modal
{
    public class ModalView : Element
    {
        public ModalItemsComponent modalItemsContainer;
        public ModalGoalView firstItem;
        
        [HideInInspector]
        private ModalComponent _modalComponent;
        private CellModalComponent _cellModalComponent;

        public void Open(string textHeader, LevelConfigData gameLevelConfig)
        {
            if (_modalComponent == null)
                _modalComponent = gameObject.GetComponent<ModalComponent>();
            
            if (_modalComponent != null)
                _modalComponent.OpenWelcomeModal(textHeader, gameLevelConfig);
        }
        

        private void ModalClose()
        {
            _modalComponent.Close();
        }

        private void ModalOnClose()
        {
            app.view.ui.ModalIsOpen = false;
        }

        private void ModalAction(string action)
        {
            app.Notify(action,this);
        }
        private void OnEnable()
        {
            if (_modalComponent == null) return;
            _modalComponent.OnCloseBtnClick += ModalOnClose;
            _modalComponent.OnBtnClick += ModalAction;

        }
        public void GenerateGoals()
        {
            var data = app.ConfigData.configData.Game.LevelConfig.Goals;
            var i = 1;
            foreach (var item in data)
            {
                if (item == null) continue;
                
                
                ModalGoalView view;
                if (i == 1)
                {
                    view = firstItem;
                }
                else
                {
                    var go = Instantiate(firstItem.gameObject, modalItemsContainer.transform );
                    view = go.GetComponent<ModalGoalView>();
                }
                        
                view.RenderInfo(item);
                i++;

            }
        }
        
        public void ClearGoals()
        {
            if (modalItemsContainer == null)
            {
                return;
            }
            var i = 0;
            foreach (Transform child in modalItemsContainer.transform)
            {
                if (i != 0)
                    Destroy(child.gameObject);
                else
                    firstItem = child.gameObject.GetComponent<ModalGoalView>();
                i++;
            }
        }

        private void OnCloseBtnClick()
        {
            ClearGoals();
            app.view.ui.ModalIsOpen = false;
        }

        private void Start()
        {
            _modalComponent = gameObject.GetComponent<ModalComponent>();
            _modalComponent.OnCloseBtnClick += OnCloseBtnClick;
        }
    }
}