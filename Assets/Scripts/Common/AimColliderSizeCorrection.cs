using Enums;
using Extensions;
using Gameplay.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Common
{
    public class AimColliderSizeCorrection : MonoBehaviour
    {
        [Inject] private IPlayerChangeSideService _sideService;
        
        private BoxCollider2D _collider;
        private float _height;
        private float _width;
        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();

            var cam = Camera.main;

            _height = cam.GetHeight(); 
            _width = cam.GetWidth();

            _collider.size = new Vector2(_width, _height);
            _collider.offset = new Vector2(0, -transform.position.y - transform.localScale.y / 2);
        }
    }
}