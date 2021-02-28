using UnityEngine;

namespace Components.ProgressBar
{
    public class ProgressBarComponent : MonoBehaviour
    {
        public BarComponent Bar;
        
        [HideInInspector]
        private float _value;

        private void SetScale()
        {
            if (Bar != null)
            {
                Bar.SetScale(_value);
            }
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void SetValue(float value)
        {
            _value = value;
            SetScale();
            if (_value >= 1f) gameObject.SetActive(false);
            if (_value <= 0.05f) gameObject.SetActive(true);
        }
    }
}