using UnityEngine;

namespace Extensions
{
    public static class CameraExtension
    {
        public static float GetHeight(this Camera cam)
        {
            return cam.orthographicSize * 2;
        }
        
        public static float GetWidth(this Camera cam)
        {
            return GetHeight(cam) * cam.aspect;
        }
    }
}