using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UIScripts
{
    public class HealthBarRapidViewer : MonoBehaviour, IHealthViewable
    {
        [SerializeField] private Image _healthBarImage;

        public void SetHealthView(float targetHealth, float maxHealth)
        {
            _healthBarImage.fillAmount = targetHealth / maxHealth;
        }
    }
}