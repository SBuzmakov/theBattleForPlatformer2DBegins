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
            SetView();

            _health.CurrentValueChanged += SetView;
        }

        public void Dispose()
        {
            _health.CurrentValueChanged -= SetView;
        }

        private void SetView()
        {
            foreach (IHealthViewable healthViewer in _healthViewers)
                healthViewer.SetHealthView(_health.CurrentValue, _health.MaxValue);
        }
    }
}