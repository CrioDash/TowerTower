using System;
using System.Collections.Generic;
using Db.TowerSettingsDatabase;
using UniRx;
using UnityEngine;

namespace Gameplay.Tower.Impls
{
    public class TowerService: ITowerService
    {
        private readonly List<ITowerBlock> _blocks = new();
        private readonly BehaviorSubject<ITowerBlock> _onBlockAdded;

        public IReadOnlyList<ITowerBlock> Blocks => _blocks;
        public IObservable<ITowerBlock> OnBlockAdded => _onBlockAdded.AsObservable();

        private readonly GameObject _blockPrefab;
        private readonly Transform _blocksParent;
        private readonly ITowerSettingsDatabase _towerSettingsDatabase;
        
        public TowerService(
            GameObject blockPrefab,
            Transform blocksParent,
                ITowerSettingsDatabase towerSettingsDatabase)
        {
            _blockPrefab = blockPrefab;
            _blocksParent = blocksParent;
            _towerSettingsDatabase = towerSettingsDatabase;

            _onBlockAdded = new BehaviorSubject<ITowerBlock>(null);
        }

        public void AddBlock()
        {
            Vector3 pos = _blocksParent.position 
                          + Vector3.up * _towerSettingsDatabase.BlockSize.y * _blocks.Count;

            var go = GameObject.Instantiate(_blockPrefab, pos, Quaternion.identity, _blocksParent);
            go.transform.localScale = _towerSettingsDatabase.BlockSize;
            ITowerBlock block = go.GetComponent<ITowerBlock>();
            _blocks.Add(block);
            _onBlockAdded.OnNext(block);
        }
    }
}