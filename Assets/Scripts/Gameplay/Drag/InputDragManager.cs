using System;
using Enums;
using Gameplay.Drag.Impls;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Gameplay.Drag
{
    public class InputDragManager : MonoBehaviour
    {
        [SerializeField] private LayerMask dragMask;

        [Inject] private IPlayerChangeSideService _changeSideService;
        
        private Camera _cam;

        private IDraggable _draggable;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            Vector3 screenPos;
            Vector3 worldPos;
            
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(0))
            {
                screenPos = Input.touches[0].position;
                worldPos = _cam.ScreenToWorldPoint(screenPos);

                var touch = Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    {
                        var hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, dragMask);
                        if (hit.collider && hit.collider.TryGetComponent(out _draggable))
                        {
                            _draggable.OnBeginDrag(worldPos);
                        }
                        break;
                    }
                    case TouchPhase.Moved:
                    {
                        if (_draggable != null)
                        {
                            _draggable.OnDrag(worldPos);
                            switch (worldPos.x)
                            {
                                case <= 0 when _changeSideService.CurrentSide != EPlayerSide.Right:
                                    _changeSideService.SetSide(EPlayerSide.Right);
                                    break;
                                case > 0 when _changeSideService.CurrentSide != EPlayerSide.Left:
                                    _changeSideService.SetSide(EPlayerSide.Left);
                                    break;
                            }
                        }
                        break;
                    }
                    case TouchPhase.Ended:
                    {
                        if (_draggable != null)
                        {
                            _draggable.OnDragEnd(worldPos);
                            _draggable = null;
                        }
                        break;
                    }
                }
                
                return;
            }
            
            if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(0))
                return;

            screenPos = Input.mousePosition;
            worldPos = _cam.ScreenToWorldPoint(screenPos);
            
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, dragMask);
                if (hit.collider && hit.collider.TryGetComponent(out _draggable))
                {
                    _draggable.OnBeginDrag(worldPos);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_draggable != null)
                {
                    _draggable.OnDrag(worldPos);
                    switch (worldPos.x)
                    {
                        case <= 0 when _changeSideService.CurrentSide != EPlayerSide.Right:
                            _changeSideService.SetSide(EPlayerSide.Right);
                            break;
                        case > 0 when _changeSideService.CurrentSide != EPlayerSide.Left:
                            _changeSideService.SetSide(EPlayerSide.Left);
                            break;
                    }
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                if (_draggable != null)
                {
                    _draggable.OnDragEnd(worldPos);
                    _draggable = null;
                }
            }

        }
    }
}