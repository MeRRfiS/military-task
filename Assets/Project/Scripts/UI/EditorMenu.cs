using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Military.Scripts.UI
{
    public class EditorMenu : UIMenu
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _pauseMenu;

        private void Start()
        {
            _menuButton.onClick.AddListener(OpenPauseMenu);
        }

        private void OpenPauseMenu()
        {
            _pauseMenu.SetActive(true);
        }
    }
}