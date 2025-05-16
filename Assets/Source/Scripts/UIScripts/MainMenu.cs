using Source.Scripts.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UIScripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _authorButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private AuthorWindow _authorWindow;
        
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _sceneLoader = new SceneLoader();
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _authorButton.onClick.AddListener(OnAuthorButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
            _authorButton.onClick.RemoveListener(OnAuthorButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }
        
        private void OnExitButtonClick()
        {
            _sceneLoader.QuitGame();
        }

        private void OnAuthorButtonClick()
        {
            _authorWindow.gameObject.SetActive(true);
        }

        private void OnPlayButtonClick()
        {
            _sceneLoader.LoadGameScene();
        }
    }
}