using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("===== REFERENCES =====")]
        [SerializeField] private GameObject _panelEndGame;
        [SerializeField] private Image _imageEndGame;
        
        private void Start()
        {
            Time.timeScale = 0;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
        }

        public void LoseGame()
        {
            _panelEndGame.SetActive(true);
            _imageEndGame.color = Color.red;
        }
        
        public void WinGame()
        {
            _panelEndGame.SetActive(true);
            _imageEndGame.color = Color.green;
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