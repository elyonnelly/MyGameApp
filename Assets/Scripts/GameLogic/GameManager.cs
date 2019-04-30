
namespace Assets.Scripts.GameLogic
{
    public class GameManager
    {
        private Player player;
        private Enemy enemy;

        public GameManager()
        {
            player = new Player();
            enemy = new Enemy();
        }
        void CheckLife()
        {
            var isPlayerLost = true;
            foreach (var fairy in player.ActiveFairies)
            {
                if (!fairy.IsDead)
                {
                    isPlayerLost = false;
                    break;
                }
            }

            if (!isPlayerLost)
            {
                foreach (var playerFairy in player.ActiveFairies)
                {
                    foreach (var enemyFairy in enemy.ActiveFairies)
                    {
                        playerFairy.ExperiencePoints += enemyFairy.Level > playerFairy.Level ? enemyFairy.Level - playerFairy.Level : 
                                                                                    (playerFairy.Level - enemyFairy.Level) / 2;
                    }
                }
                EventAggregator.OnPlayerWon();
            }
        }

    }
}