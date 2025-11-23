using Features.Hero.HeroInstance;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Features.Enemy.NavMesh
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
    [Inject] private InstanceHeroSystem _instanceHeroSystem;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_instanceHeroSystem.HeroReference != null)
        {
            agent.SetDestination(_instanceHeroSystem.HeroReference.transform.position);
        }
    }
    }
}