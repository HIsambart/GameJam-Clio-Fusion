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
        
        private void Start()
        {
            Cursor.visible = false;
            Time.timeScale = 0;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
        }

        public void LoseGame()
        {
            _panelEndGame.SetActive(true);
            _tmpEndGame.text = "GAME OVER";
            _tmpEndGame.color = Color.red;
        }
        
        public void WinGame()
        {
            _panelEndGame.SetActive(true);
            _tmpEndGame.text = "WIN";
            _tmpEndGame.color = Color.green;
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