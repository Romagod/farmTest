using Components.Notifications;
using DefaultNamespace;
using UnityEngine.UI;

namespace Views.Ui.Header
{
    public class HeaderView : Element
    {
        public Text moneyValue;

        public void SetMoneyValue(string value)
        {
            moneyValue.text = value;
            app.Notify(Notification.GameCheckGoals, this);
        }
        
        private void Action(string action)
        {
            app.Notify(action,this);
        }

        private void Start()
        {
            app.Notify(Notification.GameMoney, this);
        }
    }
}