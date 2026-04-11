using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class HoverEffect : MonoBehaviour
    {
        [Header("Réglages de Lévitation")]
        [SerializeField] private float _amplitude = 0.5f;
        [SerializeField] private float _duration = 1.5f;
        [SerializeField] private Ease _easeType = Ease.InOutSine;

        private void Start()
        {
            transform.DOMoveY(transform.position.y + _amplitude, _duration)
                .SetEase(_easeType)
                .SetLoops(-1, LoopType.Yoyo)
                .SetRelative();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}