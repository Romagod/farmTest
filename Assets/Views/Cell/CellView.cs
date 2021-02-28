using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Components.Notifications;
using Components.ProgressBar;
using DefaultNamespace;
using Models.Field;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views.Cell
{
    public class CellView : Element
    {

        [HideInInspector]
        private Components.Field.Cell _cell;
        private SpriteRenderer _sprite;
        private const float DoubleClickTimeLimit = 0.2f;
        private Ray _raycast;
        public int id;

        public void SetSprite(Sprite newSprite)
        {
            _sprite.sprite = newSprite;
        }

        public void SetItemSprite(Sprite newSprite)
        {
            _cell.SetItemSprite(newSprite);
        }

        public void OnResourceGenerated(int count)
        {
            var obj = new object[] { count };
            app.Notify(Notification.CellResourceGenerate,this, obj);
        }

        public void OnFoodChanged(int count)
        {
            object[] obj = new object[] { count };
            app.Notify(Notification.CellFoodChange,this, obj);
        }

        public void CellFeed()
        {
            _cell.Feed();
        }

        public void Feed()
        {
            var obj = new object[] { 1 };
            app.Notify(Notification.CellEatFood,this, obj);
        }
        
        private void Start()
        {
            _cell = gameObject.GetComponent<Components.Field.Cell>();
            _sprite = gameObject.GetComponent<SpriteRenderer>();
            id = gameObject.transform.GetSiblingIndex();
            app.Notify(Notification.CellLoad,this);
            _cell.OnResourceGenerated += OnResourceGenerated;
            _cell.OnFoodChange += OnFoodChanged;
            CellModel model = app.model.field.FindCellModel(this);
            _cell.LoadSprites(model);
        }


        private void OnMouseUp() {
            SingleClick();
        }


        public void SingleClick()
        {
            app.Notify(Notification.CellClick,this);
        }

        private void DoubleClick()
        {
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public void SetActiveState(bool b)
        {
            _cell._isActive = b;

            if (_cell._isActive)
            {
                if (_cell.isTop)
                {
                    _sprite.sprite = _cell._activeTopSprite;
                }
                else if (_cell.isRight)
                {
                    _sprite.sprite = _cell._activeRightSprite;
                }
                else if (_cell.isLeft)
                {
                    _sprite.sprite = _cell._activeLeftSprite;
                }
                else if (_cell.isAll)
                {
                    _sprite.sprite = _cell._activeAllSprite;
                }
                
            }
            else
            {
                if (_cell.isTop)
                {
                    _sprite.sprite = _cell._topSprite;
                }
                else if (_cell.isRight)
                {
                    _sprite.sprite = _cell._rightSprite;
                }
                else if (_cell.isLeft)
                {
                    _sprite.sprite = _cell._leftSprite;
                }
                else if (_cell.isAll)
                {
                    _sprite.sprite = _cell._allSprite;
                }
            }
        }

        public void SetResourceSprite(CellModel model)
        {
            if (model.ResourcesCount() <= 0)
            {
                _cell.resourceContainer.SetActive(false);
                return;
            }
            if (!app.model.ResourceExists(model.ResourceName))
                return;
                
            var resource = app.model.ResourcesDictionary[model.ResourceName];
            _cell.SetResourceSprite(resource.GeneratedItem == null
                ? resource.MainSprite
                : resource.GeneratedItem.MainSprite);
        }
    }
}