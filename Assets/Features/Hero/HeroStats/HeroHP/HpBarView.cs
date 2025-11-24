using UnityEngine;
using UnityEngine.UI;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HpBarView : MonoBehaviour
    {
        public Slider SliderHP;
        
        public void SetValue(float current, float max) =>
        SliderHP.value = current/max;
    }
}