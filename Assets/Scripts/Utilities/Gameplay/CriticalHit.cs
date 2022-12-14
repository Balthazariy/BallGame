namespace Chebureck.Utilities.Gameplay
{
    public class CriticalHit
    {
        private Player player;

        public CriticalHit()
        {
            player = new Player() { damage = 1.5f, criticalHit = 25f };


            for (int i = 0; i < 500; i++)
            {
                if (IsCriticalHit())
                    Logger.Log($"Critical damage - [{GetCriticalDamage()}]", Settings.LogTypeEnumerators.Info);
                else
                    Logger.Log($"Damage - [{GetDamage()}]", Settings.LogTypeEnumerators.Info);
            }
        }

        private bool IsCriticalHit()
        {
            if (UnityEngine.Random.Range(0f, 100f) < (player.criticalHit))
                return true;

            return false;
        }

        private float GetCriticalDamage()
        {
            return player.damage * (1 + player.criticalHit);
        }

        private float GetDamage()
        {
            return player.damage;
        }
    }

    public class Player
    {
        public float damage;
        public float criticalHit;
    }
}