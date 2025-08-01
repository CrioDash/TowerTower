using System;
using System.Collections.Generic;
using Db.TowerSettingsDatabase;
using UniRx;
using UnityEngine;

namespace Gameplay.Tower.Impls
{
    public class TowerService: ITowerService
    {
        private readonly Transform _blocksParent;
        private readonly Transform[] _towerBlocks; 
        
        private readonly List<ITowerBlock> _blocks = new();
        private readonly BehaviorSubject<ITowerBlock> _onBlockAdded;
        
        private readonly ITowerSettingsDatabase _towerSettingsDatabase;
        public IReadOnlyList<ITowerBlock> Blocks => _blocks;
        public IObservable<ITowerBlock> OnBlockAdded => _onBlockAdded.AsObservable();
        public Transform BlocksParent => _blocksParent;

        private List<ITowerBlock> _currentBlocks = new List<ITowerBlock>();

        public int CurrentBlocks => _currentBlocks.Count;
        
        public TowerService(Transform blocksParent,
            Transform[] towerBlocks,
            ITowerSettingsDatabase towerSettingsDatabase)
        {
            _blocksParent = blocksParent;
            _towerBlocks = towerBlocks;
            _towerSettingsDatabase = towerSettingsDatabase;
            _onBlockAdded = new BehaviorSubject<ITowerBlock>(null);
        }

        public void AddChunk()
        {
            Vector3 pos = _towerBlocks[1].position;

            var go = GameObject.Instantiate(_towerSettingsDatabase.TowerChunkPrefab, pos, Quaternion.identity, _blocksParent);
            go.transform.localScale = _towerSettingsDatabase.BlockSize;
            ITowerBlock block = go.GetComponent<ITowerBlock>();

            for(int i = 0; i < _currentBlocks.Count; i++)
            {
                _blocks.Remove(_currentBlocks[i]);
                _towerBlocks[i].gameObject.SetActive(false);
            }
            _currentBlocks.Clear();
            
            _blocks.Add(block);
            _onBlockAdded.OnNext(block);
        }
        
        public void AddBlock()
        {
            Vector3 pos = _blocks.Count == 0
                ? _blocksParent.position + Vector3.up * (_towerSettingsDatabase.BlockSize.y / 6) 
                : _currentBlocks.Count == 0 
                    ? _blocks[^1].Transform.position + Vector3.up * (_towerSettingsDatabase.BlockSize.y / 2) + Vector3.up * (_towerSettingsDatabase.BlockSize.y / 6)
                    : _blocks[^1].Transform.position + Vector3.up * (_towerSettingsDatabase.BlockSize.y / 3) ;

            var go = _towerBlocks[_currentBlocks.Count].gameObject;
            go.transform.position = pos;
            go.transform.localScale = _towerSettingsDatabase.BlockSize;
            go.SetActive(true);

            go.GetComponent<SpriteRenderer>().sprite = _towerSettingsDatabase.TowerSprites[_currentBlocks.Count];
            
            ITowerBlock block = go.GetComponent<ITowerBlock>();
            
            _currentBlocks.Add(block);
            _blocks.Add(block);
            _onBlockAdded.OnNext(block);

            if (_currentBlocks.Count == 3)
                AddChunk();
        }
        
    }
}