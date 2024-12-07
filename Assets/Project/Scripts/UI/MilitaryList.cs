using DG.Tweening;
using Military.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Military.Scripts.UI
{
    public class MilitaryList : UIMenu, IMilitaryList
    {
        [SerializeField] private Transform _listTransform;
        [SerializeField] private Transform _content;
        [SerializeField] private Image _backImage;
        [SerializeField] private SelectObject _selectButtonPrefab;

        [Header("Buttons")]
        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _openCloseButton;

        [Header("Colors")]
        [SerializeField] private Color _deselectColor;
        [SerializeField] private Color _selectColor;

        private bool _isMoving;
        private bool _isOpen;
        private Image _selectedButtonImage;
        private TextMeshProUGUI _textMeshProUGUI;

        private const float OpenXPosition = -650;
        private const float OpenAlpha = 0.75f;
        private const float CloseXPosition = -1300;
        private const float CloseAlpha = 0;
        private const float Duration = 0.5f;

        private IMapEditorManager _mapEditorManager;
        [Inject] private DiContainer _container;

        [Inject]
        private void Constructor(IMapEditorManager mapEditorManager)
        {
            _mapEditorManager = mapEditorManager;
        }

        private void Start()
        {
            _cancelButton.onClick.AddListener(DiselectButton);
            _cancelButton.onClick.AddListener(Cancel);

            _openCloseButton.onClick.AddListener(OpenCloseList);
            _textMeshProUGUI = _openCloseButton.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void DiselectButton()
        {
            if (_selectedButtonImage == null) return;

            _selectedButtonImage.DOColor(_deselectColor, 0.1f);
            UnblockCameraMoving();
            _selectedButtonImage = null;
        }

        public void Cancel()
        {
            _mapEditorManager.SetUpPrefab(null);
            _cancelButton.gameObject.SetActive(false);
        }

        public void SetSelectedButton(Image buttonImage, GameObject prefab)
        {
            _cancelButton.gameObject.SetActive(true);
            _selectedButtonImage = buttonImage;
            _selectedButtonImage.DOColor(_selectColor, 0.1f);
            BlockCameraMoving();
            _mapEditorManager.SetUpPrefab(prefab);
        }

        private void OpenCloseList()
        {
            if (_isMoving) return;

            _isMoving = true;
            float xPos = 0;
            float alpha = 0;
            if (_isOpen)
            {
                CloseList(out xPos, out alpha);
            }
            else
            {
                OpenList(out xPos, out alpha);
            }

            DOTween.Sequence().Append(_backImage.DOFade(alpha, Duration).OnComplete(() => { if (!_isOpen) _backImage.gameObject.SetActive(false); }))
                              .Join(_listTransform.DOLocalMoveX(xPos, Duration).OnComplete(() => { _isMoving = false; }));
            _isOpen = !_isOpen;
        }

        private void OpenList(out float xPos, out float alpha)
        {
            _backImage.gameObject.SetActive(true);
            xPos = OpenXPosition;
            alpha = OpenAlpha;
            _textMeshProUGUI.text = "◄";
            BlockCameraMoving();
        }

        private void CloseList(out float xPos, out float alpha)
        {
            xPos = CloseXPosition;
            alpha = CloseAlpha;
            _textMeshProUGUI.text = "►";
            UnblockCameraMoving();
        }

        private bool BlockCameraMovement()
        {
            return true;
        }

        public void CreateButtons(Object[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                var selectedButton = _container.InstantiatePrefab(_selectButtonPrefab, _content);
                selectedButton.GetComponent<SelectObject>().ObjectName = objects[i].name;
            }
        }
    }
}