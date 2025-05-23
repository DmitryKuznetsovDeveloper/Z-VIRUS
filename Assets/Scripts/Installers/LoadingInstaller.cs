using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LoadingInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreenView _loadingScreenView;

        public override void InstallBindings() => Container.Bind<LoadingScreenView>().FromInstance(_loadingScreenView).AsSingle();
    }
}