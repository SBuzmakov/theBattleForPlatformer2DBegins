using Source.Scripts.PlayerScripts;
using UnityEngine;
using Zenject;

namespace Source.Scripts.MonoInstallers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private InputService _inputService;

        public override void InstallBindings()
        {
            Container.Bind<InputService>().FromInstance(_inputService).AsSingle();
        }
    }
}