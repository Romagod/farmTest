using System;
using Components.ScriptableObjects.Field;
using UnityEngine;

namespace Data.SerializeObjects.Config
{
    [Serializable]
    public class SerializeConfigFieldData
    {
        [SerializeField] public int CountOfCellsWidth;
        [SerializeField] public int CountOfCellsHeight;
        [SerializeField] public CellData Cell;
    }
}