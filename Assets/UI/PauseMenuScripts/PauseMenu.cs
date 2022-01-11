using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool isPaused = false;
        [SerializeField] private GameObject pauseMenuUI;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused) Resume();
                else Pause();
            }
        }

        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }


        public void LoadMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
            isPaused = false;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
