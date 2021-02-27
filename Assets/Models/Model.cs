using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Components.ScriptableObjects.Item;
using DefaultNamespace;
using Models.Field;
using Models.Ui;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Models
{
    public class Model : Element
    {
        public FieldModel field;
        public UiModel ui;
        public PlayerModel player;
        public GameModel game;
        public string modelDataPath = "/data/";
        
        public List<ItemData> resources;
        [HideInInspector]
        public Dictionary<string, ItemData> ResourcesDictionary;

        public void Save(object model, string name)
        {
            BinaryFormatter bf = new BinaryFormatter(); 
            FileStream file = File.Create( $"{Application.dataPath}/data/{name}.dat"); 
            bf.Serialize(file, model);
            file.Close();
        }
        public object Load(string name)
        {
            if (File.Exists($"{Application.dataPath}/data/{name}.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = 
                    File.Open($"{Application.dataPath}/data/{name}.dat", FileMode.Open);
                var data = bf.Deserialize(file);
                file.Close();
                return data;
            }
            else
                return null;
        }

        public bool ResourceExists(string name)
        {
            if (ResourcesDictionary == null)
            {
                ResourcesDictionary = new Dictionary<string, ItemData>();
                
                foreach (var resource in resources)
                {
                    ResourcesDictionary.Add(resource.Name, resource);
                }
            }

            if (name == "" || name == "Нет")
            {
                return false;
            }
            return ResourcesDictionary.ContainsKey(name);
        }
        
        private void Start()
        {
            game.LevelConfig = app.ConfigData.configData.Game.LevelConfig;
            resources = app.ConfigData.configData.Game.Resources;
            if (ResourcesDictionary == null)
            {
                ResourcesDictionary = new Dictionary<string, ItemData>();
                foreach (var resource in resources)
                {
                    ResourcesDictionary.Add(resource.Name, resource);
                }
            }
            
        }

        public void Delete(string s)
        {
            File.Delete( $"{Application.dataPath}{modelDataPath}{s}.dat" );
            
            app.RefreshEditorProjectWindow();
        }
    }
}