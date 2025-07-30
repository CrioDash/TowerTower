using System;
using System.Collections.Generic;

namespace Gameplay.Tower
{
    public interface ITowerService
    {
        IReadOnlyList<ITowerBlock> Blocks { get; }
        IObservable<ITowerBlock> OnBlockAdded { get; }
        void AddBlock();
    }
}