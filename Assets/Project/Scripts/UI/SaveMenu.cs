using DG.Tweening;
using Military.Scripts.Interfaces;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Military.Scripts.UI
{
    public class SaveMenu : UIMenu
    {
        [SerializeField] private TMP_InputField _mapName;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private CanvasGroup _saveGroup;

        private const float Duration = 0.5f;

        private ISaveManager _saveManager;

        [Inject]
        private void Constructor(ISaveManager saveManager)
        {
            _saveManager = saveManager;
        }

        private void Start()
        {
            _saveButton.onClick.AddListener(SaveMap);
            _cancelButton.onClick.AddListener(CloseSaveMenu);
        }

        private void OnEnable()
        {
            _saveGroup.DOFade(1, Duration);
            BlockCameraMoving();
        }

        private void OnDisable()
        {
            UnblockCameraMoving();
        }

        private void SaveMap()
        {
            if (_mapName.text == String.Empty) return;

            _saveManager.Save(_mapName.text);
        }

        private void CloseSaveMenu()
        {
            _saveGroup.DOFade(0, Duration).OnComplete(() => gameObject.SetActive(false));
        }
    }
}