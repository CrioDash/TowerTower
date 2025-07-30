using Db.TowerSettingsDatabase;
using Enums;
using Gameplay.Player;
using Gameplay.Tower;
using Zenject;

namespace Core
{
    public class GameStartService : IInitializable
    {
        private readonly ITowerService _tower;
        private readonly IPlayerChangeSideService _side;
        private readonly ITowerSettingsDatabase _towerSettingsDatabase;

        public GameStartService(
            ITowerService tower,
            IPlayerChangeSideService side,
            ITowerSettingsDatabase towerSettingsDatabase)
        {
            _tower = tower;
            _side  = side;
            _towerSettingsDatabase = towerSettingsDatabase;
        }

        public void Initialize()
        {
            for (int i = 0; i < _towerSettingsDatabase.InitialBlockAmount; i++)
                _tower.AddBlock();
        }
    }
}