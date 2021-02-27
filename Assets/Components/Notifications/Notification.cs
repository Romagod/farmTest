namespace Components.Notifications
{
    public static class Notification
    {
        // Field
        public const string FieldLoad     = "field.isLoad";
        // Cell
        public const string CellLoad      = "field.cell.isLoad";
        public const string CellResourceGenerate      = "field.cell.resouceGenerated";
        public const string CellClick      = "field.cell.onClick";
        public const string CellEatFood      = "field.cell.eat";
        public const string CellFoodChange      = "field.cell.food.change";
        // Game
        public const string GameMoney  = "game.money.set";
        public const string GameCheckGoals  = "game.checkGoals";
        public const string GameComplete  = "game.complete";
        public const string GameStart     = "game.start";
        public const string GameNew     = "game.new";
        // Scene
        public const string SceneLoad     = "scene.load";
        public const string SceneLoaded     = "scene.loaded";
        public const string SceneNextLevel     = "scene.nextLevel";
        public const string SceneMainMenu     = "scene.mainMenu.open";
        // Modal
        public const string ModalOk     = "modal.ok";
        public const string ModalOpen     = "modal.open";
        // Modal Cell
        public const string ModalCellOpen     = "modal.cell.open";
        public const string ModalCellResource     = "modal.cell.getResource";
        public const string ModalCellMove     = "modal.cell.move";
        public const string ModalCellFeed     = "modal.cell.feed";
        public const string ModalCellReload     = "modal.cell.reload";
        public const string ModalCellClear     = "modal.cell.clear";
        // Modal Inventory
        public const string ModalInventoryOpen     = "modal.inventory.open";
        public const string ModalInventorySell     = "modal.inventory.sellResource";
        public const string ModalInventoryUse     = "modal.inventory.useResource";
        public const string ModalInventoryNext     = "modal.inventory.next";
        public const string ModalInventoryPrev     = "modal.inventory.prev";
        // Modal Shop
        public const string ModalShopBuy     = "modal.shop.buyResource";
        public const string ModalShopOpen     = "modal.shop.open";
        public const string ModalShopNext     = "modal.shop.next";
        public const string ModalShopPrev     = "modal.shop.prev";
    }
}