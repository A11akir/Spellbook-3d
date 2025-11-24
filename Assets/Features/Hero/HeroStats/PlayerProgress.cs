
using Zenject;

namespace Features.Hero.HeroStats.HeroHP
{
    public class PlayerProgress
    {
        [Inject] private HeroHP _heroStats;

        public void NewProgress()
        {
            _heroStats.MaxHP = 100;
            _heroStats.ResetHP();
        }
    }
}