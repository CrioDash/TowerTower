using Extensions;
using Gameplay.Tower;
using UnityEngine;
using Zenject;
using UniRx;
using Unity.Cinemachine;

namespace Common
{
    public class CameraSizeCorrection: MonoBehaviour
    {
        [Inject] private ITowerService _towerService;
        
        [SerializeField] private float defaultSize;
        [SerializeField] private float defaultAspect;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        
        private void Awake()
        {
            Camera cam = GetComponent<Camera>();
            
            var ratio = defaultAspect / cam.aspect;
            cinemachineCamera.Lens.OrthographicSize = defaultSize * ratio;
            
            Application.targetFrameRate = int.MaxValue;
        }
        
    }
}