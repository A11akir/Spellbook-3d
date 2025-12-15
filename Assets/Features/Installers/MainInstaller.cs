using System.Collections.Generic;
using Cinemachine;
using Features.AbstractMinion;
using Features.Enemy.EnemyAttack;
using Features.Enemy.EnemySpawner;
using Features.Enemy.EnemyStats;
using Features.GameBootstrap;
using Features.GoogleSheets;
using Features.Hero.HeroInstance;
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
        [SerializeField] private FireballStatsData _fireballStats;
        [SerializeField] private SpellsKitData _spellsKitData;
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<LevelBootstrap>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.BindFactory<HpBarView, IHealth, HpBarPresenter, HpBarPresenterFactory>();

            BindConfig();
            
            Container.Bind<MeleeEnemyAttack>().FromComponentInHierarchy().AsTransient();

            Container.Bind<InputGamePlay>().AsSingle().NonLazy();
            Container.Bind<IHealth>().To<Health>().AsTransient();
            
            Container.Bind<InputMovementPlayer>().AsSingle().NonLazy();
            Container.Bind<InputSpells>().AsSingle().NonLazy();
            Container.Bind<SpellSystem>().AsSingle().NonLazy();
            Container.Bind<HeroProvider>().FromComponentInHierarchy().AsSingle().NonLazy();
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

        private void BindConfig()
        {
            Container.Bind<IMinionStatsData>().To<MeleeEnemyStatsData>().FromInstance(_meleeStats).AsTransient().NonLazy();            
            Container.Bind<HeroStatsData>().FromInstance(_heroStats).AsSingle().NonLazy();
            Container.Bind<FireballStatsData>().FromInstance(_fireballStats).AsSingle().NonLazy();

            Container.Bind<SpellConfigBindSystem>().AsSingle().NonLazy();
        }

    }
}