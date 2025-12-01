using System;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;

public class HpBarPresenter : IDisposable
{
    private readonly IHealth _health;
    private readonly HpBarView _view;

    public HpBarPresenter(HpBarView view, IHealth health)
    {
        _view = view;
        _health = health;

        _health.HpChanged += UpdateView;
        UpdateView();
    }

    private void UpdateView()
    {
        _view.SetValue(_health.CurrentHp, _health.MaxHp);
    }

    public void Dispose()
    {
        _health.HpChanged -= UpdateView;
    }
}