using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HpBarView : MonoBehaviour
    {
        [SerializeField] private Slider _sliderHp; 
        [SerializeField] private Slider _sliderHpEffect;
        [SerializeField] private bool _isHeroBar;

        private GameObject _enemyPrefab;

        private Tween _effectTween;

        public void SetValue(float current, float max)
        {
            _sliderHp.maxValue = max;
            _sliderHp.value = current;
            _sliderHpEffect.maxValue = max;
            
            _effectTween?.Kill();

            if (_sliderHpEffect.value > current)
            {
                _effectTween = _sliderHpEffect
                    .DOValue(current, 0.5f) 
                    .SetDelay(0.2f)
                    .SetEase(Ease.OutQuad);
            }
            else
                _sliderHpEffect.value = current;
        }

    }
}