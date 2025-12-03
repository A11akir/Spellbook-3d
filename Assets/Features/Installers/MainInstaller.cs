using Cinemachine;
using Features.AbstractMinion;
using Features.Enemy.EnemyAttack;
using Features.Enemy.EnemySpawner;
using Features.Enemy.EnemyStats;
using Features.GameBootstrap;
using Features.GoogleSheets;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats;
using Features.Hero.HeroStats.HeroHP;
using Features.Input.Scripts;
using Features.MapGenerate;
using Features.Scripts.Input;
using Features.Spells;
using Features.Spells.Fireball;
using UnityEngine;
using Zenject;

namespace Features.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private MeleeEnemyStatsData _meleeStats;        
        [SerializeField] private HeroStatsData _heroStats;
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<LevelBootstrap>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<MeleeEnemyStatsData>().FromInstance(_meleeStats).AsTransient().NonLazy();            
            Container.Bind<HeroStatsData>().FromInstance(_heroStats).AsSingle().NonLazy();
            
            Container.Bind<EnemyAttack>().FromComponentInHierarchy().AsTransient();

            Container.Bind<InputGamePlay>().AsSingle().NonLazy();
            Container.Bind<IHealth>().To<Health>().AsTransient().NonLazy();
            

            Container.Bind<InputMovementPlayer>().AsSingle().NonLazy();
            Container.Bind<InputSpells>().AsSingle().NonLazy();
            Container.Bind<SpellSystem>().AsSingle().NonLazy();
            Container.Bind<HeroProvider>().AsSingle().NonLazy();
            Container.Bind<EnemyProvider>().AsSingle().NonLazy();
            Container.Bind<AllGameConfig>().AsSingle().NonLazy();
            
            Container.Bind<InstanceHeroSystem>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            Container.Bind<FireballLogic>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.Bind<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();
            
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