using System;
using Enums;
using UniRx;

namespace Gameplay.Player.Impls
{
    public class PlayerChangeSideService: IPlayerChangeSideService
    {
        private EPlayerSide _current = EPlayerSide.Left;
        public EPlayerSide CurrentSide => _current;

        private readonly BehaviorSubject<EPlayerSide> _sideStream;
        public IObservable<EPlayerSide> OnSideChanged => _sideStream.AsObservable();

        public PlayerChangeSideService()
        {
            _sideStream = new BehaviorSubject<EPlayerSide>(_current);
        }
        
        public void ToggleSide()
        {
            _current = _current == EPlayerSide.Left ? EPlayerSide.Right : EPlayerSide.Left;
            _sideStream.OnNext(_current);
        }

        public void SetSide(EPlayerSide side)
        {
            if (_current != side)
            {
                _current = side;
                _sideStream.OnNext(_current);
            }
        }

        public void ChangeSide()
        {
            _current = _current == EPlayerSide.Left ? EPlayerSide.Right : EPlayerSide.Left;
            _sideStream.OnNext(_current);
        }
        
    }
}