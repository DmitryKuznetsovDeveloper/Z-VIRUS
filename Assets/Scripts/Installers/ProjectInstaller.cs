using Core;
using Input;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/ProjectInstaller")]
    public sealed class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<GameExitService>().AsSingle();
            Container.BindInterfacesTo<CharacterInputActions>().AsSingle();
        }
    }
}