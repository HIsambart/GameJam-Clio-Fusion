using MiniGames;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIDisplayStats : MonoBehaviour
    {
        [Header("===== SLIDERS =====")]
        [SerializeField] private Slider _currentTempSlider;
        [SerializeField] private Slider _minTempSlider;
        [SerializeField] private Slider _currentPressureSlider;
        [SerializeField] private Slider _minPressureSlider;
        
        [Header("===== REFERENCES =====")]
        [SerializeField] private ContinueMiniGame _miniGameTemperature;
        [SerializeField] private ContinueMiniGame _miniGamePressure;
        
        private void Start()
        {
            _minTempSlider.value = _miniGameTemperature.MinimumLevel / _miniGameTemperature.RangeLevel.y;
            _minPressureSlider.value = _miniGamePressure.MinimumLevel / _miniGamePressure.RangeLevel.y;
        }
        
        private void Update()
        {
            _currentTempSlider.value = _miniGameTemperature.CurrentLevel / _miniGameTemperature.RangeLevel.y;
            _currentPressureSlider.value = _miniGamePressure.CurrentLevel / _miniGamePressure.RangeLevel.y;
        }
    }
}