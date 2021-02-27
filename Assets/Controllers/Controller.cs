using System;
using System.Collections.Generic;
using Components.Notifications;
using Controllers.Field;
using Controllers.Game;
using Controllers.Modal;
using DefaultNamespace;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class Controller : Element
    {
        public FieldController field;
        public GameController game;
        public SceneController scene;
        public ModalController modal;
        
        private Dictionary<string, Action<object[]>> Router;

        private void CheckRouter()
        {
            if (Router != null)
                return;
            Router = new Dictionary<string, Action<object[]>>();
            // Field
            Router.Add(Notification.FieldLoad, (object[] pData) => field.IsLoad(pData));
            // Cell
            Router.Add(Notification.CellLoad, (object[] pData) => field.CellIsLoad(pData));
            Router.Add(Notification.CellResourceGenerate, (object[] pData) => field.CellResouceGenerated(pData));
            Router.Add(Notification.CellClick, (object[] pData) => field.CellClick(pData));
            Router.Add(Notification.CellEatFood, (object[] pData) => field.CellEat(pData));
            Router.Add(Notification.CellFoodChange, (object[] pData) => field.CellFoodChange(pData));
            // Game
            Router.Add(Notification.GameMoney, (object[] pData) => game.SetMoney(pData));
            Router.Add(Notification.GameCheckGoals, (object[] pData) => game.CheckGoals(pData));
            Router.Add(Notification.GameComplete, (object[] pData) => game.Complete(pData));
            Router.Add(Notification.GameStart, (object[] pData) => game.StartGame(pData));
            Router.Add(Notification.GameNew, (object[] pData) => game.NewGame(pData));
            // Scene
            Router.Add(Notification.SceneLoaded, (object[] pData) => scene.Loaded(pData));
            Router.Add(Notification.SceneNextLevel, (object[] pData) => scene.NextLevel(pData));
            Router.Add(Notification.SceneMainMenu, (object[] pData) => scene.MainMenu(pData));
            // Modal
            Router.Add(Notification.ModalOk, (object[] pData) => modal.Ok(pData));
            Router.Add(Notification.ModalOpen, (object[] pData) => modal.Open(pData));
            // Modal Cell
            Router.Add(Notification.ModalCellOpen, (object[] pData) => modal.CellOpen(pData));
            Router.Add(Notification.ModalCellResource, (object[] pData) => modal.CellResource(pData));
            Router.Add(Notification.ModalCellMove, (object[] pData) => modal.CellMove(pData));
            Router.Add(Notification.ModalCellFeed, (object[] pData) => modal.CellFeed(pData));
            // Router.Add(Notification.ModalCellReload, (object[] pData) => modal.CellMove(pData));
            // Modal Inventory
            Router.Add(Notification.ModalInventoryOpen, (object[] pData) => modal.InventoryOpen(pData));
            Router.Add(Notification.ModalInventorySell, (object[] pData) => modal.InventorySell(pData));
            Router.Add(Notification.ModalInventoryUse, (object[] pData) => modal.InventoryUse(pData));
            Router.Add(Notification.ModalInventoryNext, (object[] pData) => modal.InventoryNext(pData));
            Router.Add(Notification.ModalInventoryPrev, (object[] pData) => modal.InventoryPrev(pData));
            // Modal Shop
            Router.Add(Notification.ModalShopBuy, (object[] pData) => modal.ShopBuy(pData));
            Router.Add(Notification.ModalShopOpen, (object[] pData) => modal.ShopOpen(pData));
            Router.Add(Notification.ModalShopNext, (object[] pData) => modal.ShopNext(pData));
            Router.Add(Notification.ModalShopPrev, (object[] pData) => modal.ShopPrev(pData));
        }
        
        public void OnNotification(string pEventPath,Object pTarget,params object[] pData)
        {
            CheckRouter();
            if (Router.ContainsKey(pEventPath))
            {
                Router[pEventPath](new object[]{ pTarget, pData });
            }
        }
    }
}