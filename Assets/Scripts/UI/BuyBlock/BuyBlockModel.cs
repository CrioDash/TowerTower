using System;
using System.Collections.Generic;
using Gameplay.Tower;
using UI.Base;
using UniRx;

namespace UI.BuyBlock
{
    public class BuyBlockModel : IUIModel
    {
        private readonly BehaviorSubject<int> _onBlockBuy = new BehaviorSubject<int>(1);

        private int _blockCount;
        
        public IObservable<int> OnBlockBuy => _onBlockBuy.AsObservable();
        
        
        public void BuyBlock()
        {
            _blockCount++;
            _onBlockBuy.OnNext(_blockCount);
        }
    }
}