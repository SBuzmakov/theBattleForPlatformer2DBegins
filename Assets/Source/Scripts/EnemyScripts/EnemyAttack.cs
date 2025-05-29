namespace Source.Scripts.EnemyScripts
{
    public class EnemyAttack
    {
        public float Damage { get; private set; }

        public EnemyAttack(float damage)
        {
            if (damage < 0f)
                return;

            Damage = damage;
        }
    }
}