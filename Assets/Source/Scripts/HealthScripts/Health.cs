namespace Source.Scripts.HealthScripts
{
    public class Health
    {
        public Health(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public void TakeDamage(float damage)
        {
            if (damage < 0f)
                return;

            CurrentHealth -= damage;
        }

        public void Heal(float healValue)
        {
            if (healValue < 0f)
                return;

            CurrentHealth += healValue;
        }
    }
}