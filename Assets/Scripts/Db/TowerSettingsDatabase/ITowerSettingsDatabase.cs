using UnityEngine;

namespace Db.TowerSettingsDatabase
{
    public interface ITowerSettingsDatabase
    {
        public int InitialBlockAmount { get; }
        public Vector3 BlockSize { get; }
        public GameObject TowerBlockPrefabPrefab { get; }
        public GameObject TowerChunkPrefab { get; }
        public Sprite[] TowerSprites { get; }
    }
}