using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class BootstrapStarter : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        private async void Start() => await _sceneLoader.LoadWithLoadingScreen(SceneNames.MainMenu);
    }
}