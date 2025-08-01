using System;
using Extensions;
using UnityEngine;

namespace Common
{
    public class GroundSizeCorrection : MonoBehaviour
    {
        [SerializeField] private Transform towerTransform;
        [SerializeField] private Transform groundTransform;
        [SerializeField] private BoxCollider2D collider;
        
        private float _height;
        private float _width;
        private void Awake()
        {
            _height = Camera.main.GetHeight();
            _width = Camera.main.GetWidth();
            
            Vector3 pos = groundTransform.position;
            Vector3 scale = groundTransform.localScale;

            pos.y = -_height / 2 + groundTransform.localScale.y/2;
            scale.x = _width * 10;
            
            transform.position = pos;
            groundTransform.localScale = scale;
            
            collider.size = new Vector2(_width, 100);
            collider.offset = new Vector2(0, (collider.size.y - _height) / 2 - transform.position.y);

            pos.y += groundTransform.localScale.y/2;

            //towerTransform.position = pos;
        }
    }
}