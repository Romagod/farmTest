using System;
using System.Collections.Generic;
using Components.ScriptableObjects.Item;
using UnityEngine;

namespace Components.ScriptableObjects.Level
{
    [CreateAssetMenu(fileName = "GoalData", menuName = "Level Config/Create New Goal", order = 2)]
    public class GoalData : ScriptableObject
    {
        [Tooltip("Scene Name")]
        [SerializeField]
        private bool isMoney;
        public bool IsMoney
        {
            get => isMoney;
            protected set => isMoney = value;
        }
        
        [SerializeField]
        private int count;
        public int Count
        {
            get => count;
            protected set => count = value;
        }

        [SerializeField] 
        private ItemData item; 
        public ItemData Item
        {
            get => item;
            protected set => item = value;
        }
    }
}