using Components.Notifications;
using Data.SerializeObjects;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Game
{
    public class GameController : Element
    {
        public void SetMoney(params object[] pData)
        {
            if (app.view.ui.header == null)
                return;
            app.view.ui.header.SetMoneyValue(app.model.player.money.ToString());
        }
        public void Complete(params object[] pData)
        {
            
            app.Notify(Notification.SceneNextLevel, this);
        }

        public void CheckGoals(object[] pData)
        {
            var data = app.ConfigData.configData.Game.LevelConfig.Goals;
            var i = 0;
            var countOfTrue = 0;
            foreach (var item in data)
            {
                var itemCount = app.model.player.CountOfItemInt(item.IsMoney ? "Монеты" : item.Item.Name);
                if (itemCount>= item.Count)
                {
                    countOfTrue++;
                }
                i++;

            }

            if (countOfTrue >= i)
            {
                app.Notify(Notification.GameComplete, this);
            }
            else
            {
                var name = app.model.game.LevelConfig.SceneName;
                if (name != "MainMenu")
                {
                    app.model.player.SetLevelName(name);
                }
            }
        }

        public void StartGame(object[] pData)
        {
            var player = (SerializePlayerData) app.Load("player");
            var levelName = player?.levelName != "" ? player?.levelName : null;
            var sceneName = levelName ?? app.model.game.LevelConfig.NextSceneName;
            SceneManager.LoadScene(sceneName);
        }

        public void NewGame(object[] pData)
        {
            app.model.player.DeleteSavedData();
            var sceneName = app.ConfigData.configData.Game.LevelConfig.NextSceneName;
            SceneManager.LoadScene(sceneName);
        }
    }
}