using System;
using Core;
using R3;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Mediators
{
    public sealed class MainMenuMediator : IInitializable,IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly MainMenuView _mainMenuView;
        private readonly GameExitService _gameExitService;
        
        private IDisposable _startGameSub;
        private IDisposable _settingSub;
        private IDisposable _exitSub;

        public MainMenuMediator(SceneLoader sceneLoader, MainMenuView mainMenuView, GameExitService gameExitService)
        {
            _sceneLoader = sceneLoader;
            _mainMenuView = mainMenuView;
            _gameExitService = gameExitService;
        }

        public void Initialize()
        {
            _startGameSub = _mainMenuView.StartGameButton.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(250))
                .SubscribeAwait(async (_, ct) => await _sceneLoader.LoadWithLoadingScreen(SceneNames.Gameplay)
                );

            _settingSub = _mainMenuView.SettingButton.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(250))
                .Subscribe(_ => Debug.Log("Open Settings Popup"));
                
            _exitSub = _mainMenuView.ExitButton.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(250))
                .Subscribe(_ => _gameExitService.Exit());
        }
        
        public void Dispose()
        {
            _startGameSub?.Dispose();
            _settingSub?.Dispose();
            _exitSub?.Dispose();
        }
    }
}