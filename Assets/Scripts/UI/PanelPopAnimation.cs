using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PanelPopAnimation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _fullSize = 1f;        // Taille finale du panel
        [SerializeField] private float _inDuration = 0.5f;   // Temps pour apparaître
        [SerializeField] private float _stayDuration = 1.5f; // Temps avant de disparaître
        [SerializeField] private float _outDuration = 0.4f;  // Temps pour disparaître
        [SerializeField] private Ease _easeIn = Ease.OutBack; // Effet de rebond à l'apparition
        [SerializeField] private Ease _easeOut = Ease.InBack; // Effet de retrait à la disparition

        private void OnEnable()
        {
            // 1. On force le scale à zéro au démarrage
            transform.localScale = Vector3.zero;

            // 2. Création de la séquence d'animation
            Sequence s = DOTween.Sequence();

            // Étape A : Apparition
            s.Append(transform.DOScale(_fullSize, _inDuration).SetEase(_easeIn));

            // Étape B : Pause (le panel reste affiché)
            s.AppendInterval(_stayDuration);

            // Étape C : Disparition
            s.Append(transform.DOScale(0f, _outDuration).SetEase(_easeOut));

            // Étape D : On désactive l'objet à la fin
            s.OnComplete(() => {
                gameObject.SetActive(false);
            });
        }

        private void OnDisable()
        {
            // On "tue" les tweens en cours pour éviter les bugs si on réactive 
            // l'objet très vite avant la fin de l'anim précédente
            transform.DOKill();
        }
    }
}