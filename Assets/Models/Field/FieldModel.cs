using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Components.Notifications;
using Components.ScriptableObjects.Field;
using Data.SerializeObjects;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Views.Cell;
using Application = UnityEngine.Application;
using Object = UnityEngine.Object;

namespace Models.Field
{
    public class FieldModel : Element
    {
        public int countOfCellsWidth;
        public int countOfCellsHeight;
        public CellData cell;
        
        [HideInInspector] public Dictionary<Vector3, CellModel> CellsPosition;
        private Dictionary<int, CellModel> Cells;
        private Dictionary<int, SerializeCellData> ModelsData;
        public CellModel ActiveCellModel;
        public Object activeCell;


        public void LoadFieldData()
        {
            activeCell = null;
            ActiveCellModel = null;
            // if (PlayerPrefs.HasKey("CellsOnField"))
            // {
            //     var cellsData = PlayerPrefs.GetString("CellsOnField");
            //     if (cellsData == "{}") return;
            //     var cellsOnField = JsonUtility.FromJson<Dictionary<int, CellModel>>(cellsData);
            //     Cells = cellsOnField;
            // }
            // else
            // {
            //     SaveField();
            // }
        }

        public void LoadModel()
        {
            if (Cells == null)
            {
                Cells = new Dictionary<int, CellModel>();
            }
            if (CellsPosition == null)
            {
                CellsPosition = new Dictionary<Vector3, CellModel>();
            }
            if (cell == null)
            {
                cell = app.ConfigData.configData.Field.Cell;
            }
            if (ModelsData == null)
            {
                ModelsData = new Dictionary<int, SerializeCellData>();
                var models = LoadList("field");
                if (models == null)
                {
                    ModelsData = null;
                    return;
                }
                foreach (var modelData in models)
                {
                    ModelsData.Add(modelData.Id, modelData);
                }
            }

            
        }
        
        private List<SerializeCellData> LoadList(string name)
        {
            if (File.Exists($"{Application.persistentDataPath}/data/{name}.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = 
                    File.Open($"{Application.persistentDataPath}/data/{name}.dat", FileMode.Open);
                var data = (List<SerializeCellData>)bf.Deserialize(file);
                file.Close();
                return data;
            }
            else
                return null;
        }
        
        public void SaveField()
        {
            var data = new List<SerializeCellData>();
            foreach (var key in Cells.Keys)
            {
                var model = new SerializeCellData();
                model.Id = key;
                model.ResourceName = Cells[key].ResourceName;
                var cellRes = Cells[key].NewResourcesInfo();
                if (cellRes.ContainsKey(Cells[key].ResourceName))
                    model.Count = Cells[key].NewResourcesInfo()[Cells[key].ResourceName];
                else
                    model.Count = 0;
                model.foodCount = Cells[key].foodCount;
                data.Add(model);
            }
            app.Save(data, "field");
        }

        public void AddCell(Object cellView)
        {
            LoadModel();

            var v = (CellView) cellView;
            var id = v.transform.GetSiblingIndex();
            CellModel model = v.gameObject.AddComponent<CellModel>();
            model.LoadCellModel(cell);
            if (ModelsData.ContainsKey(id))
            {
                model.Load(ModelsData[id]);
            }
            
            CellsPosition.Add(v.transform.position, model);
            Cells.Add(id, model);

            // TODO: Упростить взаимодействие
            if (Cells.Count >= (countOfCellsWidth * countOfCellsHeight))
            {
                SaveField();
                app.Notify(Notification.FieldLoad,this);
            }
        }

        public void GetCellModel(Object key)
        {
            try
            {
                var v = (CellView) key;
                ActiveCellModel = Cells[v.transform.GetSiblingIndex()];
                activeCell = key;
                var text = string.IsNullOrEmpty(ActiveCellModel.ResourceName)  ? "Нет" : ActiveCellModel.ResourceName;
                app.model.ui.modal.resourceText = $"{text}";
                app.model.ui.modal.speedText = $"{ActiveCellModel.ResourceSpeed} сек.";;
            }
            catch (KeyNotFoundException)
            {
                var v = (CellView) key;
                Debug.Log(Cells.Keys);
                Debug.LogError(v.id);
            }
        }

        public CellModel FindCellModel(Object key)
        {
            try
            {
                var v = (CellView) key;
                
                return Cells[v.transform.GetSiblingIndex()];
            }
            catch (KeyNotFoundException)
            {
                var v = (CellView) key;
                Debug.Log(Cells.Keys);
                Debug.LogError(v.id);
            }
            return null;
        }

        private void Start()
        {
            countOfCellsWidth = app.ConfigData.configData.Field.CountOfCellsWidth;
            countOfCellsHeight = app.ConfigData.configData.Field.CountOfCellsHeight;
            if (cell == null)
            {
                cell = app.ConfigData.configData.Field.Cell;
            }
        }
    }
}