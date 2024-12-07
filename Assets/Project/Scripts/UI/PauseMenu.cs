using DG.Tweening;
using Military.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Military.Scripts.UI
{
    public class PauseMenu : UIMenu
    {
        [SerializeField] private GameObject _saveMenu;

        [Header("Pause Menu")]
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _exitButtle;
        [SerializeField] private CanvasGroup _pauseGroup;

        private const float Duration = 0.5f;
        private const string ManiMenuScene = "Main Scene";

        private ISceneLoader _sceneLoader;

        [Inject]
        private void Constructor(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            _resumeButton.onClick.AddListener(ClosePauseMenu);
            _saveButton.onClick.AddListener(OpenSaveMenu);
            _exitButtle.onClick.AddListener(ExitToMainMenu);
        }

        private void OnEnable()
        {
            _pauseGroup.DOFade(1, Duration);
            BlockCameraMoving();
        }

        private void OnDisable()
        {
            UnblockCameraMoving();
        }

        private void OpenSaveMenu()
        {
            _saveMenu.SetActive(true);
        }

        private void ClosePauseMenu()
        {
            _pauseGroup.DOFade(0, Duration).OnComplete(() => gameObject.SetActive(false));
        }

        private void ExitToMainMenu()
        {
            _sceneLoader.LoadScene(ManiMenuScene);
        }
    }
}