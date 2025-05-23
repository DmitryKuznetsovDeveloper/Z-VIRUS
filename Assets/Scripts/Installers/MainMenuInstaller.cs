using Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers
{
    public sealed class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private Button _btn;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;
        
        public override void InstallBindings()
        {
        }

        private void OnEnable()
        {
            _btn.onClick.AddListener((() => _ = _sceneLoader.LoadWithLoadingScreen(SceneNames.Gameplay)));
        }
    }
}