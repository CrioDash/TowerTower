using UnityEngine;

namespace Db.ArrowSettingsDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/ArrowSettingsDatabase", fileName = "ArrowSettingsDatabase")]
    public class ArrowSettingsDatabase : ScriptableObject, IArrowSettingsDatabase
    {
        [SerializeField] private int arrowDamage;
        [SerializeField] private int despawnDelay;

        public int ArrowDamage => arrowDamage;
        public int DespawnDelay => despawnDelay;
    }
}