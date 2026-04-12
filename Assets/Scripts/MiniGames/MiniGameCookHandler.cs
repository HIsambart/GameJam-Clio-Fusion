using System.Collections;
using InputHandlersScripts;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using EventBus = Utils.EventBus;
using Random = UnityEngine.Random;

namespace MiniGames
{
    public class MiniGameCookHandler : MiniGameTrigger
    {
        [SerializeField] private bool _gameStarted;
        [SerializeField] private bool _canCook;
    
        public int DeuteriumCount;
        public int TriteriumCount;
    
        public int Deuterium;
        public int Triterium;
    
        public float MaxTime;
        [SerializeField] private float _currentTime;

        // Recette panel
        [SerializeField] private GameObject _panelRecette;
        [SerializeField] private TMP_Text _deuteriumText;
        [SerializeField] private TMP_Text _triteriumText;
    
        // Cooking panel
        [SerializeField] private GameObject _panelCooking;
        [SerializeField] private TMP_Text _timer;
    
        private void Awake()
        {
            EventBus.PutDeuterium += AddDeuterium;
            EventBus.PutTriterium += AddTriterium;
        }

        private void Update()
        {
            if(!_gameStarted) return;
            _currentTime -= Time.deltaTime;
            int time = (int)_currentTime;
            _timer.text = time.ToString();
            if (_currentTime <= 0)
            {
                GameOver();
            }
        }

        public override void Interact(PlayerScripts.Player player)
        {
            if(!IsNeedToPlay) return;
            PanelWarning.SetActive(false);
            Player = player;
            PlayerInputS = Player.GetComponent<PlayerInputSubscriber>();
            GameStart();
        }

        public override void GameStart()
        {
            DeuteriumCount = Random.Range(5, 11);
            TriteriumCount = Random.Range(5, 11);

            Deuterium = 0;
            Triterium = 0;

            _panelRecette.SetActive(true);
            _deuteriumText.text = DeuteriumCount.ToString();
            _triteriumText.text = TriteriumCount.ToString();
            _panelCooking.SetActive(false);
        
            _canCook = false;
            _currentTime = MaxTime;
            _gameStarted = true;
            
            if (Player.GetComponent<PlayerInputCooking>() == null) Player.AddComponent<PlayerInputCooking>();
            PlayerInputS.enabled = false;
            _canCook = false;
            StartCoroutine(HideRecette());
            AudioManager.Instance.PlaySound(AudioManager.Instance.BruitJeuxRecette);
        }

        [ContextMenu("Game finish")]
        public override void GameOver()
        {
            Debug.Log("Game over");
            if (Deuterium == DeuteriumCount &&  Triterium == TriteriumCount)
            {
                GameWin();
            }
            else
            {
                Retry();
            }
        
        }

        public override void PlayTutoriel()
        {
            PanelTutoriel.SetActive(true);
            AsPlayedOneTime = true;
        }

        public void Retry()
        {
            GameStart();
            Debug.Log("Retry");
        }

        public void GameWin()
        {
            Destroy(Player.GetComponent<PlayerInputCooking>());
            PlayerInputS.enabled = true;
            _gameStarted = false;
            _panelCooking.SetActive(false);
            IsNeedToPlay = false;
            GameTrigerManager.Instance.GameWarningCount--;
            PanelTutoriel.SetActive(false);
            Debug.Log("Game win");
            
            EventBus.OnCoockingWin?.Invoke();
        }

        private void AddDeuterium()
        {
            if (!_canCook) return;
            Deuterium += 1;
            if (Deuterium > DeuteriumCount)
            {
                Retry();
            }
        }

        private void AddTriterium()
        {
            if (!_canCook) return;
            Triterium += 1;
            _triteriumText.text = Triterium.ToString();
            if (Triterium > TriteriumCount)
            {
                Retry();
            }
        }

        private IEnumerator HideRecette()
        {
            yield return new WaitForSeconds(2f);
            _canCook = true;
            _panelRecette.SetActive(false);
            _panelCooking.SetActive(true);
        }
    }
}
