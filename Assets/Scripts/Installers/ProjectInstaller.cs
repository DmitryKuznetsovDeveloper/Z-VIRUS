using Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
        }
    }
}