using Components.ScriptableObjects.Level;
using DefaultNamespace;

namespace Models
{
    public class GameModel : Element
    {
        public LevelConfigData LevelConfig;
        
        private void Start()
        {
            LevelConfig = app.ConfigData.configData.Game.LevelConfig;
        } 
    } 
}