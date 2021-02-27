using System;
using Components.ScriptableObjects.Field;
using Components.ScriptableObjects.Item;
using Models.Field;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Field
{
    public class Cell : MonoBehaviour
    {
        public SpriteRenderer item;
        public SpriteRenderer resource;
        public GameObject resourceContainer;
        public float secondsBetweenSpawn;
        public bool generatorOn = false;
        public bool eatingOn = false;
        public int count = 1;
        
        public float secondsBetweenEat;
        public int countOfFood = 0;
        
        public bool isTop;
        public bool isRight;
        public bool isLeft;
        public bool isAll;

        public Action<int> OnResourceGenerated;
        public Action<int> OnFoodChange;
        [HideInInspector]
        private float _elapsedTime = 0.0f;
        private float _elapsedEatTime = 0.0f;
        public bool _isActive = false;
        public Sprite _activeTopSprite;
        public Sprite _activeRightSprite;
        public Sprite _activeLeftSprite;
        public Sprite _activeAllSprite;
        public Sprite _topSprite;
        public Sprite _rightSprite;
        public Sprite _leftSprite;
        public Sprite _allSprite;
        
        public void SetItemSprite(Sprite sprite)
        {
            item.sprite = sprite;
        }

        public void SetResourceSprite(Sprite sprite)
        {
            resourceContainer.SetActive(true);
            resource.sprite = sprite;
        }

        public void Feed()
        {
            countOfFood++;
            OnFoodChange(countOfFood);
            if (countOfFood <= 0) return;
            generatorOn = true;
            eatingOn = true;
        }

        public void StartGenerateResource(ItemData itemData)
        {
            secondsBetweenSpawn = itemData.Speed;
            secondsBetweenEat = itemData.EatingSpeed;
            if (itemData.FoodItem == null)
            {
                generatorOn = true;
            }
            else
            {
                if (countOfFood > 0) 
                    generatorOn = true;
                eatingOn = true;
            }
        }

        public void StopGenerateResource()
        {
            generatorOn = false;
        }

        public void LoadSprites(CellModel model)
        {
            _activeTopSprite = model.ActiveTopSprite;
            _activeRightSprite = model.ActiveRightSprite;
            _activeLeftSprite = model.ActiveLeftSprite;
            _activeAllSprite = model.ActiveAllSprite;
            _topSprite = model.TopSprite;
            _rightSprite = model.RightSprite;
            _leftSprite = model.LeftSprite;
            _allSprite = model.AllSprite;
        }

        
        private void Update()
        {

            Timer();
            EatTimer();
        }

        private void Timer()
        {
            if (!generatorOn) return;
            _elapsedTime += Time.deltaTime;
                 
            if (_elapsedTime > secondsBetweenSpawn)
            {
                _elapsedTime = 0;
                OnResourceGenerated(1);
            }
        }

        private void EatTimer()
        {
            if (!eatingOn) return;
            _elapsedEatTime += Time.deltaTime;
            if (countOfFood > 0) 
                generatorOn = true;
            if (_elapsedEatTime > secondsBetweenEat)
            {
                _elapsedEatTime = 0;
                if (countOfFood > 0)
                {
                    countOfFood--;
                    OnFoodChange(countOfFood);
                }
                else
                {
                    generatorOn = false;
                    eatingOn = false;
                    
                }
            }
        }

        private void Start()
        {
            if (resourceContainer != null)
                resourceContainer.SetActive(false);
        }
    }
}