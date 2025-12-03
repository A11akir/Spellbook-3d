using Features.AbstractMinion;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HpBarReference : MonoBehaviour
    {
        [Inject] private IHealth _health;
        private HpBarPresenter _presenter;

        private void Awake()
        {
            if(_health == null)
            {
                Debug.LogError("Health not injected!");
                return;
            }

            _presenter = new HpBarPresenter(GetComponent<HpBarView>(), _health);
        }
    }

}