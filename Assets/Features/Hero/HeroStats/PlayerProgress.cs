
namespace Features.Hero.HeroStats.HeroHP
{
    public class PlayerProgress
    {
        private HeroHP _heroStats;

        public void NewProgress()
        {
            var progress = new PlayerProgress();
            progress._heroStats.MaxHP = 100;
            progress._heroStats.ResetHP();
        }
    }
}