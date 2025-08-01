using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Tower
{
    public interface ITowerService
    {
        IReadOnlyList<ITowerBlock> Blocks { get; }
        IObservable<ITowerBlock> OnBlockAdded { get; }
        Transform BlocksParent { get; }
        int CurrentBlocks { get; }
        void AddChunk();
        void AddBlock();
    }
}