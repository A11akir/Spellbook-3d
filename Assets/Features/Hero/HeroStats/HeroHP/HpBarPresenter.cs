using System;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HpBarPresenter : MonoBehaviour
    {
        private HeroHP _heroHP;
        private HpBarView _hpBarView;
        [Inject]
        private void Construct(HpBarView hpBarView, HeroHP heroHP)
        {
            _hpBarView = hpBarView;
            _heroHP = heroHP;

            _heroHP.HPChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBarView.SetValue(_heroHP.CurrentHP, _heroHP.MaxHP);
        }

        private void OnDestroy()
        {
            _heroHP.HPChanged -= UpdateHpBar;
        }
    }
}