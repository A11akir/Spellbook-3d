using System.Collections;
using System.Collections.Generic;
using Features.Hero.HeroInstance;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class DynamicNavMeshBake : MonoBehaviour
{
    public NavMeshSurface surface;
    
    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }
}
