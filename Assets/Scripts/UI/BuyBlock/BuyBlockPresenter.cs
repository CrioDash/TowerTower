using System;
using Gameplay.Tower;
using UI.Impls;
using UniRx;
using Zenject;

namespace UI.BuyBlock
{
    public class BuyBlockPresenter : UIPresenter<BuyBlockView, BuyBlockModel>, IInitializable
    {
        private readonly ITowerService _towerService;

        public BuyBlockPresenter(
            ITowerService towerService)
        {
            _towerService = towerService;
        }
        
        public void Initialize()
        {
            View.BuyButton.OnClickAsObservable().Subscribe( _ => BuyBlock()).AddTo(View);
        }

        private void BuyBlock()
        {
            Model.BuyBlock();
            _towerService.AddBlock();
        }
    }
}