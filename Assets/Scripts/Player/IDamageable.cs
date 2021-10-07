public interface IDamageable
{
    HealthComponent HealthComponent 
    { 
        get; 
    }

    void TakeDamage(int _damage);
    void OnHealthZero();
}