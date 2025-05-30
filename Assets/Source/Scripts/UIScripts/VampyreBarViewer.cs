using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UIScripts
{
    public class VampyreBarViewer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _vampyrismAreaSprite;
        [SerializeField] private Image _vampyrismIndicator;

        public Transform AreaTransform => _vampyrismAreaSprite.transform;
        
        private void Awake()
        {
            SetAreaDisable();
        }

        public void SetAreaEnable()
        {
            _vampyrismAreaSprite.gameObject.SetActive(true);
        }
        
        public void SetAreaDisable()
        {
            _vampyrismAreaSprite.gameObject.SetActive(false);
        }

        public void ChangeIndicatorFill(float deltaFillAmount)
        {
            _vampyrismIndicator.fillAmount += deltaFillAmount;
        }
    }
}
