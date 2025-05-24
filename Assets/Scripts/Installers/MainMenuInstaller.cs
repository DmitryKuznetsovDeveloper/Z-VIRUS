using UI;
using UI.Mediators;
using UI.Views;
using Zenject;

namespace Installers
{
    public sealed class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenuView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuMediator>().AsSingle();
        }
    }
}