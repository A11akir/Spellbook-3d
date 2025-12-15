using System;
using Features.Enemy.EnemyAttack;
using Features.Hero.HeroInstance;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Features.Enemy.NavMesh
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [Inject] private HeroProvider _heroProvider;
        
        private bool _isMoving = true;
        public NavMeshAgent agent;

        void Awake() => agent = GetComponent<NavMeshAgent>();

        void Update()
        {
            MoveEnemy();
        }

        private void MoveEnemy()
        {
            if (!CanMove())
                return;

            if (IsInAttackRange())
            {
                StopAgent();
                return;
            }

            MoveToHero();
        }

        private bool CanMove() => _isMoving;

        private bool IsInAttackRange() => _triggerObserver.IsDictanceCanAttack;

        private void StopAgent() => agent.ResetPath();

        private void MoveToHero() => 
            agent.SetDestination(GetHeroPosition());

        private Vector3 GetHeroPosition() =>
            _heroProvider.HeroReference.transform.position;

        public void EnableMovement()
        {
            _isMoving = true;
            if (agent)
                agent.isStopped = false;
        }

        public void DisableMovement()
        {
            _isMoving = false;
            if (agent)
            {
                agent.isStopped = true;
                StopAgent();
            }
        }
    }
}