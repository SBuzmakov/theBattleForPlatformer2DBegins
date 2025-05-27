using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UIScripts
{
    public class HealthBarSmoothViewer : MonoBehaviour, IHealthViewable
    {
        [SerializeField] private float _changingSpeed;
        [SerializeField] private Image _healthBar;

        private Coroutine _coroutine;

        public void SetHealthView(float currentHealth, float maxHealth)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine((StartSmoothChangingHealth(currentHealth, maxHealth)));
        }
        
        private IEnumerator StartSmoothChangingHealth(float targetHealth, float maxHealth)
        {
            float targetHealthView = targetHealth / maxHealth;

            while (Mathf.Approximately(_healthBar.fillAmount, targetHealthView) == false)
            {
                _healthBar.fillAmount = Mathf.MoveTowards(_healthBar.fillAmount, targetHealthView, _changingSpeed * Time.deltaTime);

                yield return null;
            }
            
            _coroutine = null;
        }
    }
}
