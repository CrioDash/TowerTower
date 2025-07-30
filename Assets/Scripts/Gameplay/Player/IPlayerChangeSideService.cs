using System;
using Enums;

namespace Gameplay.Player
{
    public interface IPlayerChangeSideService
    {
        EPlayerSide CurrentSide { get; }
        void ToggleSide();
        void SetSide(EPlayerSide side);
        public void ChangeSide();
        public IObservable<EPlayerSide> OnSideChanged { get; }
    }
}