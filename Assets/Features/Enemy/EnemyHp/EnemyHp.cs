using UnityEngine;

namespace Features.Enemy.EnemyHp
{
    public class EnemyHp : MonoBehaviour
    {
        public void TakeDamage(float damage)
        {
            Destroy(gameObject);
        }
    }
}