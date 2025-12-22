using System.Collections.Generic;
using Cinemachine;
using Features.AbstractMinion;
using Features.Enemy.EnemyAttack;
using Features.Enemy.EnemySpawner;
using Features.Enemy.EnemyStats;
using Features.GameBootstrap;
using Features.GoogleSheets;
using Features.Hero.HeroInstance;
using Features.Hero.HeroMove;
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
        [SerializeField] private RangeEnemyStatsData _rangeStats; 
        [SerializeField] private GromillaEnemyStatsData _gromillaStats; 
        [SerializeField] private HeroStatsData _heroStats;
        [SerializeField] private FireballStatsData _fireballStats;
        [SerializeField] private LightningStatsData _lightningStatsData;
        [SerializeField] private SpellsKitData _spellsKitData;
        [SerializeField] private SpawnerConfigData _spawnerConfigData;
        [SerializeField] private BaseSpawnerStatsData _baseSpawnerStatsData;
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<LevelBootstrap>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.BindFactory<HpBarView, IHealth, HpBarPresenter, HpBarPresenterFactory>();

            BindConfig();
            

            Container.Bind<InputGamePlay>().AsSingle().NonLazy();
            Container.Bind<IHealth>().To<Health>().AsTransient();
            Container.Bind<IEnemyAttack>()
                .To<MeleeEnemyAttack>()
                .FromComponentInChildren()
                .AsTransient();

            Container.Bind<IEnemyAttack>()
                .To<RangeEnemyAttack>()
                .FromComponentInChildren()
                .AsTransient();
            
            Container.Bind<MovementHero>()
                .FromComponentInChildren()
                .AsTransient();
                        
   
            
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
                .FromComponentInChildren()
                .AsSingle()
                .Lazy();

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
            Container.Bind<MeleeEnemyStatsData>().FromInstance(_meleeStats).AsTransient().NonLazy();  
            Container.Bind<RangeEnemyStatsData>().FromInstance(_rangeStats).AsTransient().NonLazy();  
            Container.Bind<GromillaEnemyStatsData>().FromInstance(_gromillaStats).AsTransient().NonLazy();         
            Container.Bind<HeroStatsData>().FromInstance(_heroStats).AsSingle().NonLazy();
            Container.Bind<FireballStatsData>().FromInstance(_fireballStats).AsSingle().NonLazy();
            Container.Bind<LightningStatsData>().FromInstance(_lightningStatsData).AsSingle().NonLazy();
            Container.Bind<SpawnerConfigData>().FromInstance(_spawnerConfigData).AsSingle().NonLazy();
            Container.Bind<SpellsKitData>().FromInstance(_spellsKitData).AsSingle().NonLazy();
            Container.Bind<BaseSpawnerStatsData>().FromInstance(_baseSpawnerStatsData).AsSingle().NonLazy();
            Container.Bind<SpellConfigBindSystem>().AsSingle().NonLazy();
        }

    }
}