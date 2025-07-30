using UnityEngine;

namespace Db.LayerSettingsDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/LayerSettingsDatabase", fileName = "LayerSettingsDatabase")]
    public class LayerSettingsDatabase : ScriptableObject, ILayerSettingsDatabase
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask enemyLayer;

        public LayerMask GroundLayer => groundLayer;
        public LayerMask EnemyLayer => enemyLayer;
    }
}