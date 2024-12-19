using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private EnemyAttack _enemyAttack;

    override protected void OnDeath()
    {
        Destroy(gameObject);
    }
}
