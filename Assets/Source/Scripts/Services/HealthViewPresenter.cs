using System.Collections.Generic;
using Source.Scripts.AttributesScripts;
using Source.Scripts.UIScripts;

namespace Source.Scripts.Services
{
    public class HealthViewPresenter
    {
        private readonly Health _health;
        private readonly List<IHealthViewable> _healthViewers;

        public HealthViewPresenter(Health health, List<IHealthViewable> healthViewers)
        {
            _health = health;
            _healthViewers = healthViewers;
        }

        public void Initialize()
        {
            SetHealthView();

            _health.HealthChanged += SetHealthView;
        }

        public void Dispose()
        {
            _health.HealthChanged -= SetHealthView;
        }

        private void SetHealthView()
        {
            foreach (IHealthViewable healthViewer in _healthViewers)
                healthViewer.SetHealthView(_health.CurrentHealth, _health.MaxHealth);
        }
    }
}