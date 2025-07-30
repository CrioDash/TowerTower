using UnityEngine;

namespace Db.LayerSettingsDatabase
{
    public interface ILayerSettingsDatabase
    {
        public LayerMask GroundLayer { get; }
        public LayerMask EnemyLayer { get; }
    }
}