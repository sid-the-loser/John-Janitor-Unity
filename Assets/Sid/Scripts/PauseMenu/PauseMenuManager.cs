using Sid.Scripts.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sid.Scripts.PauseMenu
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject resumeButton;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject mainMenuButton;
    
        void Start()
        {
        
        }
    
        void Update()
        {
            if (GlobalVariables.Paused)
            {
                resumeButton.SetActive(true);
                restartButton.SetActive(true);
                mainMenuButton.SetActive(true);
            }
            else
            {
                resumeButton.SetActive(false);
                restartButton.SetActive(false);
                mainMenuButton.SetActive(false);
            }
        }

        public void ReloadLevel()
        {
            GlobalVariables.Paused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ResumeLevel()
        {
            GlobalVariables.Paused = false;
        }

        public void MainMenuLoad()
        {
            GlobalVariables.Paused = false;
            SceneManager.LoadScene("PlaceHolder");
            // Debug.Log("Functionality not added yet!");
        }
    }
}
