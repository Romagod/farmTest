using System.Collections.Generic;
using Components.Ui;
using DefaultNamespace;
using Models.Items;
using UnityEngine;

namespace Views.Ui.Modal
{
    public class ModalInventoryView : Element
    {
        public ModalItemsComponent modalItemsContainer;
        public ModalItemView firstItem;
        public InventoryPagerComponent pager;
        
        [HideInInspector]
        private ModalComponent _modalComponent;
        private CellModalComponent _cellModalComponent;
        private int count = 0;
        private int page = 1;
        private int pageSize = 5;

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

        public void Next()
        {
            if ((count/pageSize) < page +1) return;
            page++;
            ClearInventory();
            GenerateInventory();
        }

        public void Prev()
        {
            if ((page - 1) <= 0) return;
            page--;
            ClearInventory();
            GenerateInventory();
        }

        public void GenerateInventory()
        {
            var data = app.model.player.serializeInventory.Items;
            var c = 1;
            foreach (var item in data)
            {
                if (item == null) continue;
                var i = 1;
                
                while (i <= item.Count)
                {
                    var min = ((pageSize * page) - pageSize);
                    var max = (pageSize * page);
                    if (c > min && c <= max)
                    {
                        ModalItemView view;
                        if (c == min + 1)
                        {
                            view = firstItem;
                        }
                        else
                        {
                            var go = Instantiate(firstItem.gameObject, modalItemsContainer.transform );
                            view = go.GetComponent<ModalItemView>();
                        }
                        
                        if (app.model.ResourceExists(item.itemData.Name))
                        {
                            view.RenderInfo(app.model.ResourcesDictionary[item.itemData.Name]);
                        }
                    }
                    c++;
                    i++;
                }

                count += item.Count;
            }
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
                {
                    firstItem = child.gameObject.GetComponent<ModalItemView>();
                    firstItem.gameObject.SetActive(false);
                }
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
            page = 1;
            pager.SetPage(page);
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