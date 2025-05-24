using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public sealed class MainMenuView : MonoBehaviour
    {
        public Button StartGameButton => _startGameButton;
        public Button SettingButton => _settingButton;
        public Button ExitButton => _exitButton;
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _exitButton;
    }
}