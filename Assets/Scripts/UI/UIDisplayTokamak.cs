using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIDisplayTokamak : MonoBehaviour
    {
        [Header("===== REFERENCES =====")]
        [SerializeField] private Slider _currentSlider;
        [SerializeField] private Slider _loseSlider;
        [SerializeField] private Slider _winSlider;

        private void Start()
        {
            _loseSlider.value =
                TokamakManager.Instance.LooseStabilityLevel / TokamakManager.Instance.RangeStability.y;
            _winSlider.value =
                TokamakManager.Instance.WinStabilityLevel / TokamakManager.Instance.RangeStability.y;
        }

        private void Update()
        {
            _currentSlider.value =
                TokamakManager.Instance.CurrentStabilityLevel / TokamakManager.Instance.RangeStability.y;
        }
    }
}