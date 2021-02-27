using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Views.Ui.Modal;

namespace Components.Ui
{
    public class InventoryPagerComponent : MonoBehaviour
    {
        public ModalInventoryView modalView;

        public Text pageNumber;

        public int page = 1;

        public void NextAction(string name)
        {
            page++;
            pageNumber.text = $"{page}";
            modalView.ModalAction(name);
        }

        public void PrevAction(string name)
        {
            page--;
            pageNumber.text = $"{page}";
            modalView.ModalAction(name);
        }

        public void SetPage(int number)
        {
            page = number;
            pageNumber.text = $"{page}";
        }

        private void Start()
        {
            pageNumber.text = $"{page}";
        }
    }
}