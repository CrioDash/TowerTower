using Core;
using Gameplay;
using Gameplay.Arrow;
using Gameplay.Arrow.Impls;
using Gameplay.Drag;
using Gameplay.Drag.Impls;
using Gameplay.Enemy;
using Gameplay.Enemy.Impls;
using Gameplay.Player.Impls;
using UI.BuyBlock;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private Arrow arrowPrefab;
        [SerializeField] private EnemyBase enemyPrefab;
        [SerializeField] private Transform arrowsContainer;
        [SerializeField] private Transform enemyContainer;
        
        public override void InstallBindings()
        {
            BindModels();
            BindPools();
            
            Container.BindInterfacesAndSelfTo<PlayerChangeSideService>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<PooledArrowFactory>()
                .AsSingle()
                .WithArguments(arrowsContainer);
            Container.BindInterfacesAndSelfTo<PooledEnemyFactory>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyBehaviourFactory>()
                .AsSingle();
            Container.BindInterfacesTo<GameStartService>()
                .AsSingle();
            Container.Bind<IArrowHitHandler>()
                .To<ArrowGroundHitHandler>()
                .AsSingle();
            Container.Bind<IArrowHitHandler>()
                .To<ArrowEnemyHitHandler>()
                .AsSingle();
        }

        private void BindPools()
        {
            Container.BindMemoryPool<Arrow, Arrow.ArrowPool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(arrowPrefab)
                .UnderTransform(arrowsContainer);
            Container.BindMemoryPool<EnemyBase, EnemyBase.EnemyPool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransform(enemyContainer);
        }

        private void BindModels()
        {
            Container.BindInterfacesAndSelfTo<BuyBlockModel>()
                .AsSingle();
        }
        
    }
}