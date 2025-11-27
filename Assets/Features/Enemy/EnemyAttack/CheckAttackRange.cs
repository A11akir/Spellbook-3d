using UnityEngine;

namespace Features.Enemy.EnemyAttack
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider other)
        {
            _enemyAttack.EnablleAttack();
        }

        private void TriggerExit(Collider other)
        {
            _enemyAttack.DisableAttack();
        }
    }
}