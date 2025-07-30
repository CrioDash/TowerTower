using System;
using Db.ArrowSettingsDatabase;
using Db.LayerSettingsDatabase;
using Gameplay.Enemy;
using UniRx;
using UnityEngine;

namespace Gameplay.Arrow.Impls
{
    public class ArrowEnemyHitHandler : IArrowHitHandler
    {
        private readonly IArrowSettingsDatabase _arrowSettingsDatabase;
        private readonly ILayerSettingsDatabase _layerSettingsDatabase;

        public ArrowEnemyHitHandler(
            IArrowSettingsDatabase arrowSettingsDatabase,
            ILayerSettingsDatabase layerSettingsDatabase)
        {
            _arrowSettingsDatabase = arrowSettingsDatabase;
            _layerSettingsDatabase = layerSettingsDatabase;
        }

        public bool HandleHit(Arrow arrow, Collision2D collision)
        {
            if ((_layerSettingsDatabase.EnemyLayer.value & (1 << collision.gameObject.layer)) == 0)
                return false;
            
            var dmg = collision.gameObject.GetComponent<IDamageable>();
            if (dmg == null) 
                return false;
            
            Transform parent = collision.transform;
            arrow.StickInto(parent);
            
            dmg.TakeDamage(_arrowSettingsDatabase.ArrowDamage);
            
            return true;
        }
    }
}