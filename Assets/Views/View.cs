using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using Views.Cell;
using Views.Ui;

namespace Views
{
    public class View : Element
    {
        public FieldView field;
        public UiView ui;
        


        
        
        // private bool TestClickEvent(Vector3 mousePos)
        // {
        //     PointerEventData data = new PointerEventData(_eventSystem);
        //     data.position = mousePos;
        //
        //     List<RaycastResult> results = new List<RaycastResult>();
        //
        //     _graphicRaycaster.Raycast(data, results);
        //     if(results.Count > 0)
        //     {
        //         //You can loop through the results to test for a specific UI element
        //         return true;
        //     }       
        //     return false;
        // }
    }
}