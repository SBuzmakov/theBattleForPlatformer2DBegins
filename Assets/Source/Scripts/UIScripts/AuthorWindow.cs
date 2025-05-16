using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UIScripts
{
    public class AuthorWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseWindow);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseWindow);
        }

        private void CloseWindow()
        {
            gameObject.SetActive(false);
        }
    }
}
