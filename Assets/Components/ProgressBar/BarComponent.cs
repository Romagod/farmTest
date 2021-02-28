using UnityEngine;

namespace Components.ProgressBar
{
    public class BarComponent : MonoBehaviour
    {
        public void SetScale(float x, float y = 1f, float z = 0f)
        {
            transform.localScale = new Vector3(x,y,z);
        }
    }
}