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
            CheckDirectory();
            BinaryFormatter bf = new BinaryFormatter(); 
            FileStream file = File.Create( $"{Application.persistentDataPath}{modelDataPath}{name}.dat"); 
            bf.Serialize(file, model);
            file.Close();
        }
        public object Load(string name)
        {
            if (File.Exists($"{Application.persistentDataPath}{modelDataPath}{name}.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = 
                    File.Open($"{Application.persistentDataPath}{modelDataPath}{name}.dat", FileMode.Open);
                var data = bf.Deserialize(file);
                file.Close();
                return data;
            }
            else
                return null;
        }

        private void CheckDirectory()
        {
            var exists = System.IO.Directory.Exists($"{Application.persistentDataPath}/data");

            if(!exists)
                System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/data");
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
            File.Delete( $"{Application.persistentDataPath}{modelDataPath}{s}.dat" );
            
            app.RefreshEditorProjectWindow();
        }
    }
}