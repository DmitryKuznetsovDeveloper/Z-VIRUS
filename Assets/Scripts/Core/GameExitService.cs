#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core
{
    public sealed class GameExitService
    {
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}