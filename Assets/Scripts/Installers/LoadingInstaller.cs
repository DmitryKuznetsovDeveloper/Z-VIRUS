using UI;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class LoadingInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreenView _loadingScreenView;

        public override void InstallBindings() => Container.Bind<LoadingScreenView>().FromInstance(_loadingScreenView).AsSingle();
    }
}