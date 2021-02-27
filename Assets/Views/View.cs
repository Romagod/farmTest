using DefaultNamespace;
using UnityEngine;
using Views.Cell;
using Views.Ui;

namespace Views
{
    public class View : Element
    {
        public FieldView field;
        public UiView ui;
        
        private Ray _raycast;


        private void FixedUpdate()
        {
            HandleTouch();
        }
        
        private void HandleTouch()
        {
// #if !UNITY_EDITOR
            if ((Input.touchCount <= 0)) return;
            if (Input.GetTouch(0).phase != TouchPhase.Ended) return;
            _raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (!Physics.Raycast(_raycast, out var raycastHit)) return;
            var cell = raycastHit.collider.gameObject;
            var cellView = cell.GetComponent<CellView>();
            if (cellView == null) return;
            cellView.SingleClick();
// #endif
        }
    }
}