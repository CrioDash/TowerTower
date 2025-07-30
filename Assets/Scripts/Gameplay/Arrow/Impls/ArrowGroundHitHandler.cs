using System;
using Db.ArrowSettingsDatabase;
using Db.LayerSettingsDatabase;
using UniRx;
using UnityEngine;

namespace Gameplay.Arrow.Impls
{
    public class ArrowGroundHitHandler : IArrowHitHandler
    {
        private readonly IArrowSettingsDatabase _arrowSettingsDatabase;
        private readonly ILayerSettingsDatabase _layerSettingsDatabase;

        public ArrowGroundHitHandler(
            IArrowSettingsDatabase arrowSettingsDatabase,
            ILayerSettingsDatabase layerSettingsDatabase)
        {
            _arrowSettingsDatabase = arrowSettingsDatabase;
            _layerSettingsDatabase = layerSettingsDatabase;
        }

        public bool HandleHit(Arrow arrow, Collision2D collision)
        {
            if ((_layerSettingsDatabase.GroundLayer.value & (1 << collision.gameObject.layer)) == 0)
                return false;

            arrow.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            return true;
        }
    }
}