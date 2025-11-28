using Features.Hero.HeroInstance;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Features.Enemy.NavMesh
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [Inject] private HeroProvider _heroProvider;

        private NavMeshAgent agent;

        void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (_heroProvider.HeroReference)
            {
                agent.SetDestination(_heroProvider.HeroReference.transform.position);
            }
        }
    }
}