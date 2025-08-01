using System;
using Db.PlayerAimDatabase;
using Enums;
using Gameplay.Aim;
using Gameplay.Arrow;
using Gameplay.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Drag.Impls
{
    public class PlayerAimController : MonoBehaviour, IDraggable
    {
        [SerializeField] private Transform spriteContainer;
        
        private IAimVisualizer _visualizer;
        private IAimLogic _logic;
        private IArrowManipulator _arrowManipulator;
        private IPlayerAimDatabase _playerAimDatabase;
        private IArrowFactory _arrowFactory;
        private IPlayerChangeSideService _sideService;
        private ITrajectoryVisualizer _trajectoryVisualizer;

        private Animator _animator;
        private Collider2D _collider;
        private Vector2 _dragStart;

        private float _currentPull;
        private float _currentAngle;

        [Inject]
        public void Construct(
            IAimVisualizer visualizer,
            IAimLogic logic,
            IArrowManipulator arrowManipulator,
            IPlayerAimDatabase playerAimDatabase,
            IArrowFactory arrowFactory,
            IPlayerChangeSideService sideService,
            ITrajectoryVisualizer trajectoryVisualizer)
        {
            _visualizer = visualizer;
            _logic = logic;
            _arrowManipulator = arrowManipulator;
            _playerAimDatabase = playerAimDatabase;
            _arrowFactory = arrowFactory;
            _sideService = sideService;
            _trajectoryVisualizer = trajectoryVisualizer;
        }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            _visualizer.ResetSpline();
        }

        public void OnBeginDrag(Vector2 worldPointer)
        {
            _dragStart = worldPointer;
        }

        public void OnDrag(Vector2 worldPointer)
        {
            _currentPull = _logic.CalculatePullFactor(_dragStart, worldPointer);
            _currentAngle = _logic.CalculateAngle(_dragStart, worldPointer, _sideService.CurrentSide);

            if (_currentPull <= _playerAimDatabase.MinimumPullPower) return;
            
            _trajectoryVisualizer.DrawTrajectory(-spriteContainer.right * (_playerAimDatabase.ArrowMaxForce * _currentPull));
            _animator.SetFloat(_playerAimDatabase.AnimatorBowMagnitude, _currentPull);
            _visualizer.UpdateSpline(_currentPull);
            _arrowManipulator.UpdateArrow(_currentPull);

            spriteContainer.localRotation = Quaternion.Euler(0, _sideService.CurrentSide == EPlayerSide.Right ? 180f : 0, _currentAngle);
        }

        public void OnDragEnd(Vector2 worldPointer)
        {
            _animator.SetFloat(_playerAimDatabase.AnimatorBowMagnitude, 0);
            _visualizer.ResetSpline();
            _arrowManipulator.ResetArrow();
            _trajectoryVisualizer.DisableTrajectory();
            
            _currentPull = _logic.CalculatePullFactor(_dragStart, worldPointer);
            
            if (_currentPull <= _playerAimDatabase.MinimumPullPower) return;

            var pos = _arrowManipulator.ArrowPos;
            var angle = spriteContainer.eulerAngles;
            var force = -spriteContainer.right * (_playerAimDatabase.ArrowMaxForce * _currentPull);;
            
            _arrowFactory.Create(pos, angle, force);
        }
        
    }
}