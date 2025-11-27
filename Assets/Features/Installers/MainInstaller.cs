using Features.Enemy.EnemyAttack;
using Features.Enemy.EnemySpawner;
using Features.GameBootstrap;
using Features.Hero.HeroInstance;
using Features.Hero.HeroMove;
using Features.Hero.HeroStats;
using Features.Hero.HeroStats.HeroHP;
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
            Container.Bind<LevelBootstrap>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.Bind<EnemyAttack>().FromComponentInHierarchy().AsSingle();

            Container.Bind<InputGamePlay>().AsSingle().NonLazy();
            Container.Bind<HeroStats>().AsSingle().NonLazy();
            Container.Bind<HeroHp>().AsSingle().NonLazy();
            Container.Bind<HpBarPresenter>().AsSingle().NonLazy();

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

            Container.Bind<HpBarView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerProgress>()
                .AsSingle().NonLazy();

        }
    }
}