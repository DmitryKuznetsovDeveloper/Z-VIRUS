using Cysharp.Threading.Tasks;
using UI;
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
            operation.allowSceneActivation = true;
            _loadingScreen.SetVisible(false);
        }
    }
}