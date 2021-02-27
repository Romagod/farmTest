using UnityEngine;

namespace Components.ScriptableObjects.Field
{
    [CreateAssetMenu(fileName = "CellData", menuName = "Field/Create Cell Data", order = 0)]
    public class CellData : ScriptableObject
    {
        
        [Tooltip("Top Sprite")]
        [SerializeField] private Sprite topSprite;
        public Sprite TopSprite
        {
            get => topSprite;
            set => topSprite = value;
        }
        [Tooltip("Right Sprite")]
        [SerializeField] private Sprite rightSprite;
        public Sprite RightSprite
        {
            get => rightSprite;
            set => rightSprite = value;
        }
        [Tooltip("Left Sprite")]
        [SerializeField] private Sprite leftSprite;
        public Sprite LeftSprite
        {
            get => leftSprite;
            set => leftSprite = value;
        }
        [Tooltip("All Sprite")]
        [SerializeField] private Sprite allSprite;
        public Sprite AllSprite
        {
            get => allSprite;
            set => allSprite = value;
        }
        [Tooltip("Active Top Sprite")]
        [SerializeField] private Sprite topActiveSprite;
        public Sprite ActiveTopSprite
        {
            get => topActiveSprite;
            set => topSprite = value;
        }
        [Tooltip("Active Right Sprite")]
        [SerializeField] private Sprite rightActiveSprite;
        public Sprite ActiveRightSprite
        {
            get => rightActiveSprite;
            set => rightSprite = value;
        }
        [Tooltip("Active Left Sprite")]
        [SerializeField] private Sprite leftActiveSprite;
        public Sprite ActiveLeftSprite
        {
            get => leftActiveSprite;
            set => leftSprite = value;
        }
        [Tooltip("Active All Sprite")]
        [SerializeField] private Sprite allActiveSprite;
        public Sprite ActiveAllSprite
        {
            get => allActiveSprite;
            set => allSprite = value;
        }
        // [Tooltip("Main Sprite")]
        // [SerializeField] private Sprite mainSprite;
        // public Sprite MainSprite
        // {
        //     get => mainSprite;
        //     set => mainSprite = value;
        // }
        // [Tooltip("Active Sprite")]
        // [SerializeField] private Sprite activeSprite;
        // public Sprite ActiveSprite
        // {
        //     get => activeSprite;
        //     set => mainSprite = value;
        // }
    
        [Tooltip("Size of Cell")]
        [SerializeField] private Vector2 size = new Vector2();
        public Vector2 Size
        {
            get => size;
            set => size = value;
        }
    
        [Tooltip("Produced resource name")]
        [SerializeField] private string resourceName;
        public string ResourceName
        {
            get => resourceName;
            set => resourceName = value;
        }
    
        [Tooltip("Produced resource speed")]
        [Range(0.00f, 100.00f)]
        [SerializeField] private float resourceSpeed;
        public float ResourceSpeed
        {
            get => resourceSpeed;
            set => resourceSpeed = value;
        }
    }
}