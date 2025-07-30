using UnityEngine;

namespace Db.PlayerControlsDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/PlayerControlsDatabase", fileName = "PlayerControlsDatabase")]
    public class PlayerControlsDatabase: ScriptableObject, IPlayerControlsDatabase
    {
        [SerializeField] private float swipeThreshold;

        public float SwipeThreshold => swipeThreshold;
    }
}