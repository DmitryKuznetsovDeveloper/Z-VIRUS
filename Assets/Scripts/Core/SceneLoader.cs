using Cysharp.Threading.Tasks;
using UI;
using UI.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public sealed class SceneLoader
    {
        private readonly LoadingScreenView _loadingScreen;

        public SceneLoader(LoadingScreenView loadingScreen) => _loadingScreen = loadingScreen;
        
        public async UniTask LoadWithLoadingScreen(string targetScene)
        {
            _loadingScreen.SetProgress(0);
            _loadingScreen.SetVisible(true);
            await UniTask.NextFrame();

            var operation = SceneManager.LoadSceneAsync(targetScene);
            operation.allowSceneActivation = false;

            float displayed = 0f;

            while (operation.progress < 0.9f)
            {
                displayed = Mathf.MoveTowards(displayed, operation.progress, Time.unscaledDeltaTime);
                _loadingScreen.SetProgressAnimated(displayed);
                await UniTask.NextFrame(PlayerLoopTiming.LastPostLateUpdate);
            }

            _loadingScreen.SetProgressAnimated(1f);
            await UniTask.Delay(300);
            
            _loadingScreen.ShowPressAnyKey();
            await UniTask.WaitUntil(() => Input.anyKeyDown);
            operation.allowSceneActivation = true;
            await UniTask.NextFrame();
            _loadingScreen.SetVisible(false);
        }

    }
}