using System;
using UnityEngine;

namespace Gameplay.Drag
{
    public interface IDraggable
    {
        void OnBeginDrag(Vector2 worldPointer);
        void OnDrag(Vector2 worldPointer);
        void OnDragEnd(Vector2 worldPointer);
    }
}