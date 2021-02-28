using System.Collections.Generic;
using Components.Notifications;
using DefaultNamespace;
using Models.Field;
using UnityEngine;
using UnityEngine.Tilemaps;
using Views.Cell;

namespace Views
{
    public class FieldView : Element
    {

        private Ray _raycast;
        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
            // app.Notify(Notification.SceneLoaded,this);
        }

        private void Update()
        {
            HandleTouch();
        }
        
        private void HandleTouch()
        {
            for (var i = 0; i < Input.touchCount; ++i)
            {
                if (!Input.GetTouch(i).phase.Equals(TouchPhase.Began)) continue;
                // Construct a ray from the current touch coordinates
                if (_camera is null) continue;
                var ray = _camera.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray, out var hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseUp");
                }
            }
            
            // if (app.view.ui.ModalIsOpen) return;
            //
            // if ((Input.touchCount <= 0)) return;
            //
            // if (Input.GetTouch(0).phase != TouchPhase.Ended) return;
            //
            // var tilemap = gameObject.GetComponent<Tilemap>();
            //
            // if (Camera.main is null) return;
            //
            // var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            // var worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            // var position = tilemap.LocalToWorld(worldPoint, 0.3f);
            // var view = app.model.field.CellsPosition[position];
            //
            // if (view == null) return;
            //
            // var cell = view.gameObject;
            // var cellView = cell.GetComponent<CellView>();
            //
            // if (cellView == null) return;
            //
            // cellView.SingleClick();


            // #if !UNITY_EDITOR
            
            // if (!Physics.Raycast(_raycast, out var raycastHit)) return;
            
// #endif
        }
    }
}