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

        private void TryReposition(ITowerBlock block)
        {
            if (_tower.Blocks.Count == 0) return;

            transform.position = block.Transform.position + _towerSettingsDatabase.BlockSize.y * Vector3.up * 1.5f;
        }
    }
}