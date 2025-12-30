using Features.AbstractMinion;
using Features.AbstractMinion.Script;
using Features.Hero.HeroStats.HeroHP;
using Zenject;

namespace Features.Enemy.EnemySpawner
{
    public class HpBarPresenterFactory 
        : PlaceholderFactory<HpBarView, IHealth, HpBarPresenter>
    {
    }
}