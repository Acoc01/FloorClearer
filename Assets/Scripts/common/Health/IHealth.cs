public interface IHealth {
    float health {get; set;}
    float remainingHealth {get; set;}
    void TakeDamage();
    void Die();
}