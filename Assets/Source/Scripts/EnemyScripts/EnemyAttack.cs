namespace Source.Scripts.EnemyScripts
{
    public class EnemyAttack
    {
        public bool IsAttacking { get; private set; }
        public float Damage { get; private set; }

        public EnemyAttack(float damage)
        {
            if (damage < 0f)
                return;

            Damage = damage;
        }

        public void SwitchOnAttacking()
        {
            IsAttacking = true;
        }
    }
}