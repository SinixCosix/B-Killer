using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

        public void SetHealthMax(int health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(int health)
        {
            slider.value = health;
        }
    }
}
