using System;
using Db.TowerSettingsDatabase;
using Gameplay.Tower;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerPositioner : MonoBehaviour
    {
        [Inject] private ITowerService _tower;
        [Inject] private ITowerSettingsDatabase _towerSettingsDatabase;

        void Awake()
        {
            _tower.OnBlockAdded
                .Subscribe(TryReposition)
                .AddTo(this);
        }

        private void Start()
        { 
            transform.position = _tower.BlocksParent.position + Vector3.up * (_towerSettingsDatabase.BlockSize.y / 6);
        }

        private void TryReposition(ITowerBlock block)
        {
            if (_tower.Blocks.Count == 0) return;

            transform.position = _tower.CurrentBlocks == 0 
                ? _tower.Blocks[^1].Transform.position + Vector3.up * (_towerSettingsDatabase.BlockSize.y / 2) + Vector3.up * (transform.localScale.y/2) 
                : _tower.Blocks[^1].Transform.position + Vector3.up * (_towerSettingsDatabase.BlockSize.y / 6) + Vector3.up * (transform.localScale.y/2);
        }
    }
}