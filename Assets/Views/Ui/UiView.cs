using System;
using Components.Ui;
using DefaultNamespace;
using UnityEngine;
using Views.Ui.Header;
using Views.Ui.Modal;

namespace Views.Ui
{
    public class UiView : Element
    {
        public HeaderView header;
        
        public ModalShopView modalShop;
        public ModalInventoryView modalInventory;
        public ModalCellView modalCell;
        public ModalView modal;

        public bool ModalIsOpen = false;
        
        public void OnSomeBtnClick(string notificationName)
        {
            
            app.Notify(notificationName,this);
        }

        public void IsOpen(bool value)
        {
            ModalIsOpen = value;
        }
    }
}