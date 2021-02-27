using Components.Notifications;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneController : Element
    {
        public void Loaded(params object[] pData)
        {
            var ctx = pData[0];
            var arguments = (object[]) pData[1];
            var scene = (Scene) arguments[0];
            var sceneName = scene.name;
            switch (sceneName)
            {
                case "MainMenu":
                    break;
                case "Victory":
                    break;
                default:
                    app.model.player.levelName = sceneName;
                    break;
            }
            
        }

        public void NextLevel(object[] pData)
        {
            SceneManager.LoadScene (app.model.game.LevelConfig.NextSceneName);
        }

        public void MainMenu(object[] pData)
        {
            SceneManager.LoadScene ("MainMenu");
        }
    }
}