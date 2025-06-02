using Source.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UIScripts
{
    public class VampyreBarViewer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _vampyrismAreaSprite;
        [SerializeField] private Image _vampyrismIndicator;
        [SerializeField] private VampyrismAttack _vampyrismAttack;
        
        private void Awake()
        {
            SetAreaDisable();
        }

        public void OnEnable()
        {
            _vampyrismAttack.ChangedValue += ChangeIndicatorFill;
            _vampyrismAttack.Enabled += SetAreaEnable;
            _vampyrismAttack.Disabled += SetAreaDisable;
        }

        public void OnDisable()
        {
            _vampyrismAttack.ChangedValue -= ChangeIndicatorFill;
            _vampyrismAttack.Enabled -= SetAreaEnable;
            _vampyrismAttack.Disabled -= SetAreaDisable;
        }
        
        private void SetAreaEnable()
        {
            _vampyrismAreaSprite.gameObject.SetActive(true);
        }

        private void SetAreaDisable()
        {
            _vampyrismAreaSprite.gameObject.SetActive(false);
        }

        private void ChangeIndicatorFill(float deltaFillAmount)
        {
            _vampyrismIndicator.fillAmount += deltaFillAmount;
        }
    }
}