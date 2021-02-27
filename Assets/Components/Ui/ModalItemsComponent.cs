using Models.Items;
using UnityEngine;
using Views.Ui.Modal;

namespace Components.Ui
{
    public class ModalItemsComponent : MonoBehaviour
    {
        public GameObject GenerateItem(ModalItemView item, Transform target)
        {
            return Instantiate(item.gameObject, target);
        }
    }
}