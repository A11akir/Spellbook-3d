using UnityEngine;
using UnityEngine.UI;

namespace Features.Spells
{
    public class SpellCostView : MonoBehaviour
    {
        [SerializeField] private Slider _orderSlider;
        [SerializeField] private Slider _chaosSlider;

        private int _balance;

        private void Awake()
        {
            SetupSliders();
        }

        private void SetupSliders()
        {
            _orderSlider.minValue = 0f;
            _orderSlider.maxValue = 1f;

            _chaosSlider.minValue = 0f;
            _chaosSlider.maxValue = 1f;
        }
        public void SetBalanceNormalized(float normalizedBalance)
        {
            normalizedBalance = Mathf.Clamp(normalizedBalance, -1f, 1f);
            _balance = normalizedBalance < 0 ? -1 : 1;
            UpdateViewNormalized(normalizedBalance);
        }

        private void UpdateViewNormalized(float normalizedBalance)
        {
            if (normalizedBalance < 0)
            {
                _orderSlider.value = Mathf.Abs(normalizedBalance);
                _chaosSlider.value = 0f;
            }
            else if (normalizedBalance > 0)
            {
                _chaosSlider.value = normalizedBalance;
                _orderSlider.value = 0f;
            }
            else
            {
                _orderSlider.value = 0f;
                _chaosSlider.value = 0f;
            }
        }
    }
}
