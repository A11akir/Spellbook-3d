using System;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HpBarPresenter : IDisposable
    {
        private HeroHp _heroHp;
        private HpBarView _hpBarView;
        
        public HpBarPresenter(HpBarView hpBarView, HeroHp heroHp)
        {
            _hpBarView = hpBarView;
            _heroHp = heroHp;

            _heroHp.HpChanged += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBarView.SetValue(_heroHp.CurrentHp, _heroHp.MaxHp);
        }

        public void Dispose()
        {
            _heroHp.HpChanged -= UpdateHpBar;
        }
    }
}