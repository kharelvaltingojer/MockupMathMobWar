using UnityEngine;

namespace Game.Scripts
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        
        private void Awake()
        {
            // check if there is already an instance of GameController
            if (_instance == null)
            {
                // if not, set the instance to this
                _instance = this;
            }
            else if (_instance != this)
            {
                // if there is already an instance of GameController, destroy this
                Destroy(gameObject);
            }
        }
        
        // public static bool IsPaused = false;
        public static bool IsDead = false;
        public GameObject gameOverPanel;
        // public GameObject pausePanel;

        public static bool IsPlayable()
        {
            bool result = true;
            
            if (PauseInputController.IsPaused) result = false;
            if (IsDead) result = false;

            return result;
        }
        
        // private static void TogglePause()
        // {
        //     IsPaused = !IsPaused;
        // }
        
        void Update()
        {
            gameOverPanel.SetActive(IsDead);
            // pausePanel.SetActive(IsPaused);
        }
    }
}


