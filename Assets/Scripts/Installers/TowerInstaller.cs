using Gameplay.Tower.Impls;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private Transform blocksParent;
        [SerializeField] private Transform[] _towerBlocks; 
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TowerService>()
                .AsSingle()
                .WithArguments(blocksParent, _towerBlocks);
        }
        
    }
}