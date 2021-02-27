using System.Collections.Generic;
using Components.Ui;
using DefaultNamespace;
using Models.Field;
using Models.Items;
using UnityEngine;
using Views.Cell;

namespace Views.Ui.Modal
{
    public class ModalCellView : Element
    {
        public ModalItemsComponent modalItemsContainer;
        public ModalItemView firstItem;
        
        [HideInInspector]
        private ModalComponent _modalComponent;
        private CellModalComponent _cellModalComponent;

        public void Open(string textHeader, string textBody = "")
        {
            app.view.ui.IsOpen(true);
            if (_modalComponent == null)
                _modalComponent = gameObject.GetComponent<ModalComponent>();
            if (_cellModalComponent == null)
                _cellModalComponent = gameObject.GetComponent<CellModalComponent>();
            if (_cellModalComponent == null)
                _modalComponent.OpenModal(textHeader, textBody);
            if (_modalComponent == null)
                _cellModalComponent.OpenModal(textHeader, textBody);
        }
        public void OpenCell(string textHeader, string textResource, string textSpeed)
        {
            app.view.ui.IsOpen(true);
            if (_cellModalComponent == null)
                _cellModalComponent = gameObject.GetComponent<CellModalComponent>();
            _cellModalComponent.CheckHand(app.model.player.GetHand());
            _cellModalComponent.OpenCell(textHeader, textResource, textSpeed);
        }
        public void OpenCell(string textHeader, object item)
        {
            app.view.ui.IsOpen(true);
            if (_cellModalComponent == null)
                _cellModalComponent = gameObject.GetComponent<CellModalComponent>();
            _cellModalComponent.CheckHand(app.model.player.GetHand());
            var model = (CellModel) item;
            var cellView = (CellView) app.model.field.activeCell;
            var cell = cellView.GetComponent<Components.Field.Cell>();

            if (!app.model.ResourceExists(model.ResourceName))
            {
                _cellModalComponent.OpenCell(textHeader, model.ResourceName, $"{model.ResourceSpeed}");
                return;
            }
                    
            var resource = app.model.ResourcesDictionary[model.ResourceName];
            _cellModalComponent.OpenCell(textHeader, resource, cell.countOfFood);
        }

        public void ClearInventory()
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
                    firstItem = child.gameObject.GetComponent<ModalItemView>();
                i++;
            }
        }

        public void ModalCellClose()
        {
            app.view.ui.IsOpen(false);
            _cellModalComponent.Close();
        }

        private void ModalClose()
        {
            app.view.ui.IsOpen(false);
            _modalComponent.Close();
        }

        private void ModalOnClose()
        {
            app.view.ui.IsOpen(false);
            ClearInventory();
        }

        public void ModalAction(string action)
        {
            app.Notify(action,this);
        }
        private void OnEnable()
        {
            if (_modalComponent != null)
            {
                _modalComponent.OnCloseBtnClick += ModalOnClose;
                _modalComponent.OnBtnClick += ModalAction;    
            }

            if (_cellModalComponent != null)
            {
                _cellModalComponent.OnCloseBtnClick += ModalOnClose;
                _cellModalComponent.OnBtnClick += ModalAction;    
            }
            
        }

        private void Start()
        {
            _modalComponent = gameObject.GetComponent<ModalComponent>();
            _cellModalComponent = gameObject.GetComponent<CellModalComponent>();
        }
    }
}