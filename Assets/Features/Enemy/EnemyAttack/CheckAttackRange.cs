using UnityEngine;

namespace Features.Enemy.EnemyAttack
{
    [RequireComponent(typeof(IEnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        private IEnemyAttack _enemyAttack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void OnEnable()
        {
            _enemyAttack = transform.GetComponent<IEnemyAttack>();
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider other)
        {
            _enemyAttack.EnableAttack();
        }

        private void TriggerExit(Collider other)
        {
            _enemyAttack.DisableAttack();
        }
    }
}