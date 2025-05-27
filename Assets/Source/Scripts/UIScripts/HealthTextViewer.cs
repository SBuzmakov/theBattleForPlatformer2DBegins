using System.Globalization;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UIScripts
{
    public class HealthTextViewer : MonoBehaviour , IHealthViewable
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        
        public void SetHealthView(float currentHealth, float maxHealth)
        {
            _healthText.text = $"{currentHealth.ToString(CultureInfo.CurrentCulture)} / {maxHealth.ToString(CultureInfo.CurrentCulture)}";
        }
    }
}
