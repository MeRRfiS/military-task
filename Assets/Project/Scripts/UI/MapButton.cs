using Military.Scripts.Interfaces;
using Military.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Military.Scripts.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public string MapName { private get; set; }
        public string SceneLoadName { private get; set; }

        private ISceneLoader _sceneLoader;
        private ISaveManager _saveManager;

        [Inject]
        private void Constructor(ISceneLoader sceneLoader,
                                 ISaveManager saveManager)
        {
            _sceneLoader = sceneLoader;
            _saveManager = saveManager;
        }

        private void Start()
        {
            _button.GetComponentInChildren<TextMeshProUGUI>().text = MapName;
            _button.onClick.AddListener(OpenScene);
        }

        private void OpenScene()
        {
            _saveManager.Load(MapName);
            _sceneLoader.LoadScene(SceneLoadName);
        }
    }
}