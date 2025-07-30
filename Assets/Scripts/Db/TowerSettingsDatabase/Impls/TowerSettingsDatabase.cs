using UnityEngine;

namespace Db.TowerSettingsDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/TowerSettingsDatabase", fileName = "TowerSettingsDatabase")]
    public class TowerSettingsDatabase: ScriptableObject, ITowerSettingsDatabase
    {
        [SerializeField] private int initialBlockAmount;
        [SerializeField] private Vector3 blockSize;

        public int InitialBlockAmount => initialBlockAmount;
        public Vector3 BlockSize => blockSize;
    }
}