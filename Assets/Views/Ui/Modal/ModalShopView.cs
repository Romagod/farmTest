using System.Collections.Generic;
using Components.Ui;
using DefaultNamespace;
using Models.Items;
using UnityEngine;

namespace Views.Ui.Modal
{
    public class ModalShopView : Element
    {
        public ModalItemsComponent modalItemsContainer;
        public ModalItemView firstItem;
        public ShopPagerComponent pager;
        
        [HideInInspector]
        private ModalComponent _modalComponent;
        private CellModalComponent _cellModalComponent;
        private int count = 0;
        private int page = 1;
        private int pageSize = 5;

        public void Open(string textHeader, string textBody = "")
        {
            app.view.ui.IsOpen(true);
            if (_cellModalComponent == null)
                _cellModalComponent = gameObject.GetComponent<CellModalComponent>();
                
            if (_cellModalComponent != null)
                _cellModalComponent.OpenModal(textHeader, textBody);
        }
        
        public void Next()
        {
            if ((count/pageSize) < page +1) return;
            page++;
            ClearShop();
            GenerateShop();
        }

        public void Prev()
        {
            if ((page - 1) <= 0) return;
            page--;
            ClearShop();
            GenerateShop();
        }

        public void GenerateShop()
        {
            var data = app.model.player.serializeShop.Items;
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

        public void ClearShop()
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

        public void ModalClose()
        {
            app.view.ui.IsOpen(false);
            _cellModalComponent.Close();
        }

        private void ModalOnClose()
        {
            app.view.ui.IsOpen(false);
            ClearShop();
            page = 1;
            pager.SetPage(page);
        }

        public void ModalAction(string action)
        {
            app.Notify(action,this);
        }
        private void OnEnable()
        {
            if (_cellModalComponent != null)
            {
                _cellModalComponent.OnCloseBtnClick += ModalOnClose;
                _cellModalComponent.OnBtnClick += ModalAction;    
            }
            
        }

        private void Start()
        {
            _cellModalComponent = gameObject.GetComponent<CellModalComponent>();
        }
    }
}