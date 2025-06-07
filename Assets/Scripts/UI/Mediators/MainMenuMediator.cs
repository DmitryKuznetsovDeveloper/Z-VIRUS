using System;
using Core;
using Cysharp.Threading.Tasks;
using UI.Views;
using Utils;
using Zenject;

namespace UI.Mediators
{
    public sealed class MainMenuMediator : IInitializable,IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly MainMenuView _mainMenuView;
        private readonly GameExitService _gameExitService;

        public MainMenuMediator(SceneLoader sceneLoader, MainMenuView mainMenuView, GameExitService gameExitService)
        {
            _sceneLoader = sceneLoader;
            _mainMenuView = mainMenuView;
            _gameExitService = gameExitService;
        }

        public void Initialize()
        { 
            _mainMenuView.StartGameButton.onClick.AddListener(OnStartClicked);
            _mainMenuView.SettingButton.onClick.AddListener((() => DebugUtil.Log("Settings Open"))); 
            _mainMenuView.ExitButton.onClick.AddListener(_gameExitService.Exit);

        }
        
        public void Dispose()
        {
            _mainMenuView.StartGameButton.onClick.RemoveListener(OnStartClicked);
            _mainMenuView.SettingButton.onClick.RemoveListener((() => DebugUtil.Log("Settings Open"))); 
            _mainMenuView.ExitButton.onClick.RemoveListener(_gameExitService.Exit);
        }
        
        private void OnStartClicked() => _sceneLoader.LoadWithLoadingScreen(SceneNames.Gameplay).Forget();
    }
}