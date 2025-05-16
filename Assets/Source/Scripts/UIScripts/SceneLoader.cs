using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.UIScripts
{
    public class SceneLoader 
    {
        private const string GameSceneName = "SampleScene";
        
        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameSceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
