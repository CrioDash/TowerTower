using Gameplay.Tower.Impls;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TowerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject blockPrefab;
        [SerializeField] private Transform blocksParent;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TowerService>()
                .AsSingle()
                .WithArguments(blockPrefab, blocksParent);
        }
        
    }
}