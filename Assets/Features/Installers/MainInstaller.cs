using Features.Enemy.EnemySpawner;
using Features.GameBootstrap;
using Features.Hero.HeroInstance;
using Features.Hero.HeroMove;
using Features.Input.Scripts;
using Features.MapGenerate;
using Features.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<GameBootstrap>()
                .FromComponentInHierarchy() 
                .AsSingle()
                .NonLazy();
            
            Container.Bind<InputGamePlay>().AsSingle().NonLazy();
            
            Container.Bind<InputMovementPlayer>().AsSingle().NonLazy();
            
            Container.Bind<InstanceHeroSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();    
            
            Container.Bind<SpawnMapSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy(); 
            
            Container.Bind<EnemySpawnerSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();            
            
            Container.Bind<DynamicNavMeshBake>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            
            
            
            
        }
    }
}