using System;
using System.Collections.Generic;
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
        private Touch _touch;
        private Collider2D[] _overlap = new Collider2D[1];
        private PointerEventData _ped;
        private readonly List<RaycastResult> _uiHits = new(1);
        private ContactFilter2D _filter;
        
        private bool _startedOverUITouch;
        private bool _startedOverUIMouse;
        
        private Vector3 _worldPos;
        
        private void Awake()
        {
            _cam = Camera.main;
            _filter = new ContactFilter2D { useLayerMask = true, layerMask = dragMask, useTriggers = true };
        }
        
        private bool IsPointerOverUI(Vector2 screenPos)
        {
            if (EventSystem.current == null) return false;
            _ped ??= new PointerEventData(EventSystem.current);
            _ped.Reset();
            _ped.position = screenPos;
            _uiHits.Clear();
            EventSystem.current.RaycastAll(_ped, _uiHits);
            return _uiHits.Count > 0;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);
                _worldPos = _cam.ScreenToWorldPoint(_touch.position);
                
                if (_touch.phase == TouchPhase.Began)
                    _startedOverUITouch = IsPointerOverUI(_touch.position);

                if (_startedOverUITouch)
                    return;


                switch (_touch.phase)
                {
                    case TouchPhase.Began:
                    {
                        Physics2D.OverlapPoint(_worldPos, _filter, _overlap);
                        if (_overlap.Length != 0 && _overlap[0].TryGetComponent(out _draggable)) 
                            _draggable.OnBeginDrag(_worldPos);
                        break;
                    }
                    case TouchPhase.Moved:
                    {
                        if (_draggable != null)
                        {
                            _draggable.OnDrag(_worldPos);
                            switch (_worldPos.x)
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
                            _draggable.OnDragEnd(_worldPos);
                            _draggable = null;
                        }
                        _startedOverUITouch = false;
                        break;
                    }
                }
                
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
                _startedOverUIMouse = IsPointerOverUI(Input.mousePosition);

            if (_startedOverUIMouse)
            {
                if (Input.GetMouseButtonUp(0)) _startedOverUIMouse = false;
                return;
            }

            _worldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetMouseButtonDown(0))
            { 
                int count = Physics2D.OverlapPoint(_worldPos, _filter, _overlap);
                if (count > 0 && _overlap.Length != 0 && _overlap[0].TryGetComponent(out _draggable)) 
                    _draggable.OnBeginDrag(_worldPos);
            }

            if (Input.GetMouseButton(0))
            {
                if (_draggable != null)
                {
                    _draggable.OnDrag(_worldPos);
                    switch (_worldPos.x)
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

            if (!Input.GetMouseButtonUp(0) || _draggable == null) return;
            
            _draggable.OnDragEnd(_worldPos);
            _draggable = null;

        }
    }
}