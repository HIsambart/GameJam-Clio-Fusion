using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
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
            SceneManager.LoadScene("GameScene");
        }
        
        public void WinGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}