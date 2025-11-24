using System;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HpBarPresenter : IDisposable
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
            Debug.Log("UpdateHpBar");
            _hpBarView.SetValue(_heroHP.CurrentHP, _heroHP.MaxHP);
        }

        public void Dispose()
        {
            _heroHP.HPChanged -= UpdateHpBar;
        }
    }
}