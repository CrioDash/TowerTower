using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
// или UnityEngine.Experimental.Rendering.Universal в зависимости от версии

namespace Common
{
    [RequireComponent(typeof(Light2D))]
    public class LightFlicker : MonoBehaviour
    {
        [Header("Flicker Settings")]
        [Tooltip("Минимальный радиус свечения")]
        [SerializeField] private float minOuterRadius = 0.5f;
        [Tooltip("Максимальный радиус свечения")]
        [SerializeField] private float maxOuterRadius = 1.5f;
        [Tooltip("Время полного цикла (увеличение + уменьшение) в секундах")]
        [SerializeField] private float cycleDuration = 2f;
        [Tooltip("Кривая изменения (по умолчанию плавное in/out)")]
        [SerializeField] private Ease ease = Ease.InOutSine;

        private Light2D _light2D;

        void Awake()
        {
            _light2D = GetComponent<Light2D>();
        }

        void Start()
        {
            // Установим начальный радиус
            _light2D.pointLightOuterRadius = minOuterRadius;

            // Запускаем бесконечный твин: Yoyo от min до max
            DOVirtual.Float(minOuterRadius, maxOuterRadius, cycleDuration / 2f, value =>
                {
                    _light2D.pointLightOuterRadius = value;
                })
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo)
                .SetDelay(Random.Range(0f, cycleDuration));
        }
    }
}