using Db.ArrowSettingsDatabase;
using Db.ArrowSettingsDatabase.Impls;
using Db.EnemyBehaviourDatabase;
using Db.EnemyBehaviourDatabase.Impls;
using Db.EnemySettingsDatabase;
using Db.EnemySettingsDatabase.Impls;
using Db.EnemySpawnDatabase;
using Db.EnemySpawnDatabase.Impls;
using Db.LayerSettingsDatabase;
using Db.LayerSettingsDatabase.Impls;
using Db.PlayerAimDatabase;
using Db.PlayerAimDatabase.Impls;
using Db.PlayerControlsDatabase;
using Db.PlayerControlsDatabase.Impls;
using Db.TowerSettingsDatabase;
using Db.TowerSettingsDatabase.Impls;
using UnityEngine;
using Extensions;
using UI.BuyBlock;
using UnityEngine.Serialization;
using Zenject;


namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/GamePrefabInstaller", fileName = "GamePrefabInstaller")]
    public class GamePrefabInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private PlayerControlsDatabase playerControlsDatabase;
        [SerializeField] private PlayerAimDatabase playerAimDatabase;
        [SerializeField] private TowerSettingsDatabase towerSettingsDatabase;
        [SerializeField] private LayerSettingsDatabase layerSettingsDatabase;
        [SerializeField] private ArrowSettingsDatabase arrowSettingsDatabase;
        [SerializeField] private EnemySettingsDatabase enemySettingsDatabase;
        [SerializeField] private EnemyBehaviorDatabase enemyBehaviorDatabase;
        [SerializeField] private EnemySpawnDatabase enemySpawnDatabase;
        
        [SerializeField] private Canvas canvasPrefab;
        [SerializeField] private GameObject buyBlockPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerControlsDatabase>().FromInstance(playerControlsDatabase).AsSingle();
            Container.Bind<IPlayerAimDatabase>().FromInstance(playerAimDatabase).AsSingle();
            Container.Bind<ITowerSettingsDatabase>().FromInstance(towerSettingsDatabase).AsSingle();
            Container.Bind<ILayerSettingsDatabase>().FromInstance(layerSettingsDatabase).AsSingle();
            Container.Bind<IArrowSettingsDatabase>().FromInstance(arrowSettingsDatabase).AsSingle();
            Container.Bind<IEnemySettingsDatabase>().FromInstance(enemySettingsDatabase).AsSingle();
            Container.Bind<IEnemyBehaviorDatabase>().FromInstance(enemyBehaviorDatabase).AsSingle();
            Container.Bind<IEnemySpawnDatabase>().FromInstance(enemySpawnDatabase).AsSingle();
            
            BindUI();
        }
        
        private void BindUI()
        {
            GameObject canvas = Container.InstantiatePrefab(canvasPrefab);
            
            Container.BindUIView<BuyBlockPresenter, BuyBlockView>(
                buyBlockPrefab, canvas.transform);
        }
        
    }
}