using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("===== REFERENCES =====")]
        [SerializeField] private GameObject _panelEndGame;
        [SerializeField] private TextMeshProUGUI _tmpEndGame;
        [SerializeField] private GameObject _panelPauseGame;
        
        [Header("===== PS ======")]
        [SerializeField] private ParticleSystem _confetisParticles;

        private bool _alreadyPlayConfeti = false;
        private bool IsGamePause = false;
        
        private void Start()
        {
            Time.timeScale = 0;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void LoseGame()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _panelEndGame.SetActive(true);
            _tmpEndGame.text = "GAME OVER";
            _tmpEndGame.color = Color.red;
            if (!_alreadyPlayConfeti) AudioManager.Instance.PlaySound(AudioManager.Instance.BruitDefaite);
            
            _alreadyPlayConfeti = true;
        }

        public void PauseGame()
        {
            if (!IsGamePause)
            {
                _panelPauseGame.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                IsGamePause = true;
            }
            else
            {
                _panelPauseGame.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                IsGamePause = false;
            }
        }

        public void ResumeGame()
        {
            _panelPauseGame.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void WinGame()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (!_alreadyPlayConfeti) _confetisParticles.Play();
            _panelEndGame.SetActive(true);
            _tmpEndGame.text = "WIN";
            _tmpEndGame.color = Color.green;
            if (!_alreadyPlayConfeti) AudioManager.Instance.PlaySound(AudioManager.Instance.BruitVictoire);
            
            _alreadyPlayConfeti = true;
        }
        
        public void ReloadGame()
        {
            SceneManager.LoadScene("GameScene");
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}