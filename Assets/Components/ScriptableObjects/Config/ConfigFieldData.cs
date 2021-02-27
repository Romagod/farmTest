using Components.ScriptableObjects.Field;
using UnityEngine;

namespace Components.ScriptableObjects.Config
{
    [CreateAssetMenu(fileName = "ConfigData", menuName = "Application/Config/Create New Config Field Data", order = 0)]
    public class ConfigFieldData : ScriptableObject
    {
        [SerializeField] public int CountOfCellsWidth;
        [SerializeField] public int CountOfCellsHeight;
        [SerializeField] public CellData Cell;
    }
}