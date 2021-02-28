using Components.ScriptableObjects.Item;
using DefaultNamespace;
using UnityEngine;
using Views.Cell;
using Views.Ui.Modal;

namespace Controllers.Modal
{
    public class ModalController : Element
    {
        public void Ok(params object[] pData)
        {
            Debug.Log("Ok!!");
        }

        public void Open(params object[] pData)
        {
            if (app.view.ui.modal == null)
                return;
            app.view.ui.modal.GenerateGoals();
            app.view.ui.modal.Open("Добро пожаловать!", app.ConfigData.configData.Game.LevelConfig);
            app.view.ui.ModalIsOpen = true;
        }
        public void CellOpen(params object[] pData)
        {
            if (app.view.ui.ModalIsOpen)
                return;
            var cellView = (CellView) app.model.field.activeCell;
            cellView.SetActiveState(true);

            var model = app.model.field.FindCellModel((Object) pData[0]);
            app.view.ui.modalCell.OpenCell("Информация", model);
        }

        public void CellResource(object[] pData)
        {
            app.model.field.ActiveCellModel.PickUpResource();
            var cellView = (CellView) app.model.field.activeCell;
            cellView.SetResourceSprite(app.model.field.ActiveCellModel);
        }

        public void CellFeed(object[] pData)
        {
            
            var view = (CellView) app.model.field.activeCell;
            view.Feed();
            app.view.ui.modalCell.ModalCellClose();
        }

        public void CellMove(object[] pData)
        {
            if (app.model.player.GetHand() == null)
            {
                app.model.player.PutToHand(app.model.ResourcesDictionary[app.model.field.ActiveCellModel.ResourceName]);
                app.model.field.ActiveCellModel.DeleteResource();
                app.view.ui.modalCell.ModalCellClose();
            }
            else
            {
                var data = (ItemData) app.model.player.GetHand();
                app.model.field.ActiveCellModel.SetResource(app.model.ResourcesDictionary[data.Name]);
                app.model.field.SaveField();
                app.model.player.PutToHand();
                app.view.ui.modalCell.ModalCellClose();
            }
        }

        public void InventoryOpen(object[] pData)
        {
            var hand = app.model.player.GetHand();
            if (hand != null)
            {
                var item = (ItemData) hand;
                app.model.player.AddResourceToInventory(item.Name, 1);
                app.model.player.PutToHand();
            }
            app.view.ui.modalInventory.ClearInventory();
            app.view.ui.modalInventory.GenerateInventory();
            app.view.ui.modalInventory.Open("Инвентарь");
        }

        public void InventorySell(object[] pData)
        {
            var itemView = (ModalItemView) pData[0];
            app.model.ui.modal.RemoveItem(itemView._itemData.Name);
            app.model.player.SetMoney(app.model.player.money +itemView._itemData.SellPrice);
            // itemView.RemoveElement();
            app.view.ui.modalInventory.ClearInventory();
            app.view.ui.modalInventory.GenerateInventory();
        }

        public void InventoryUse(object[] pData)
        {
            var itemView = (ModalItemView) pData[0];
                    
            app.model.ui.modal.RemoveItem(itemView._itemData.Name);
            app.model.player.PutToHand(app.model.ResourcesDictionary[itemView._itemData.Name]);

            // itemView.RemoveElement();
            app.view.ui.modalInventory.ModalCellClose();
            // app.view.ui.modalInventory.ClearInventory();
            // app.view.ui.modalInventory.GenerateInventory();
        }

        public void InventoryNext(object[] pData)
        {
            app.view.ui.modalInventory.Next();
        }

        public void InventoryPrev(object[] pData)
        {
            app.view.ui.modalInventory.Prev();
        }

        public void ShopBuy(object[] pData)
        {
            var itemView = (ModalItemView) pData[0];
            app.model.player.AddResourceToInventory(itemView._itemData.Name, 1);
            app.model.player.SetMoney(app.model.player.money -app.model.ui.modal.value.BuyPrice);
            app.view.ui.modalShop.ModalClose();
        }

        public void ShopOpen(object[] pData)
        {
            app.view.ui.modalShop.ClearShop();
            app.view.ui.modalShop.GenerateShop();
            app.view.ui.modalShop.Open("Магазин");
        }

        public void ShopNext(object[] pData)
        {
            app.view.ui.modalShop.Next();
        }

        public void ShopPrev(object[] pData)
        {
            app.view.ui.modalShop.Prev();
        }
    }
}