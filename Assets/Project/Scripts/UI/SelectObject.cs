using DG.Tweening;
using Military.Scripts.CameraLogic;
using Military.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Military.Scripts.UI
{
    public class SelectObject : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        private GameObject _objectPrefab;

        public string ObjectName { private get; set; }

        private const string PrefabPath = "Prefabs/Military Objects/";
        private const string SpritePath = "Sprites/";

        private IMilitaryList _militaryList;

        [Inject]
        private void Constructor(IMilitaryList militaryList)
        {
            _militaryList = militaryList;
        }

        private void Start()
        {
            _objectPrefab = Resources.Load<GameObject>($"{PrefabPath}{ObjectName}");
            _image.sprite = Resources.Load<Sprite>($"{SpritePath}{ObjectName}");

            _button.onClick.AddListener(Select);
        }

        private void Select()
        {
            _militaryList.DiselectButton();
            _militaryList.SetSelectedButton(_button.GetComponent<Image>(), _objectPrefab);
        }
    }
}