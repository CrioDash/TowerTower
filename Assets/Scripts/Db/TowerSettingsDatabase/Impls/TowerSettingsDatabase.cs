using UnityEngine;
using UnityEngine.Serialization;

namespace Db.TowerSettingsDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/TowerSettingsDatabase", fileName = "TowerSettingsDatabase")]
    public class TowerSettingsDatabase : ScriptableObject, ITowerSettingsDatabase
    {
        [SerializeField] private int initialBlockAmount;
        [SerializeField] private Vector3 blockSize;
        [SerializeField] private GameObject towerBlockPrefab;
        [SerializeField] private GameObject towerChunkPrefab;
        [SerializeField] private Sprite[] towerSprites;

        public int InitialBlockAmount => initialBlockAmount;
        public Vector3 BlockSize => blockSize;
        public GameObject TowerBlockPrefabPrefab => towerBlockPrefab;
        public GameObject TowerChunkPrefab => towerChunkPrefab;
        public Sprite[] TowerSprites => towerSprites;
    }
}